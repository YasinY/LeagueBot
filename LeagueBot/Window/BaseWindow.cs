using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using LeagueBot.Capture;
using LeagueBot.Dimension;
using LeagueBot.Exceptions;
using LeagueBot.Mapper;
using LeagueBot.Model;

namespace LeagueBot.Window
{
    public abstract class BaseWindow
    {
        private string ProcessName { get; }

        private Process Process { get; set; }

        private IntPtr MainWindowHandlePointer { get; set; }


        public BaseWindow(string processName)
        {
            ProcessName = processName;
            AssignProcess();
        }

        private void AssignProcess()
        {
            try
            {
                Process = RetrieveProcess(); //find first occurence
                MainWindowHandlePointer = Process.MainWindowHandle;
            }
            catch (ProcessNotFoundException exception)
            {
                Console.Error.WriteLine($"Could not find process");
            }
            
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
            var rectangleWidth = rectangle.Width;
            var rectangleHeight = rectangle.Height;
            
            if (rectangleWidth <= 20 || rectangleHeight <= 20)
            {
                throw new PossibleOutOfBoundsException(rectangleWidth, rectangleHeight);
            }

            //whole screen
            var screenBitmap = new Bitmap(rectangleWidth, rectangleHeight, PixelFormat.Format32bppArgb);
            var graphics = Graphics.FromImage(screenBitmap);

            graphics.CopyFromScreen(rectangle.Left, rectangle.Top, 0, 0, screenBitmap.Size,
                CopyPixelOperation.SourceCopy);
            //screenBitmap.Save(@"C:\Users\Yasin\screen.png", ImageFormat.Png); //full image

            DimensionFactory factory = new DimensionFactory();
            var rectanglePositions = factory.Produce(rectangleWidth, rectangleHeight);
            RectanglePositionToRectangleMapper rectangleMapper = new RectanglePositionToRectangleMapper();
            //x1       y2         x2          y1
            var adBitmap = GetAdBitmap(rectangleMapper, rectanglePositions, screenBitmap);
            var adBitmap2 = screenBitmap.Clone(Rectangle.FromLTRB(579, 996, 620, 1010), screenBitmap.PixelFormat);
            adBitmap.Save(@"C:\Users\Yasin\ad.png", ImageFormat.Png); //full image
            graphics.Dispose();
            //TODO create 3 methods with different CopyFromScreen values
            return screenBitmap;
        }

        private Bitmap GetAdBitmap(RectanglePositionToRectangleMapper rectangleMapper,
            Dictionary<string, RectanglePosition> rectanglePositions,
            Bitmap screenBitmap)
        {
            var rectangleF = rectangleMapper.ToDestination(rectanglePositions["ad"]);
            var adBitmap = screenBitmap.Clone(rectangleF, screenBitmap.PixelFormat);
            return adBitmap;
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