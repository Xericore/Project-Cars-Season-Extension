using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectCarsSeasonExtension.Utils
{
    internal sealed class CarImageManager
    {
        public static CarImageManager Instance => Lazy.Value;

        private static readonly Lazy<CarImageManager> Lazy = new Lazy<CarImageManager>(() => new CarImageManager());
        private readonly string[] _imagePaths;
        private const string AssetFolder = "Cars";

        private CarImageManager()
        {
            var allImagesFolderPath = Environment.CurrentDirectory + $@"\Assets\{AssetFolder}\";

            if (Directory.Exists(allImagesFolderPath))
            {
                _imagePaths = Directory.GetFiles(allImagesFolderPath, "*.jpg");
            }
        }

        public string GetImagePath(string carName)
        {
            var sortedDistance = new SortedList<int, string>();

            foreach (var trackImagePath in _imagePaths)
            {
                string fileName = Path.GetFileName(trackImagePath);
                int distance = LevenshteinDistance.Compute(carName, fileName);

                if (!sortedDistance.ContainsKey(distance))
                    sortedDistance.Add(distance, fileName);
            }

            if (sortedDistance.Count > 0)
                return $"/Assets/{AssetFolder}/" + sortedDistance.First().Value;

            return null;
        }
    }
}