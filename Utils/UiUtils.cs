namespace ProjectCarsSeasonExtension.Utils
{
    internal static class UiUtils
    {
        public static string CreateChallengeTabHeaderText(string challengeLongName)
        {
            string headerText = challengeLongName;
            string[] split = challengeLongName.Split('/');

            if (split.Length >= 2)
            {
                headerText = split[0].Trim() + "\r\n" + split[1].Trim();
            }

            return headerText;
        }
    }
}
