using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using pCarsAPI_Demo;
using ProjectCarsSeasonExtension.Annotations;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for ProjectCarsLiveView.xaml
    /// </summary>
    public partial class ProjectCarsLiveView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event Action<ChallengeResult> ChallengeResultEvent;

        public pCarsDataClass ProjectCarsData { get; set; } = new pCarsDataClass();

        public ChallengeResult FakeChallengeResult { get; set; }

        private float _lastFiredLapTime;
        private bool _wasLastLapValid = true;

        public ProjectCarsLiveView()
        {
            InitializeComponent();

            var dispatchTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1) };
            dispatchTimer.Tick += ProjectCarsCarsDataGetterLoop;
            dispatchTimer.Start();
        }

        private void ProjectCarsLiveView_OnLoaded(object sender, RoutedEventArgs e)
        {
            FakeChallengeResult = new ChallengeResult
            {
                LastValidLapTime = new TimeSpan(0, 0, 1, 10, 123)
            };

            OnPropertyChanged(nameof(FakeChallengeResult));
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

        private void SimulateResult_OnClick(object sender, RoutedEventArgs e)
        {
            ChallengeResultEvent?.Invoke(FakeChallengeResult);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
