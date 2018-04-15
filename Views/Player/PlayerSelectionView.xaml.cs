using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using ProjectCarsSeasonExtension.Annotations;
using ProjectCarsSeasonExtension.Models;
using ProjectCarsSeasonExtension.Models.Player;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for PlayerSelection.xaml
    /// </summary>
    public partial class PlayerSelection : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public PlayerController PlayerController { get; }

        public PlayerSelection(PlayerController playerController)
        {
            PlayerController = playerController;
            InitializeComponent();
        }

        private void PlayerSelected_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button button))
                return;

            if (!(button.DataContext is Player player))
                return;

            bool isNewPlayerClicked = player.Id == -1;

            if (isNewPlayerClicked)
            {
                ShowAndHandleNewPlayerWindow();
            }
            else
            {
                PlayerController.SelectedPlayer = player;
            }
        }

        private void ShowAndHandleNewPlayerWindow()
        {
            var newPlayerWindow = new NewPlayerWindow(PlayerController);

            var dialogResult = newPlayerWindow.ShowDialog();

            if (dialogResult != true || string.IsNullOrEmpty(newPlayerWindow.PlayerName)) return;

            Player newPlayer = new Player
            {
                Name = newPlayerWindow.PlayerName
            };

            PlayerController.AddPlayer(newPlayer);
            OnPropertyChanged(nameof(PlayerController));
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
