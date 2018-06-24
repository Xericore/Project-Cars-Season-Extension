﻿using System;
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
            set
            {
                _challenges = value;

                ChallengeIds.Clear();

                foreach (var challenge in _challenges)
                {
                    ChallengeIds.Add(challenge.Id);
                }
            }
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
