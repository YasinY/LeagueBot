using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace LeagueBot.Capture
{
    public class WindowCapture
    {

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rectangle rectangle);
        
    }
}