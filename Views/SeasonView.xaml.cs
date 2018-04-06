using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ProjectCarsSeasonExtension.ViewModels;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for SeasonView.xaml
    /// </summary>
    public partial class SeasonView : Page
    {
        public ObservableCollection<ChallengeView> ChallengeViews { get; set; } = new ObservableCollection<ChallengeView>();

        public SeasonView(IEnumerable<ChallengeStanding> challengeStandings)
        {
            InitializeComponent();

            CreateTabs(challengeStandings);
        }

        private void CreateTabs(IEnumerable<ChallengeStanding> challengeStandings)
        {
            foreach (var challengeStanding in challengeStandings)
            {
                var challengeView = new ChallengeView(challengeStanding);
                ChallengeViews.Add(challengeView);

                TabItem tabItem = new TabItem {Header = challengeStanding.Challenge.Name};

                Frame contentFrame = new Frame { Content = challengeView };
                tabItem.Content = contentFrame;

                ChallengesTabControl.Items.Add(tabItem);
            }
        }
    }
}
