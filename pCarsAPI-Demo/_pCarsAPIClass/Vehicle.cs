using System.ComponentModel;

namespace pCarsAPI_Demo
{
    public partial class pCarsDataClass : INotifyPropertyChanged
    {
        private string mcarclassname;
        // Vehicle & Track information

        private string mcarname;

        public string CarName
        {
            get { return mcarname; }
            set
            {
                if (mcarname == value)
                    return;
                SetProperty(ref mcarname, value);
            }
        }

        public string CarClassName
        {
            get { return mcarclassname; }
            set
            {
                if (mcarclassname == value)
                    return;
                SetProperty(ref mcarclassname, value);
            }
        }
    }
}