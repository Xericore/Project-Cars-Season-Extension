using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
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

        public Dictionary<int, TrackAndCarStandings> TrackAndCarStandings { get; }
        public ObservableCollection<ChampionshipStanding> ChampionshipStandings { get; set; } = new ObservableCollection<ChampionshipStanding>();

        public ChampionshipView(SeasonModel currentSeason, ObservableCollection<PlayerResult> playerResults, ObservableCollection<PlayerModel> players)
        {
            CurrentSeason = currentSeason;
            PlayerResults = playerResults;
            Players = players;

            TrackAndCarStandings = new Dictionary<int, TrackAndCarStandings>();
            
            foreach (PlayerResult playerResult in PlayerResults)
            {
                if (!TrackAndCarStandings.ContainsKey(playerResult.TrackAndCarId))
                {
                    TrackAndCarStandings trackAndCarStandings = new TrackAndCarStandings(playerResult.TrackAndCarId);
                    
                    TrackAndCarStandings.Add(playerResult.TrackAndCarId,trackAndCarStandings);
                }

                TrackAndCarStandings[playerResult.TrackAndCarId].SortedPlayers.Add(playerResult.FastestLap, playerResult.PlayerId);
            }

            foreach (var player in Players)
            {
                if (player.Id < 0)
                    continue;

                foreach (var trackAndCarStandings in TrackAndCarStandings)
                {
                    var playerPoints = trackAndCarStandings.Value.GetPlayerPoints(player.Id);

                    var foundPlayer = ChampionshipStandings.FirstOrDefault(p => p.PlayerName == player.Name);

                    if (foundPlayer == null)
                    {
                        ChampionshipStandings.Add(
                            new ChampionshipStanding
                            {
                                PlayerName = player.Name,
                                TotalPoints = playerPoints
                            });
                    }
                    else
                    {
                        foundPlayer.TotalPoints += playerPoints;
                    }
                }
            }

            InitializeComponent();
        }

    }

    public static class PointsUtil
    {
        public static int PositionToPoints(int position)
        {
            int[] positionToPointsMapping = new[] { 25, 18, 15, 12, 10, 8, 6, 4, 2, 1 };

            if (position > positionToPointsMapping.Length)
                return 0;

            if (position < 0)
                throw new ArgumentOutOfRangeException("Provided position cannot be below zero.");

            return positionToPointsMapping[position - 1];
        }
    }

    public class TrackAndCarStandings
    {
        public int TrackAndCarId { get; set; }
        public SortedList<TimeSpan, int> SortedPlayers = new SortedList<TimeSpan, int>();

        public TrackAndCarStandings(int trackAndCarId)
        {
            TrackAndCarId = trackAndCarId;
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
        public string PlayerName { get; set; }

        public int TotalPoints { get; set; }
    }
}
