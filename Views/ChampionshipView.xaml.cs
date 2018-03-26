using System.Collections.ObjectModel;
using System.Windows.Controls;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for ChampionshipView.xaml
    /// </summary>
    public partial class ChampionshipView : Page
    {
        public ObservableCollection<PlayerResult> PlayerResults { get; }

        public ChampionshipView(ObservableCollection<PlayerResult> playerResults)
        {
            PlayerResults = playerResults;
            InitializeComponent();
        }
    }
}
