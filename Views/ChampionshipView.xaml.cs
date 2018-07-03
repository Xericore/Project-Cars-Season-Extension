using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Data;
using ProjectCarsSeasonExtension.Annotations;
using ProjectCarsSeasonExtension.Converters;
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

        private void GenerateChallengeColumns()
        {
            ChampionshipDataGrid.Columns.Clear();

            CreatePositionColumn();
            CreatePlayerColumn();

            int challengeCount = 0;
            foreach (var challengeStanding in _allChallengeStandings.ChallengeStandings.Values)
            {
                if (!_dataView.CurrentSeason.ContainsChallenge(challengeStanding.Challenge.Id))
                    continue;

                var column = new DataGridTextColumn
                {
                    Header = UiUtils.GetTrackImage(challengeStanding.Challenge),
                    Width = 128,
                    Binding = new Binding($"ChallengePoints[{challengeCount}]")
                };

                ChampionshipDataGrid.Columns.Add(column);
                challengeCount++;
            }

            GenerateTotalPointsColumn();
        }
        
        private void CreatePositionColumn()
        {
            DataGridTextColumn column0 = new DataGridTextColumn {Header = "Pos."};

            Binding bindingColumn0 = new Binding
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(DataGridRow), 1),
                Converter = new RowToPositionConverter()
            };

            column0.Binding = bindingColumn0;
            
            ChampionshipDataGrid.Columns.Add(column0);
        }

        private void CreatePlayerColumn()
        {
            var playerColumn = new DataGridTextColumn
            {
                Header = "Player",
                Binding = new Binding("Player.Name"),
                Width = 160
            };

            ChampionshipDataGrid.Columns.Add(playerColumn);
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
            GenerateChallengeColumns();
            OnPropertyChanged(nameof(ChampionshipStandings));
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}