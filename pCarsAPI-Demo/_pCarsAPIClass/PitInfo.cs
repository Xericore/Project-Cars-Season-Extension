using System.ComponentModel;

namespace pCarsAPI_Demo
{
    public partial class pCarsDataClass : INotifyPropertyChanged
    {
        private PitMode mpitmode; // [ enum (Type#7) Pit Mode ]
        private PitSchedule mpitschedule; // [ enum (Type#8) Pit Stop Schedule ]

        public PitMode PitMode
        {
            get { return mpitmode; }
            set
            {
                if (mpitmode == value)
                    return;
                SetProperty(ref mpitmode, value);
            }
        } // [ enum (Type#6) Flag Reason ]

        public PitSchedule PitSchedule
        {
            get { return mpitschedule; }
            set
            {
                if (mpitschedule == value)
                    return;
                SetProperty(ref mpitschedule, value);
            }
        } // [ enum (Type#6) Flag Reason ]
    }
}