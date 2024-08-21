namespace AutoClick
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;
    using AutoClick.Utils;
    using Timer = System.Windows.Forms.Timer;

    //using Timer = System.Threading.Timer;

    public partial class Form1 : Form
    {

        private int X;
        private int Y;
        private int N;

        public Form1()
        {
            InitializeComponent();
            // Đăng ký sự kiện KeyDown cho form
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            // Đảm bảo rằng form nhận sự kiện KeyDown
            this.KeyPreview = true;

        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.G)
            {
                X =Cursor.Position.X;
                Y = Cursor.Position.Y;
                label1.Text = $"({X},{Y})";
                button1.Enabled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (X == 0 && Y == 0)
                button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            N = (int)numericUpDown1.Value;
            Thread.Sleep(5000);
            //MouseUtils.SimulateClick(X,Y);
            for(int i = 0; i < N; i++)
            {
                MouseUtils.ClickAtPosition(X, Y);
                Thread.Sleep(500);
            }
            

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            N = (int)numericUpDown1.Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Click me");
        }
    }
}