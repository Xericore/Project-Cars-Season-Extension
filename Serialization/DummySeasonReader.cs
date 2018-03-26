using System;
using System.Collections.ObjectModel;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.Serialization
{
    public class DummySeasonReader: ISeasonReader
    {
        public SeasonModel GetCurrentSeason()
        {
            ObservableCollection<TrackAndCar> trackAndCars = new ObservableCollection<TrackAndCar>
            {
                new TrackAndCar()
                {
                    Id = 0,
                    CarName = "Formula A",
                    TrackName = "Circuit de Barcelone Catalunya GP",
                    Difficulty = Difficulty.Medium,
                    Description = "A track where getting into the right rythm is key."
                },
                new TrackAndCar()
                {
                    Id = 1,
                    CarName = "250cc Superkart",
                    TrackName = "Azure Circuit",
                    Difficulty = Difficulty.Hard,
                    Description = "Get as close to the walls as possible!"
                },
                new TrackAndCar()
                {
                    Id = 2,
                    CarName = "Formula Golf 1000",
                    TrackName = "Dubai National",
                    Difficulty = Difficulty.Easy,
                    Description = "Ideal to begin your racing career!"
                },
                new TrackAndCar()
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
                TracksAndCars = trackAndCars
            };

            return seasonModel;
        }

        public ObservableCollection<PlayerResult> GetPlayerResults()
        {
            var playerResults = new ObservableCollection<PlayerResult>
            {
                new PlayerResult
                {
                    PlayerId = 0,
                    TrackAndCarId = 0,
                    FastestLap = new TimeSpan(0,0,1,22,567)
                },
                new PlayerResult
                {
                    PlayerId = 1,
                    TrackAndCarId = 0,
                    FastestLap = new TimeSpan(0,0,1,23,117)
                },
                new PlayerResult
                {
                    PlayerId = 2,
                    TrackAndCarId = 0,
                    FastestLap = new TimeSpan(0,0,1,21,892)
                },
                new PlayerResult
                {
                    PlayerId = 3,
                    TrackAndCarId = 0,
                    FastestLap = new TimeSpan(0,0,1,25,007)
                },
                new PlayerResult
                {
                    PlayerId = 0,
                    TrackAndCarId = 1,
                    FastestLap = new TimeSpan(0,0,1,32,567)
                },
                new PlayerResult
                {
                    PlayerId = 1,
                    TrackAndCarId = 1,
                    FastestLap = new TimeSpan(0,0,1,32,117)
                },
                new PlayerResult
                {
                    PlayerId = 2,
                    TrackAndCarId = 1,
                    FastestLap = new TimeSpan(0,0,1,34,892)
                },
                new PlayerResult
                {
                    PlayerId = 3,
                    TrackAndCarId = 1,
                    FastestLap = new TimeSpan(0,0,1,31,007)
                }
            };

            return playerResults;
        }
    }
}
