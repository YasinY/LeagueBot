using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace LeagueBot.Capture
{
    public class WindowOptions
    {
        [DllImport("User32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint exportedProcessId);

        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr handle);

        [DllImport("User32.dll")]
        private static extern bool ShowWindow(IntPtr handle, int nCmdShow);

        [DllImport("User32.dll")]
        private static extern bool IsIconic(IntPtr handle);

        [DllImport("User32.dll")]
        public static extern IntPtr GetForegroundWindow();

        public static bool IsActive(Process process)
        {
            GetWindowThreadProcessId(GetForegroundWindow(), out var exportedProcessId);
            return exportedProcessId != process.Id;
        }

        public static void BringWindowToFront(Process process)
        {
            var handle = process.MainWindowHandle;
            if (IsIconic(handle))
            {
                ShowWindow(handle, 9);
            }

            SetForegroundWindow(handle);
        }
    }
}