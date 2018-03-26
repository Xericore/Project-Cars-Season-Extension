using ProjectCarsSeasonExtension.Controller;
using ProjectCarsSeasonExtension.Views;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Application = System.Windows.Application;

namespace ProjectCarsSeasonExtension
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly RoutedCommand _closeApplicationCommand = new RoutedCommand();

        // ----------------------------------------------------------------------------------------

        public MainWindow()
        {
            InitializeComponent();
            _closeApplicationCommand.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(_closeApplicationCommand, CloseApplication_Executed));
        }

        // ----------------------------------------------------------------------------------------
        // listener
        // ----------------------------------------------------------------------------------------

        private void Window_Initialized(object sender, EventArgs e)
        {
            HighscoreViewFrame.Content = Injector.Get<HighscoreView>();
        }

        // ----------------------------------------------------------------------------------------

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            HotkeyController.Init(this);
            HotkeyController.Register(9000, HotkeyController.None, VirtualKeyCode.VkAdd, ToggleWindowState);
        }

        // ----------------------------------------------------------------------------------------

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            HotkeyController.Clear();
        }

        // ----------------------------------------------------------------------------------------

        private void ToggleWindowState()
        {
            WindowState = WindowState == WindowState.Minimized ? WindowState.Normal : WindowState.Minimized;
        }

        // ----------------------------------------------------------------------------------------

        private void CloseApplication_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // ----------------------------------------------------------------------------------------
    }
}