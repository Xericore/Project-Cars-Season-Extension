using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace ProjectCarsSeasonExtension.Controller
{
    internal class HotkeyController
    {
        public delegate void KeyHook();

        public const uint None = 0x0000;
        public const uint ModAlt = 0x0001;
        public const uint ModControl = 0x0002;
        public const uint ModNorepeat = 0x4000;
        public const uint ModShift = 0x0004;
        public const uint ModWin = 0x0008;

        private static HotkeyController _instance;
        private readonly WindowInteropHelper _helper;
        private readonly Dictionary<int, KeyHook> _keyHookMap;
        private HwndSource _source;

        private HotkeyController(Window window)
        {
            _keyHookMap = new Dictionary<int, KeyHook>();
            _helper = new WindowInteropHelper(window);
            _source = HwndSource.FromHwnd(_helper.Handle);
            _source?.AddHook(HwndHook);
        }

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

        public static void Init(Window window)
        {
            if (_instance == null)
            {
                _instance = new HotkeyController(window);
            }
        }

        private static HotkeyController GetInstance()
        {
            if (_instance == null)
            {
                throw new Exception("HotkeyController not initialized");
            }
            return _instance;
        }

        public static void Register(int hotkeyId, uint fsModifiers, uint vk, KeyHook hook)
        {
            GetInstance().InternalRegister(hotkeyId, fsModifiers, vk, hook);
        }

        private void InternalRegister(int hotkeyId, uint fsModifiers, uint vk, KeyHook hook)
        {
            if (_keyHookMap.ContainsKey(hotkeyId))
            {
                throw new Exception("hotkeyId already in use");
            }

            _keyHookMap.Add(hotkeyId, hook);
            if (!RegisterHotKey(_helper.Handle, hotkeyId, fsModifiers, vk))
            {
                throw new Exception("Hotkey could not be registered");
            }
        }

        public static void Unregister(int hotkeyId)
        {
            GetInstance().InternalUnregister(hotkeyId);
        }

        private void InternalUnregister(int hotkeyId)
        {
            KeyHook hook;
            if (_keyHookMap.TryGetValue(hotkeyId, out hook))
            {
                _keyHookMap.Remove(hotkeyId);
                UnregisterHotKey(_helper.Handle, hotkeyId);
            }
        }

        public static void Clear()
        {
            GetInstance().InternalClear();
        }

        private void InternalClear()
        {
            _source.RemoveHook(HwndHook);
            _source = null;

            foreach (var entry in _keyHookMap)
            {
                UnregisterHotKey(_helper.Handle, entry.Key);
            }
            _keyHookMap.Clear();
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            KeyHook hook;
            if (_keyHookMap.TryGetValue(wParam.ToInt32(), out hook))
            {
                hook.Invoke();
            }
            return IntPtr.Zero;
        }
    }
}