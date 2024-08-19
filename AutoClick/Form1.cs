namespace AutoClick
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;
    using Timer = System.Windows.Forms.Timer;

    //using Timer = System.Threading.Timer;

    public partial class Form1 : Form
    {
        
        private int X;
        private int Y;
        private int N;
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

        private const uint INPUT_MOUSE = 0;
        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;


        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);
        private const int VK_ESCAPE = 0x1B; // Mã phím ESC

        public Form1()
        {
            InitializeComponent();
            // Đăng ký sự kiện KeyDown cho form
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            // Đảm bảo rằng form nhận sự kiện KeyDown
            this.KeyPreview = true;

        }
        // Phương thức thực hiện click tại tọa độ (x, y) n lần
        public void ClickNTimesWithoutMovingCursor(int n)
        {
            for (int i = 0; i < n; i++)
            {
                // Kiểm tra nếu phím ESC được nhấn
                if (GetAsyncKeyState(VK_ESCAPE) != 0)
                {
                    Console.WriteLine("Vòng lặp bị dừng bởi phím ESC.");
                    break; // Thoát khỏi vòng lặp nếu phím ESC được nhấn
                }

                // Tạo sự kiện click chuột mà không di chuyển chuột
                INPUT[] inputs = new INPUT[2];

                inputs[0].type = INPUT_MOUSE;
                inputs[0].mi.dwFlags = MOUSEEVENTF_LEFTDOWN;

                inputs[1].type = INPUT_MOUSE;
                inputs[1].mi.dwFlags = MOUSEEVENTF_LEFTUP;

                SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));

                // Tạm dừng giữa các lần click
                System.Threading.Thread.Sleep(100); // Dừng 100ms giữa mỗi lần click (có thể điều chỉnh)
            }
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.G)
            {
                Point mousePosition = Cursor.Position;
                this.X = mousePosition.X;
                this.Y = mousePosition.Y;
                label1.Text = $"({X},{Y})";
                button1.Enabled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if(X == 0 && Y == 0)
                button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClickNTimesWithoutMovingCursor(100);
            N = (int)numericUpDown1.Value;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            N = (int)numericUpDown1.Value;
        }
    }
}