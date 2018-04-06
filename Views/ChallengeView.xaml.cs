using System.Windows.Controls;
using ProjectCarsSeasonExtension.ViewModels;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for ChallengeView.xaml
    /// </summary>
    public partial class ChallengeView : Page
    {
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
    }
}
