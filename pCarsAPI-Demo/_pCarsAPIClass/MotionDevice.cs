using System.Collections.Generic;
using System.ComponentModel;

namespace pCarsAPI_Demo
{
    public partial class pCarsDataClass : INotifyPropertyChanged
    {
        private List<float> mangularvelocity; // [ UNITS = Radians per-second ]
        private List<float> mextentscentre; // [ UNITS = Local Space  X  Y  Z ]
        private List<float> mlocalacceleration; // [ UNITS = Metres per-second ]
        private List<float> mlocalvelocity; // [ UNITS = Metres per-second ]
        // Motion & Device Related
        private List<float> morientation; // [ UNITS = Euler Angles ]
        private List<float> mworldacceleration; // [ UNITS = Metres per-second ]
        private List<float> mworldvelocity; // [ UNITS = Metres per-second ]

        public List<float> Orientation
        {
            get { return morientation; }
            set
            {
                if (morientation == value)
                    return;
                SetProperty(ref morientation, value);
            }
        }

        public List<float> LocalVelocity
        {
            get { return mlocalvelocity; }
            set
            {
                if (mlocalvelocity == value)
                    return;
                SetProperty(ref mlocalvelocity, value);
            }
        }

        public List<float> WorldVelocity
        {
            get { return mworldvelocity; }
            set
            {
                if (mworldvelocity == value)
                    return;
                SetProperty(ref mworldvelocity, value);
            }
        }

        public List<float> AngularVelocity
        {
            get { return mangularvelocity; }
            set
            {
                if (mangularvelocity == value)
                    return;
                SetProperty(ref mangularvelocity, value);
            }
        }

        public List<float> LocalAcceleration
        {
            get { return mlocalacceleration; }
            set
            {
                if (mlocalacceleration == value)
                    return;
                SetProperty(ref mlocalacceleration, value);
            }
        }

        public List<float> WorldAcceleration
        {
            get { return mworldacceleration; }
            set
            {
                if (mworldacceleration == value)
                    return;
                SetProperty(ref mworldacceleration, value);
            }
        }

        public List<float> ExtentsCentre
        {
            get { return mextentscentre; }
            set
            {
                if (mextentscentre == value)
                    return;
                SetProperty(ref mextentscentre, value);
            }
        }
    }
}