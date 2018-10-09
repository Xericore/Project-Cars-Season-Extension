using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace pCarsAPI_Demo
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int STRING_LENGTH_MAX = 64;

        private readonly DispatcherTimer dispatchTimer = new DispatcherTimer();
        public pCarsDataClass pcarsData = new pCarsDataClass();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = pcarsData;

            dispatchTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1) };
            dispatchTimer.Tick += pCarsDataGetterLoop;
            dispatchTimer.Start();
        }


        private void pCarsDataGetterLoop(object sender, EventArgs e)
        {
            var returnTuple = pCarsAPI_GetData.ReadSharedMemoryData();

            //item1 is the true/false indicating a good read or not
            if (returnTuple.Item1)
            {
                //map the data that's read from the struct and map it to the class
                pcarsData = pcarsData.MapStructToClass(returnTuple.Item2, pcarsData);

                //Thread.Sleep(500);
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            dispatchTimer.Stop();
        }
    }
}