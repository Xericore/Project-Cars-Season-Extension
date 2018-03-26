using System.Windows.Controls;
using Ninject;
using ProjectCarsSeasonExtension.ViewModels;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for HighscoreView.xaml
    /// </summary>
    public partial class HighscoreView : Page
    {
        public HighscoreViewModel HighScoreViewModel { get; set; }
        
        [Inject]
        public HighscoreView(HighscoreViewModel highscoreViewModel)
        {
            HighScoreViewModel = highscoreViewModel;
            InitializeComponent();
        }
    }
}