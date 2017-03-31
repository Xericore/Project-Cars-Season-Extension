using System.ComponentModel;

namespace pCarsAPI_Demo
{
    public partial class pCarsDataClass : INotifyPropertyChanged
    {
        private bool mantilockactive; // [ UNITS = boolean ]   [ RANGE = false->true ]   [ UNSET = false ]
        private bool mboostactive; // [ UNITS = boolean ]   [ RANGE = false->true ]   [ UNSET = false ]
        private float mboostamount; // [ RANGE = 0.0f->100.0f ] 
        private float mbrake; // [ RANGE = 0.0f->1.0f ]
        private CarFlags mcarflags = CarFlags.None; // [ enum (Type#6) Car Flags ]
        private float mclutch; // [ RANGE = 0.0f->1.0f ]
        private float mfuelcapacity; // [ UNITS = Liters ]   [ RANGE = 0.0f->1.0f ]   [ UNSET = 0.0f ]
        private float mfuellevel; // [ RANGE = 0.0f->1.0f ]
        private float mfuelpressurekpa; // [ UNITS = Kilopascal ]   [ RANGE = 0.0f->... ]   [ UNSET = 0.0f ]

        private int mgear;
        // [ RANGE = -1 (Reverse)  0 (Neutral)  1 (Gear 1)  2 (Gear 2)  etc... ]   [ UNSET = 0 (Neutral) ]

        private float mlastopponentcollisionindex; // [ RANGE = 0->STORED_PARTICIPANTS_MAX ]   [ UNSET = -1 ]
        private float mlastopponentcollisionmagnitude; // [ RANGE = 0.0f->... ]
        private float mmaxrpm; // [ UNITS = Revolutions per minute ]   [ RANGE = 0.0f->... ]   [ UNSET = 0.0f ]
        public int mnumgears; // [ RANGE = 0->... ]   [ UNSET = -1 ]
        private float modometerkm; // [ RANGE = 0.0f->... ]   [ UNSET = -1.0f ]
        private float moilpressurekpa; // [ UNITS = Kilopascal ]   [ RANGE = 0.0f->... ]   [ UNSET = 0.0f ]
        private float moiltempcelsius; // [ UNITS = Celsius ]   [ UNSET = 0.0f ]
        private float mrpm; // [ UNITS = Revolutions per minute ]   [ RANGE = 0.0f->... ]   [ UNSET = 0.0f ]
        private float mspeed; // [ UNITS = Metres per-second ]   [ RANGE = 0.0f->... ]
        private float msteering; // [ RANGE = -1.0f->1.0f ]
        private float mthrottle; // [ RANGE = 0.0f->1.0f ]
        private float mwaterpressurekpa; // [ UNITS = Kilopascal ]   [ RANGE = 0.0f->... ]   [ UNSET = 0.0f ]
        private float mwatertempcelsius; // [ UNITS = Celsius ]   [ UNSET = 0.0f ]

        public CarFlags CarFlags
        {
            get { return mcarflags; }
            set
            {
                if (mcarflags == value)
                    return;
                SetProperty(ref mcarflags, value);
            }
        }

        public float OilTempCelsius
        {
            get { return moiltempcelsius; }
            set
            {
                if (moiltempcelsius == value)
                    return;
                SetProperty(ref moiltempcelsius, value);
            }
        }

        public float OilPressureKPa
        {
            get { return moilpressurekpa; }
            set
            {
                if (moilpressurekpa == value)
                    return;
                SetProperty(ref moilpressurekpa, value);
            }
        }

        public float WaterTempCelsius
        {
            get { return mwatertempcelsius; }
            set
            {
                if (mwatertempcelsius == value)
                    return;
                SetProperty(ref mwatertempcelsius, value);
            }
        }

        public float WaterPressureKPa
        {
            get { return mwaterpressurekpa; }
            set
            {
                if (mwaterpressurekpa == value)
                    return;
                SetProperty(ref mwaterpressurekpa, value);
            }
        }

        public float FuelPressureKPa
        {
            get { return mfuelpressurekpa; }
            set
            {
                if (mfuelpressurekpa == value)
                    return;
                SetProperty(ref mfuelpressurekpa, value);
            }
        }

        public float FuelLevel
        {
            get { return mfuellevel; }
            set
            {
                if (mfuellevel == value)
                    return;
                SetProperty(ref mfuellevel, value);
            }
        }

        public float FuelCapacity
        {
            get { return mfuelcapacity; }
            set
            {
                if (mfuelcapacity == value)
                    return;
                SetProperty(ref mfuelcapacity, value);
            }
        }

        public float Speed
        {
            get { return mspeed; }
            set
            {
                if (mspeed == value)
                    return;
                SetProperty(ref mspeed, value);
            }
        }


        public float RPM
        {
            get { return mrpm; }
            set
            {
                if (mrpm == value)
                    return;
                SetProperty(ref mrpm, value);
            }
        }

        public float MaxRPM
        {
            get { return mmaxrpm; }
            set
            {
                if (mmaxrpm == value)
                    return;
                SetProperty(ref mmaxrpm, value);
            }
        }

        public float Brake
        {
            get { return mbrake; }
            set
            {
                if (mbrake == value)
                    return;
                SetProperty(ref mbrake, value);
            }
        }

        public float Throttle
        {
            get { return mthrottle; }
            set
            {
                if (mthrottle == value)
                    return;
                SetProperty(ref mthrottle, value);
            }
        }

        public float Clutch
        {
            get { return mclutch; }
            set
            {
                if (mclutch == value)
                    return;
                SetProperty(ref mclutch, value);
            }
        }

        public float Steering
        {
            get { return msteering; }
            set
            {
                if (msteering == value)
                    return;
                SetProperty(ref msteering, value);
            }
        }

        public int Gear
        {
            get { return mgear; }
            set
            {
                if (mgear == value)
                    return;
                SetProperty(ref mgear, value);
            }
        }

        public int NumGears
        {
            get { return mnumgears; }
            set
            {
                if (mnumgears == value)
                    return;
                SetProperty(ref mnumgears, value);
            }
        }

        public float OdometerKM
        {
            get { return modometerkm; }
            set
            {
                if (modometerkm == value)
                    return;
                SetProperty(ref modometerkm, value);
            }
        }

        public bool AntiLockActive
        {
            get { return mantilockactive; }
            set
            {
                if (mantilockactive == value)
                    return;
                SetProperty(ref mantilockactive, value);
            }
        }

        public float LastOpponentCollisionIndex
        {
            get { return mlastopponentcollisionindex; }
            set
            {
                if (mlastopponentcollisionindex == value)
                    return;
                SetProperty(ref mlastopponentcollisionindex, value);
            }
        }

        public float LastOpponentCollisionMagnitude
        {
            get { return mlastopponentcollisionmagnitude; }
            set
            {
                if (mlastopponentcollisionmagnitude == value)
                    return;
                SetProperty(ref mlastopponentcollisionmagnitude, value);
            }
        }

        public bool BoostActive
        {
            get { return mboostactive; }
            set
            {
                if (mboostactive == value)
                    return;
                SetProperty(ref mboostactive, value);
            }
        }

        public float BoostAmount
        {
            get { return mboostamount; }
            set
            {
                if (mboostamount == value)
                    return;
                SetProperty(ref mboostamount, value);
            }
        }
    }
}