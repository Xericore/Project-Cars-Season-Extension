using System;
using pCarsAPI_Demo;

namespace ProjectCarsSeasonExtension.Models
{
    public struct ChallengeResult
    {
        public string TrackLocationAndVariant;
        public string CarName;
        public TimeSpan LastValidLapTime;

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
