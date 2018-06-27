using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    internal sealed class TrackImageManager
    {
        private static readonly Lazy<TrackImageManager> Lazy = new Lazy<TrackImageManager>(() => new TrackImageManager());
        private readonly string[] _trackImagePaths;

        public static TrackImageManager Instance => Lazy.Value;

        private TrackImageManager()
        {
            var allTracksFolderPath = Environment.CurrentDirectory + @"\Assets\Tracks\";

            if (Directory.Exists(allTracksFolderPath))
            {
                _trackImagePaths = Directory.GetFiles(allTracksFolderPath, "*.png");
            }
        }

        public string GetImagePath(string trackName)
        {
            var sortedDistance = new SortedList<int, string>();

            foreach (var trackImagePath in _trackImagePaths)
            {
                string fileName = Path.GetFileName(trackImagePath);
                int distance = LevenshteinDistance.Compute(trackName, fileName);

                if(!sortedDistance.ContainsKey(distance))
                    sortedDistance.Add(distance, fileName);
            }

            if (sortedDistance.Count > 0)
                return "/Assets/Tracks/" + sortedDistance.First().Value;

            return null;
        }
    }

    internal static class LevenshteinDistance
    {
        public static int Compute(string s, string t)
        {
            if (string.IsNullOrEmpty(s))
            {
                if (string.IsNullOrEmpty(t))
                    return 0;
                return t.Length;
            }

            if (string.IsNullOrEmpty(t))
            {
                return s.Length;
            }

            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // initialize the top and right of the table to 0, 1, 2, ...
            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 1; j <= m; d[0, j] = j++) ;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    int min1 = d[i - 1, j] + 1;
                    int min2 = d[i, j - 1] + 1;
                    int min3 = d[i - 1, j - 1] + cost;
                    d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                }
            }
            return d[n, m];
        }
    }
}
