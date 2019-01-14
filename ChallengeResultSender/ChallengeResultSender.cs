using System;
using pCarsAPI_Demo;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.ChallengeResultSender
{
    public class ChallengeResultSender : IChallengeResultSender
    {
        public event Action<ChallengeResult> ChallengeResultEvent;

        private ProjectCarsStateData _projectCarsStateData;

        private float _lastFiredLapTime;
        private bool _wasLastLapValid = true;

        public void CheckProjectCarsStateData(ProjectCarsStateData projectCarsStateData)
        {
            _projectCarsStateData = projectCarsStateData;

            if (!IsResultValid())
                return;

            _lastFiredLapTime = _projectCarsStateData.LastLapTime;

            if (!_wasLastLapValid)
            {
                _wasLastLapValid = true;
                return;
            }

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

        private bool IsResultValid()
        {
            if (_projectCarsStateData.GameState != GameState.GameIngamePlaying)
                return false;

            if (_projectCarsStateData.RaceState != RaceState.RacestateRacing)
                return false;

            var isWarmupLap = _projectCarsStateData.LastLapTime < 0;

            if (_projectCarsStateData.LapInvalidated && !isWarmupLap)
            {
                _wasLastLapValid = false;
                return false;
            }

            var isLapFinished = Math.Abs(_lastFiredLapTime - _projectCarsStateData.LastLapTime) > 0.0001 && !isWarmupLap;

            var isDataOk = !string.IsNullOrEmpty(_projectCarsStateData.CarName) &&
                           !string.IsNullOrEmpty(_projectCarsStateData.TrackLocation) &&
                           !isWarmupLap;

            return isLapFinished && isDataOk;
        }
    }
}
