using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;

namespace ProjectCarsSeasonExtension.Models.Player
{
    [Serializable]
    public class PlayerHandicap
    {
        public int PlayerId { get; set; }
        public int SeasonId { get; set; }

        [XmlIgnore]
        public TimeSpan Handicap { get; set; }

        // XmlSerializer does not support TimeSpan, so use this property for 
        // serialization instead.
        [Browsable(false)]
        [XmlElement(DataType = "duration", ElementName = "Handicap")]
        public string HandicapString
        {
            get
            {
                return XmlConvert.ToString(Handicap);
            }
            set
            {
                Handicap = string.IsNullOrEmpty(value) ? TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
            }
        }

        public PlayerHandicap() { }

        public PlayerHandicap(int playerId, int seasonId, TimeSpan handicap)
        {
            PlayerId = playerId;
            SeasonId = seasonId;
            Handicap = handicap;
        }
    }
}
