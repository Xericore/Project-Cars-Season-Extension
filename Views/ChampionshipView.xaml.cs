﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ProjectCarsSeasonExtension.Annotations;
using ProjectCarsSeasonExtension.Models;
using ProjectCarsSeasonExtension.Models.Player;
using ProjectCarsSeasonExtension.Utils;
using ProjectCarsSeasonExtension.ViewModels;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for ChampionshipView.xaml
    /// </summary>
    public partial class ChampionshipView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly DataView _dataView;
        private readonly AllChallengeStandings _allChallengeStandings;
        private readonly PlayerController _playerController;

        public ObservableCollection<ChampionshipStanding> ChampionshipStandings { get; set; } = new ObservableCollection<ChampionshipStanding>();

        public ChampionshipStanding SelectedChampionshipStanding { get; set; }

        public ChampionshipView(DataView dataView, AllChallengeStandings allChallengeStandings, PlayerController playerController)
        {
            _dataView = dataView;
            _allChallengeStandings = allChallengeStandings;

            _playerController = playerController;

            _playerController.PlayerSelectionChanged += PlayerController_OnPlayerSelectionChanged;

            CreateChampionshipStandings();

            InitializeComponent();

            CreateAllColumns();
        }

        private void PlayerController_OnPlayerSelectionChanged()
        {
            SelectedChampionshipStanding = null;

            foreach (var championshipStanding in ChampionshipStandings)
            {
                if (championshipStanding.Player != _playerController.SelectedPlayer) continue;

                SelectedChampionshipStanding = championshipStanding;
                break;
            }
            OnPropertyChanged(nameof(SelectedChampionshipStanding));
        }

        private void CreateChampionshipStandings()
        {
            ChampionshipStandings.Clear();

            foreach (var player in _dataView.Players)
            {
                if (player.Id < 0)
                    continue;

                var championshipStanding = new ChampionshipStanding(player);

                foreach (var challengeStanding in _allChallengeStandings.ChallengeStandings)
                {
                    var playerPoints = challengeStanding.Value.GetPlayerPoints(player.Id);
                    championshipStanding.ChallengePoints.Add(playerPoints);
                }

                if(championshipStanding.TotalPoints > 0)
                    ChampionshipStandings.Add(championshipStanding);
            }

            ChampionshipStandings.Sort();
        }

        private void CreateAllColumns()
        {
            DeleteAllColumnsExceptFirstThree();
            
            CreateRaceColumns();
        }

        private void DeleteAllColumnsExceptFirstThree()
        {
            while (ChampionshipDataGrid.Columns.Count > 3)
            {
                ChampionshipDataGrid.Columns.RemoveAt(3);
            }
        }

        private void CreateRaceColumns()
        {
            int challengeCount = 0;
            foreach (var challengeStanding in _allChallengeStandings.ChallengeStandings.Values)
            {
                if (!_dataView.CurrentSeason.ContainsChallenge(challengeStanding.Challenge.Id))
                    continue;

                var column = new DataGridTextColumn
                {
                    Header = UiUtils.GetTrackImage(challengeStanding.Challenge),
                    Width = 128,
                    Binding = new Binding($"ChallengePoints[{challengeCount}]"),
                    FontWeight = FontWeights.Normal
                };

                ChampionshipDataGrid.Columns.Add(column);
                challengeCount++;
            }
        }

        public void UpdateUI()
        {
            CreateChampionshipStandings();
            CreateAllColumns();
            OnPropertyChanged(nameof(ChampionshipStandings));
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}