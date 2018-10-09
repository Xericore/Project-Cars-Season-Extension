using System.ComponentModel;

namespace pCarsAPI_Demo
{
    public partial class pCarsDataClass : INotifyPropertyChanged
    {
        private FlagColors mflagcolour;
        private FlagReason mflagreason;

        public FlagColors FlagColour
        {
            get { return mflagcolour; }
            set
            {
                if (mflagcolour == value)
                    return;
                SetProperty(ref mflagcolour, value);
            }
        } // [ enum (Type#5) Flag Colour ]

        public FlagReason FlagReason
        {
            get { return mflagreason; }
            set
            {
                if (mflagreason == value)
                    return;
                SetProperty(ref mflagreason, value);
            }
        } // [ enum (Type#6) Flag Reason ]
    }
}