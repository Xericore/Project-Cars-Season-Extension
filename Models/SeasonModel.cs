using System;
using System.Collections.ObjectModel;

namespace ProjectCarsSeasonExtension.Models
{
    public class SeasonModel : BaseModel
    {
        public int Id;
        public DateTime StartDate;
        public DateTime EndDate;
        public string Name;
        public string Description;

        public ObservableCollection<TrackAndCar> TracksAndCars { get; set; }
    }
}
