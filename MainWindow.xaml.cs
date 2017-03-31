using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using ProjectCarsSeasonExtension.Controller;
using ProjectCarsSeasonExtension.ViewModels;
using ProjectCarsSeasonExtension.Views;
using Application = System.Windows.Application;

namespace ProjectCarsSeasonExtension
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // ----------------------------------------------------------------------------------------

        private OverlayWindow _overlayWindow;
        private Screen _mainScreen;
        private readonly RoutedCommand _closeApplicationCommand = new RoutedCommand();

        // ----------------------------------------------------------------------------------------

        public MainWindow()
        {
            InitializeComponent();
            _closeApplicationCommand.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(_closeApplicationCommand, CloseApplication_Executed));
        }

        // ----------------------------------------------------------------------------------------

        private Screen GetMainScreen()
        {
            foreach (var screen in Screen.AllScreens)
            {
                if (screen.Primary)
                    return screen;
            }
            throw new Exception("Where do you play the game on??? We do need a main screen!");
        }

        // ----------------------------------------------------------------------------------------
        // listener
        // ----------------------------------------------------------------------------------------

        private void Window_Initialized(object sender, EventArgs e)
        {
            Content = Injector.Get<HighscoreView>();
            _mainScreen = GetMainScreen();
        }

        // ----------------------------------------------------------------------------------------

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            HotkeyController.Init(this);
            HotkeyController.Register(9000, HotkeyController.None, VirtualKeyCode.VkAdd, OpenOverlayWindow);
        }

        // ----------------------------------------------------------------------------------------

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            HotkeyController.Clear();
        }

        // ----------------------------------------------------------------------------------------

        private void OpenOverlayWindow()
        {
            if (_overlayWindow == null)
            {
                _overlayWindow = Injector.Get<OverlayWindow>();
                _overlayWindow.Topmost = true;
                _overlayWindow.Loaded += (sender, e) =>
                {
                    _overlayWindow.WindowState = WindowState.Normal;
                    _overlayWindow.Left = _mainScreen.WorkingArea.Left;
                    _overlayWindow.Top = _mainScreen.WorkingArea.Top;
                    _overlayWindow.WindowState = WindowState.Maximized;
                };
                _overlayWindow.Show();
            }
            else
            {
                _overlayWindow.Close();
                _overlayWindow = null;
            }
        }

        // ----------------------------------------------------------------------------------------

        private void CloseApplication_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // ----------------------------------------------------------------------------------------
    }
}