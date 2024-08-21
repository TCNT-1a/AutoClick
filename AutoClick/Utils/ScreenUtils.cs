using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace AutoClick.Utils
{
    public static class ScreenUtils
    {
        public static Screen[] Screen()
        {
            var screens = System.Windows.Forms.Screen.AllScreens;
            return screens;
        }
        public static Screen PrimaryScreen()
        {
            var screen = System.Windows.Forms.Screen.PrimaryScreen;
            return screen;
        }

    }
}
