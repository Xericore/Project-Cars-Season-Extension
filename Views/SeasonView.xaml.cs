using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using ProjectCarsSeasonExtension.Annotations;
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
                string headerText = UiUtils.CreateChallengeTabHeaderText(challengeView.ChallengeStanding.Challenge.Name);

                TabItem tabItem = new TabItem { Header = headerText };

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
