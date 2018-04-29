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
            var players = new ObservableCollection<Player>();

            if (!File.Exists(FileLocations.PlayerFileUri))
                return players;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Player>));

            List<Player> readPlayers;

            using (var reader = new StreamReader(FileLocations.PlayerFileUri))
            {
                readPlayers = xmlSerializer.Deserialize(reader) as List<Player>;
            }

            if (readPlayers != null)
            {
                foreach (var readPlayer in readPlayers)
                {
                    players.Add(readPlayer);
                }
            }
            
            return players;
        }

        ObservableCollection<PlayerResult> ISeasonReader.GetPlayerResults()
        {
            var playerResults = new ObservableCollection<PlayerResult>();

            if (!File.Exists(FileLocations.PlayerResultFileUri))
                return playerResults;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<PlayerResult>));

            List<PlayerResult> results;

            using (var reader = new StreamReader(FileLocations.PlayerResultFileUri))
            {
                results = xmlSerializer.Deserialize(reader) as List<PlayerResult>;
            }

            if (results != null)
            {
                foreach (var readPlayer in results)
                {
                    playerResults.Add(readPlayer);
                }
            }

            return playerResults;
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
