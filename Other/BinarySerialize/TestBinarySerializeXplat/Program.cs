using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using Touryo.Infrastructure.Public.IO;
using Touryo.Infrastructure.Public.Str;
using Touryo.Infrastructure.Public.Util;

namespace TestBinarySerializeXplat
{
    /// <summary>Program</summary>
    class Program
    {
        /// <summary>Main</summary>
        /// <param name="args">string[]</param>
        static void Main(string[] args)
        {
            string ver = "";

#if NET45
            ver = "NET45";
#elif NET46
            ver="NET46";
#elif NET47
            ver="NET47";
#elif NET48
            ver="NET48";
#elif NETCORE20
            ver="NETCORE20";
#elif NETCORE30
            ver="NETCORE30";
#else
#endif

            string filePath = "";
            string fileName = "TestBinarySerializeXplat.txt";
            if (System.Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                filePath = @"C:\Users\nishi\";
            }
            else
            {
                filePath = "/mnt/c/Users/nishi/";
            }

            filePath += fileName;

            Dictionary<string, string> dic = null;
            if (File.Exists(filePath))
            {
                dic = (Dictionary<string, string>)
                    BinarySerialize.BytesToObject(
                        CustomEncode.FromBase64String(
                            ResourceLoader.LoadAsString(filePath, Encoding.UTF8)));

                Console.WriteLine("loaded string : " + dic["ver"]);
                Console.ReadKey();
            }

            dic = new Dictionary<string, string>();
            dic["ver"] = ver;

            using (StreamWriter sr = new System.IO.StreamWriter(filePath, false, Encoding.UTF8))
            {
                sr.WriteLine(
                    CustomEncode.ToBase64String(
                        BinarySerialize.ObjectToBytes(dic)));
            }
        }
    }
}
