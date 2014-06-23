using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CallerEXE2
{
    class Program
    {
        [DllImport("BridgeDLL.dll", CharSet=CharSet.Unicode)]
        static extern string TestMethod(string Message);


        static void Main(string[] args)
        {
            MessageBox.Show("戻り値", Program.TestMethod("マネージ → DLL（VC++ & C++/CLI） → マネージ"));
        }
    }
}
