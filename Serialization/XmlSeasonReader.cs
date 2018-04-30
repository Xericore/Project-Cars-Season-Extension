using System;
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
        public SeasonModel GetCurrentSeason()
        {
            ObservableCollection<Challenge> challenges =
                GetObservableCollectionFromFile<Challenge>(FileLocations.ChallangeFileUri);

            var seasonModel = GetSeasonModelFromFile();
            seasonModel.Challenges = challenges;

            return seasonModel;
        }

        private static SeasonModel GetSeasonModelFromFile()
        {
            if (!File.Exists(FileLocations.SeasonFileUri))
                throw new FileNotFoundException($"Couldn't find {FileLocations.SeasonFileUri}.");

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SeasonModel));

            SeasonModel seasonModel;

            using (var reader = new StreamReader(FileLocations.SeasonFileUri))
            {
                seasonModel = xmlSerializer.Deserialize(reader) as SeasonModel;
            }

            return seasonModel;
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
            var players = new ObservableCollection<PlayerHandicap>
            {
                new PlayerHandicap(1, 0, new TimeSpan(0,0,0,1))
            };

            return players;
        }
    }
}
