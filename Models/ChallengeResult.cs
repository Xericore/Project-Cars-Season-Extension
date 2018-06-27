using System;
using pCarsAPI_Demo;

namespace ProjectCarsSeasonExtension.Models
{
    public struct ChallengeResult
    {
        public string TrackLocationAndVariant { get; set; }
        public string CarName { get; set; }
        public TimeSpan LastValidLapTime { get; set; }

        public ChallengeResult(pCarsDataClass projectCarsData)
        {
            TrackLocationAndVariant = projectCarsData.TrackLocation + " " + projectCarsData.TrackVariant;
            CarName = projectCarsData.CarName;
            LastValidLapTime = TimeSpan.FromSeconds(projectCarsData.LastLapTime); ;
        }

        public override string ToString()
        {
            return TrackLocationAndVariant + " / " + CarName;
        }
    }
}
