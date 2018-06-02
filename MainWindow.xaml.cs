using ProjectCarsSeasonExtension.Controller;
using ProjectCarsSeasonExtension.Views;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
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
    public partial class MainWindow : Window
    {
        private readonly RoutedCommand _closeApplicationCommand = new RoutedCommand();
        private ProjectCarsLiveView _projectCarsLiveView;
        private PlayerController _playerController;
        private SeasonView _seasonView;
        private AllChallengeStandings _allChallengeStandings;
        private ChampionshipView _championshipView;
        private SeasonEditor _seasonEditor;

        public DataView DataView { get; private set; }

        public MainWindow()
        {
            ReadSeasonData();

            InitializeComponent();
            _closeApplicationCommand.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(_closeApplicationCommand, CloseApplication_Executed));
        }

        private void ReadSeasonData()
        {
            ISeasonReader seasonReader = new DummySeasonReader();
            DataView = new DataView(seasonReader);
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            _allChallengeStandings = new AllChallengeStandings(DataView);
            _playerController = new PlayerController(DataView.Players);

            PlayerSelectionFrame.Content = new PlayerSelection(_playerController);

            _championshipView = new ChampionshipView(DataView, _allChallengeStandings);
            PlayerResultsFrame.Content = _championshipView;
            
            _seasonView = new SeasonView(_allChallengeStandings.ChallengeStandings.Values);
            SeasonViewFrame.Content = _seasonView;

            _projectCarsLiveView = new ProjectCarsLiveView();
            _projectCarsLiveView.ChallengeResultEvent += OnChallengeResultEvent;
            ProjectCarsLiveFrame.Content = _projectCarsLiveView;

            _seasonEditor = new SeasonEditor(DataView);
            _seasonEditor.SeasonChanged += UpdateAllUIs;
            SeasonEditorFrame.Content = _seasonEditor;
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

            DataView?.AddChallengeResult(_playerController.SelectedPlayer.Id, challengeResult);

            UpdateAllUIs();

            SaveData();
        }

        private void UpdateAllUIs()
        {
            _allChallengeStandings.UpdateUI();
            _seasonView.UpdateUI();
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
            seasonWriter.SaveChallenges(DataView.CurrentSeason.Challenges);
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
    }
}