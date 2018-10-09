using System.Collections.ObjectModel;
using System.ComponentModel;

namespace pCarsAPI_Demo
{
    public partial class pCarsDataClass : INotifyPropertyChanged
    {
        private float mbestlaptime; // [ UNITS = seconds ]   [ RANGE = 0.0f->... ]   [ UNSET = -1.0f ]
        private ObservableCollection<LapTimesClass> mcurrentlaptime = new ObservableCollection<LapTimesClass>();
        private float mcurrenttime; // [ UNITS = seconds ]   [ RANGE = 0.0f->... ]   [ UNSET = 0.0f ]
        private float meventtimeremaining; // [ UNITS = milli-seconds ]   [ RANGE = 0.0f->... ]   [ UNSET = -1.0f ]
        private bool mlapinvalidated; // [ UNITS = boolean ]   [ RANGE = false->true ]   [ UNSET = false ]
        private float mlastlaptime; // [ UNITS = seconds ]   [ RANGE = 0.0f->... ]   [ UNSET = 0.0f ]
        private ObservableCollection<LapTimesClass> mpersonalfastestlaptime = new ObservableCollection<LapTimesClass>();
        private ObservableCollection<LapTimesClass> msessionfastestlaptime = new ObservableCollection<LapTimesClass>();
        private float msplittime; // [ UNITS = seconds ]   [ RANGE = 0.0f->... ]   [ UNSET = 0.0f ]
        private float msplittimeahead; // [ UNITS = seconds ]   [ RANGE = 0.0f->... ]   [ UNSET = -1.0f ]
        private float msplittimebehind; // [ UNITS = seconds ]   [ RANGE = 0.0f->... ]   [ UNSET = -1.0f ]
        private ObservableCollection<LapTimesClass> mworldfastestlaptime = new ObservableCollection<LapTimesClass>();


        public bool LapInvalidated
        {
            get { return mlapinvalidated; }
            set
            {
                if (mlapinvalidated == value)
                    return;
                SetProperty(ref mlapinvalidated, value);
            }
        }

        public float mBestLapTime
        {
            get { return mbestlaptime; }
            set
            {
                if (mbestlaptime == value)
                    return;
                SetProperty(ref mbestlaptime, value);
            }
        }

        public float LastLapTime
        {
            get { return mlastlaptime; }
            set
            {
                if (mlastlaptime == value)
                    return;
                SetProperty(ref mlastlaptime, value);
            }
        }

        public float CurrentTime
        {
            get { return mcurrenttime; }
            set
            {
                if (mcurrenttime == value)
                    return;
                SetProperty(ref mcurrenttime, value);
            }
        }

        public float SplitTimeAhead
        {
            get { return msplittimeahead; }
            set
            {
                if (msplittimeahead == value)
                    return;
                SetProperty(ref msplittimeahead, value);
            }
        }

        public float SplitTimeBehind
        {
            get { return msplittimebehind; }
            set
            {
                if (msplittimebehind == value)
                    return;
                SetProperty(ref msplittimebehind, value);
            }
        }

        public float SplitTime
        {
            get { return msplittime; }
            set
            {
                if (msplittime == value)
                    return;
                SetProperty(ref msplittime, value);
            }
        }


        public float EventTimeRemaining
        {
            get { return meventtimeremaining; }
            set
            {
                if (meventtimeremaining == value)
                    return;
                SetProperty(ref meventtimeremaining, value);
            }
        }

        public ObservableCollection<LapTimesClass> CurrentLapTime
        {
            get { return mcurrentlaptime; }
            set
            {
                if (mcurrentlaptime == value)
                    return;
                SetProperty(ref mcurrentlaptime, value);
            }
        }

        public ObservableCollection<LapTimesClass> SessionFastestLapTime
        {
            get { return msessionfastestlaptime; }
            set
            {
                if (msessionfastestlaptime == value)
                    return;
                SetProperty(ref msessionfastestlaptime, value);
            }
        }

        public ObservableCollection<LapTimesClass> PersonalFastestLapTime
        {
            get { return mpersonalfastestlaptime; }
            set
            {
                if (mpersonalfastestlaptime == value)
                    return;
                SetProperty(ref mpersonalfastestlaptime, value);
            }
        }

        public ObservableCollection<LapTimesClass> WorldFastestLapTime
        {
            get { return mworldfastestlaptime; }
            set
            {
                if (mworldfastestlaptime == value)
                    return;
                SetProperty(ref mworldfastestlaptime, value);
            }
        }
    }
}