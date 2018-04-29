using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;

namespace ProjectCarsSeasonExtension.Models.Player
{
    [Serializable]
    public class PlayerResult : BaseModel
    {
        private int _playerId;

        [XmlIgnore]
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

        [XmlIgnore]
        public TimeSpan FastestLap
        {
            get { return _fastestLap; }
            set
            {
                _fastestLap = value;
                OnPropertyChanged();
            }
        }

        // XmlSerializer does not support TimeSpan, so use this property for 
        // serialization instead.
        [Browsable(false)]
        [XmlElement(DataType = "duration", ElementName = "FastestLap")]
        public string FastestLapString
        {
            get
            {
                return XmlConvert.ToString(FastestLap);
            }
            set
            {
                FastestLap = string.IsNullOrEmpty(value) ? TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
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