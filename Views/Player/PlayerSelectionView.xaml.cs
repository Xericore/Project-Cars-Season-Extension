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

            if (!(button.DataContext is Models.Player.Player player))
                return;

            bool isNewPlayerClicked = player.Id == -1;
            bool doesPlayerHavePassword = !string.IsNullOrEmpty(player.PasswordHash);

            if (isNewPlayerClicked)
            {
                ShowAndHandleNewPlayerWindow();
            }
            else if (doesPlayerHavePassword)
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
                PasswordSalt = newPlayerWindow.NewPlayer.PasswordSalt
            };

            PlayerController.AddPlayer(newPlayer);
            OnPropertyChanged(nameof(PlayerController));
        }

        private void ShowAndHandlePlayerPassword(Models.Player.Player player)
        {
            var passwordWindow = new PasswordWindow(player);

            var dialogResult = passwordWindow.ShowDialog();

            PlayerController.SelectedPlayer = dialogResult == true ? player : null;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void EditPlayer_OnClick(object sender, RoutedEventArgs e)
        {
            var editPlayerWindow = new PlayerWindow(PlayerController, true);
            var dialogResult = editPlayerWindow.ShowDialog();

            if (dialogResult != true || editPlayerWindow.NewPlayer == null) return;

            PlayerController.SelectedPlayer.AvatarFileName = editPlayerWindow.NewPlayer.AvatarFileName;
        }
    }
}
