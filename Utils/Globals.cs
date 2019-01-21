using System.Speech.Synthesis;

namespace ProjectCarsSeasonExtension.Utils
{
    public static class Globals
    {
        public static readonly log4net.ILog Logger = log4net.LogManager.GetLogger("DefaultLogger");
        public static readonly log4net.ILog DebugLogger = log4net.LogManager.GetLogger("DebugLogger");
        public static readonly SpeechSynthesizer SpeechSynthesizer = new SpeechSynthesizer();
    }
}
