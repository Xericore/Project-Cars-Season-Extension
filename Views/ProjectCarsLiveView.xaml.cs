using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
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

        public SimulatedChallengeResult SimulatedChallengeResult { get; set; }

        public ObservableCollection<string> AllRaceNames { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> AllPlayerNames { get; set; } = new ObservableCollection<string>();

        private float _lastFiredLapTime;
        private bool _wasLastLapValid = true;

        private readonly DataView _dataView;
        private bool _wasInitialized;

        public ProjectCarsLiveView(DataView dataView)
        {
            InitializeComponent();

            _dataView = dataView;

            BackgroundWorker worker = new BackgroundWorker { WorkerReportsProgress = true };
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!(sender is BackgroundWorker backgroundWorker))
                return;

            while (true)
            {
                backgroundWorker.ReportProgress(0);
                Thread.Sleep(1000);
            }
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProjectCarsCarsDataGetterLoop();
        }

        private void ProjectCarsLiveView_OnLoaded(object sender, EventArgs eventArgs)
        {
            if (_wasInitialized)
                return;

            _wasInitialized = true;

            SetAllRaceNames();
            SetAllPlayerNames();
            InitializeFakeChallengeResult();
        }

        private void SetAllRaceNames()
        {
            foreach (Challenge challenge in _dataView.AllChallenges)
                AllRaceNames.Add(challenge.Name);
        }

        private void SetAllPlayerNames()
        {
            foreach (Models.Player.Player player in _dataView.Players)
            {
                if (player.Id >= 0)
                    AllPlayerNames.Add(player.Name);
            }
        }

        private void InitializeFakeChallengeResult()
        {
            SimulatedChallengeResult = new SimulatedChallengeResult
            {
                LastValidLapTime = new TimeSpan(0, 0, 1, 10, 123)
            };
            
            OnPropertyChanged(nameof(SimulatedChallengeResult));

            PlayerNameSelector.SelectedIndex = 0;
            RaceNameSelector.SelectedIndex = 0;
        }

        private void ProjectCarsCarsDataGetterLoop()
        {
            Tuple<bool, pCarsAPIStruct> returnTuple = pCarsAPI_GetData.ReadSharedMemoryData();

            bool isDataValid = returnTuple.Item1;

            if (isDataValid)
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    ProjectCarsData = ProjectCarsData.MapStructToClass(returnTuple.Item2, ProjectCarsData);

                    CheckAndFireChallengeResultEvent();
                });
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
            try
            {
                string[] split = RaceNameSelector.Text.Split('/');
                SimulatedChallengeResult.TrackLocationAndVariant = split[0].Trim();
                SimulatedChallengeResult.CarName = split[1].Trim();

                SimulatedChallengeResult.PlayerName = PlayerNameSelector.SelectedItem as string;

                ChallengeResultEvent?.Invoke(SimulatedChallengeResult);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddAsRace_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ProjectCarsData?.CarName) || string.IsNullOrEmpty(ProjectCarsData.TrackLocation))
                return;

            var trackName = ProjectCarsData.TrackLocation;
            if (!string.IsNullOrEmpty(ProjectCarsData.TrackVariant))
                trackName += " " + ProjectCarsData.TrackVariant;

            _dataView.AddChallenge(new Challenge
            {
                CarName = ProjectCarsData.CarName,
                TrackName = trackName,
                Difficulty = Difficulty.Medium,
                Description = "Default race description."
            });

        }
    }
}
