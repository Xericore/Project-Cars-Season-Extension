using System.Collections.Generic;
using System.ComponentModel;

namespace ProjectCarsSeasonExtension.Models
{
    public class TrackModel : ModelBase
    {
        // ----------------------------------------------------------------------------------------

        private string _trackName;
        private string _description;
        private List<PlayerModel> _playerList = new List<PlayerModel>();

        // ----------------------------------------------------------------------------------------
        // getter and setter
        // ----------------------------------------------------------------------------------------

        public string TrackName
        {
            get { return _trackName; }
            set
            {
                _trackName = value;
                NotifyPropertyChanged("TrackName");
            }
        }
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyPropertyChanged("Description");
            }
        }

        public List<PlayerModel> PlayerList
        {
            get { return _playerList; }
            set
            {
                _playerList = value;
                NotifyPropertyChanged("PlayerList");
            }
        }

        // ----------------------------------------------------------------------------------------
    }
}
