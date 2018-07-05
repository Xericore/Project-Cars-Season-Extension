using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using ProjectCarsSeasonExtension.Annotations;
using ProjectCarsSeasonExtension.Models.Player;
using ProjectCarsSeasonExtension.Views.Player;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for PlayerSelection.xaml
    /// </summary>
    public partial class PlayerSelection : Page, INotifyPropertyChanged
    {
        public PlayerController PlayerController { get; }

        public Visibility RemovePlayerButtonVisibility
        {
            get => _removePlayerButtonVisibility;
            set
            {
                _removePlayerButtonVisibility = value;
                OnPropertyChanged();
            }
        }
        private Visibility _removePlayerButtonVisibility;

        public PlayerSelection(PlayerController playerController)
        {
            PlayerController = playerController;
            PlayerController.PlayerSelectionChanged += PlayerControllerOnPlayerSelectionChanged;
            InitializeComponent();
            SetRemovePlayerButtonVisibility();
        }

        private void PlayerControllerOnPlayerSelectionChanged()
        {
            SetRemovePlayerButtonVisibility();
        }

        private void SetRemovePlayerButtonVisibility()
        {
            if (PlayerController?.SelectedPlayer != null &&
                PlayerController.SelectedPlayer.Group >= AuthenticationGroup.Moderator)
            {
                RemovePlayerButtonVisibility = Visibility.Visible;
            }
            else
            {
                RemovePlayerButtonVisibility = Visibility.Collapsed;
            }
        }

        private void PlayerSelected_OnClick(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count <= 0)
                return;

            if (!(e.AddedItems[0] is Models.Player.Player player))
                return;

            bool doesPlayerHavePassword = !string.IsNullOrEmpty(player.PasswordHash);

            if (doesPlayerHavePassword)
                ShowAndHandlePlayerPassword(player);
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

            if (dialogResult == false)
                PlayerController.LogoutCurrentPlayer();
        }

        private void EditPlayer_OnClick(object sender, RoutedEventArgs e)
        {
            var editPlayerWindow = new PlayerWindow(PlayerController, true);
            var dialogResult = editPlayerWindow.ShowDialog();

            NewPlayer editedPlayer = editPlayerWindow.NewPlayer;

            if (dialogResult != true || editedPlayer == null) return;

            PlayerController.SelectedPlayer.AvatarFileName = editedPlayer.AvatarFileName;

            if (!string.IsNullOrEmpty(editedPlayer.PasswordHash))
            {
                PlayerController.SelectedPlayer.PasswordHash = editedPlayer.PasswordHash;
                PlayerController.SelectedPlayer.PasswordSalt = editedPlayer.PasswordSalt;
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

        private void RemovePlayer_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
