using ProjectCarsSeasonExtension.Controller;
using ProjectCarsSeasonExtension.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using ProjectCarsSeasonExtension.Annotations;
using ProjectCarsSeasonExtension.Models;
using ProjectCarsSeasonExtension.Models.Player;
using ProjectCarsSeasonExtension.Properties;
using ProjectCarsSeasonExtension.Serialization;
using ProjectCarsSeasonExtension.Utils;
using ProjectCarsSeasonExtension.ViewModels;
using ProjectCarsSeasonExtension.Views.Player;
using Application = System.Windows.Application;

namespace ProjectCarsSeasonExtension
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public DataView DataView { get; private set; }

        public Player CurrentlyLoggedInPlayer
        {
            get
            {
                if (_playerController?.SelectedPlayer != null)
                    return _playerController.SelectedPlayer;

                return _currentlyLoggedInPlayerName;
            }
            set
            {
                _currentlyLoggedInPlayerName = value;
                OnPropertyChanged();
            }
        }

        private readonly Player _noPlayer = new Player {Name = "No player logged in", AvatarFileName = "/Assets/Players/NoPlayer.png"};
        private Player _currentlyLoggedInPlayerName;

        private ProjectCarsLiveView _projectCarsLiveView;
        private PlayerController _playerController;
        private SeasonView _seasonView;
        private AllChallengeStandings _allChallengeStandings;
        private AllChallengeStandings _allRookieChallengeStandings;
        private ChampionshipView _championshipView;
        private RookieChampionshipView _rookieChampionshipView;
        private SeasonEditor _seasonEditor;

        private AutoScreenCycler _autoCycler;

        private int _visibleTabItemsCount;
        private bool _wasInitialized;

        public MainWindow()
        {
            ReadSeasonData();
            InitializeComponent();
        }

        private void ReadSeasonData()
        {
            ISeasonReader seasonReader = new XmlSeasonReader();
            DataView = new DataView(seasonReader);

            DataView.PlayerRemoved += () => UpdateAllUIs();
            DataView.HandicapChanged += () => UpdateAllUIs();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            _allChallengeStandings = AllChallengeStandingsFactory.CreateAllChallengeStandings(DataView);
            _allRookieChallengeStandings = AllChallengeStandingsFactory.CreateAllRookieChallengeStandings(DataView);
            _playerController = new PlayerController(DataView);
            _playerController.PlayerSelectionChanged += OnPlayerSelectionChanged;

            PlayerSelectionFrame.Content = new PlayerSelection(_playerController);

            _championshipView = new ChampionshipView(DataView, _allChallengeStandings, _playerController);
            ChampionshipFrame.Content = _championshipView;

            _rookieChampionshipView = new RookieChampionshipView(DataView, _allRookieChallengeStandings, _playerController);
            RookieChampionshipFrame.Content = _rookieChampionshipView;
            
            _seasonView = new SeasonView(_allChallengeStandings.ChallengeStandings);
            SeasonViewFrame.Content = _seasonView;

            _projectCarsLiveView = new ProjectCarsLiveView(DataView);
            _projectCarsLiveView.ChallengeResultEvent += OnChallengeResultEvent;
            ProjectCarsLiveFrame.Content = _projectCarsLiveView;

            _seasonEditor = new SeasonEditor(DataView);
            _seasonEditor.SeasonChanged += () => UpdateAllUIs();
            SeasonEditorFrame.Content = _seasonEditor;

            HandicapsFrame.Content = new HandicapView(DataView, _playerController);

            _visibleTabItemsCount = MainTabControl.Items.Cast<TabItem>().Count(item => item.Visibility == Visibility.Visible);
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_wasInitialized)
                return;

            _wasInitialized = true;

            OnPlayerSelectionChanged();

            AddAutoCycler();
        }

        private void UpdateTabWidths()
        {
            if (_visibleTabItemsCount <= 0)
                return;

            var extraMargin = CalculateExtraMargin(_visibleTabItemsCount);

            var newWidth = (ActualWidth / _visibleTabItemsCount) - extraMargin;

            for (var i = 0; i < MainTabControl.Items.Count; i++)
            {
                var tabItem = (TabItem) MainTabControl.Items[i];
                tabItem.Width = newWidth;
            }
        }

        private void AddAutoCycler()
        {
            var tabsToCycle = new List<CycleableTabControl>
            {
                new CycleableTabControl(MainTabControl, new List<int>{0,1}), 
                new CycleableTabControl(_seasonView.ChallengesTabControl)
            };

            _autoCycler = new AutoScreenCycler(tabsToCycle);
        }

        private static float CalculateExtraMargin(float visibleTabItemsCount)
        {
            // The fewer tabs are visible, the more margin we need. I also don't know why.
            if(visibleTabItemsCount > 0)
                return (1/visibleTabItemsCount) * 22;

            return 0;
        }

        private void OnPlayerSelectionChanged()
        {
            UpdateCurrentlySelectedPlayerForTabHeader();
            ShowOrHideTabs();
            UpdateTabWidths();
        }

        private void UpdateCurrentlySelectedPlayerForTabHeader()
        {
            CurrentlyLoggedInPlayer = _playerController.SelectedPlayer ?? _noPlayer;
        }

        public void ShowOrHideTabs()
        {
            AuthenticationGroup? group = _playerController.SelectedPlayer?.Group;

            if (_playerController.SelectedPlayer == null)
                group = AuthenticationGroup.User;

            SeasonEditorTab.Visibility = group < AuthenticationGroup.Administrator ? Visibility.Collapsed : Visibility.Visible;
            ProjectCarsLiveTab.Visibility = group < AuthenticationGroup.Moderator ? Visibility.Collapsed : Visibility.Visible;

            HandicapsTab.Visibility = Settings.Default.ShowHandicaps ? Visibility.Visible : Visibility.Collapsed;

            if (MainTabControl.SelectedItem is TabItem selectedTab)
            {
                if (Equals(selectedTab, SeasonEditorTab) || Equals(selectedTab, ProjectCarsLiveTab))
                    MainTabControl.SelectedIndex = 0;
            }

            _visibleTabItemsCount = MainTabControl.Items.Cast<TabItem>().Count(item => item.Visibility == Visibility.Visible);
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            HotkeyController.Init(this);
            HotkeyController.Register(9001, HotkeyController.ModControl, VirtualKeyCode.VkNumpad0, LogoutPlayer);
        }

        private void OnChallengeResultEvent(ChallengeResult challengeResult)
        {
            Player selectedPlayer = GetSelectedPlayer(challengeResult);

            if (selectedPlayer == null)
            {
                Globals.Logger.Debug($"{challengeResult.ToLongString()} discarded because no player is logged in.");
                return;
            }

            var wasDataAdded = DataView.AddChallengeResult(selectedPlayer.Id, challengeResult);

            if (!wasDataAdded)
            {
                Globals.Logger.Debug($"Player: {selectedPlayer}, {challengeResult.ToLongString()} was not added because DataView.AddChallengeResult() returned false.");
                return;
            }

            Globals.Logger.Debug($"Player: {selectedPlayer}, {challengeResult.ToLongString()} was added successfully. Updating UIs.");
            UpdateAllUIs(challengeResult);
            Globals.Logger.Debug($"Player: {selectedPlayer}, {challengeResult.ToLongString()} was added successfully. Saving data.");
            SaveData();

            Globals.SpeechSynthesizer.SpeakAsync($"New personal best for {selectedPlayer.Name}.");
            int playerPosition = _allChallengeStandings.GetPlayerPosition(challengeResult, selectedPlayer);
            if (playerPosition > 0)
            {
                Globals.SpeechSynthesizer.SpeakAsync($"You are now in position {playerPosition} in this race.");
            }
        }

        private Player GetSelectedPlayer(ChallengeResult challengeResult)
        {
            if (_playerController == null)
                return null;

            Player selectedPlayer = _playerController?.SelectedPlayer;

            if (challengeResult is SimulatedChallengeResult result)
            {
                var name = result.PlayerName;
                selectedPlayer = _playerController.Players.FirstOrDefault(p => p.Name == name);
            }

            return selectedPlayer;
        }

        private void UpdateAllUIs(ChallengeResult challengeResult = null)
        {
            _allChallengeStandings.UpdateDataAndUI();
            _seasonView.UpdateUI(challengeResult);
            _championshipView.UpdateUI();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ImageSelectionWindow.Instance?.CloseForced();
            HotkeyController.Clear();
            SaveData();
            Environment.Exit(0);
        }

        private void SaveData()
        {
            ISeasonWriter seasonWriter = new XmlSeasonWriter();
            seasonWriter.SavePlayers(DataView.Players);
            seasonWriter.SaveSeasons(DataView.Seasons);
            seasonWriter.SaveChallenges(DataView.AllChallenges);
            seasonWriter.SavePlayerResults(DataView.PlayerResults);
            seasonWriter.SaveHandicaps(DataView.Handicaps);
        }

        private void ToggleWindowState()
        {
            WindowState = WindowState == WindowState.Minimized ? WindowState.Normal : WindowState.Minimized;
        }

        private static void CloseApplication()
        {
            Application.Current.Shutdown();
        }

        private void LogoutPlayer()
        {
            _playerController?.LogoutCurrentPlayer();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateTabWidths();
        }
    }
}