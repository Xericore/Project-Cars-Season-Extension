using System.Collections.ObjectModel;
using System.Windows.Controls;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for SeasonEditor.xaml
    /// </summary>
    public partial class SeasonEditor : Page
    {
        private readonly DataView _dataView;

        public SeasonEditor(DataView dataView)
        {
            _dataView = dataView;
            InitializeComponent();
        }

        public ObservableCollection<Challenge> Challenges => _dataView.CurrentSeason.Challenges;
    }
}
