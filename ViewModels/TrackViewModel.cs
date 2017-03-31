using System.Collections.ObjectModel;
using System.Drawing;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.ViewModels
{
    public class TrackViewModel : BaseModel
    {
        // ----------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------
        // getter and setter
        // ----------------------------------------------------------------------------------------

        public string Name { get; set; }
        public string Description { get; set; }
        public Image TrackImage { get; set; }
        public Image CarImage { get; set; }

        public ObservableCollection<PlayerTimeListItemModel> Player { get; set; } =
            new ObservableCollection<PlayerTimeListItemModel>();

        // ----------------------------------------------------------------------------------------
    }
}