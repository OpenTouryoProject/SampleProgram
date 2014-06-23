using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.IO.Pipes;
using System.Diagnostics;
using System.Threading;

/// <summary>親・子プロセスの匿名パイプ通信の親側</summary>
namespace Parent
{
    /// <summary>親・子プロセスの匿名パイプ通信の親側</summary>
    class Program
    {
        /// <summary>
        /// 子プロセス
        /// </summary>
        static Process Child = new Process();

        /// <summary>
        /// AnonymousPipeServerStream
        /// 　子プロセスにデータを出力可能な匿名パイプ
        /// </summary>
        static AnonymousPipeServerStream PipeServerOut = null;

        /// <summary>
        /// AnonymousPipeServerStream
        /// 　子プロセスからデータを入力可能な匿名パイプ
        /// </summary>
        static AnonymousPipeServerStream PipeServerIn = null;

        /// <summary>
        /// 入力処理スレッド
        /// </summary>
        static Thread InputThread = null;

        /// <summary>
        /// 入力処理スレッドの継続・中断フラグ
        /// </summary>
        static volatile bool Continue = true;

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
            try
            {
                // 子プロセスにデータを出力可能な匿名パイプ（継承可能に設定）
                Program.PipeServerOut =
                    new AnonymousPipeServerStream(PipeDirection.Out,
                        HandleInheritability.Inheritable);

                // 子プロセスにデータを出力可能な匿名パイプ（継承可能に設定）
                Program.PipeServerIn =
                    new AnonymousPipeServerStream(PipeDirection.In,
                        HandleInheritability.Inheritable);

                // ここまでの処理で
                //  サーバ側で使用するハンドルは
                // ・継承不可能にコピーされ、
                // ・コピー元はクローズされている
                // ものと思われる。

                // 匿名パイプでは、Message送信モードはサポートされません。
                // http://msdn.microsoft.com/ja-jp/library/system.io.pipes.pipetransmissionmode.aspx

                // 匿名パイプの送信モードを取得：Byteになっている筈（デバッグ）
                Debug.WriteLine(string.Format(
                    "[Parent] Program.PipeServerOut.TransmissionMode: {0}.",
                    Program.PipeServerOut.TransmissionMode.ToString()));
                Debug.WriteLine(string.Format(
                    "[Parent] Program.PipeServerIn.TransmissionMode: {0}.",
                    Program.PipeServerIn.TransmissionMode.ToString()));

                // 子プロセスを起動する。
                Program.Child.StartInfo.FileName = "Child.exe";

                // AnonymousPipeClientStreamで使用する
                // ハンドルをコマンドライン引数で渡す。
                Program.Child.StartInfo.Arguments =
                    Program.PipeServerOut.GetClientHandleAsString() + " "
                    + Program.PipeServerIn.GetClientHandleAsString();

                // シェル（コマンド）は使わない。
                Program.Child.StartInfo.CreateNoWindow = true;
                Program.Child.StartInfo.UseShellExecute = false;

                // ここで子プロセスを起動
                // AnonymousPipeClientStreamで使用するハンドルが継承される。
                Program.Child.Start();

                // AnonymousPipeClientStreamで使用するハンドルを閉じる。
                Program.PipeServerOut.DisposeLocalCopyOfClientHandle();
                Program.PipeServerIn.DisposeLocalCopyOfClientHandle();

                // 以下は子プロセス→コンソールへの出力処理
                // コンソール→子プロセスへの入力処理は別スレッドに実装する。
                Program.InputThread = new Thread(new ThreadStart(Program.InputThreadTask));
                Program.InputThread.IsBackground = true;
                Program.InputThread.Start();

                try
                {
                    // StreamReader + using
                    using (StreamReader sr = new StreamReader(Program.PipeServerIn))
                    {
                        string temp;

                        while ((temp = sr.ReadLine()) != null)
                        {
                            if (temp.ToUpper().IndexOf("EXIT") != -1)
                            {
                                Program.Continue = false;
                                break;
                            }
                            else
                            {
                                // コンソールへの出力処理
                                Console.WriteLine("[Parent] Output text: " + temp);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("[Parent] Output Task Error: {0}", e.ToString());
                }
            }
            finally
            {
                // 入力処理スレッド
                Program.InputThread.Abort(); // 強制終了
                Program.InputThread.Join(); // 終了待ち合わせ
                // ホントは、Console入力のPINGを打って
                // 入力処理スレッドのConsole.ReadLine()を終了させたいが・・・。

                // 子プロセス
                Program.Child.WaitForExit();
                Program.Child.Close();

                // 匿名パイプ
                Program.PipeServerOut.Close();
                Program.PipeServerIn.Close();
            }

            // 終了
            Console.WriteLine("[Parent] Exit");
            Console.ReadLine();
        }

        /// <summary>入力処理を別スレッドで実施</summary>
        private static void InputThreadTask()
        {
            try
            {
                // StreamWriter + using
                using (StreamWriter sw = new StreamWriter(Program.PipeServerOut))
                {
                    sw.AutoFlush = true;

                    while (Program.Continue)
                    {
                        string temp = string.Empty;

                        // 前・子プロセスへの書き込みが
                        // 全て読み取られるまで待つ。
                        Program.PipeServerOut.WaitForPipeDrain();

                        // Consoleに入力を促し、子プロセスに書き込み。
                        Console.Write("[Parent] Input text: ");
                        temp = Console.In. ReadLine();
                        Debug.WriteLine("[Parent] Input text: " + temp);
                        sw.WriteLine(temp);

                        Thread.Sleep(100); // 見せ方の問題
                    }
                }
            }
            catch (ThreadAbortException tae)
            {
                // Abortで発生する。
                Debug.WriteLine("[Parent] Input Thread Task End: {0}", tae.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("[Parent] Input Thread Task Error: {0}", e.ToString());
            }
        }
    }
}
