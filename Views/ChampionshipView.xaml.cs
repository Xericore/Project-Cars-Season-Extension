using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using ProjectCarsSeasonExtension.Annotations;
using ProjectCarsSeasonExtension.Converters;
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

                foreach (ChallengeStanding challengeStanding in _allChallengeStandings.ChallengeStandings)
                {
                    var playerPoints = challengeStanding.GetPlayerPoints(player.Id);
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
            foreach (var challengeStanding in _allChallengeStandings.ChallengeStandings)
            {
                if (!_dataView.CurrentSeason.ContainsChallenge(challengeStanding.Challenge.Id))
                    continue;

                var column = new DataGridTemplateColumn
                {
                    Header = UiUtils.GetTrackImage(challengeStanding.Challenge),
                    Width = 96,
                };

                var dataTemplate = new DataTemplate();

                var gridFactory = new FrameworkElementFactory(typeof(Grid));

                var textBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
                textBlockFactory.SetValue(TextBlock.FontWeightProperty, FontWeights.Normal);
                textBlockFactory.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center);
                textBlockFactory.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);

                Binding challengePointsBinding = new Binding
                {
                    Path = new PropertyPath($"ChallengePoints[{challengeCount}]")
                };

                textBlockFactory.SetBinding(TextBlock.TextProperty, challengePointsBinding);

                Binding pointsToColorBinding = new Binding
                {
                    Path = new PropertyPath($"ChallengePoints[{challengeCount}]"),
                    Converter = new PointsToColorConverter()
                };

                var ellipseFactory = new FrameworkElementFactory(typeof(Ellipse));
                ellipseFactory.SetValue(Shape.FillProperty, pointsToColorBinding);
                ellipseFactory.SetValue(HeightProperty, 36d);
                ellipseFactory.SetValue(WidthProperty, 36d);

                gridFactory.AppendChild(ellipseFactory);
                gridFactory.AppendChild(textBlockFactory);
                dataTemplate.VisualTree = gridFactory;

                column.CellTemplate = dataTemplate;

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