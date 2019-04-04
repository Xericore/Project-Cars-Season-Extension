using System;
using pCarsAPI_Demo;
using ProjectCarsSeasonExtension.Models;
using ProjectCarsSeasonExtension.Utils;

namespace ProjectCarsSeasonExtension.ChallengeResultSender
{
    public class ChallengeResultSender : IChallengeResultSender
    {
        public event Action<ChallengeResult> ChallengeResultEvent;

        private IProjectCarsStateData _lastStoredState;

        private float _lastFiredLapTime;
        private bool _ignoreNextLap = false;

        public void CheckProjectCarsStateData(IProjectCarsStateData state)
        {
            if(state.GameState == GameState.GameIngamePaused)
                return;

            if (_lastStoredState != null && _lastStoredState.Equals(state))
            {
                return;
            }

            _lastStoredState = state;

            if (!IsUserRacing(state))
            {
                ResetState();
                return;
            }
            
            if (state.LapInvalidated)
            {
                _ignoreNextLap = true;
                return;
            }

            if (IsWarmupLap(state))
                return;

            if (!IsLapFinished(state))
                return;

            if (_ignoreNextLap)
            {
                _ignoreNextLap = false;
                return;
            }

            InvokeChallengeResultEvent();

            _lastFiredLapTime = _lastStoredState.LastLapTime;
        }

        private static bool IsUserRacing(IProjectCarsStateData projectCarsStateData)
        {
            if (projectCarsStateData.GameState != GameState.GameIngamePlaying && projectCarsStateData.GameState != GameState.GameIngamePaused)
                return false;

            if (projectCarsStateData.RaceState != RaceState.RacestateRacing)
                return false;

            if (projectCarsStateData.SessionState != SessionState.SessionTimeAttack)
                return false;

            if (string.IsNullOrEmpty(projectCarsStateData.CarName) ||
                string.IsNullOrEmpty(projectCarsStateData.TrackLocation))
                return false;

            return true;
        }

        private void ResetState()
        {
            _lastFiredLapTime = 0f;
            _ignoreNextLap = false;
        }

        private bool IsLapFinished(IProjectCarsStateData projectCarsStateData)
        {
            return Math.Abs(_lastFiredLapTime - projectCarsStateData.LastLapTime) > 0.00001f;
        }

        private static bool IsWarmupLap(IProjectCarsStateData projectCarsStateData)
        {
            return projectCarsStateData.LastLapTime < 0;
        }

        private void InvokeChallengeResultEvent()
        {
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
    }
}
