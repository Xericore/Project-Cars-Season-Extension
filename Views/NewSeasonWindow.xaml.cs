using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using ProjectCarsSeasonExtension.Annotations;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for NewSeasonWindow.xaml
    /// </summary>
    public partial class NewSeasonWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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

        public Season NewSeason { get; set; } = new Season();

        public NewSeasonWindow()
        {
            InitializeComponent();
            
            StartDatePicker.SelectedDate = DateTime.Today;
            EndDatePicker.SelectedDate = DateTime.Today + new TimeSpan(7, 0, 0, 0);

            TextBoxNewSeasonName.Focus();
        }

        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            UpdateValidation(e);
        }

        private void UpdateValidation(ValidationErrorEventArgs e = null)
        {
            IsValidationPassed = e?.Action != ValidationErrorEventAction.Added
                                 && !string.IsNullOrEmpty(NewSeason.Name)
                                 && !string.IsNullOrEmpty(NewSeason.Description)
                                 && StartDatePicker.SelectedDate < EndDatePicker.SelectedDate;
        }

        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void StartDatePicker_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StartDatePicker?.SelectedDate > EndDatePicker?.SelectedDate)
                EndDatePicker.SelectedDate = StartDatePicker.SelectedDate + new TimeSpan(1, 0, 0, 0);

            UpdateValidation();
        }

        private void EndDatePicker_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StartDatePicker?.SelectedDate > EndDatePicker?.SelectedDate)
                StartDatePicker.SelectedDate = EndDatePicker.SelectedDate - new TimeSpan(1, 0, 0, 0);

            UpdateValidation();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateValidation();
        }


    }
}
