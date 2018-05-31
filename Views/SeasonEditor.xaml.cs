﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using ProjectCarsSeasonExtension.Annotations;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for SeasonEditor.xaml
    /// </summary>
    public partial class SeasonEditor : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Challenge> CurrentSeasonChallenges => _dataView.CurrentSeason.Challenges;

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

            UpdateFilteredChallenges();

            InitializeComponent();

            ChallengesComboBox.SelectedIndex = 0;
            SeasonComboBox.SelectedIndex = 0;
        }

        private void UpdateFilteredChallenges()
        {
            FilteredChallenges.Clear();
            foreach (var challenge in _dataView.AllChallenges)
            {
                if (!CurrentSeasonChallenges.Contains(challenge))                        
                    FilteredChallenges.Add(challenge);
            }

            OnPropertyChanged(nameof(SelectedSeason));
            OnPropertyChanged(nameof(CurrentSeasonChallenges));
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
                SelectedChallenge = SelectedSeason.Challenges.FirstOrDefault();
                ChallengesComboBox.SelectedIndex = 0;
            }
            else
            {
                SelectedChallenge = e.AddedItems[0] as Challenge;
            }
            
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

            SelectedSeason.Challenges.Add(_selectedFromAllChallenge);

            UpdateFilteredChallenges();
        }

        private void ButtonRemove_OnClick(object sender, RoutedEventArgs e)
        {
            SelectedSeason.Challenges.Remove(_selectedSeasonChallenge);

            UpdateFilteredChallenges();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
