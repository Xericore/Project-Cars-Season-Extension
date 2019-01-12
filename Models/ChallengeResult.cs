using System;
using pCarsAPI_Demo;
using ProjectCarsSeasonExtension.ChallengeResultSender;

namespace ProjectCarsSeasonExtension.Models
{
    public class ChallengeResult
    {
        public string TrackLocationAndVariant { get; set; }
        public string CarName { get; set; }
        public TimeSpan LastValidLapTime { get; set; }

        public ChallengeResult() { }

        public ChallengeResult(pCarsDataClass projectCarsData)
        {
            TrackLocationAndVariant = projectCarsData.TrackLocation + " " + projectCarsData.TrackVariant;
            CarName = projectCarsData.CarName;
            LastValidLapTime = TimeSpan.FromSeconds(projectCarsData.LastLapTime); ;
        }

        public ChallengeResult(ProjectCarsStateData projectCarsData)
        {
            TrackLocationAndVariant = projectCarsData.TrackLocation + " " + projectCarsData.TrackVariant;
            CarName = projectCarsData.CarName;
            LastValidLapTime = TimeSpan.FromSeconds(projectCarsData.LastLapTime); ;
        }

        public override string ToString()
        {
            return TrackLocationAndVariant + " / " + CarName;
        }

        public string ToLongString()
        {
            return TrackLocationAndVariant + ", " + CarName + ", " + LastValidLapTime;
        }
    }

    public class SimulatedChallengeResult : ChallengeResult
    {
        public string PlayerName { get; set; }
    }
}
