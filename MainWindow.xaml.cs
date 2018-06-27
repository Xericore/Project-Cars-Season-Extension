using ProjectCarsSeasonExtension.Controller;
using ProjectCarsSeasonExtension.Views;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ProjectCarsSeasonExtension.Annotations;
using ProjectCarsSeasonExtension.Models;
using ProjectCarsSeasonExtension.Models.Player;
using ProjectCarsSeasonExtension.Serialization;
using ProjectCarsSeasonExtension.ViewModels;
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

        public string CurrentlyLoggedInPlayerName
        {
            get
            {
                if (!string.IsNullOrEmpty(_playerController?.SelectedPlayer?.Name))
                    return _playerController.SelectedPlayer.Name;

                return _currentlyLoggedInPlayerName;
            }
            set
            {
                _currentlyLoggedInPlayerName = value;
                OnPropertyChanged();
            }
        }

        private const string NoPlayerText = "No player";
        private string _currentlyLoggedInPlayerName = NoPlayerText;

        private readonly RoutedCommand _closeApplicationCommand = new RoutedCommand();
        private ProjectCarsLiveView _projectCarsLiveView;
        private PlayerController _playerController;
        private SeasonView _seasonView;
        private AllChallengeStandings _allChallengeStandings;
        private ChampionshipView _championshipView;
        private SeasonEditor _seasonEditor;
        private int _visibleTabItemsCount;


        public MainWindow()
        {
            ReadSeasonData();

            InitializeComponent();

            _closeApplicationCommand.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(_closeApplicationCommand, CloseApplication_Executed));
        }

        private void ReadSeasonData()
        {
            ISeasonReader seasonReader = new XmlSeasonReader();
            DataView = new DataView(seasonReader);
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            _allChallengeStandings = new AllChallengeStandings(DataView);
            _playerController = new PlayerController(DataView.Players);
            _playerController.PlayerSelectionChanged += OnPlayerSelectionChanged;

            PlayerSelectionFrame.Content = new PlayerSelection(_playerController);

            _championshipView = new ChampionshipView(DataView, _allChallengeStandings);
            ChampionshipFrame.Content = _championshipView;
            
            _seasonView = new SeasonView(_allChallengeStandings.ChallengeStandings.Values);
            SeasonViewFrame.Content = _seasonView;

            _projectCarsLiveView = new ProjectCarsLiveView(DataView);
            _projectCarsLiveView.ChallengeResultEvent += OnChallengeResultEvent;
            ProjectCarsLiveFrame.Content = _projectCarsLiveView;

            _seasonEditor = new SeasonEditor(DataView);
            _seasonEditor.SeasonChanged += () => UpdateAllUIs();
            SeasonEditorFrame.Content = _seasonEditor;

            _visibleTabItemsCount = MainTabControl.Items.Cast<TabItem>().Count(item => item.Visibility == Visibility.Visible);
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            OnPlayerSelectionChanged();
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
            if (_playerController.SelectedPlayer == null)
                CurrentlyLoggedInPlayerName = NoPlayerText;
            else
                CurrentlyLoggedInPlayerName = _playerController.SelectedPlayer.Name;
        }

        public void ShowOrHideTabs()
        {
            AuthenticationGroup? group = _playerController.SelectedPlayer?.Group;

            if (_playerController.SelectedPlayer == null)
                group = AuthenticationGroup.User;

            SeasonEditorTab.Visibility = group < AuthenticationGroup.Administrator ? Visibility.Collapsed : Visibility.Visible;
            ProjectCarsLiveTab.Visibility = group < AuthenticationGroup.Moderator ? Visibility.Collapsed : Visibility.Visible;

            _visibleTabItemsCount = MainTabControl.Items.Cast<TabItem>().Count(item => item.Visibility == Visibility.Visible);
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            HotkeyController.Init(this);
            HotkeyController.Register(9000, HotkeyController.None, VirtualKeyCode.VkAdd, ToggleWindowState);
        }

        private void OnChallengeResultEvent(ChallengeResult challengeResult)
        {
            if (_playerController?.SelectedPlayer == null)
                return;

            var wasDataAdded = DataView.AddChallengeResult(_playerController.SelectedPlayer.Id, challengeResult);

            if (!wasDataAdded)
                return;

            UpdateAllUIs(challengeResult.ToString());

            SaveData();
        }

        private void UpdateAllUIs(string challengeResultTrackLocationAndVariant = null)
        {
            _allChallengeStandings.UpdateUI();
            _seasonView.UpdateUI(challengeResultTrackLocationAndVariant);
            _championshipView.UpdateUI();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            HotkeyController.Clear();
            SaveData();
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

        private static void CloseApplication_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
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