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
        private Rectangle _rectangle;

        private string ProcessName { get; }

        private Process Process { get; set; }

        private IntPtr MainWindowHandlePointer { get; set; }


        private int RectangleWidth { get; set; }

        private int RectangleHeight { get; set; }

        private Dictionary<string, RectanglePosition> RectanglePositions { get; set; }


        public BaseWindow(string processName)
        {
            ProcessName = processName;
            AssignProcess();
            ToFront();
            AssignRectangleData();
        }

        private void AssignRectangleData()
        {
            _rectangle = new Rectangle();
            WindowCapture.GetWindowRect(MainWindowHandlePointer, ref _rectangle);
            RectangleWidth = _rectangle.Width;
            RectangleHeight = _rectangle.Height;
            RectanglePositions = new DimensionFactory().Produce(RectangleWidth, RectangleHeight);
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
            if (RectangleWidth <= 20 || RectangleHeight <= 20)
            {
                throw new PossibleOutOfBoundsException(RectangleWidth, RectangleHeight);
            }

            var screenBitmap = new Bitmap(RectangleWidth, RectangleHeight, PixelFormat.Format32bppArgb);
            var graphics = Graphics.FromImage(screenBitmap);
            graphics.CopyFromScreen(_rectangle.Left, _rectangle.Top, 0, 0, screenBitmap.Size,
                CopyPixelOperation.SourceCopy);

            var adBitmap = GetBitmap("ad", screenBitmap);
            var apBitmap = GetBitmap("ap", screenBitmap);

            adBitmap.Save(@"C:\Users\Yasin\ad.png", ImageFormat.Png);
            apBitmap.Save(@"C:\Users\Yasin\ap.png", ImageFormat.Png);
            graphics.Dispose();
            return screenBitmap;
        }


        private Bitmap GetBitmap(string modifier, Bitmap screenBitmap)
        {
            var rectangleMapper = new RectanglePositionToRectangleMapper();
            if (!RectanglePositions.ContainsKey(modifier))
            {
                return new Bitmap(0, 0);
            }

            var rectangleF = rectangleMapper.ToDestination(RectanglePositions[modifier]);
            var bitMap = screenBitmap.Clone(rectangleF, screenBitmap.PixelFormat);

            return bitMap;
        }

        private bool ToFront()
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