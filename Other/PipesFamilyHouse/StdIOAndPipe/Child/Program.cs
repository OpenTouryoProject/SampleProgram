using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.IO.Pipes;
using System.Diagnostics;

/// <summary>親・子プロセスの匿名パイプ通信の子側</summary>
namespace Child
{
    /// <summary>親・子プロセスの匿名パイプ通信の子側</summary>
    /// <remarks>
    /// Debugにはコレを使うと良い
    /// 
    /// 方法 : デバッガを自動的に起動する
    /// http://msdn.microsoft.com/ja-jp/library/a329t4ed(v=vs.80).aspx
    /// 備忘録 Image File Execution Options
    /// http://limejuicer.blog66.fc2.com/blog-entry-18.html
    /// </remarks>
    class Program
    {
        /// <summary>
        /// コンソール・アプリケーションの出力を取り込むには？［C#、VB］ － ＠IT
        /// http://www.atmarkit.co.jp/fdotnet/dotnettips/657redirectstdout/redirectstdout.html
        /// 外部プログラム実行時に処理が固まる場合には？［2.0、C#、VB］ － ＠IT
        /// http://www.atmarkit.co.jp/fdotnet/dotnettips/805pipeasync/pipeasync.html
        /// の親プロセス側実装（入出力に対応させた）
        /// 
        /// ・ProcessStartInfo.RedirectStandardInput プロパティ (System.Diagnostics)
        /// 　http://msdn.microsoft.com/ja-jp/library/system.diagnostics.processstartinfo.redirectstandardinput.aspx
        /// ・Process.StandardInput プロパティ (System.Diagnostics)
        /// 　http://msdn.microsoft.com/ja-jp/library/system.diagnostics.process.standardinput.aspx
        /// 
        /// ・ProcessStartInfo.RedirectStandardOutput プロパティ (System.Diagnostics)
        /// 　http://msdn.microsoft.com/ja-jp/library/system.diagnostics.processstartinfo.redirectstandardoutput.aspx
        /// ・Process.StandardOutput プロパティ (System.Diagnostics)
        /// 　http://msdn.microsoft.com/ja-jp/library/system.diagnostics.process.standardoutput.aspx
        /// 
        /// ・ProcessStartInfo.RedirectStandardError プロパティ (System.Diagnostics)
        /// 　http://msdn.microsoft.com/ja-jp/library/system.diagnostics.processstartinfo.redirectstandarderror.aspx
        /// ・Process.StandardError プロパティ (System.Diagnostics)
        /// 　http://msdn.microsoft.com/ja-jp/library/system.diagnostics.process.standarderror.aspx
        /// </summary>
        public static void Main(string[] args)
        {
            //using (StreamReader sr = Process.GetCurrentProcess().StandardOutput)
            //{
            //    using (StreamWriter sw = Process.GetCurrentProcess().StandardInput)
            //    {

            string temp;
            //sw.AutoFlush = true;

            while ((temp = Console.ReadLine()) != null)
            {
                Debug.WriteLine("[Child] Debug: " + temp);

                //// 前・子プロセスへの書き込みが
                //// 全て読み取られるまで待つ。
                //pipeClientOut.WaitForPipeDrain();

                if (temp.ToUpper().IndexOf("EXIT") != -1)
                {
                    // 終了する場合
                    Debug.WriteLine("[Child] Debug EXIT");
                    Console.WriteLine("[Child] EXIT");
                    //Console.Flush();
                    break;
                }
                else
                {
                    // 継続する場合

                    // 入力を反転して出力する。
                    Debug.WriteLine("[Child] Debug Reversal: " + Program.Reverse(temp));
                    Console.WriteLine("[Child] Reversal: " + Program.Reverse(temp));
                    //Console.Flush();
                }
            }

            //    }
            //}

        }

        /// <summary>文字列反転</summary>
        /// <param name="s">文字列</param>
        /// <returns>反転文字列</returns>
        private static string Reverse(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}
