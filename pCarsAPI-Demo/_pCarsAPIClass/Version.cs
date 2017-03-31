using System.ComponentModel;

namespace pCarsAPI_Demo
{
    public partial class pCarsDataClass : INotifyPropertyChanged
    {
        private uint mbuildversion; // [ RANGE = 0->... ]
        private uint mversion; // [ RANGE = 0->... ]

        public uint Version
        {
            get { return mversion; }
            set
            {
                if (mversion == value)
                    return;
                SetProperty(ref mversion, value);
            }
        }

        public uint BuildVersion
        {
            get { return mbuildversion; }
            set
            {
                if (mbuildversion == value)
                    return;
                SetProperty(ref mbuildversion, value);
            }
        }
    }
}