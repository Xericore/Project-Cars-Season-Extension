using System;

namespace ProjectCarsSeasonExtension.Models
{
    public class PlayerResult : BaseModel
    {
        private int _playerId;
        private TimeSpan _fastestLap;
        private int _challengeId;

        public int PlayerId
        {
            get { return _playerId; }
            set
            {
                _playerId = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan FastestLap
        {
            get { return _fastestLap; }
            set
            {
                _fastestLap = value;
                OnPropertyChanged();
            }
        }

        public int ChallengeId
        {
            get { return _challengeId; }
            set
            {
                _challengeId = value;
                OnPropertyChanged();
            }
        }
    }
}