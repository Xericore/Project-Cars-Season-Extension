using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace ProjectCarsSeasonExtension.Utils
{
    internal class AutoScreenCycler
    {
        private readonly List<CycleableTabControl> _tabControlsToCycle;
        private readonly TimeSpan _cycleTime;

        private BackgroundWorker _backgroundWorker;
        private bool _shouldCycle;
        
        private int _currentMainTabIndex;
        private int _currentSubTabIndex;
        private int _selectedSubTab;

        public AutoScreenCycler(List<CycleableTabControl> tabControlsToCycle, TimeSpan cycleTime)
        {
            _tabControlsToCycle = tabControlsToCycle;
            _cycleTime = cycleTime;

            StartBackgroundWorker();
        }

        private void StartBackgroundWorker()
        {
            _backgroundWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            _backgroundWorker.DoWork += StartCyclingWorker;
            _backgroundWorker.ProgressChanged += OnCycle;

            _backgroundWorker.RunWorkerAsync();
            StartCycling();
        }

        private void StartCyclingWorker(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (_shouldCycle)
                {
                    _currentSubTabIndex++;

                    if (_currentSubTabIndex >= _tabControlsToCycle[_currentMainTabIndex].IndexesToCycle.Count)
                    {
                        _currentMainTabIndex = (_currentMainTabIndex + 1) % _tabControlsToCycle.Count;
                        _currentSubTabIndex = 0;
                    }

                    if (_tabControlsToCycle[_currentMainTabIndex].IndexesToCycle.Count > 0)
                    {
                        if (_selectedSubTab > _tabControlsToCycle[_currentMainTabIndex].IndexesToCycle.Max())
                            _selectedSubTab = 0;

                        _selectedSubTab = _tabControlsToCycle[_currentMainTabIndex].IndexesToCycle[_currentSubTabIndex];
                    }
                    else
                    {
                        Application.Current.Dispatcher.Invoke(delegate
                        {
                            _selectedSubTab = (_tabControlsToCycle[_currentMainTabIndex].TabControl.SelectedIndex + 1) %
                                              _tabControlsToCycle[_currentMainTabIndex].TabControl.Items.Count;
                        });
                    }
                    
                    _backgroundWorker.ReportProgress(0);
                }

                Thread.Sleep((int) _cycleTime.TotalMilliseconds);
            }
        }

        private void OnCycle(object sender, ProgressChangedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                _tabControlsToCycle[_currentMainTabIndex].TabControl.Focus();
                _tabControlsToCycle[_currentMainTabIndex].TabControl.SelectedIndex = _selectedSubTab;
            });
        }

        public void StartCycling()
        {
            _shouldCycle = true;
        }

        public void StopCycling()
        {
            _shouldCycle = false;
        }
    }

    internal class CycleableTabControl
    {
        public TabControl TabControl { get; }
        public List<int> IndexesToCycle { get; }

        public CycleableTabControl(TabControl tabControl, List<int> indexesToCycle)
        {
            TabControl = tabControl;
            IndexesToCycle = indexesToCycle;
        }

        /// <summary>
        /// All child items will be cycled, when die indexes to cycle are not specified.
        /// </summary>
        /// <param name="tabControl"></param>
        public CycleableTabControl(TabControl tabControl)
        {
            TabControl = tabControl;
            IndexesToCycle = new List<int>(TabControl.Items.Count);

            for (var i = 0; i < IndexesToCycle.Count; i++)
            {
                IndexesToCycle.Add(i);
            }
        }
    }
}
