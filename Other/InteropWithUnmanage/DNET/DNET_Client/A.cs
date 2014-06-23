using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace DNET_Client
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct A
    {
        public int m1;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
        public string m2_in;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 48)]
        public string m3_out;
    }
}
