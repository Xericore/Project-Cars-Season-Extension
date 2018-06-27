using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
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
                    Header = headerGrid
                };

                Frame contentFrame = new Frame
                {
                    Content = challengeView,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Width = 640
                };
                tabItem.Content = contentFrame;

                ChallengesTabControl.Items.Add(tabItem);
            }
        }

        private static Grid GetHeaderGrid(Challenge challengeStandingChallenge)
        {
            Grid headerGrid = new Grid();
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition());
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition());

            Image trackImage = UiUtils.GetTrackImage(challengeStandingChallenge);
            trackImage.Margin = new Thickness(5);

            headerGrid.Children.Add(trackImage);

            string headerText = UiUtils.CreateChallengeTabHeaderText(challengeStandingChallenge.Name);
            var textBlock = new TextBlock{Text = headerText};
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(textBlock, 1);

            headerGrid.Children.Add(textBlock);

            return headerGrid;
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
