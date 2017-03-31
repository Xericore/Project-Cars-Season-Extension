using System.ComponentModel;

namespace pCarsAPI_Demo
{
    public partial class pCarsDataClass : INotifyPropertyChanged
    {
        private float munfilteredbrake; // [ RANGE = 0.0f->1.0f ]
        private float munfilteredclutch; // [ RANGE = 0.0f->1.0f ]
        private float munfilteredsteering; // [ RANGE = -1.0f->1.0f ]
        // Unfiltered Input

        private float munfilteredthrottle; // [ RANGE = 0.0f->1.0f ]

        public float UnfilteredThrottle
        {
            get { return munfilteredthrottle; }
            set
            {
                if (munfilteredthrottle == value)
                    return;
                SetProperty(ref munfilteredthrottle, value);
            }
        }

        public float UnfilteredBrake
        {
            get { return munfilteredbrake; }
            set
            {
                if (munfilteredbrake == value)
                    return;
                SetProperty(ref munfilteredbrake, value);
            }
        }

        public float UnfilteredSteering
        {
            get { return munfilteredsteering; }
            set
            {
                if (munfilteredsteering == value)
                    return;
                SetProperty(ref munfilteredsteering, value);
            }
        }

        public float UnfilteredClutch
        {
            get { return munfilteredclutch; }
            set
            {
                if (munfilteredclutch == value)
                    return;
                SetProperty(ref munfilteredclutch, value);
            }
        }
    }
}