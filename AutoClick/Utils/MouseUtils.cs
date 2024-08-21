using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AutoClick.Utils
{
    public static class MouseUtils
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct INPUT
        {
            public uint type;
            public MOUSEINPUT mi;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        public const uint INPUT_MOUSE = 0;
        public const uint MOUSEEVENTF_MOVE = 0x0001;
        public const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        public const uint MOUSEEVENTF_LEFTUP = 0x0004;
        public const uint MOUSEEVENTF_ABSOLUTE = 0x8000;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);
        private const int VK_ESCAPE = 0x1B; // Mã phím ESC

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        public static void ClickAtPosition(int x, int y)
        {
            SetCursorPos(x, y);
            mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)x, (uint)y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, (uint)x, (uint)y, 0, 0);
        }
        public static void SimulateClick(int x, int y)
        {
            INPUT[] inputs = new INPUT[3];

            //// Tính toán tọa độ tuyệt đối cho SendInput
            //int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            //int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            // Xác định màn hình chứa tọa độ (x, y)
            Screen screen = Screen.FromPoint(new Point(x, y));
            Rectangle bounds = screen.Bounds;

            // Tính toán tọa độ tuyệt đối cho SendInput
            int absoluteX = (int)(((x - bounds.X) / (float)bounds.Width) * 65535);
            int absoluteY = (int)(((y - bounds.Y) / (float)bounds.Height) * 65535);



            inputs[0].type = INPUT_MOUSE;
            inputs[0].mi.dx = absoluteX;
            inputs[0].mi.dy = absoluteY;
            inputs[0].mi.dwFlags = MOUSEEVENTF_MOVE;
            //inputs[0].mi.dwFlags = MOUSEEVENTF_ABSOLUTE;

            // Nhấn chuột trái
            inputs[1].type = INPUT_MOUSE;
            inputs[1].mi.dwFlags = MOUSEEVENTF_LEFTDOWN;

            // Thả chuột trái
            inputs[2].type = INPUT_MOUSE;
            inputs[2].mi.dwFlags = MOUSEEVENTF_LEFTUP;
            

            // Gửi sự kiện chuột
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
            ShowPoint();
        }
        public static Point MousePosition()
        {
            return Cursor.Position;

        }
        public static void ShowPoint()
        {
            var point = MousePosition();
            MessageBox.Show($"({point.X},{point.Y})");
        }
    }
}
