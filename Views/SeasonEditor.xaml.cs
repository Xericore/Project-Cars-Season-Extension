﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using ProjectCarsSeasonExtension.Annotations;
using ProjectCarsSeasonExtension.Models;
using ProjectCarsSeasonExtension.Properties;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for SeasonEditor.xaml
    /// </summary>
    public partial class SeasonEditor : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event Action SeasonChanged;

        public ObservableCollection<Challenge> AllChallenges => _dataView.AllChallenges;

        public ObservableCollection<Challenge> FilteredChallenges { get; set; } = new ObservableCollection<Challenge>();

        public Challenge SelectedChallenge { get; set; }

        public ObservableCollection<Season> Seasons => _dataView.Seasons;
        public Season SelectedSeason { get; set; }

        private readonly DataView _dataView;
        private Challenge _selectedSeasonChallenge;
        private Challenge _selectedFromAllChallenge;
        

        public SeasonEditor(DataView dataView)
        {
            _dataView = dataView;
            _dataView.ChallengesChanged += UpdateFilteredChallenges;

            InitializeComponent();

            ChallengesComboBox.SelectedIndex = 0;
            DifficultySelector.SelectedItem = SelectedChallenge?.Difficulty;

            CurrentlyActiveSeasonSelector.SelectedItem = _dataView.CurrentSeason;
            SeasonComboBox.SelectedItem = _dataView.CurrentSeason;

            UpdateFilteredChallenges();
        }

        private void UpdateFilteredChallenges()
        {
            if (SelectedSeason == null)
                return;

            FilteredChallenges.Clear();
            foreach (var challenge in _dataView.AllChallenges)
            {
                if (!SelectedSeason.ContainsChallenge(challenge.Id))                        
                    FilteredChallenges.Add(challenge);
            }

            SeasonChanged?.Invoke();
        }

        private void SeasonSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                SelectedSeason = e.AddedItems[0] as Season;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            OnPropertyChanged(nameof(SelectedSeason));
        }

        private void ChallengeSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count <= 0)
            {
                SelectedChallenge = AllChallenges.FirstOrDefault();
                ChallengesComboBox.SelectedIndex = 0;
            }
            else
            {
                SelectedChallenge = e.AddedItems[0] as Challenge;
            }

            DifficultySelector.SelectedItem = SelectedChallenge?.Difficulty;
            OnPropertyChanged(nameof(SelectedChallenge));
        }

        private void SeasonChallenges_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
                _selectedSeasonChallenge = e.AddedItems[0] as Challenge;
        }

        private void AllChallenges_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count > 0)
                _selectedFromAllChallenge = e.AddedItems[0] as Challenge;
        }

        private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
        {
            if (_selectedFromAllChallenge == null || SelectedSeason.ContainsChallenge(_selectedFromAllChallenge.Id))
                return;

            SelectedSeason.AddChallenge(_selectedFromAllChallenge);

            UpdateFilteredChallenges();
        }

        private void ButtonRemove_OnClick(object sender, RoutedEventArgs e)
        {
            SelectedSeason.RemoveChallenge(_selectedSeasonChallenge);

            UpdateFilteredChallenges();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ButtonAddChallenge_OnClick(object sender, RoutedEventArgs e)
        {
            var newChallengeWindow = new NewChallengeWindow(_dataView);

            var dialogResult = newChallengeWindow.ShowDialog();

            if (dialogResult != true) return;

            int maxChallengeId = 0;

            if(_dataView.AllChallenges.Count > 0)
                maxChallengeId = _dataView.AllChallenges.Max(c => c.Id) + 1;

            Challenge newChallenge = new Challenge
            {
                Id = maxChallengeId,
                TrackName = newChallengeWindow.NewChallenge.TrackName,
                CarName = newChallengeWindow.NewChallenge.CarName,
                Description = newChallengeWindow.NewChallenge.Description,
                Difficulty = newChallengeWindow.NewChallenge.Difficulty
            };

            _dataView.AddChallenge(newChallenge);

            ChallengesComboBox.SelectedIndex = ChallengesComboBox.Items.Count-1;

            UpdateFilteredChallenges();
        }

        private void ButtonRemoveChallenge_OnClick(object sender, RoutedEventArgs e)
        {
            string messageBoxText = $"Do you really want to delete \"{ChallengesComboBox.SelectedItem}\"? This might affect current seasons and results.";
            const string caption = "Season Editor";

            var messageBoxResult = MessageBox.Show(messageBoxText, caption, MessageBoxButton.YesNo, MessageBoxImage.Warning);

            switch (messageBoxResult)
            {
                case MessageBoxResult.Yes:
                    _dataView.RemoveChallenge(SelectedChallenge);
                    SelectedChallenge = AllChallenges.FirstOrDefault();
                    OnPropertyChanged(nameof(SelectedChallenge));
                    UpdateFilteredChallenges();
                    break;
            }
        }

        private void ButtonAddSeason_OnClick(object sender, RoutedEventArgs e)
        {
            var newSeasonWindow = new NewSeasonWindow();
            var dialogResult = newSeasonWindow.ShowDialog();
            if (dialogResult != true) return;

            var newSeason = new Season
            {
                Name = newSeasonWindow.NewSeason.Name,
                Description = newSeasonWindow.NewSeason.Description,
                StartDate = newSeasonWindow.NewSeason.StartDate,
                EndDate = newSeasonWindow.NewSeason.EndDate
            };

            _dataView.AddSeason(newSeason);
            SelectedSeason = Seasons.FirstOrDefault(s => s.Name == newSeason.Name);
            OnPropertyChanged(nameof(SelectedSeason));
            SeasonComboBox.SelectedItem = SelectedSeason;
        }

        private void ButtonRemoveSeason_OnClick(object sender, RoutedEventArgs e)
        {
            string messageBoxText = $"Do you really want to delete season \"{SeasonComboBox.SelectedItem}\"?";
            const string caption = "Season Editor";

            var messageBoxResult = MessageBox.Show(messageBoxText, caption, MessageBoxButton.YesNo, MessageBoxImage.Warning);

            switch (messageBoxResult)
            {
                case MessageBoxResult.Yes:
                    _dataView.RemoveSeason(SelectedSeason);
                    SelectedSeason = Seasons.FirstOrDefault();
                    OnPropertyChanged(nameof(SelectedSeason));
                    SeasonComboBox.SelectedItem = SelectedSeason;
                    break;
            }
        }

        private void CurrentlyActiveSeasonSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count < 0)
                return;

            if (!(e.AddedItems[0] is Season selectedSeason))
                return;

            Settings.Default.CurrentSeasonId = selectedSeason.Id;
            Settings.Default.Save();
        }

        private void DifficultySelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = e.AddedItems[0];
            SelectedChallenge.Difficulty = (Difficulty)selectedItem;
        }
    }
}
