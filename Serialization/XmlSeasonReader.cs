using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using ProjectCarsSeasonExtension.Models;
using ProjectCarsSeasonExtension.Models.Player;

namespace ProjectCarsSeasonExtension.Serialization
{
    public class XmlSeasonReader : ISeasonReader
    {
        public ObservableCollection<Season> GetSeasons()
        {
            var seasons = GetObservableCollectionFromFile<Season>(FileLocations.SeasonFileUri);

            var challenges = GetObservableCollectionFromFile<Challenge>(FileLocations.ChallangeFileUri);

            foreach (var season in seasons)
            {
                foreach (var challenge in challenges)
                {
                    if(season.ChallengeIds.Contains(challenge.Id))
                        season.Challenges.Add(challenge);
                }
            }

            return seasons;
        }

        public ObservableCollection<Player> GetPlayers()
        {
            return GetObservableCollectionFromFile<Player>(FileLocations.PlayerFileUri);
        }

        ObservableCollection<PlayerResult> ISeasonReader.GetPlayerResults()
        {
            return GetObservableCollectionFromFile<PlayerResult>(FileLocations.PlayerResultFileUri);
        }

        internal static ObservableCollection<T> GetObservableCollectionFromFile<T>(string fileName)
        {
            var returnedCollection = new ObservableCollection<T>();

            if (!File.Exists(fileName))
                return returnedCollection;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));

            List<T> results;

            using (var reader = new StreamReader(fileName))
            {
                results = xmlSerializer.Deserialize(reader) as List<T>;
            }

            if (results != null)
            {
                foreach (var readResult in results)
                {
                    returnedCollection.Add(readResult);
                }
            }

            return returnedCollection;
        }

        public ObservableCollection<PlayerHandicap> GetPlayerHandicaps()
        {
            return GetObservableCollectionFromFile<PlayerHandicap>(FileLocations.HandicapsFileUri);
        }
    }
}
