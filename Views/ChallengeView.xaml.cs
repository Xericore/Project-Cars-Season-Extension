using System.Windows.Controls;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for ChallengeView.xaml
    /// </summary>
    public partial class ChallengeView : Page
    {
        public ChampionshipView ChampionshipView { get; set; }

        public ChallengeView(ChampionshipView championshipView)
        {
            ChampionshipView = championshipView;
            InitializeComponent();
        }
    }
}
