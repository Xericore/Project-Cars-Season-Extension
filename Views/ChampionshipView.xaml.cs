using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for ChampionshipView.xaml
    /// </summary>
    public partial class ChampionshipView : Page
    {
        public SeasonModel CurrentSeason { get; }
        public ObservableCollection<PlayerResult> PlayerResults { get; }
        public ObservableCollection<PlayerModel> Players { get; set; }

        public Dictionary<int, ChallengeStanding> ChallengeStandings { get; } =
            new Dictionary<int, ChallengeStanding>();

        public ObservableCollection<ChampionshipStanding> ChampionshipStandings { get; set; } =
            new ObservableCollection<ChampionshipStanding>();

        public ChampionshipView(SeasonModel currentSeason, ObservableCollection<PlayerResult> playerResults,
            ObservableCollection<PlayerModel> players)
        {
            CurrentSeason = currentSeason;
            PlayerResults = playerResults;
            Players = players;

            CreateChallengeStandings();

            CreateChampionshipStandings();

            InitializeComponent();

            GenerateChallengeColumns();
        }

        private void CreateChallengeStandings()
        {
            foreach (PlayerResult playerResult in PlayerResults)
            {
                if (!ChallengeStandings.ContainsKey(playerResult.ChallengeId))
                {
                    Challenge challenge = CurrentSeason.GetChallengeById(playerResult.ChallengeId);

                    ChallengeStanding challengeStanding = new ChallengeStanding(challenge);

                    ChallengeStandings.Add(playerResult.ChallengeId, challengeStanding);
                }

                PlayerModel foundPlayer = Players.FirstOrDefault(p => p.Id == playerResult.PlayerId);

                if(foundPlayer != null)
                {
                    ChallengeStandings[playerResult.ChallengeId].SortedPlayers
                        .Add(playerResult.FastestLap, foundPlayer);
                }
            }
        }

        private void CreateChampionshipStandings()
        {
            foreach (var player in Players)
            {
                if (player.Id < 0)
                    continue;

                foreach (var challengeStanding in ChallengeStandings)
                {
                    var foundPlayerStanding = ChampionshipStandings.FirstOrDefault(p => p.Player.Id == player.Id);

                    if (foundPlayerStanding == null)
                    {
                        foundPlayerStanding = new ChampionshipStanding(player);
                        ChampionshipStandings.Add(foundPlayerStanding);
                    }

                    var playerPoints = challengeStanding.Value.GetPlayerPoints(player.Id);
                    foundPlayerStanding.ChallengePoints.Add(playerPoints);
                }
            }
        }

        private void GenerateChallengeColumns()
        {
            int challengeCount = 0;
            foreach (var challengeStanding in ChallengeStandings.Values)
            {
                var column = new DataGridTextColumn
                {
                    Header = challengeStanding.Challenge.Name,
                    Binding = new Binding($"ChallengePoints[{challengeCount}]")
                };

                ChampionshipDataGrid.Columns.Add(column);
                challengeCount++;
            }

            GenerateTotalPointsColumn();
        }

        private void GenerateTotalPointsColumn()
        {
            var totalPointsColumn = new DataGridTextColumn
            {
                Header = "Total Points",
                Binding = new Binding("TotalPoints")
            };

            ChampionshipDataGrid.Columns.Add(totalPointsColumn);
        }
    }

    public static class PointsUtil
    {
        public static int PositionToPoints(int position)
        {
            int[] positionToPointsMapping = new[] {25, 18, 15, 12, 10, 8, 6, 4, 2, 1};

            if (position > positionToPointsMapping.Length)
                return 0;

            if (position < 0)
                throw new ArgumentOutOfRangeException("Provided position cannot be below zero.");

            return positionToPointsMapping[position - 1];
        }
    }
}