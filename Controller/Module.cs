using Ninject.Modules;
using ProjectCarsSeasonExtension.ViewModels;

namespace ProjectCarsSeasonExtension
{
    class Module : NinjectModule
    {
        // ----------------------------------------------------------------------------------------

        public override void Load()
        {
            Bind<HighscoreViewModel>().To<HighscoreViewModel>().InSingletonScope();
            Bind<CurrentTrackViewModel>().To<CurrentTrackViewModel>().InSingletonScope();
            Bind<PlayerSelectionViewModel>().To<PlayerSelectionViewModel>().InSingletonScope();
        }

        // ----------------------------------------------------------------------------------------
    }
}
