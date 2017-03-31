using System.ComponentModel;

namespace pCarsAPI_Demo
{
    public partial class pCarsDataClass : INotifyPropertyChanged
    {
        private float maerodamage; // [ RANGE = 0.0f->1.0f ]
        private CrashDamageState mcrashstate; // [ enum (Type#4) Crash Damage State ]
        private float menginedamage; // [ RANGE = 0.0f->1.0f ]

        public CrashDamageState CrashState
        {
            get { return mcrashstate; }
            set
            {
                if (mcrashstate == value)
                    return;
                SetProperty(ref mcrashstate, value);
            }
        }

        public float AeroDamage
        {
            get { return maerodamage; }
            set
            {
                if (maerodamage == value)
                    return;
                SetProperty(ref maerodamage, value);
            }
        }

        public float EngineDamage
        {
            get { return menginedamage; }
            set
            {
                if (menginedamage == value)
                    return;
                SetProperty(ref menginedamage, value);
            }
        }
    }
}