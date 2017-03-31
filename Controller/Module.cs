using Ninject.Modules;
using ProjectCarsSeasonExtension.ViewModels;

namespace ProjectCarsSeasonExtension.Controller
{
    public class Module : NinjectModule
    {
        // ----------------------------------------------------------------------------------------

        public override void Load()
        {
            Bind<HighscoreViewModel>().To<HighscoreViewModel>().InSingletonScope();
        }

        // ----------------------------------------------------------------------------------------
    }
}