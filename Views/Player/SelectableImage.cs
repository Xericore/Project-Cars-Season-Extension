using System.Windows.Controls;

namespace ProjectCarsSeasonExtension.Views.Player
{
    public class SelectableImage
    {
        public Image Image { get; set; }
        public string Name => System.IO.Path.GetFileNameWithoutExtension(Path);
        public string Path { get; set; }
    }
}