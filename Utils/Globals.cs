using System.Speech.Synthesis;

namespace ProjectCarsSeasonExtension.Utils
{
    public static class Globals
    {
        public static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static readonly SpeechSynthesizer SpeechSynthesizer = new SpeechSynthesizer();
    }
}
