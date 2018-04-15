using System;
using System.Collections.ObjectModel;
using ProjectCarsSeasonExtension.Models;
using ProjectCarsSeasonExtension.Models.Player;
using ProjectCarsSeasonExtension.Serialization;

namespace ProjectCarsSeasonExtensionTests
{
    public class SeasonReaderTest: ISeasonReader
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
            var players = new ObservableCollection<Player>
            {
                new Player {Id = 0, Name = "Sascha"},
                new Player {Id = 1, Name = "Mario"},
                new Player {Id = 2, Name = "Schumacher"},
                new Player {Id = -1, Name = "New player"}
            };

            return players;
        }

        ObservableCollection<PlayerResult> ISeasonReader.GetPlayerResults()
        {
            var playerResults = new ObservableCollection<PlayerResult>
            {
                new PlayerResult
                {
                    PlayerId = 0,
                    ChallengeId = 0,
                    FastestLap = new TimeSpan(0,0,1,22,567)
                },
                new PlayerResult
                {
                    PlayerId = 1,
                    ChallengeId = 0,
                    FastestLap = new TimeSpan(0,0,1,23,117)
                },
                new PlayerResult
                {
                    PlayerId = 2,
                    ChallengeId = 0,
                    FastestLap = new TimeSpan(0,0,1,21,892)
                },
                new PlayerResult
                {
                    PlayerId = 3,
                    ChallengeId = 0,
                    FastestLap = new TimeSpan(0,0,1,25,007)
                },
                new PlayerResult
                {
                    PlayerId = 0,
                    ChallengeId = 1,
                    FastestLap = new TimeSpan(0,0,1,32,567)
                },
                new PlayerResult
                {
                    PlayerId = 1,
                    ChallengeId = 1,
                    FastestLap = new TimeSpan(0,0,1,32,117)
                },
                new PlayerResult
                {
                    PlayerId = 2,
                    ChallengeId = 1,
                    FastestLap = new TimeSpan(0,0,1,34,892)
                },
                new PlayerResult
                {
                    PlayerId = 3,
                    ChallengeId = 1,
                    FastestLap = new TimeSpan(0,0,1,31,007)
                },
                new PlayerResult
                {
                    PlayerId = 1,
                    ChallengeId = 2,
                    FastestLap = new TimeSpan(0,0,1,10,999)
                },
                new PlayerResult
                {
                    PlayerId = 0,
                    ChallengeId = 2,
                    FastestLap = new TimeSpan(0,0,1,11,671)
                }
            };

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
