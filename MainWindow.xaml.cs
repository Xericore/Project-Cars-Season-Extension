using ProjectCarsSeasonExtension.Controller;
using ProjectCarsSeasonExtension.Views;
using System;
using System.Collections.ObjectModel;
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
            var allChallengeStandings = new AllChallengeStandings(DataView);
            var playerController = new PlayerController(DataView.Players);

            PlayerSelectionFrame.Content = new PlayerSelection(playerController);
            PlayerResultsFrame.Content = new ChampionshipView(DataView, allChallengeStandings);
            SeasonViewFrame.Content = new SeasonView(allChallengeStandings.ChallengeStandings.Values);
            ProjectCarsLiveFrame.Content = new ProjectCarsLiveView();
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            HotkeyController.Init(this);
            HotkeyController.Register(9000, HotkeyController.None, VirtualKeyCode.VkAdd, ToggleWindowState);
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
            seasonWriter.SaveSeason(DataView.CurrentSeason);
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