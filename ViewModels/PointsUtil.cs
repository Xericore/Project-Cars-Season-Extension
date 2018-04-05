using System;

namespace ProjectCarsSeasonExtension.ViewModels
{
    public static class PointsUtil
    {
        public static int PositionToPoints(int position)
        {
            int[] positionToPointsMapping = new[] {25, 18, 15, 12, 10, 8, 6, 4, 2, 1};

            if (position > positionToPointsMapping.Length)
                return 0;

            if (position < 0)
                throw new ArgumentOutOfRangeException("Provided position cannot be below zero.");

            return positionToPointsMapping[position - 1];
        }
    }
}