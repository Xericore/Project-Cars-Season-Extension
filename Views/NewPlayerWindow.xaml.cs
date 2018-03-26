using System.Windows;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for NewPlayerWindow.xaml
    /// </summary>
    public partial class NewPlayerWindow : Window
    {
        public string PlayerName { get; set; }

        public NewPlayerWindow()
        {
            InitializeComponent();
        }

        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            PlayerName = TextBoxNewPlayerName.Text;
            DialogResult = true;
        }

    }
}
