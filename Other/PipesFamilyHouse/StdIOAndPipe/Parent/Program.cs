//**********************************************************************************
//* Copyright (C) 2007,2014 Hitachi Solutions,Ltd.
//**********************************************************************************

#region Apache License
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

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
        /// StreamReader
        /// 　子プロセスにデータを出力可能な匿名パイプ
        /// </summary>
        static StreamWriter SwPipeServerOut = null;

        /// <summary>
        /// StreamReader
        /// 　子プロセスからデータを入力可能な匿名パイプ
        /// </summary>
        static StreamReader SrPipeServerIn = null;

        /// <summary>
        /// 入力処理スレッド
        /// </summary>
        static Thread InputThread = null;

        /// <summary>
        /// 入力処理スレッドの継続・中断フラグ
        /// </summary>
        static volatile bool Continue = true;

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
            try
            {
                // 子プロセスを起動する。
                Program.Child.StartInfo.FileName = "Child.exe";

                // シェル（コマンド）は使わない。
                Program.Child.StartInfo.CreateNoWindow = true;
                Program.Child.StartInfo.UseShellExecute = false;

                // 標準入出力を使用する（内部実装は匿名パイプ）
                Program.Child.StartInfo.RedirectStandardInput = true;
                Program.Child.StartInfo.RedirectStandardOutput = true;
                Program.Child.StartInfo.RedirectStandardError = true;

                // ここで子プロセスを起動
                Program.Child.Start();

                // ストリームを取得
                Program.SwPipeServerOut = Program.Child.StandardInput;
                Program.SrPipeServerIn = Program.Child.StandardOutput;

                // 以下は子プロセス→コンソールへの出力処理
                // コンソール→子プロセスへの入力処理は別スレッドに実装する。
                Program.InputThread = new Thread(new ThreadStart(Program.InputThreadTask));
                Program.InputThread.IsBackground = true;
                Program.InputThread.Start();

                try
                {
                    // StreamReader + using
                    using (Program.SrPipeServerIn)
                    {
                        string temp;

                        while ((temp = Program.SrPipeServerIn.ReadLine()) != null)
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
                using (Program.SwPipeServerOut)
                {
                    Program.SwPipeServerOut.AutoFlush = true;

                    while (Program.Continue)
                    {
                        string temp = string.Empty;

                        //// 前・子プロセスへの書き込みが
                        //// 全て読み取られるまで待つ。
                        //Program.PipeServerOut.WaitForPipeDrain();

                        // Consoleに入力を促し、子プロセスに書き込み。
                        Console.Write("[Parent] Input text: ");
                        temp = Console.In. ReadLine();
                        Debug.WriteLine("[Parent] Input text: " + temp);
                        Program.SwPipeServerOut.WriteLine(temp);

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
