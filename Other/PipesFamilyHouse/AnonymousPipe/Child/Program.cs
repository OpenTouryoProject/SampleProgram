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
        /// 方法  匿名パイプを使用してローカル プロセス間の通信を行う
        /// http://msdn.microsoft.com/ja-jp/library/bb546102.aspx
        /// の親プロセス側実装（入出力に対応させた）
        /// ・AnonymousPipeServerStream クラス (System.IO.Pipes)
        /// 　http://msdn.microsoft.com/ja-jp/library/system.io.pipes.anonymouspipeserverstream.aspx
        /// ・AnonymousPipeClientStream クラス (System.IO.Pipes)
        /// 　http://msdn.microsoft.com/ja-jp/library/system.io.pipes.anonymouspipeclientstream.aspx
        /// </summary>
        public static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                using (PipeStream pipeClientIn =
                    new AnonymousPipeClientStream(PipeDirection.In, args[0]))
                {
                    using (PipeStream pipeClientOut =
                    new AnonymousPipeClientStream(PipeDirection.Out, args[1]))
                    {
                        // 匿名パイプでは、Message送信モードはサポートされません。
                        // http://msdn.microsoft.com/ja-jp/library/system.io.pipes.pipetransmissionmode.aspx

                        // 匿名パイプの送信モードを取得：Byteになっている筈（デバッグ）
                        Debug.WriteLine(string.Format(
                            "[Child] pipeClientIn.TransmissionMode: {0}.",
                            pipeClientIn.TransmissionMode.ToString()));
                        Debug.WriteLine(string.Format(
                            "[Child] pipeClientOut.TransmissionMode: {0}.",
                            pipeClientOut.TransmissionMode.ToString()));

                        using (StreamReader sr = new StreamReader(pipeClientIn))
                        {
                            using (StreamWriter sw = new StreamWriter(pipeClientOut))
                            {
                                string temp;
                                sw.AutoFlush = true;

                                while ((temp = sr.ReadLine()) != null)
                                {
                                    Debug.WriteLine("[Child] Debug: " + temp);

                                    // 前・子プロセスへの書き込みが
                                    // 全て読み取られるまで待つ。
                                    pipeClientOut.WaitForPipeDrain();


                                    if (temp.ToUpper().IndexOf("EXIT") != -1)
                                    {
                                        // 終了する場合
                                        Debug.WriteLine("[Child] Debug EXIT");
                                        sw.WriteLine("[Child] EXIT");
                                        sw.Flush();
                                        break;
                                    }
                                    else
                                    {
                                        // 継続する場合

                                        // 入力を反転して出力する。
                                        Debug.WriteLine("[Child] Debug Reversal: " + Program.Reverse(temp));
                                        sw.WriteLine("[Child] Reversal: " + Program.Reverse(temp));
                                        sw.Flush();
                                    }
                                }
                            }
                        }
                    }
                }
            }
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
