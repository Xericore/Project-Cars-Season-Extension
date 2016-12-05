using System;
using System.Windows;
using ProjectCarsSeasonExtension.Controller;
using ProjectCarsSeasonExtension.Views;

namespace ProjectCarsSeasonExtension
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            Content = new HighscoreView();
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            HotkeyController.Init(this);
            HotkeyController.Register(9000, HotkeyController.None, VirtualKeyCode.VkAdd,
                () => { MessageBox.Show("hotkey got clicked"); });
        }
    }
}