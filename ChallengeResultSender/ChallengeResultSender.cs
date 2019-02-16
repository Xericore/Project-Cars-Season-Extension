using System;
using pCarsAPI_Demo;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.ChallengeResultSender
{
    public class ChallengeResultSender : IChallengeResultSender
    {
        public event Action<ChallengeResult> ChallengeResultEvent;

        private IProjectCarsStateData _lastStoredState;

        private float _lastFiredLapTime;
        private bool _ignoreNextStateChange = false;

        public void CheckProjectCarsStateData(IProjectCarsStateData projectCarsStateData)
        {
            if (!AreGameRaceAndSessionStateCorrect(projectCarsStateData))
                return;

            if (!DidStateChange(projectCarsStateData))
                return;
            
            if (_ignoreNextStateChange)
            {
                _ignoreNextStateChange = false;
                return;
            }

            if (projectCarsStateData.LapInvalidated)
            {
                _ignoreNextStateChange = true;
                return;
            }
            
            _lastStoredState = projectCarsStateData;

            if (!IsResultValid())
                return;

            _lastFiredLapTime = _lastStoredState.LastLapTime;

            var challengeResult = new ChallengeResult(_lastStoredState);

            try
            {
                ChallengeResultEvent?.Invoke(challengeResult);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private bool DidStateChange(IProjectCarsStateData projectCarsStateData)
        {
            return _lastStoredState == null || !_lastStoredState.Equals(projectCarsStateData);
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
            var isWarmupLap = _lastStoredState.LastLapTime < 0;

            if (_lastStoredState.LapInvalidated && !isWarmupLap)
            { 
                return false;
            }

            var isLapFinished = Math.Abs(_lastFiredLapTime - _lastStoredState.LastLapTime) > 0.00001f && !isWarmupLap;

            var isDataOk = !string.IsNullOrEmpty(_lastStoredState.CarName) &&
                           !string.IsNullOrEmpty(_lastStoredState.TrackLocation) &&
                           !isWarmupLap;

            return isLapFinished && isDataOk;
        }
    }
}
