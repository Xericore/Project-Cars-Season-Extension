using System.Collections.ObjectModel;
using System.Windows.Controls;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for PlayerSelection.xaml
    /// </summary>
    public partial class PlayerSelection : Page
    {
        public ObservableCollection<PlayerModel> Players { get; set; } = new ObservableCollection<PlayerModel>();

        public PlayerSelection()
        {
            InitializeComponent();

            AddPlayerDummyData();
        }

        private void AddPlayerDummyData()
        {
            Players.Add(new PlayerModel { Id = 0, Name = "Sascha" });
            Players.Add(new PlayerModel { Id = 1, Name = "Mario" });
            Players.Add(new PlayerModel { Id = 2, Name = "Schumacher" });
        }
    }
}
