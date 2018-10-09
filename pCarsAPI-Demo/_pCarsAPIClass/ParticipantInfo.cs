using System.Collections.ObjectModel;
using System.ComponentModel;

namespace pCarsAPI_Demo
{
    public partial class pCarsDataClass : INotifyPropertyChanged
    {
        private ObservableCollection<pCarsParticipantsClass> listparticipantinfo =
            new ObservableCollection<pCarsParticipantsClass>();

        private int mnumparticipants;
        private int mplayerparticipantindex = -1;
        private int mviewedparticipantindex = -1;

        public int PlayerParticipantIndex
        {
            get { return mplayerparticipantindex; }
            set
            {
                if (mplayerparticipantindex == value)
                    return;
                SetProperty(ref mplayerparticipantindex, value);
            }
        }

        public int ViewedParticipantIndex
        {
            get { return mviewedparticipantindex; }
            set
            {
                if (mviewedparticipantindex == value)
                    return;
                SetProperty(ref mviewedparticipantindex, value);
            }
        }

        public int NumParticipants
        {
            get { return mnumparticipants; }
            set
            {
                if (mnumparticipants == value)
                    return;
                SetProperty(ref mnumparticipants, value);
            }
        }

        public ObservableCollection<pCarsParticipantsClass> listParticipantInfo
        {
            get { return listparticipantinfo; }
            set
            {
                if (listparticipantinfo == value)
                    return;
                SetProperty(ref listparticipantinfo, value);
            }
        }
    }
}