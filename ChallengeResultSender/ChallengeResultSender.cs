using System;
using pCarsAPI_Demo;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.ChallengeResultSender
{
    public class ChallengeResultSender : IChallengeResultSender
    {
        public event Action<ChallengeResult> ChallengeResultEvent;

        private IProjectCarsStateData _projectCarsStateData;

        private float _lastFiredLapTime;

        public void CheckProjectCarsStateData(IProjectCarsStateData projectCarsStateData)
        {
            if (!AreGameRaceAndSessionStateCorrect(projectCarsStateData))
                return;

            if (_projectCarsStateData != null && _projectCarsStateData.Equals(projectCarsStateData))
                return;

            _projectCarsStateData = projectCarsStateData;

            if (!IsResultValid())
                return;

            _lastFiredLapTime = _projectCarsStateData.LastLapTime;

            var challengeResult = new ChallengeResult(_projectCarsStateData);

            try
            {
                ChallengeResultEvent?.Invoke(challengeResult);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private static bool AreGameRaceAndSessionStateCorrect(IProjectCarsStateData projectCarsStateData)
        {
            if (projectCarsStateData.GameState != GameState.GameIngamePlaying)
                return false;

            if (projectCarsStateData.RaceState != RaceState.RacestateRacing)
                return false;

            if (projectCarsStateData.SessionState != SessionState.SessionTimeAttack)
                return false;

            return true;
        }

        private bool IsResultValid()
        {
            var isWarmupLap = _projectCarsStateData.LastLapTime < 0;

            if (_projectCarsStateData.LapInvalidated && !isWarmupLap)
            { 
                return false;
            }

            var isLapFinished = Math.Abs(_lastFiredLapTime - _projectCarsStateData.LastLapTime) > 0.00001f && !isWarmupLap;

            var isDataOk = !string.IsNullOrEmpty(_projectCarsStateData.CarName) &&
                           !string.IsNullOrEmpty(_projectCarsStateData.TrackLocation) &&
                           !isWarmupLap;

            return isLapFinished && isDataOk;
        }
    }
}
