using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ProjectCarsSeasonExtension.Annotations;
using ProjectCarsSeasonExtension.Models.Player;
using ProjectCarsSeasonExtension.Views.Player;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for PlayerWindow.xaml
    /// </summary>
    public partial class PlayerWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public NewPlayer NewPlayer { get; set; }

        public bool IsNameValidationPassed
        {
            get => _isNameValidationPassed;
            set
            {
                _isNameValidationPassed = value;
                OnPropertyChanged();
            }
        }

        public bool IsPasswordValidationPassed
        {
            get => _isPasswordValidationPassed;
            set
            {
                _isPasswordValidationPassed = value;
                OnPropertyChanged();
            }
        }

        public bool AreAllValidationsPassed => IsNameValidationPassed && IsPasswordValidationPassed;

        public bool UsePassword
        {
            get => _usePassword;
            set
            {
                _usePassword = value;
                if (!_usePassword)
                {
                    PasswordBoxFirst.Clear();
                    PasswordBoxRepeated.Clear();
                }
                OnPropertyChanged();
            }
        }

        private readonly Brush _passwordWrongBackground = Brushes.Salmon;
        private readonly Brush _passwordOriginalBackground;

        private bool _isNameValidationPassed;
        private bool _isPasswordValidationPassed = true;
        private bool _usePassword;

        public PlayerWindow(PlayerController playerController, bool isInEditMode = false)
        {
            var alreadyPresentPlayers = playerController.Players;
            NewPlayer = new NewPlayer(alreadyPresentPlayers);

            InitializeComponent();

            _passwordOriginalBackground = PasswordBoxFirst.Background;

            TextBoxNewPlayerName.Focus();
        }

        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PasswordBoxFirst.Password))
            {
                SetPasswordSaltAndHash();
            }

            DialogResult = true;
        }

        private void SetPasswordSaltAndHash()
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(PasswordBoxFirst.Password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            NewPlayer.PasswordSalt = Convert.ToBase64String(salt);
            NewPlayer.PasswordHash = Convert.ToBase64String(hashBytes);            
        }

        private void NameValidation_Error(object sender, ValidationErrorEventArgs e)
        {
            IsNameValidationPassed = e.Action != ValidationErrorEventAction.Added;
            OnPropertyChanged(nameof(AreAllValidationsPassed));
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            IsPasswordValidationPassed = PasswordBoxFirst.Password == PasswordBoxRepeated.Password;

            PasswordBoxRepeated.Background = !IsPasswordValidationPassed ? _passwordWrongBackground : _passwordOriginalBackground;

            OnPropertyChanged(nameof(AreAllValidationsPassed));
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ChangeImage_OnClick(object sender, RoutedEventArgs e)
        {
            var imageSelectionWindow = new ImageSelectionWindow();
            imageSelectionWindow.Show();
        }
    }
}
