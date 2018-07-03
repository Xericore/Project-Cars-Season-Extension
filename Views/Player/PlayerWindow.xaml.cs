using System;
using System.ComponentModel;
using System.IO;
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

        public bool IsInEditMode
        {
            get => _isInEditMode;
            set
            {
                _isInEditMode = value;
                if(TextBoxNewPlayerName != null)
                    TextBoxNewPlayerName.Visibility = _isInEditMode ? Visibility.Collapsed : Visibility.Visible;
                OnPropertyChanged();
            }
        }

        public NewPlayer NewPlayer { get; set; }
        
        public bool IsNameValidationPassed
        {
            get => _isNameValidationPassed;
            set
            {
                _isNameValidationPassed = value || IsInEditMode;
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
                if (!_usePassword || IsInEditMode)
                {
                    PasswordBoxFirst.Clear();
                    PasswordBoxRepeated.Clear();
                }
                
                if(_usePassword)
                    PasswordBoxFirst.Focus();

                OnPropertyChanged();
            }
        }

        private const string DummyPassword = "$DummyPassword$";
        private readonly Brush _passwordWrongBackground = Brushes.Salmon;
        private readonly Brush _passwordOriginalBackground;

        private bool _isNameValidationPassed;
        private bool _isPasswordValidationPassed = true;
        private bool _usePassword;
        private bool _isInEditMode;


        public PlayerWindow(PlayerController playerController, bool isInEditMode = false)
        {
            var alreadyPresentPlayers = playerController.Players;
            NewPlayer = new NewPlayer(alreadyPresentPlayers) {AvatarFileName = "/Assets/Players/Default.png"};

            IsInEditMode = isInEditMode;

            InitializeComponent();

            _passwordOriginalBackground = PasswordBoxFirst.Background;

            if (IsInEditMode)
            {
                PlayerNameGrid.IsEnabled = false;
                PlayerNameGrid.Visibility = Visibility.Collapsed;
                ChangeImageButton.Focus();
                Title = "Edit player";

                if (!string.IsNullOrEmpty(playerController.SelectedPlayer.PasswordHash))
                {
                    EnablePasswordCheckBox.Content = "Change optional password";
                    PasswordBoxFirst.Password = DummyPassword;
                    PasswordBoxRepeated.Password = DummyPassword;
                }

                NewPlayer.AvatarFileName = playerController.SelectedPlayer.AvatarFileName;
                OnPropertyChanged(nameof(NewPlayer));
            }
            else
            {
                TextBoxNewPlayerName.Focus();
            }
        }

        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PasswordBoxFirst.Password) && 
                !string.Equals(PasswordBoxFirst.Password, DummyPassword))
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

        private void ChangeImage_OnClick(object sender, RoutedEventArgs e)
        {
            var imageSelectionWindow = new ImageSelectionWindow();
            var dialogResult = imageSelectionWindow.ShowDialog();

            if (dialogResult == false || imageSelectionWindow.SelectedImage == null)
                return;

            SetRelativeAvatarFileName(imageSelectionWindow);
        }

        private void SetRelativeAvatarFileName(ImageSelectionWindow imageSelectionWindow)
        {
            try
            {
                var selectedImagePath = imageSelectionWindow.SelectedImage.Source.ToString();

                var currentDirectory = Environment.CurrentDirectory;
                currentDirectory = currentDirectory.Replace("\\", "/");

                // Will be e.g. /Assets/Players/Avatars/001-man-13.png
                var relativeImagePathWitoutCurrentDirectory =
                    selectedImagePath.Remove(0, selectedImagePath.LastIndexOf(currentDirectory) + currentDirectory.Length);

                NewPlayer.AvatarFileName = relativeImagePathWitoutCurrentDirectory;
                OnPropertyChanged(nameof(NewPlayer));
            }
            catch (Exception)
            {
                // ignored
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
