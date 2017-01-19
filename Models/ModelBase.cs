using System.ComponentModel;

namespace ProjectCarsSeasonExtension.Models
{
    abstract public class ModelBase : INotifyPropertyChanged
    {
        // ----------------------------------------------------------------------------------------

        public event PropertyChangedEventHandler PropertyChanged;

        // ----------------------------------------------------------------------------------------

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // ----------------------------------------------------------------------------------------
    }
}
