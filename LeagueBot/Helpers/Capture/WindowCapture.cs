using System;
using System.Runtime.InteropServices;

namespace LeagueBot.Helpers.Capture
{
    public class WindowCapture
    {

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref System.Drawing.Rectangle rectangle);
        
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        
    }
}