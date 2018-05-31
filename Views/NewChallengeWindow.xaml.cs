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
    public partial class NewChallengeWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public NewChallenge NewChallenge { get; set; }

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

        public NewChallengeWindow(DataView dataView)
        {
            var alreadyPresentChallenges = dataView.AllChallenges;
            NewChallenge = new NewChallenge(alreadyPresentChallenges);

            InitializeComponent();

            DifficultySelector.SelectedIndex = 0;
            TextBoxNewTrackName.Focus();
        }

        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            IsValidationPassed = e.Action != ValidationErrorEventAction.Added 
                                 && !string.IsNullOrEmpty(NewChallenge.TrackName) 
                                 && !string.IsNullOrEmpty(NewChallenge.CarName);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void DifficultySelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = e.AddedItems[0];
            NewChallenge.Difficulty = (Difficulty) selectedItem;
        }
    }

    public class NewChallenge : IDataErrorInfo
    {
        private readonly ObservableCollection<Challenge> _alreadyPresentChallenges;

        public NewChallenge(ObservableCollection<Challenge> alreadyPresentChallenges)
        {
            _alreadyPresentChallenges = alreadyPresentChallenges;
        }

        public string TrackName { get; set; }
        public string CarName { get; set; }
        public string Description { get; set; }
        public Difficulty Difficulty { get; set; } = Difficulty.Easy;

        public string this[string columnName]
        {
            get
            {
                string result = null;

                if (columnName != "TrackName" && columnName != "CarName") return null;

                if (columnName == "TrackName" && string.IsNullOrEmpty(TrackName))
                    result = "Please enter a track name.";

                if (columnName == "CarName" && string.IsNullOrEmpty(CarName))
                    result = "Please enter a car name.";

                if (_alreadyPresentChallenges.Any(p => p.TrackName == TrackName && p.CarName == CarName))
                    result = "Challenge is already present. Please choose a different track or car.";

                return result;
            }
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
