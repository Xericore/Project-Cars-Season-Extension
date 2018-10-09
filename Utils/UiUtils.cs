using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.Utils
{
    internal static class UiUtils
    {
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
