using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectCarsSeasonExtension.Utils
{
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
}