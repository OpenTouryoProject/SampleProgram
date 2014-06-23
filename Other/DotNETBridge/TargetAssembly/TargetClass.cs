using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace TargetAssembly
{
    public class TargetClass
    {
        public string MessageBox_Show(string s)
        {
            MessageBox.Show(s, "DotNetから表示");

            return "成功！";
        }
    }
}
