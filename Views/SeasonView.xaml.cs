using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ProjectCarsSeasonExtension.Annotations;
using ProjectCarsSeasonExtension.Models;
using ProjectCarsSeasonExtension.Utils;
using ProjectCarsSeasonExtension.ViewModels;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for SeasonView.xaml
    /// </summary>
    public partial class SeasonView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ChallengeView> ChallengeViews { get; set; } = new ObservableCollection<ChallengeView>();

        private readonly IEnumerable<ChallengeStanding> _challengeStandings;
        private int _lastSelectedTabIndex;

        public SeasonView(IEnumerable<ChallengeStanding> challengeStandings)
        {
            InitializeComponent();
            _challengeStandings = challengeStandings;

            CreateTabs();
        }

        private void CreateTabs()
        {
            foreach (var challengeStanding in _challengeStandings)
            {
                var challengeView = new ChallengeView(challengeStanding);
                ChallengeViews.Add(challengeView);
            }

            CreateTabsFromView();
        }

        private void CreateTabsFromView()
        {
            foreach(var challengeView in ChallengeViews)
            {
                var headerGrid = GetHeaderGrid(challengeView.ChallengeStanding.Challenge);

                TabItem tabItem = new TabItem
                {
                    Header = headerGrid,
                    Height = 120,
                    Width = 460,
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                Frame contentFrame = new Frame
                {
                    Content = challengeView,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Width = 680
                };
                tabItem.Content = contentFrame;

                ChallengesTabControl.Items.Add(tabItem);
            }
        }

        private static Grid GetHeaderGrid(Challenge challenge)
        {
            Grid headerGrid = new Grid
            {
                Margin = new Thickness(10, 0, 10, 0)
            };

            headerGrid.ColumnDefinitions.Add(new ColumnDefinition());
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(320)});

            Image trackImage = UiUtils.GetTrackImage(challenge);
            trackImage.Margin = new Thickness(5);
            trackImage.Width = 96;
            trackImage.Height = 96;

            headerGrid.Children.Add(trackImage);
            
            var textGrid = GetHeaderTextGrid(challenge);
            Grid.SetColumn(textGrid, 1);

            headerGrid.Children.Add(textGrid);

            return headerGrid;
        }

        private static Grid GetHeaderTextGrid(Challenge challenge)
        {
            Grid textGrid = new Grid();
            textGrid.RowDefinitions.Add(new RowDefinition());
            textGrid.RowDefinitions.Add(new RowDefinition());
            textGrid.RowDefinitions.Add(new RowDefinition());

            textGrid.ColumnDefinitions.Add(new ColumnDefinition{ Width = new GridLength(80) });
            textGrid.ColumnDefinitions.Add(new ColumnDefinition ());

            AddTextBlockToGrid("Track:", 0, 0, textGrid);
            AddTextBlockToGrid(challenge.TrackName, 0, 1, textGrid);

            AddTextBlockToGrid("Car:", 1, 0, textGrid);
            AddTextBlockToGrid(challenge.CarName, 1, 1, textGrid);

            AddTextBlockToGrid("Difficulty:", 2, 0, textGrid);
            AddTextBlockToGrid(challenge.Difficulty.ToString(), 2, 1, textGrid);

            return textGrid;
        }

        private static void AddTextBlockToGrid(string textBlockText, int row, int column, Grid textGrid)
        {
            var textBlock = new TextBlock
            {
                Text = textBlockText,
                VerticalAlignment = VerticalAlignment.Center,
                TextAlignment = TextAlignment.Left,
            };
            Grid.SetRow(textBlock, row);
            Grid.SetColumn(textBlock, column);
            textGrid.Children.Add(textBlock);
        }

        public void UpdateUI(string challengeToString = null)
        {
            _lastSelectedTabIndex = ChallengesTabControl.SelectedIndex;

            ChallengesTabControl.Items.Clear();
            ChallengeViews.Clear();

            CreateTabs();

            if (challengeToString == null)
                ChallengesTabControl.SelectedIndex = _lastSelectedTabIndex;
            else
                SelectTabFromChallenge(challengeToString);

            OnPropertyChanged(nameof(ChallengeViews));
        }

        private void SelectTabFromChallenge(string challengeToString)
        {
            foreach (TabItem item in ChallengesTabControl.Items)
            {
                if (Equals(item.Header, challengeToString))
                {
                    ChallengesTabControl.SelectedItem = item;
                    break;
                }
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
