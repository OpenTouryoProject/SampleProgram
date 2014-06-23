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

using System.Net;
using System.Net.Sockets;

using System.Threading;
using System.Diagnostics;

namespace InterProcComm
{
    /// <summary>
    /// UDP.xaml の相互作用ロジック
    /// </summary>
    public partial class UDP : Window
    {
        public UDP()
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

            // Threadを生成してThread関数（ListeningUDP）を実行
            Thread th = new Thread(new ThreadStart(this.ListeningUDP));
            th.Start();
        }

        /// <summary>サーバ停止</summary>
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.End = !this.End;

            if (this.End)
            {
                this.button2.Content = "サーバ（リスナ）は有効化する。";

                #region リスナーの停止

                // 待機
                Thread.Sleep(3000);

                // UDPダイアグラムソケット
                Socket s = null;

                try
                {
                    // 待機していた場合、止まらないので、
                    // ここで、PING（みたいなもの）を送信する。

                    // UDPダイアグラム ソケットを生成
                    s = new Socket(
                        AddressFamily.InterNetwork,
                        SocketType.Dgram, ProtocolType.Udp);

                    // 「終わり」と送信
                    byte[] byt = Encoding.UTF8.GetBytes("終わり");
                    s.SendTo(byt, new IPEndPoint(IPAddress.Loopback, 9999));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
                finally
                {
                    if (s != null)
                    {
                        // UDPダイアグラム ソケットをクローズ
                        s.Close();
                    }
                }

                #endregion
            }
            else
            {
                this.button2.Content = "サーバ（リスナ）を停止する。";
            }
        }

        /// <summary>サーバ起動 - listenerスレッド関数</summary>
        private void ListeningUDP()
        {
            // サーバlistener
            UdpClient listener = null;

            // 送信元のIPEndPoint
            IPEndPoint endPoint = null;

            // スレッドID
            int managedThreadId = Thread.CurrentThread.ManagedThreadId;

            try
            {
                // 本サンプルでは、Loopbackの9999ポートをListening

                // Listening開始
                listener = new UdpClient(new IPEndPoint(IPAddress.Loopback, 9999));

                // Listening開始を表示
                this.SetResult_Svr(
                    string.Format(
                        "Listening開始！ - ThreadId:{0}", managedThreadId));

                // 終了させるまで延々と読み続ける。

                while (!this.End)
                {
                    // 受信

                    // 送信元のIPEndPoint（＝クライアント）毎に読み込むことができる。
                    byte[] byt = listener.Receive(ref endPoint); //（待機）

                    // 送信元のIPEndPoint（＝クライアント）
                    string ep = endPoint.ToString();

                    // メッセージを受信
                    string msg = Encoding.UTF8.GetString(byt, 0, byt.Length);

                    // 受信メッセージを表示
                    this.SetResult_Svr(
                        string.Format(
                            "({0})EndPoint:{1}" + "\r\n" +
                            "受信:{2}", managedThreadId, ep, msg));
                }
            }
            catch (Exception ex)
            {
                // エラーを表示
                this.SetResult_Svr(
                    string.Format("({0})エラー：{1}", managedThreadId, ex.ToString()));
            }
            finally
            {
                if (listener != null)
                {
                    // Listening停止
                    listener.Close();
                }
            }

            // Listening停止を表示
            this.SetResult_Svr(
                string.Format(
                    "Listening停止！ - ThreadId:{0}", managedThreadId));
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

        /// <summary>送信回数</summary>
        private Dictionary<int, int> Frequency = new Dictionary<int,int>();
        /// <summary>メッセージ</summary>
        private Dictionary<int, string> Message = new Dictionary<int,string>();

        /// <summary>クライアント起動</summary>
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            int frequency;
            string message;

            if (Int32.TryParse(this.textBox2.Text, out frequency) == false)
            {
                MessageBox.Show("送信回数が認識できない！");
                return;
            }
            else
            {
                // メッセージを取得
                message = this.textBox3.Text;

                // ※ 必要であれば、\Asynchronousのサンプルを参照して
                //    起動数制御、ThreadPool化、キューイング数の制御を追加しても良い。

                // Threadを生成してThread関数（ClientUDP）を実行
                Thread th = new Thread(new ThreadStart(this.ClientUDP));

                // Dictionaryを使用して引数を渡す
                Frequency[th.ManagedThreadId] = frequency;
                Message[th.ManagedThreadId] = message;

                th.Start();
            }
        }

        /// <summary>クライアント生成 - Socketスレッド関数</summary>
        private void ClientUDP()
        {
            // UDPダイアグラム ソケット
            Socket s = null;

            int frequency = this.Frequency[Thread.CurrentThread.ManagedThreadId];
            string message = this.Message[Thread.CurrentThread.ManagedThreadId];

            // スレッドID
            int managedThreadId = Thread.CurrentThread.ManagedThreadId;

            try
            {
                // UDPダイアグラム ソケットを生成
                s = new Socket(
                    AddressFamily.InterNetwork,
                    SocketType.Dgram, ProtocolType.Udp);

                // メッセージをｎ回送信
                for (int i = 1; i <= frequency; i++)
                {
                    // メッセージを送信
                    string str = i.ToString() + "-" + message;
                    byte[] byt = Encoding.UTF8.GetBytes(str);
                    s.SendTo(byt, new IPEndPoint(IPAddress.Loopback, 9999));

                    // 送信メッセージを表示
                    this.SetResult_Client(
                        string.Format("({0})送信:{1}", managedThreadId, str));
                }
            }
            catch (Exception ex)
            {
                // エラーを表示
                this.SetResult_Client(
                    string.Format("({0})エラー：{1}", managedThreadId, ex.ToString()));
            }
            finally
            {   
                if (s != null)
                {
                    // UDPダイアグラム ソケットをクローズ
                    s.Close();
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
                    new CmnClass.SetResultDelegate(this.SetResult_Client), result);
            }
        }

        #endregion

        /// <summary>終了処理</summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // サーバ停止
            if (!this.End)
            {
                this.button2_Click(null, null);
            }
        }
    }
}
