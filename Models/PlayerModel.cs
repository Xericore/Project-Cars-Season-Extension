using System;
using System.ComponentModel;

namespace ProjectCarsSeasonExtension.Models
{
    public class PlayerModel : INotifyPropertyChanged
    {
        // ----------------------------------------------------------------------------------------

        public event PropertyChangedEventHandler PropertyChanged;
        private string _name;
        private DateTime _time;

        // ----------------------------------------------------------------------------------------

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

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
