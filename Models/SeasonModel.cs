using System;
using System.Collections.Generic;

namespace ProjectCarsSeasonExtension.Models
{
    public class SeasonModel : BaseModel
    {
        // ----------------------------------------------------------------------------------------

        public int Id;
        public DateTime StartDate;
        public DateTime EndDate;
        public string Name;
        public string Description;

        private List<TrackModel> _trackList;

        // ----------------------------------------------------------------------------------------
        // getter and setter
        // ----------------------------------------------------------------------------------------

        public List<TrackModel> TrackList
        {
            get { return _trackList; }
            set
            {
                _trackList = value;
                NotifyPropertyChanged("TrackList");
            }
        }

        // ----------------------------------------------------------------------------------------
    }
}
