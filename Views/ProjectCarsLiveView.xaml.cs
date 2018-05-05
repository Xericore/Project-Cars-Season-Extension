using System;
using System.Windows;
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

        private readonly DispatcherTimer _dispatchTimer;

        private float _lastStoredLapTime;

        public ProjectCarsLiveView()
        {
            InitializeComponent();
            
            _dispatchTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1) };
            _dispatchTimer.Tick += ProjectCarsCarsDataGetterLoop;
            _dispatchTimer.Start();
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
            if (IsLapInvalid())
                return;

            _lastStoredLapTime = ProjectCarsData.LastLapTime;

            var challengeResult = new ChallengeResult(ProjectCarsData);

            ChallengeResultEvent?.Invoke(challengeResult);
        }

        private bool IsLapInvalid()
        {
            return ProjectCarsData.LapInvalidated ||
                   Math.Abs(_lastStoredLapTime - ProjectCarsData.LastLapTime) < 0.001 ||
                   string.IsNullOrEmpty(ProjectCarsData.CarName) || 
                   string.IsNullOrEmpty(ProjectCarsData.TrackLocation) ||
                   ProjectCarsData.LastLapTime < 0;
        }

        private void ProjectCarsLiveView_OnUnloaded(object sender, RoutedEventArgs e)
        {
            _dispatchTimer.Stop();
        }
    }
}
