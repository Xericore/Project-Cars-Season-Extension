using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using ProjectCarsSeasonExtension.Annotations;
using ProjectCarsSeasonExtension.Models;
using ProjectCarsSeasonExtension.Models.Player;
using ProjectCarsSeasonExtension.Views.Player;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for NewPlayerWindow.xaml
    /// </summary>
    public partial class NewPlayerWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public NewPlayer NewPlayer { get; set; }
        
        private bool _isValidationPassed;

        public bool IsValidationPassed
        {
            get => _isValidationPassed;
            set
            {
                _isValidationPassed = value;
                OnPropertyChanged();
            }
        }

        public NewPlayerWindow(PlayerController playerController)
        {
            var alreadyPresentPlayers = playerController.Players;
            NewPlayer = new NewPlayer(alreadyPresentPlayers);

            InitializeComponent();
            
            TextBoxNewPlayerName.Focus();
        }

        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxPassword.Text))
            {
                SetPasswordSaltAndHash();
            }

            DialogResult = true;
        }

        private void SetPasswordSaltAndHash()
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(TextBoxPassword.Text, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            NewPlayer.PasswordSalt = Convert.ToBase64String(salt);
            NewPlayer.PasswordHash = Convert.ToBase64String(hashBytes);            
        }

        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            IsValidationPassed = e.Action != ValidationErrorEventAction.Added;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
