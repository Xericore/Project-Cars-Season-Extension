using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;

namespace ProjectCarsSeasonExtension.Models
{
    [Serializable]
    public class Season : BaseModel
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<int> ChallengeIds = new List<int>();

        [XmlIgnore]
        private ObservableCollection<Challenge> _challenges = new ObservableCollection<Challenge>();

        [XmlIgnore]
        public ObservableCollection<Challenge> Challenges
        {
            get => _challenges;
            set => _challenges = value;
        }

        public void AddChallenge(Challenge challenge)
        {
            if (Challenges.Contains(challenge))
                return;

            Challenge newChallenge = new Challenge
            {
                Id = challenge.Id,
                TrackName = challenge.TrackName,
                CarName = challenge.CarName,
                Description = challenge.Description,
                Difficulty = challenge.Difficulty
            };
            Challenges.Add(newChallenge);
            UpdateChallengeIds();
        }

        private void UpdateChallengeIds()
        {
            ChallengeIds.Clear();

            foreach (var challenge in _challenges)
            {
                ChallengeIds.Add(challenge.Id);
            }
        }

        public void RemoveChallenge(Challenge selectedSeasonChallenge)
        {
            Challenges.Remove(selectedSeasonChallenge);
            UpdateChallengeIds();
        }

        public Challenge GetChallengeById(int challengeId)
        {
            return Challenges.FirstOrDefault(c => c.Id == challengeId);
        }

        public bool ContainsChallenge(int challengeId)
        {
            return GetChallengeById(challengeId) != null;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
