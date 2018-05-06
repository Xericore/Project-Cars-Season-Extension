using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using ProjectCarsSeasonExtension.Annotations;
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
                TabItem tabItem = new TabItem { Header = challengeView.ChallengeStanding.Challenge.Name };

                Frame contentFrame = new Frame { Content = challengeView };
                tabItem.Content = contentFrame;

                ChallengesTabControl.Items.Add(tabItem);
            }
        }

        public void UpdateUI()
        {
            ChallengesTabControl.Items.Clear();
            ChallengeViews.Clear();

            CreateTabs();

            OnPropertyChanged(nameof(ChallengeViews));
        }
        
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
