using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using pCarsAPI_Demo;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for ProjectCarsLiveView.xaml
    /// </summary>
    public partial class ProjectCarsLiveView : Page
    {
        public static int StringLengthMax = 64;

        private readonly DispatcherTimer _dispatchTimer;
        public pCarsDataClass ProjectCarsData = new pCarsDataClass();

        public ProjectCarsLiveView()
        {
            InitializeComponent();

            DataContext = ProjectCarsData;

            _dispatchTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1) };
            _dispatchTimer.Tick += ProjectCarsCarsDataGetterLoop;
            _dispatchTimer.Start();
        }

        private void ProjectCarsCarsDataGetterLoop(object sender, EventArgs e)
        {
            var returnTuple = pCarsAPI_GetData.ReadSharedMemoryData();

            //item1 is the true/false indicating a good read or not
            if (returnTuple.Item1)
            {
                ProjectCarsData = ProjectCarsData.MapStructToClass(returnTuple.Item2, ProjectCarsData);
            }
        }

        private void ProjectCarsLiveView_OnUnloaded(object sender, RoutedEventArgs e)
        {
            _dispatchTimer.Stop();
        }
    }
}
