using ProjectCarsSeasonExtension.Controller;
using ProjectCarsSeasonExtension.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ProjectCarsSeasonExtension.Models;
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

            DataView = new DataView(seasonReader.GetCurrentSeason(), seasonReader.GetPlayerResults(), new ObservableCollection<PlayerModel>());
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            PlayerSelectionFrame.Content = new PlayerSelection(DataView.Players);

            var allChallengeStandings = new AllChallengeStandings(DataView);

            var championshipView = new ChampionshipView(DataView, allChallengeStandings);
            PlayerResultsFrame.Content = championshipView;

            var seasonView = new SeasonView(allChallengeStandings.ChallengeStandings.Values);
            SeasonViewFrame.Content = seasonView;
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            HotkeyController.Init(this);
            HotkeyController.Register(9000, HotkeyController.None, VirtualKeyCode.VkAdd, ToggleWindowState);
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            HotkeyController.Clear();
        }

        private void ToggleWindowState()
        {
            WindowState = WindowState == WindowState.Minimized ? WindowState.Normal : WindowState.Minimized;
        }

        private void CloseApplication_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}