using System.Windows.Controls;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for SeasonEditor.xaml
    /// </summary>
    public partial class SeasonEditor : Page
    {
        private DataView _dataView;

        public SeasonEditor(DataView dataView)
        {
            _dataView = dataView;
            InitializeComponent();
        }
    }
}
