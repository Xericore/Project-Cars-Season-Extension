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

        public Dictionary<int, ChallengeStandings> ChallengeStandings { get; } =
            new Dictionary<int, ChallengeStandings>();

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
                    ChallengeStandings challengeStandings = new ChallengeStandings(playerResult.ChallengeId);

                    ChallengeStandings.Add(playerResult.ChallengeId, challengeStandings);
                }

                ChallengeStandings[playerResult.ChallengeId].SortedPlayers
                    .Add(playerResult.FastestLap, playerResult.PlayerId);
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
            foreach (var challengeStanding in ChallengeStandings.Values)
            {
                var column = new DataGridTextColumn
                {
                    Header = challengeStanding.ChallengeId,
                    Binding = new Binding($"ChallengePoints[1]")
                };

                ChampionshipDataGrid.Columns.Add(column);
            }
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

    public class ChallengeStandings
    {
        public int ChallengeId { get; set; }
        public SortedList<TimeSpan, int> SortedPlayers = new SortedList<TimeSpan, int>();

        public ChallengeStandings(int challengeId)
        {
            ChallengeId = challengeId;
        }

        public int GetPlayerPoints(int playerId)
        {
            int position = 0;
            foreach (var sortedPlayer in SortedPlayers)
            {
                position++;
                if (sortedPlayer.Value == playerId)
                    return PointsUtil.PositionToPoints(position);
            }

            return 0;
        }
    }

    public class ChampionshipStanding
    {
        public PlayerModel Player { get; set; }

        public ObservableCollection<int> ChallengePoints { get; } = new ObservableCollection<int>();

        public int TotalPoints => ChallengePoints.Sum();

        public ChampionshipStanding(PlayerModel playerName)
        {
            Player = playerName;
        }
    }
}