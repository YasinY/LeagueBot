using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using LeagueBot.Exceptions;
using LeagueBot.Helpers.Capture;
using LeagueBot.Mapper;
using LeagueBot.Model;

namespace LeagueBot.Window
{
    public abstract class BaseWindow
    {
        private Rectangle _rectangle;

        protected Bitmap ActiveScreenBitmap { get; set; }

        protected string ProcessName { get; }

        protected Process Process { get; set; }

        protected IntPtr MainWindowHandlePointer { get; set; }

        protected int RectangleWidth { get; set; }

        protected int RectangleHeight { get; set; }

        protected Dictionary<string, RectanglePosition> RectanglePositions { get; set; }


        public BaseWindow(string processName)
        {
            ProcessName = processName;
            AssignProcess();
            ToFront();
            AssignRectangleData();
        }


        private void AssignProcess()
        {
            try
            {
                Process = RetrieveProcess(); //find first occurence
                MainWindowHandlePointer = Process.MainWindowHandle;
            }
            catch (ProcessNotFoundException)
            {
                Console.Error.WriteLine($"Could not find process.");
            }
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

        private void AssignRectangleData()
        {
            _rectangle = new Rectangle();
            WindowCapture.GetWindowRect(MainWindowHandlePointer, ref _rectangle);
            RectangleWidth = _rectangle.Width;
            RectangleHeight = _rectangle.Height;
            InitialiseBitmap();
        }

        private void InitialiseBitmap()
        {
            ActiveScreenBitmap = new Bitmap(
                RectangleWidth,
                RectangleHeight,
                PixelFormat.Format32bppArgb
            );
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

        public void Capture()
        {
            var correctWindow = WindowCapture.GetForegroundWindow() == MainWindowHandlePointer;
            if (!correctWindow)
            {
                Console.WriteLine("Focused window is not handle. Please focus " + ProcessName);
                return;
            }

            if (RectangleWidth <= 20 || RectangleHeight <= 20)
            {
                throw new PossibleOutOfBoundsException(RectangleWidth, RectangleHeight);
            }

            using var graphics = Graphics.FromImage(ActiveScreenBitmap);
            graphics.CopyFromScreen(
                _rectangle.Left,
                _rectangle.Top,
                0,
                0,
                ActiveScreenBitmap.Size,
                CopyPixelOperation.SourceCopy
            );
        }

        public Bitmap GetBitmap(string modifier, Bitmap screenBitmap)
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
    }
}