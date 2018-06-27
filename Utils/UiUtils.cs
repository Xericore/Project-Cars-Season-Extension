using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ProjectCarsSeasonExtension.Models;

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

        public static Image GetTrackImage(Challenge challenge)
        {
            string imagePath = TrackImageManager.Instance.GetImagePath(challenge.TrackName);

            if (string.IsNullOrEmpty(imagePath))
                return new Image();

            return new Image
            {
                Source = new BitmapImage(new Uri(imagePath, UriKind.Relative)),
                ToolTip = challenge.Name
            };
        }
    }
}
