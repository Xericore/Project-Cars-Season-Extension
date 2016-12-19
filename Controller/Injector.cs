using Ninject;
using Ninject.Modules;

namespace ProjectCarsSeasonExtension.Controller
{
    public static class Injector
    {
        // ----------------------------------------------------------------------------------------

        private static StandardKernel _kernel;

        // ----------------------------------------------------------------------------------------

        public static void Initialize(params INinjectModule[] modules)
        {
            if (_kernel == null)
            {
                _kernel = new StandardKernel(modules);
            }
        }

        // ----------------------------------------------------------------------------------------

        public static T Get<T>()
        {
            return _kernel.Get<T>();
        }

        // ----------------------------------------------------------------------------------------
    }
}
