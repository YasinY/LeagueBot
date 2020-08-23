using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using LeagueBot.Capture;
using LeagueBot.Dimension;
using LeagueBot.Exceptions;

namespace LeagueBot.Window
{
    public abstract class BaseWindow
    {
        private string ProcessName { get; }

        private Process Process { get; }

        private IntPtr MainWindowHandlePointer { get; }


        public BaseWindow(string processName)
        {
            ProcessName = processName;
            Process = RetrieveProcess(); //find first occurence
            MainWindowHandlePointer = Process.MainWindowHandle;
        }

        private Process RetrieveProcess()
        {
            var processesByName = Process.GetProcessesByName(ProcessName);
            if (processesByName.Length > 0)
            {
                return processesByName.First();
            }
            throw new ProcessNotFoundException(ProcessName);
        }


        public Bitmap Capture()
        {
            ToFront();
            var rectangle = new Rectangle();
            WindowCapture.GetWindowRect(MainWindowHandlePointer, ref rectangle);
            
            //whole screen
            var screenBitmap = new Bitmap(rectangle.Width, rectangle.Height, PixelFormat.Format32bppArgb);
            var graphics = Graphics.FromImage(screenBitmap);
            graphics.CopyFromScreen(rectangle.Left, rectangle.Top, 0, 0, screenBitmap.Size, CopyPixelOperation.SourceCopy);
            
            //screenBitmap.Save(@"C:\Users\Yasin\screen.png", ImageFormat.Png); //full image
            DimensionFactory factory = new DimensionFactory();
            var rectanglePositions = factory.Produce(rectangle.Width, rectangle.Height);
            //x1       y2         x2          y1
            var adBitmap = screenBitmap.Clone(Rectangle.FromLTRB(579, 996, 620, 1010), screenBitmap.PixelFormat);
           // adBitmap.Save(@"C:\Users\Yasin\ad.png", ImageFormat.Png); //full image
            graphics.Dispose();
            //TODO create 3 methods with different CopyFromScreen values
            return screenBitmap;
        }

        public bool ToFront()
        {
            //TODO figure out why using lambda here throws warning lol
            foreach (var process in Process.GetProcesses())
            {
                if (!process.ProcessName.Equals(ProcessName))
                {
                    continue;
                }
                WindowOptions.BringWindowToFront(process);

                return true;
            }

            return false;
        }
    }
}