using System.ComponentModel;

namespace pCarsAPI_Demo
{
    public partial class pCarsDataClass : INotifyPropertyChanged
    {
        // Session Type

        private GameState mgamestate = GameState.GameExited; // [ enum (Type#1) Game state ]
        private RaceState mracestate; // [ RANGE = 0->... ]
        private SessionState msessionstate = SessionState.SessionInvalid; // [ enum (Type#2) Session state ]

        public GameState GameState
        {
            get { return mgamestate; }
            set
            {
                if (mgamestate == value)
                    return;
                SetProperty(ref mgamestate, value);
            }
        }

        public SessionState SessionState
        {
            get { return msessionstate; }
            set
            {
                if (msessionstate == value)
                    return;
                SetProperty(ref msessionstate, value);
            }
        }

        public RaceState RaceState
        {
            get { return mracestate; }
            set
            {
                if (mracestate == value)
                    return;
                SetProperty(ref mracestate, value);
            }
        }
    }
}