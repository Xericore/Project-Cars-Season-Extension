using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Windows;
using ProjectCarsSeasonExtension.Annotations;

namespace ProjectCarsSeasonExtension.Views.Player
{
    /// <summary>
    /// Interaction logic for PasswordWindow.xaml
    /// </summary>
    public partial class PasswordWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsValidationPassed
        {
            get => _isValidationPassed;
            set
            {
                _isValidationPassed = value;
                OnPropertyChanged(nameof(IsValidationPassed));
            }
        }

        public string PasswordHash { get; set; }

        private readonly Models.Player.Player _player;
        private bool _isValidationPassed;

        public PasswordWindow(Models.Player.Player player)
        {
            _player = player;
            InitializeComponent();

            PasswordBox.Focus();
            IsValidationPassed = false;
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            IsValidationPassed = !string.IsNullOrEmpty(PasswordBox.Password);
        }

        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            /* Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(_player.PasswordHash);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(PasswordBox.Password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    DialogResult = false;
                    return;
                }
            }
            DialogResult = true;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
