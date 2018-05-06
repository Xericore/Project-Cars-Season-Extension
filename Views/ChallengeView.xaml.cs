using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using ProjectCarsSeasonExtension.Annotations;
using ProjectCarsSeasonExtension.ViewModels;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for ChallengeView.xaml
    /// </summary>
    public partial class ChallengeView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ChallengeStanding ChallengeStanding { get; set; }

        public ChallengeView()
        {
            InitializeComponent();
        }

        public ChallengeView(ChallengeStanding challengeStanding)
        {
            ChallengeStanding = challengeStanding;
            InitializeComponent();
        }

        public void UpdateUI()
        {
            OnPropertyChanged(nameof(ChallengeStanding));
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
