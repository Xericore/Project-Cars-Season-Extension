﻿using System.Windows;
using System.Windows.Controls;
using ProjectCarsSeasonExtension.Models.Player;
using ProjectCarsSeasonExtension.Views.Player;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for PlayerSelection.xaml
    /// </summary>
    public partial class PlayerSelection : Page
    {
        public PlayerController PlayerController { get; }

        public PlayerSelection(PlayerController playerController)
        {
            PlayerController = playerController;
            InitializeComponent();
        }

        private void PlayerSelected_OnClick(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count <= 0)
                return;

            if (!(e.AddedItems[0] is Models.Player.Player player))
                return;
            
            bool doesPlayerHavePassword = !string.IsNullOrEmpty(player.PasswordHash);

            if (doesPlayerHavePassword)
            {
                ShowAndHandlePlayerPassword(player);
            }
            else
            {
                PlayerController.SelectedPlayer = player;
            }
        }

        private void ShowAndHandleNewPlayerWindow()
        {
            var newPlayerWindow = new PlayerWindow(PlayerController);

            var dialogResult = newPlayerWindow.ShowDialog();

            if (dialogResult != true || string.IsNullOrEmpty(newPlayerWindow.NewPlayer.Name)) return;

            var newPlayer = new Models.Player.Player()
            {
                Name = newPlayerWindow.NewPlayer.Name,
                PasswordHash = newPlayerWindow.NewPlayer.PasswordHash,
                PasswordSalt = newPlayerWindow.NewPlayer.PasswordSalt,
                AvatarFileName = newPlayerWindow.NewPlayer.AvatarFileName
            };

            PlayerController.AddPlayer(newPlayer);
        }

        private void ShowAndHandlePlayerPassword(Models.Player.Player player)
        {
            var passwordWindow = new PasswordWindow(player);

            var dialogResult = passwordWindow.ShowDialog();

            if (dialogResult == true)
                PlayerController.SelectedPlayer = player;
            else
                PlayerController.LogoutCurrentPlayer();
        }

        private void EditPlayer_OnClick(object sender, RoutedEventArgs e)
        {
            var editPlayerWindow = new PlayerWindow(PlayerController, true);
            var dialogResult = editPlayerWindow.ShowDialog();

            if (dialogResult != true || editPlayerWindow.NewPlayer == null) return;

            PlayerController.SetSelectedPlayerAvatar(editPlayerWindow.NewPlayer.AvatarFileName);

            if (!string.IsNullOrEmpty(editPlayerWindow.NewPlayer.PasswordHash))
            {
                PlayerController.SelectedPlayer.PasswordHash = editPlayerWindow.NewPlayer.PasswordHash;
                PlayerController.SelectedPlayer.PasswordSalt = editPlayerWindow.NewPlayer.PasswordSalt;
            }

            var lastSelectedPlayer = PlayerController.SelectedPlayer;

            // Calling OnPropertyChanged() doesn't work for any of the property (PlayerController, etc.).
            // Thus I had to resort to this workaround.
            PlayersItemsControl.ItemsSource = null;
            PlayersItemsControl.ItemsSource = PlayerController.Players;

            PlayerController.SelectedPlayer = lastSelectedPlayer;
        }

        private void LogoutPlayer_OnClick(object sender, RoutedEventArgs e)
        {
            PlayerController.LogoutCurrentPlayer();
        }

        private void NewPlayer_OnClick(object sender, RoutedEventArgs e)
        {
            ShowAndHandleNewPlayerWindow();
        }
    }
}
