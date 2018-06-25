using System;

namespace ProjectCarsSeasonExtension.Serialization
{
    public static class FileLocations
    {
        public static string BaseFolder = AppDomain.CurrentDomain.BaseDirectory+ "savedata/";
        public static string PlayerFileUri = BaseFolder + "/players.xml";
        public static string PlayerResultFileUri = BaseFolder + "/playerResults.xml";
        public static string ChallengeFileUri = BaseFolder + "/challenges.xml";
        public static string SeasonFileUri =  BaseFolder + "/seasons.xml";
        public static string HandicapsFileUri = BaseFolder + "/handicaps.xml";
    }
}
