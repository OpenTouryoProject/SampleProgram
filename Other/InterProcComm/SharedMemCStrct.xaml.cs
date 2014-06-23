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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Touryo.Infrastructure.Public.Util;
using Touryo.Infrastructure.Public.Win32;

using System.Threading;
using System.Diagnostics;

using System.Runtime;
using System.Runtime.InteropServices;

namespace InterProcComm
{
    /// <summary>
    /// SharedMemCStrct.xaml の相互作用ロジック
    /// </summary>
    public partial class SharedMemCStrct : Window
    {
         /// <summary>SetResultのDelegate</summary>
        private delegate void SetResultDelegate(string result);

        public SharedMemCStrct()
        {
            InitializeComponent();
        }

        #region サーバ処理

        /// <summary>停止フラグ</summary>
        private bool End = false;

        /// <summary>サーバ起動</summary>
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // ※ 必要であれば、\Asynchronousのサンプルを参照して
            //    起動数制御、ThreadPool化、キューイング数の制御を追加しても良い。

            // Threadを生成してThread関数（PollingSharedMemory）を実行
            Thread th = new Thread(new ThreadStart(this.PollingSharedMemory));

            th.Start();
        }

        /// <summary>サーバ起動 - pollingスレッド関数</summary>
        /// <remarks>unsafeキーワードが必要になる</remarks>
        private void PollingSharedMemory()
        {
            // 共有メモリ（サーバ）
            SharedMemory sm = null;

            // スレッドID
            int managedThreadId = Thread.CurrentThread.ManagedThreadId;

            try
            {
                // 共有メモリを生成
                sm = new SharedMemory("my-sm", 256, "my-mtx");

                // マップ
                sm.Map(0, 0);

                // Polling開始を表示
                this.SetResult_Svr(
                    string.Format("Polling開始！ - ThreadId:{0}", managedThreadId));

                // Polling処理

                while (!End)
                {
                    // システム時間、ローカル時間の「Manage SYSTEMTIME構造体」
                    SYSTEMTIME cst = new SYSTEMTIME();

                    byte[] cstBytes = new byte[Marshal.SizeOf(cst)];
                    byte[] cstsBytes = new byte[Marshal.SizeOf(cst) * 2];
                    
                    // 共有メモリから「Unmanage SYSTEMTIME構造体」配列のバイト表現を読み込む。
                    sm.GetMemory(out cstsBytes, cstsBytes.Length);

                    // マーシャリング（「Unmanage SYSTEMTIME構造体」配列のバイト表現を「Manage SYSTEMTIME構造体」へ変換）

                    //// （１）
                    //object[] os = new object[2];

                    //Array.Copy(cstsBytes, 0, cstBytes, 0, Marshal.SizeOf(cst));
                    //os[0] = CustomMarshaler.BytesToStructure(cstBytes, typeof(SYSTEMTIME));

                    //Array.Copy(cstsBytes, Marshal.SizeOf(cst) * 1, cstBytes, 0, Marshal.SizeOf(cst));
                    //os[1] = CustomMarshaler.BytesToStructure(cstBytes, typeof(SYSTEMTIME));

                    // （２）
                    object[] os = CustomMarshaler.BytesToStructures(cstsBytes, typeof(SYSTEMTIME), 2);

                    SYSTEMTIME[] csts = new SYSTEMTIME[] { (SYSTEMTIME)os[0], (SYSTEMTIME)os[1] };

                    // システム時間
                    string systemTime =
                        string.Format("{0:0000}/{1:00}/{2:00} {3:00}:{4:00}:{5:00}.{6:000}",
                            csts[0].Year, csts[0].Month, csts[0].Day,
                            csts[0].Hour, csts[0].Minute, csts[0].Second, csts[0].Milliseconds);

                    // ローカル時間
                    string localTime =
                        string.Format("{0:0000}/{1:00}/{2:00} {3:00}:{4:00}:{5:00}.{6:000}",
                            csts[1].Year, csts[1].Month, csts[1].Day,
                            csts[1].Hour, csts[1].Minute, csts[1].Second, csts[1].Milliseconds);
                    
                    // 受信メッセージを表示
                    this.SetResult_Svr(
                        string.Format("({0})受信:{1}", managedThreadId,
                        "\r\nsystemTime:" + systemTime + "\r\nlocalTime:" + localTime));

                    Thread.Sleep(1000); // Polling間隔
                }

                // Polling停止を表示
                this.SetResult_Svr(
                    string.Format("Polling停止！ - ThreadId:{0}", managedThreadId));
            }
            catch (Exception ex)
            {
                // エラーを表示
                this.SetResult_Svr(
                    string.Format("({0})エラー：{1}", managedThreadId, ex.ToString()));
            }
            finally
            {
                if (sm != null)
                {
                    // 共有メモリをクローズ
                    // アンロック＆マネージ・アンマネージリソースの解放
                    sm.Close();// ←コメントアウトするとGC任せになるが、ミューテックスの解放が遅れる！
                }
            }
        }

        /// <summary>サーバ停止</summary>
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.End = !this.End;

            if (this.End)
            {
                this.button2.Content = "サーバを有効化する。";
            }
            else
            {
                this.button2.Content = "サーバを停止（ポーリング停止＋アンマップ）する。";
            }
        }

        /// <summary>
        /// Thread関数から結果を反映する。
        /// </summary>
        /// <remarks>
        /// Dispatcher.CheckAccess,Dispatcher.Invokeを
        /// 使用して主スレッドから実行するようにする。
        /// </remarks>
        private void SetResult_Svr(string result)
        {
            // this.Dispatcherを使用する前にthis.CheckAccessを確認

            // 呼び出し元のスレッドがこの Dispatcher に関連付けられた
            // スレッドである場合は true。それ以外の場合は false。 

            if (this.CheckAccess())
            {
                // Dispatcherを利用しなくてもいい場合（主スレッドから実行）

                this.textBox1.Text +=
                    result + "\r\n"
                    + "----------\r\n";
            }
            else
            {
                // Dispatcherを利用する必要がある場合（副スレッドから実行）
                this.Dispatcher.Invoke(
                    new SetResultDelegate(this.SetResult_Svr), result);
            }
        }

        #endregion

        #region クライアント処理
        
        /// <summary>クライアント起動</summary>
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            // ※ 必要であれば、\Asynchronousのサンプルを参照して
            //    起動数制御、ThreadPool化、キューイング数の制御を追加しても良い。

            // Threadを生成してThread関数（WriteSharedMemory）を実行
            Thread th = new Thread(new ThreadStart(this.WriteSharedMemory));
            th.Start();
        }

        /// <summary>クライアント生成 - Writeスレッド関数</summary>
        private void WriteSharedMemory()
        {
            // 共有メモリ（サーバ）
            SharedMemory sm = null;

            // スレッドID
            int managedThreadId = Thread.CurrentThread.ManagedThreadId;

            try
            {
                // 共有メモリを生成（256バイト）
                sm = new SharedMemory("my-sm", 256, "my-mtx");

                // マップ
                sm.Map(0, 0);
                // ロック
                sm.Lock();

                // システム時間、ローカル時間の「Manage SYSTEMTIME構造体」
                SYSTEMTIME[] csts = new SYSTEMTIME[2];

                // システム時間
                CmnWin32.GetSystemTime(out csts[0]);
                string systemTime = 
                    string.Format("{0:0000}/{1:00}/{2:00} {3:00}:{4:00}:{5:00}.{6:000}",
                        csts[0].Year, csts[0].Month, csts[0].Day,
                        csts[0].Hour, csts[0].Minute, csts[0].Second, csts[0].Milliseconds);

                // ローカル時間
                CmnWin32.GetLocalTime(out csts[1]);
                string localTime =
                    string.Format("{0:0000}/{1:00}/{2:00} {3:00}:{4:00}:{5:00}.{6:000}",
                        csts[1].Year, csts[1].Month, csts[1].Day,
                        csts[1].Hour, csts[1].Minute, csts[1].Second, csts[1].Milliseconds);

                // 共有メモリを初期化
                sm.SetMemory(CmnClass.InitBuff(256), 256);

                // マーシャリング（「Unmanage SYSTEMTIME構造体」のバイト表現を取得）

                //// （１）
                //SYSTEMTIME cst = new SYSTEMTIME();
                //int sizeCst = Marshal.SizeOf(cst);
                //byte[] cstBytes = new byte[sizeCst];
                //byte[] cstsBytes = new byte[sizeCst * 2];

                //Array.Copy(CustomMarshaler.StructureToBytes(csts[0]), 0, cstsBytes, 0, sizeCst);
                //Array.Copy(CustomMarshaler.StructureToBytes(csts[1]), 0, cstsBytes, sizeCst * 1, sizeCst);

                // （２）
                byte[] cstsBytes = CustomMarshaler.StructuresToBytes(new object[] { csts[0], csts[1] }, 2);

                // 共有メモリへ書き込む。
                sm.SetMemory(cstsBytes, cstsBytes.Length);

                // 送信メッセージを表示
                this.SetResult_Client(
                    string.Format("({0})送信:{1}", managedThreadId,
                    "\r\nsystemTime:" + systemTime + "\r\nlocalTime:" + localTime));
            }
            catch (Exception ex)
            {
                // エラーを表示
                this.SetResult_Client(
                    string.Format("({0})エラー：{1}", managedThreadId, ex.ToString()));
            }
            finally
            {
                if (sm != null)
                {
                    // 共有メモリをクローズ
                    // アンロック＆マネージ・アンマネージリソースの解放
                    sm.Close();// ←コメントアウトするとGC任せになるが、ミューテックスの解放が遅れる！
                }
            }
        }

        

        /// <summary>
        /// Thread関数から結果を反映する。
        /// </summary>
        /// <remarks>
        /// Dispatcher.CheckAccess,Dispatcher.Invokeを
        /// 使用して主スレッドから実行するようにする。
        /// </remarks>
        private void SetResult_Client(string result)
        {
            // this.Dispatcherを使用する前にthis.CheckAccessを確認

            // 呼び出し元のスレッドがこの Dispatcher に関連付けられた
            // スレッドである場合は true。それ以外の場合は false。 

            if (this.CheckAccess())
            {
                // Dispatcherを利用しなくてもいい場合（主スレッドから実行）

                this.textBox4.Text += result + "\r\n";
            }
            else
            {
                // Dispatcherを利用する必要がある場合（副スレッドから実行）
                this.Dispatcher.Invoke(
                    new SetResultDelegate(this.SetResult_Client), result);
            }
        }

        #endregion

        /// <summary>終了処理</summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.button2_Click(null, null);
        }
    }
}
