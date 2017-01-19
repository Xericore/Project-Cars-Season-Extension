using System;
using System.ComponentModel;

namespace ProjectCarsSeasonExtension.Models
{
    public class PlayerModel : ModelBase
    {
        // ----------------------------------------------------------------------------------------

        private string _name;
        private DateTime _time;

        // ----------------------------------------------------------------------------------------
        // getter and setter
        // ----------------------------------------------------------------------------------------

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        // ----------------------------------------------------------------------------------------

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
