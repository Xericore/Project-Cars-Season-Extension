using System;
using System.Windows.Controls;
using System.Windows.Threading;
using pCarsAPI_Demo;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for ProjectCarsLiveView.xaml
    /// </summary>
    public partial class ProjectCarsLiveView : Page
    {
        public event Action<ChallengeResult> ChallengeResultEvent;

        public pCarsDataClass ProjectCarsData { get; set; } = new pCarsDataClass();

        private float _lastFiredLapTime;
        private bool _wasLastLapValid = true;

        public ProjectCarsLiveView()
        {
            InitializeComponent();
            
            var dispatchTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1) };
            dispatchTimer.Tick += ProjectCarsCarsDataGetterLoop;
            dispatchTimer.Start();
        }

        private void ProjectCarsCarsDataGetterLoop(object sender, EventArgs e)
        {
            Tuple<bool, pCarsAPIStruct> returnTuple = pCarsAPI_GetData.ReadSharedMemoryData();

            bool isDataValid = returnTuple.Item1;

            if (isDataValid)
            {
                ProjectCarsData = ProjectCarsData.MapStructToClass(returnTuple.Item2, ProjectCarsData);

                CheckAndFireChallengeResultEvent();
            }
        }

        private void CheckAndFireChallengeResultEvent()
        {
            if (!IsResultValid())
                return;

            _lastFiredLapTime = ProjectCarsData.LastLapTime;

            if (!_wasLastLapValid)
            {
                _wasLastLapValid = true;
                return;
            }

            var challengeResult = new ChallengeResult(ProjectCarsData);

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
            var isWarmupLap = ProjectCarsData.LastLapTime < 0;

            if (ProjectCarsData.LapInvalidated && !isWarmupLap)
            {
                _wasLastLapValid = false;
                return false;
            }

            var isLapFinished = Math.Abs(_lastFiredLapTime - ProjectCarsData.LastLapTime) > 0.0001;

            var isDataOk = !string.IsNullOrEmpty(ProjectCarsData.CarName) &&
                           !string.IsNullOrEmpty(ProjectCarsData.TrackLocation) &&
                           !isWarmupLap;

            return isLapFinished && isDataOk;
        }
    }
}
