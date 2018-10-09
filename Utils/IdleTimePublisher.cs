using System;
using System.ComponentModel;
using System.Threading;
using ProjectCarsSeasonExtension.Properties;

namespace ProjectCarsSeasonExtension.Utils
{
    public class IdleTimePublisher
    {
        public static IdleTimePublisher Instance => Lazy.Value;

        private static readonly Lazy<IdleTimePublisher> Lazy = new Lazy<IdleTimePublisher>(() => new IdleTimePublisher());

        public event Action UserIsIdle;
        public event Action UserNotIdle;

        public TimeSpan TimeUserIsIdle { get; set; }

        private readonly BackgroundWorker _idleWorker;

        private bool _userIsIdleInvoked;
        private bool _userNotIdleInvoked;

        public IdleTimePublisher()
        {
            _idleWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            _idleWorker.DoWork += IdleWorker_OnDoWork;
            _idleWorker.ProgressChanged += IdleWorker_OnProgressChanged;

            _idleWorker.RunWorkerAsync();
        }

        private void IdleWorker_OnDoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                _idleWorker.ReportProgress(0);
                Thread.Sleep(3000);
            }
        }

        private void IdleWorker_OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            TimeUserIsIdle = TimeSpan.FromMilliseconds(IdleTimeFinder.GetIdleTime());

            if (TimeUserIsIdle > Settings.Default.IdleTimeUntilLogout && _userIsIdleInvoked == false)
            {
                _userIsIdleInvoked = true;
                _userNotIdleInvoked = false;

                UserIsIdle?.Invoke();
            }
            else if (TimeUserIsIdle < TimeSpan.FromMilliseconds(4500) && _userNotIdleInvoked == false)
            {
                _userIsIdleInvoked = false;
                _userNotIdleInvoked = true;

                UserNotIdle?.Invoke();
            }
        }
    }
}
