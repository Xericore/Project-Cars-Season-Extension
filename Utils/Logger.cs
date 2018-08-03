namespace ProjectCarsSeasonExtension.Utils
{
    public static class Logger
    {
        public static readonly log4net.ILog MyLogger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
