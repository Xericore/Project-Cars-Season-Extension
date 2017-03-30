using System;

namespace ProjectCarsSeasonExtension.Models
{
    public class PlayerTimeModel : BaseModel
    {
        // ----------------------------------------------------------------------------------------

        private DateTime _time;
        private PlayerModel _player;

        // ----------------------------------------------------------------------------------------
        // getter and setter
        // ----------------------------------------------------------------------------------------

        public PlayerModel Player
        {
            get { return _player; }
            set
            {
                _player = value;
                NotifyPropertyChanged("Player");
            }
        }

        public DateTime Time
        {
            get { return _time; }
            set
            {
                _time = value;
                NotifyPropertyChanged("Time");
            }
        }

        // ----------------------------------------------------------------------------------------
    }
}