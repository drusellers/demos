using System;
using System.Runtime.InteropServices;

namespace demo.webdriver
{
    public class MouseDriver
    {
        private const UInt32 MOUSEEVENTF_MOVE = 0x0001;
        private const UInt32 MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const UInt32 MOUSEEVENTF_LEFTUP = 0x0004;
        private const UInt32 MOUSEEVENTF_ABSOLUTE = 0x8000;

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        public static void Move(int x, int y)
        {
            SetCursorPos(x, y);
        }

        public static void MoveToBottomCorner()
        {
            Move(0,0);
        }
    }
}