using System;

namespace ProjectCarsSeasonExtension.ViewModels
{
    public class PlayerTimeListItemModel
    {
        // ----------------------------------------------------------------------------------------

        public int Position { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public DateTime Lap { get; set; }

        // ----------------------------------------------------------------------------------------
    }
}