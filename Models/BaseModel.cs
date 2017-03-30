using System.ComponentModel;

namespace ProjectCarsSeasonExtension.Models
{
    public abstract class BaseModel : INotifyPropertyChanged
    {
        // ----------------------------------------------------------------------------------------

        public event PropertyChangedEventHandler PropertyChanged;

        // ----------------------------------------------------------------------------------------

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // ----------------------------------------------------------------------------------------
    }
}
