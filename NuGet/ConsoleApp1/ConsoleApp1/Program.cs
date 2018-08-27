using System;
using System.Collections.Generic;
using System.Diagnostics;
using OssCons.DotNetSubcommittee.TestClassLibrary1;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] aaa = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            List<string> bbb = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
            Dictionary<string, string> ccc = new Dictionary<string, string>()
            {
                { "aaa", "aaaaa"},
                { "bbb", "bbbbb"},
                { "ccc", "ccccc"},
                { "ddd", "ddddd"}
            };

            Console.WriteLine(Class1.SerializeObject(aaa));
            Console.WriteLine(Class2.SerializeObject(bbb));
            Console.WriteLine(Class3.SerializeObject(ccc));

            Console.ReadKey();
        }
    }
}
