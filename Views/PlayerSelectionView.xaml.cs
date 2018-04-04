using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using ProjectCarsSeasonExtension.Annotations;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for PlayerSelection.xaml
    /// </summary>
    public partial class PlayerSelection : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<PlayerModel> Players { get; set; }

        public PlayerModel SelectedPlayer
        {
            get
            {
                return _selectedPlayer;
            }

            set
            {
                _selectedPlayer = value;
                OnPropertyChanged();
            }
        }

        private PlayerModel _selectedPlayer;

        public PlayerSelection(ObservableCollection<PlayerModel> players)
        {
            Players = players;

            AddPlayerDummyData();

            InitializeComponent();
        }

        private void AddPlayerDummyData()
        {
            Players.Add(new PlayerModel { Id = 0, Name = "Sascha" });
            Players.Add(new PlayerModel { Id = 1, Name = "Mario" });
            Players.Add(new PlayerModel { Id = 2, Name = "Schumacher" });

            Players.Add(new PlayerModel { Id = -1, Name = "New player" });
        }

        private void PlayerSelected_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button button))
                return;

            if (!(button.DataContext is PlayerModel playerModel))
                return;

            bool isNewPlayerClicked = playerModel.Id == -1;

            if (isNewPlayerClicked)
            {
                ShowAndHandleNewPlayerWindow();
            }
            else
            {
                SelectedPlayer = playerModel;
            }
        }

        private void ShowAndHandleNewPlayerWindow()
        {
            var newPlayerWindow = new NewPlayerWindow(Players);

            var dialogResult = newPlayerWindow.ShowDialog();

            if (dialogResult != true || string.IsNullOrEmpty(newPlayerWindow.PlayerName)) return;

            var maxId = Players.Max(p => p.Id);

            PlayerModel newPlayer = new PlayerModel
            {
                Id = ++maxId,
                Name = newPlayerWindow.PlayerName
            };

            Players.Add(newPlayer);

            MoveAddPlayerToEnd();

            SelectedPlayer = newPlayer;
        }

        private void MoveAddPlayerToEnd()
        {
            PlayerModel addPlayer = Players.First(p => p.Id == -1);

            if (addPlayer == null) return;

            Players.Remove(addPlayer);
            Players.Add(addPlayer);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
