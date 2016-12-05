using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace ProjectCarsSeasonExtension.Controller
{
    class HotkeyController
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

        public const uint NONE = 0x0000;
        public const uint MOD_ALT = 0x0001;
        public const uint MOD_CONTROL = 0x0002;
        public const uint MOD_NOREPEAT = 0x4000;
        public const uint MOD_SHIFT = 0x0004;
        public const uint MOD_WIN = 0x0008;

        public delegate void KeyHook();

        private static HotkeyController instance;
        private WindowInteropHelper helper;
        private HwndSource _source;
        private Dictionary<int, KeyHook> keyHookMap;

        private HotkeyController(Window window)
        {
            keyHookMap = new Dictionary<int, KeyHook>();
            helper = new WindowInteropHelper(window);
            _source = HwndSource.FromHwnd(helper.Handle);
            _source.AddHook(HwndHook);
        }

        ~HotkeyController()
        {
            Clear();
        }

        public static void Init(Window window)
        {
            if (instance == null)
            {
                instance = new HotkeyController(window);
            }
        }

        private static HotkeyController getInstance()
        {
            if (instance == null)
            {
                throw new Exception("HotkeyController not initialized");
            }
            return instance;
        }

        public static void Register(int hotkeyId, uint fsModifiers, uint vk, KeyHook hook)
        {
            getInstance().InternalRegister(hotkeyId, fsModifiers, vk, hook);
        }

        private void InternalRegister(int hotkeyId, uint fsModifiers, uint vk, KeyHook hook)
        {

            if(keyHookMap.ContainsKey(hotkeyId))
            {
                throw new Exception("hotkeyId already in use");
            }

            keyHookMap.Add(hotkeyId, hook);
            if (!RegisterHotKey(helper.Handle, hotkeyId, fsModifiers, vk))
            {
                throw new Exception("Hotkey could not be registered");
            }
        }

        public static void Unregister(int hotkeyId)
        {
            getInstance().InternalUnregister(hotkeyId);
        }

        private void InternalUnregister(int hotkeyId)
        {
            KeyHook hook;
            if(keyHookMap.TryGetValue(hotkeyId, out hook))
            {
                keyHookMap.Remove(hotkeyId);
                UnregisterHotKey(helper.Handle, hotkeyId);
            }
        }

        public static void Clear()
        {
            getInstance().InternalClear();
        }

        private void InternalClear()
        {
            _source.RemoveHook(HwndHook);
            _source = null;

            foreach (KeyValuePair<int, KeyHook> entry in keyHookMap)
            {
                UnregisterHotKey(helper.Handle, entry.Key);
            }
            keyHookMap.Clear();
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            KeyHook hook;
            if(keyHookMap.TryGetValue(wParam.ToInt32(), out hook))
            {
                hook.Invoke();
            }
            return IntPtr.Zero;
        }
    }
}
