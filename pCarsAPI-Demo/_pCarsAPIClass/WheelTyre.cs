using System.Collections.Generic;
using System.ComponentModel;

namespace pCarsAPI_Demo
{
    public partial class pCarsDataClass : INotifyPropertyChanged
    {
        private List<float> mbrakedamage; // [ RANGE = 0.0f->1.0f ]
        private List<float> mbraketempcelsius; // [ RANGE = 0.0f->1.0f ]
        private List<float> msuspensiondamage; // [ RANGE = 0.0f->1.0f ]
        private List<uint> mterrain; // [ enum (Type#3) Terrain Materials ]
        private List<float> mtyrecarcasstemp; // [ RANGE = 0.0f->1.0f ]
        private List<uint> mtyreflags; // [ enum (Type#7) Tyre Flags ]
        private List<float> mtyregrip; // [ RANGE = 0.0f->1.0f ]
        private List<float> mtyreheightaboveground; // [ UNITS = Local Space  Y ]
        private List<float> mtyreinternalairtemp; // [ RANGE = 0.0f->1.0f ]
        private List<float> mtyrelateralstiffness; // [ UNITS = Lateral stiffness coefficient used in tyre deformation ]
        private List<float> mtyrelayertemp; // [ RANGE = 0.0f->1.0f ]
        private List<float> mtyrerimtemp; // [ RANGE = 0.0f->1.0f ]
        private List<float> mtyrerps; // [ UNITS = Revolutions per second ]
        private List<float> mtyreslipspeed; // [ UNITS = Metres per-second ]
        private List<float> mtyretemp; // [ UNITS = Celsius ]   [ UNSET = 0.0f ]
        private List<float> mtyretreadtemp; // [ RANGE = 0.0f->1.0f ]
        private List<float> mtyrewear; // [ RANGE = 0.0f->1.0f ]
        private List<float> mtyrey; // [ UNITS = Local Space  Y ]


        public List<uint> TyreFlags
        {
            get { return mtyreflags; }
            set
            {
                if (mtyreflags == value)
                    return;
                SetProperty(ref mtyreflags, value);
            }
        }

        public List<uint> Terrain
        {
            get { return mterrain; }
            set
            {
                if (mterrain == value)
                    return;
                SetProperty(ref mterrain, value);
            }
        }

        public List<float> TyreY
        {
            get { return mtyrey; }
            set
            {
                if (mtyrey == value)
                    return;
                SetProperty(ref mtyrey, value);
            }
        }

        public List<float> TyreRPS
        {
            get { return mtyrerps; }
            set
            {
                if (mtyrerps == value)
                    return;
                SetProperty(ref mtyrerps, value);
            }
        }

        public List<float> TyreSlipSpeed
        {
            get { return mtyreslipspeed; }
            set
            {
                if (mtyreslipspeed == value)
                    return;
                SetProperty(ref mtyreslipspeed, value);
            }
        }

        public List<float> TyreTemp
        {
            get { return mtyretemp; }
            set
            {
                if (mtyretemp == value)
                    return;
                SetProperty(ref mtyretemp, value);
            }
        }

        public List<float> TyreGrip
        {
            get { return mtyregrip; }
            set
            {
                if (mtyregrip == value)
                    return;
                SetProperty(ref mtyregrip, value);
            }
        }

        public List<float> TyreHeightAboveGround
        {
            get { return mtyreheightaboveground; }
            set
            {
                if (mtyreheightaboveground == value)
                    return;
                SetProperty(ref mtyreheightaboveground, value);
            }
        }

        public List<float> TyreLateralStiffness
        {
            get { return mtyrelateralstiffness; }
            set
            {
                if (mtyrelateralstiffness == value)
                    return;
                SetProperty(ref mtyrelateralstiffness, value);
            }
        }

        public List<float> TyreWear
        {
            get { return mtyrewear; }
            set
            {
                if (mtyrewear == value)
                    return;
                SetProperty(ref mtyrewear, value);
            }
        }

        public List<float> BrakeDamage
        {
            get { return mbrakedamage; }
            set
            {
                if (mbrakedamage == value)
                    return;
                SetProperty(ref mbrakedamage, value);
            }
        }

        public List<float> SuspensionDamage
        {
            get { return msuspensiondamage; }
            set
            {
                if (msuspensiondamage == value)
                    return;
                SetProperty(ref msuspensiondamage, value);
            }
        }

        public List<float> BrakeTempCelsius
        {
            get { return mbraketempcelsius; }
            set
            {
                if (mbraketempcelsius == value)
                    return;
                SetProperty(ref mbraketempcelsius, value);
            }
        }

        public List<float> TyreTreadTemp
        {
            get { return mtyretreadtemp; }
            set
            {
                if (mtyretreadtemp == value)
                    return;
                SetProperty(ref mtyretreadtemp, value);
            }
        }

        public List<float> TyreLayerTemp
        {
            get { return mtyrelayertemp; }
            set
            {
                if (mtyrelayertemp == value)
                    return;
                SetProperty(ref mtyrelayertemp, value);
            }
        }

        public List<float> TyreCarcassTemp
        {
            get { return mtyrecarcasstemp; }
            set
            {
                if (mtyrecarcasstemp == value)
                    return;
                SetProperty(ref mtyrecarcasstemp, value);
            }
        }

        public List<float> TyreRimTemp
        {
            get { return mtyrerimtemp; }
            set
            {
                if (mtyrerimtemp == value)
                    return;
                SetProperty(ref mtyrerimtemp, value);
            }
        }

        public List<float> TyreInternalAirTemp
        {
            get { return mtyreinternalairtemp; }
            set
            {
                if (mtyreinternalairtemp == value)
                    return;
                SetProperty(ref mtyreinternalairtemp, value);
            }
        }
    }
}