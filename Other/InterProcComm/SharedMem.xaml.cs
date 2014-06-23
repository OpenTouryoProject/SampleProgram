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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Touryo.Infrastructure.Public.Util;
using Touryo.Infrastructure.Public.Win32;

using System.Threading;
using System.Diagnostics;

namespace InterProcComm
{
    /// <summary>
    /// SharedMem.xaml の相互作用ロジック
    /// </summary>
    public partial class SharedMem : Window
    {
        public SharedMem()
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
                    byte[] byt = null;
                    sm.GetMemory(out byt, 0);
                    //int msg = BitConverter.ToInt32(byt, 0);
                    short msg = BitConverter.ToInt16(byt, 0);

                    // 受信メッセージを表示
                    this.SetResult_Svr(
                        string.Format("({0})受信:{1}", managedThreadId, msg.ToString()));

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
                    new CmnClass.SetResultDelegate(this.SetResult_Svr), result);
            }
        }

        #endregion

        #region クライアント処理
        
        /// <summary>クライアント起動</summary>
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            // ※ 必要であれば、\Asynchronousのサンプルを参照して
            //    起動数制御、ThreadPool化、キューイング数の制御を追加しても良い。

            // Threadを生成してThread関数（ReadWriteSharedMemory）を実行
            Thread th = new Thread(new ThreadStart(this.ReadWriteSharedMemory));
            th.Start();
        }

        /// <summary>クライアント生成 - ReadWriteスレッド関数</summary>
        private void ReadWriteSharedMemory()
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

                // 受信（バイト → int）
                byte[] rcvByte = null;
                sm.GetMemory(out rcvByte, 0);
                //int i = BitConverter.ToInt32(rcvByte, 0);
                short i = BitConverter.ToInt16(rcvByte, 0);

                // 受信メッセージを表示
                this.SetResult_Client(
                    string.Format("({0})受信:{1}", managedThreadId, i.ToString()));

                // 送信（int → バイト）
                byte[] sndByte = null;

                // 共有メモリを初期化
                sm.SetMemory(this.InitBuff(256), 256);

                // (++i)を送信
                sndByte = BitConverter.GetBytes(++i);
                sm.SetMemory(sndByte, sndByte.Length);
                //sm.SetMemory(sndByte, sndByte.Length);

                // 送信メッセージを表示
                this.SetResult_Client(
                    string.Format("({0})送信:{1}", managedThreadId, i.ToString()));
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

        /// <summary>バッファを初期化する</summary>
        /// <param name="size">バッファのサイズ</param>
        /// <returns>初期化したバッファ</returns>
        private byte[] InitBuff(int size)
        {
            byte[] temp = new byte[size];

            for (int i = 0; i < size; i++)
            {
                temp[i] = 0;
            }

            return temp;
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
                    new CmnClass.SetResultDelegate(this.SetResult_Client), result);
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
