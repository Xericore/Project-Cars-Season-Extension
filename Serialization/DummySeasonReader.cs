using System;
using System.Collections.ObjectModel;
using ProjectCarsSeasonExtension.Models;
using ProjectCarsSeasonExtension.Models.Player;

namespace ProjectCarsSeasonExtension.Serialization
{
    public class DummySeasonReader: ISeasonReader
    {
        private Season GetCurrentSeason()
        {
            var seasonModel = new Season
            {
                Id = 0,
                Name = "Season Dummy",
                Description = "Dummy season for testing.",
                StartDate = new DateTime(2018,03,01),
                EndDate = new DateTime(2018,12,31),
                Challenges = GetChallenges()
            };

            return seasonModel;
        }

        public ObservableCollection<Season> GetSeasons()
        {
            var seasons = new ObservableCollection<Season>();

            seasons.Add(GetCurrentSeason());

            return seasons;
        }

        public ObservableCollection<Challenge> GetChallenges()
        {
            ObservableCollection<Challenge> challenges = new ObservableCollection<Challenge>
            {
                new Challenge()
                {
                    Id = 0,
                    CarName = "Formula C",
                    TrackName = "Circuit de Barcelona-Catalunya Club",
                    Difficulty = Difficulty.Easy,
                    Description = "A track where getting into the right rythm is key."
                },
                new Challenge()
                {
                    Id = 1,
                    CarName = "250cc Superkart",
                    TrackName = "Azure Circuit Grand Prix",
                    Difficulty = Difficulty.Hard,
                    Description = "Get as close to the walls as possible!"
                },
                new Challenge()
                {
                    Id = 2,
                    CarName = "Formula Golf 1000",
                    TrackName = "Dubai Autodrome National",
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
                }
            };

            return challenges;
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
                    FastestLap = new TimeSpan(0,0,1,59,117)
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
                    FastestLap = new TimeSpan(0,0,1,42,567)
                },
                new PlayerResult
                {
                    PlayerId = 1,
                    ChallengeId = 1,
                    FastestLap = new TimeSpan(0,0,1,42,117)
                },
                new PlayerResult
                {
                    PlayerId = 2,
                    ChallengeId = 1,
                    FastestLap = new TimeSpan(0,0,1,44,892)
                },
                new PlayerResult
                {
                    PlayerId = 3,
                    ChallengeId = 1,
                    FastestLap = new TimeSpan(0,0,1,41,007)
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
