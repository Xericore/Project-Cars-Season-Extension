using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using ProjectCarsSeasonExtension.Models;
using ProjectCarsSeasonExtension.Utils;
using ProjectCarsSeasonExtension.ViewModels;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for ChampionshipView.xaml
    /// </summary>
    public partial class ChampionshipView : Page
    {
        private readonly DataView _dataView;
        private readonly AllChallengeStandings _allChallengeStandings;
     
        public ObservableCollection<ChampionshipStanding> ChampionshipStandings { get; set; } = new ObservableCollection<ChampionshipStanding>();

        public ChampionshipView(DataView dataView, AllChallengeStandings allChallengeStandings)
        {
            _dataView = dataView;
            _allChallengeStandings = allChallengeStandings;

            CreateChampionshipStandings();

            InitializeComponent();

            GenerateChallengeColumns();
        }

        private void CreateChampionshipStandings()
        {
            foreach (var player in _dataView.Players)
            {
                if (player.Id < 0)
                    continue;

                foreach (var challengeStanding in _allChallengeStandings.ChallengeStandings)
                {
                    var championshipStanding = ChampionshipStandings.FirstOrDefault(p => p.Player.Id == player.Id);

                    if (championshipStanding == null)
                    {
                        championshipStanding = new ChampionshipStanding(player);
                        ChampionshipStandings.Add(championshipStanding);
                    }

                    var playerPoints = challengeStanding.Value.GetPlayerPoints(player.Id);
                    championshipStanding.ChallengePoints.Add(playerPoints);
                }
            }

            ChampionshipStandings.Sort();
        }

        private void GenerateChallengeColumns()
        {
            int challengeCount = 0;
            foreach (var challengeStanding in _allChallengeStandings.ChallengeStandings.Values)
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
}