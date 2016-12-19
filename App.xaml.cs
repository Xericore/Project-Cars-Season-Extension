using ProjectCarsSeasonExtension.Controller;
using System.Windows;

namespace ProjectCarsSeasonExtension
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // ----------------------------------------------------------------------------------------

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Injector.Initialize(new Module());
            Current.MainWindow = Injector.Get<MainWindow>();
            Current.MainWindow.Show();
        }

        // ----------------------------------------------------------------------------------------
    }
}
