using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using ProjectCarsSeasonExtension.Models;
using ProjectCarsSeasonExtension.Models.Player;

namespace ProjectCarsSeasonExtension.Serialization
{
    public class DummySeasonReader: ISeasonReader
    {
        public SeasonModel GetCurrentSeason()
        {
            ObservableCollection<Challenge> trackAndCars = new ObservableCollection<Challenge>
            {
                new Challenge()
                {
                    Id = 0,
                    CarName = "Formula A",
                    TrackName = "Circuit de Barcelona Catalunya GP",
                    Difficulty = Difficulty.Medium,
                    Description = "A track where getting into the right rythm is key."
                },
                new Challenge()
                {
                    Id = 1,
                    CarName = "250cc Superkart",
                    TrackName = "Azure Circuit",
                    Difficulty = Difficulty.Hard,
                    Description = "Get as close to the walls as possible!"
                },
                new Challenge()
                {
                    Id = 2,
                    CarName = "Formula Golf 1000",
                    TrackName = "Dubai National",
                    Difficulty = Difficulty.Easy,
                    Description = "Ideal to begin your racing career!"
                },
                new Challenge()
                {
                    Id = 3,
                    CarName = "Madza MX-5 Radbul",
                    TrackName = "Willow Springs",
                    Difficulty = Difficulty.Insane,
                    Description = "You think you're a champion? Try bringing this beast to it's limit!"
                },
            };

            var seasonModel = new SeasonModel
            {
                Id = 0,
                Name = "Season III",
                Description = "Dummy test season for testing.",
                StartDate = new DateTime(2018,03,01),
                EndDate = new DateTime(2018,12,31),
                Challenges = trackAndCars
            };

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
