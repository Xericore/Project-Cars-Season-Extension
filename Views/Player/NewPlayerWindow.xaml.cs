using System;
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
            DialogResult = true;
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

    public class NewPlayer: IDataErrorInfo
    {
        private readonly ObservableCollection<Player> _alreadyPresentPlayers;

        public NewPlayer(ObservableCollection<Player> alreadyPresentPlayers)
        {
            _alreadyPresentPlayers = alreadyPresentPlayers;
        }

        public string Name { get; set; }

        public string this[string columnName]
        {
            get
            {
                string result = null;

                if (columnName != "Name") return null;

                if (string.IsNullOrEmpty(Name))
                    result = "Please enter a name.";

                if (_alreadyPresentPlayers.Any(p => p.Name == Name))
                    result = "Player is already present. Please choose a different name.";

                return result;
            }
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
