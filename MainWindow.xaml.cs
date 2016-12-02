using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectCarsSeasonExtension
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("User32.dll")]
        private static extern bool RegisterHotKey(
            [In] IntPtr hWnd,
            [In] int id,
            [In] uint fsModifiers,
            [In] uint vk);

        [DllImport("User32.dll")]
        private static extern bool UnregisterHotKey(
            [In] IntPtr hWnd,
            [In] int id);

        private const int HOTKEY_ID = 9000;
        private HwndSource _source;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            this.Content = new Views.HighscoreView();
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            var helper = new WindowInteropHelper(this);
            _source = HwndSource.FromHwnd(helper.Handle);
            _source.AddHook(HwndHook);

            if (!RegisterHotKey(helper.Handle, HOTKEY_ID, 0x0001, 0x50))
            {
                MessageBox.Show("could not register the hotkey");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _source.RemoveHook(HwndHook);
            _source = null;

            var helper = new WindowInteropHelper(this);
            UnregisterHotKey(helper.Handle, HOTKEY_ID);

            base.OnClosed(e);
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (wParam.ToInt32())
            {
                case HOTKEY_ID:
                    OnHotKeyPressed();
                    handled = true;
                    break;
            }
            return IntPtr.Zero;
        }

        private void OnHotKeyPressed()
        {
            MessageBox.Show("hotkey got clicked");
        }
    }
}
