using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;

namespace ProjectCarsSeasonExtension.Models
{
    [Serializable]
    public class SeasonModel : BaseModel
    {
        public int Id;
        public DateTime StartDate;
        public DateTime EndDate;
        public string Name;
        public string Description;

        [XmlIgnore]
        public ObservableCollection<Challenge> Challenges { get; set; }

        public Challenge GetChallengeById(int challengeId)
        {
            return Challenges.FirstOrDefault(c => c.Id == challengeId);
        }
    }
}
