using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using ProjectCarsSeasonExtension.Annotations;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for NewPlayerWindow.xaml
    /// </summary>
    public partial class NewPlayerWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public NewPlayer NewPlayer { get; set; } = new NewPlayer();

        public string PlayerName => NewPlayer.Name;

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

        public NewPlayerWindow()
        {
            InitializeComponent();
            TextBoxNewPlayerName.Focus();
        }

        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            NewPlayer.Name = TextBoxNewPlayerName.Text;
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
        public string Name { get; set; }

        public string this[string columnName]
        {
            get
            {
                string result = null;

                if (columnName != "Name") return null;

                if (string.IsNullOrEmpty(Name))
                    result = "Please enter a name.";

                return result;
            }
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
