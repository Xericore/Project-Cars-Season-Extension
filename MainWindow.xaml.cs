using ProjectCarsSeasonExtension.Controller;
using ProjectCarsSeasonExtension.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using ProjectCarsSeasonExtension.Models;
using ProjectCarsSeasonExtension.Serialization;
using Application = System.Windows.Application;

namespace ProjectCarsSeasonExtension
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly RoutedCommand _closeApplicationCommand = new RoutedCommand();
        private ChampionshipView _challengeStanding;

        public SeasonModel CurrentSeason { get; set; }
        public ObservableCollection<PlayerResult> PlayerResults { get; set; }
        public ObservableCollection<PlayerModel> Players { get; set; }

        // ----------------------------------------------------------------------------------------

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
            CurrentSeason = seasonReader.GetCurrentSeason();
            PlayerResults = seasonReader.GetPlayerResults();
            Players = new ObservableCollection<PlayerModel>();
        }
        
        // ----------------------------------------------------------------------------------------
        // listener
        // ----------------------------------------------------------------------------------------

        private void Window_Initialized(object sender, EventArgs e)
        {
            HighscoreViewFrame.Content = Injector.Get<HighscoreView>();
            PlayerSelectionFrame.Content = new PlayerSelection(Players);

            var championshipView = new ChampionshipView(CurrentSeason, PlayerResults, Players);
            PlayerResultsFrame.Content = championshipView;

            var seasonView = new SeasonView(championshipView.ChallengeStandings.Values);
            SeasonViewFrame.Content = seasonView;
        }

        // ----------------------------------------------------------------------------------------

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            HotkeyController.Init(this);
            HotkeyController.Register(9000, HotkeyController.None, VirtualKeyCode.VkAdd, ToggleWindowState);
        }

        // ----------------------------------------------------------------------------------------

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            HotkeyController.Clear();
        }

        // ----------------------------------------------------------------------------------------

        private void ToggleWindowState()
        {
            WindowState = WindowState == WindowState.Minimized ? WindowState.Normal : WindowState.Minimized;
        }

        // ----------------------------------------------------------------------------------------

        private void CloseApplication_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // ----------------------------------------------------------------------------------------
    }
}