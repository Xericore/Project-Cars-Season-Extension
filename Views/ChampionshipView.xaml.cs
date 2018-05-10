using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Data;
using ProjectCarsSeasonExtension.Annotations;
using ProjectCarsSeasonExtension.Models;
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
            ChampionshipStandings.Clear();

            foreach (var player in _dataView.Players)
            {
                if (player.Id < 0)
                    continue;

                var championshipStanding = new ChampionshipStanding(player);
                ChampionshipStandings.Add(championshipStanding);                

                foreach (var challengeStanding in _allChallengeStandings.ChallengeStandings)
                {
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

        public void UpdateUI()
        {
            CreateChampionshipStandings();
            OnPropertyChanged(nameof(ChampionshipStandings));
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}