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

        public ObservableCollection<ChallengeStanding> ChallengeStandings { get; set; }
        
        public SeasonView(IEnumerable<ChallengeStanding> challengeStandings)
        {
            ChallengeStandings = new ObservableCollection<ChallengeStanding>(challengeStandings);

            InitializeComponent();
        }

        public void UpdateUI(IEnumerable<ChallengeStanding> challengeStandings)
        {
            ChallengeStandings = new ObservableCollection<ChallengeStanding>(challengeStandings);

            //            SelectTabFromChallenge(challengeToString);
            //            (ChallengesTabControl.SelectedItem as ChallengeStanding).UpdateView();

            ChallengesTabControl.SelectedIndex = 0;

            OnPropertyChanged(nameof(ChallengeStandings));
            OnPropertyChanged("ChallengeView");
        }

        private void SelectTabFromChallenge(string challengeToString)
        {
            foreach (ChallengeStanding challengeStanding in ChallengesTabControl.Items)
            {
                if (Equals(challengeStanding.Challenge.Name, challengeToString))
                {
                    ChallengesTabControl.SelectedItem = challengeStanding;
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
