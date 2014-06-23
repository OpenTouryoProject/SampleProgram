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

using System.IO;
using System.IO.Pipes;

using System.Threading;
using System.Diagnostics;

namespace InterProcComm
{
    /// <summary>
    /// NdPipe.xaml の相互作用ロジック
    /// </summary>
    public partial class NdPipe : Window
    {
        public NdPipe()
        {
            InitializeComponent();
        }

        #region サーバ処理

        /// <summary>サーバ起動</summary>
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // ※ 必要であれば、\Asynchronousのサンプルを参照して
            //    起動数制御、ThreadPool化、キューイング数の制御を追加しても良い。

            // Threadを生成してThread関数（ListeningNamedPipe）を実行
            Thread th = new Thread(new ThreadStart(this.ListeningNamedPipe));

            th.Start();
        }

        /// <summary>サーバ起動 - listenerスレッド関数</summary>
        private void ListeningNamedPipe()
        {
            // 通信シーケンスは自分で設計する
            // （本サンプルはリクエスト・レスポンス型の通信シーケンス）。

            // 名前付きパイプ（サーバ）
            NamedPipeServerStream np = null;
            MemoryStream ms = null;

            // スレッドID
            int managedThreadId = Thread.CurrentThread.ManagedThreadId;

            try
            {
                // 名前付きパイプ（サーバ）を生成
                // ※ 双方向のメッセージ ストリーム（最大５個オープン可能）
                np = new NamedPipeServerStream(
                        "mypipe", PipeDirection.InOut, 5);

                // Listening開始を表示
                this.SetResult_Svr(
                    string.Format("Listening開始！ - ThreadId:{0}", managedThreadId));

                np.WaitForConnection(); //（待機）

                // 接続を表示
                this.SetResult_Svr(
                    string.Format("接続 - ThreadId:{0}", managedThreadId));

                // 終了させるまで延々と読み続ける。
                bool disConnect = false;

                byte[] buff = new byte[256];
                byte[] byt = null;

                int bytSize = 0;

                string msg = "";

                do
                {
                    ms = new MemoryStream();

                    // 受信処理

                    while (true)
                    {
                        // 送信元（＝クライアント）毎に読み込むことができる。
                        bytSize = np.Read(buff, 0, buff.Length);

                        // 受信したメッセージをメモリに蓄積
                        ms.Write(buff, 0, bytSize);

                        // 受信したメッセージを文字列に変換
                        msg = Encoding.UTF8.GetString(ms.ToArray());

                        // ここでは、シーケンス制御文字の
                        // <end-send>が送られるまで受信を続行する。
                        if (msg.IndexOf("<end-send>") != -1)
                        {
                            // シーケンス制御文字を消去
                            msg = msg.Replace("<end-send>", "");
                            break;
                        }

                        // ここでは、シーケンス制御文字の
                        // <end-connect>が送られるまで受信を続行する。
                        if (msg.IndexOf("<end-connect>") != -1)
                        {
                            // これがきたときに切断
                            disConnect = true;

                            // シーケンス制御文字を消去
                            msg = msg.Replace("<end-connect>", "");
                            break;
                        }
                    }

                    // 受信メッセージを表示
                    this.SetResult_Svr(
                        string.Format("({0})受信:{1}", managedThreadId, msg));

                    // 反転処理
                    msg = CmnClass.strrev(msg);

                    // 送信処理

                    if (!disConnect) // <end-connect>の際は、接続がクライアントから切断されている。
                    {
                        // 送信処理
                        byt = Encoding.UTF8.GetBytes(msg + "<end-send>");
                        np.Write(byt, 0, byt.Length);

                        // 送信メッセージを表示
                        this.SetResult_Svr(
                            string.Format("({0})送信:{1}", managedThreadId, msg));
                    }

                } while (!disConnect);

            }
            catch (Exception ex)
            {
                // エラーを表示
                this.SetResult_Svr(
                    string.Format("({0})エラー：{1}", managedThreadId, ex.ToString()));
            }
            finally
            {
                if (np != null)
                {
                    // 名前付きパイプ（サーバ）をクローズ
                    np.Close();
                }
            }

            // 切断を表示
            this.SetResult_Svr(
                string.Format("切断 - ThreadId:{0}", managedThreadId));
        }

        /// <summary>サーバ停止</summary>
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            // ※ 必要であれば、\Asynchronousのサンプルを参照して
            //    起動数制御、ThreadPool化、キューイング数の制御を追加しても良い。

            // Threadを生成してThread関数（StopListenNamedPipe）を実行
            Thread th = new Thread(new ThreadStart(this.StopListenNamedPipe));
            th.Start();
        }

        /// <summary>サーバ停止 - メッセージ送信スレッド関数</summary>
        /// <remarks>なぜか、非同期化しないとハングするため</remarks>
        private void StopListenNamedPipe()
        {
            // 名前付きパイプ（クライアント）
            // 双方向のメッセージ ストリーム
            NamedPipeClientStream s = null;

            try
            {
                // 待機していた場合、止まらないので、
                // ここで、PING（みたいなもの）を送信する。

                // 名前付きパイプ（クライアント）を生成
                s = new NamedPipeClientStream(
                    ".", "mypipe", PipeDirection.InOut, PipeOptions.None);

                s.Connect(3 * 1000);
                //s.ReadTimeout = 3 * 1000;
                //s.WriteTimeout = 3 * 1000;

                // 「終わり」と送信
                byte[] byt = Encoding.UTF8.GetBytes("終わり<end-connect>");
                s.Write(byt, 0, byt.Length);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                if (s != null)
                {
                    // TCPストリーム ソケットをクローズ
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

                // Threadを生成してThread関数（ClientNamedPipe）を実行
                Thread th = new Thread(new ThreadStart(this.ClientNamedPipe));

                // Dictionaryを使用して引数を渡す
                Frequency[th.ManagedThreadId] = frequency;
                Message[th.ManagedThreadId] = message;

                th.Start();
            }
        }

        /// <summary>クライアント生成 - Socketスレッド関数</summary>
        private void ClientNamedPipe()
        {
            // 通信シーケンスは自分で設計する
            // （本サンプルはリクエスト・レスポンス型の通信シーケンス）。

            // 名前付きパイプ（クライアント）
            // 双方向のメッセージ ストリーム
            NamedPipeClientStream s = null;
            MemoryStream ms = null;

            byte[] buff = new byte[256];
            byte[] byt = null;

            int bytSize = 0;
            string str = "";

            int frequency = this.Frequency[Thread.CurrentThread.ManagedThreadId];
            string message = this.Message[Thread.CurrentThread.ManagedThreadId];

            // スレッドID
            int managedThreadId = Thread.CurrentThread.ManagedThreadId;

            try
            {
                // 名前付きパイプ（クライアント）を生成
                s = new NamedPipeClientStream(
                    ".", "mypipe", PipeDirection.InOut);

                s.Connect(3 * 1000);
                //s.ReadTimeout = 3 * 1000;
                //s.WriteTimeout = 3 * 1000;

                // 接続を表示
                this.SetResult_Client(
                    string.Format("接続 - ThreadId:{0}" , managedThreadId));

                // メッセージをｎ回送信
                for (int i = 1; i <= frequency; i++)
                {
                    // メッセージを送信
                    str = i.ToString() + "-" + message + "<end-send>";
                    byt = Encoding.UTF8.GetBytes(str);
                    s.Write(byt, 0, byt.Length);

                    // 送信メッセージを表示
                    this.SetResult_Client(
                        string.Format("({0})送信:{1}", managedThreadId, str));

                    // 受信処理
                    ms = new MemoryStream();

                    while (true)
                    {
                        // メッセージ（の一部）を受信
                        bytSize = s.Read(buff, 0, buff.Length); //（待機）

                        // 受信したメッセージを蓄積
                        ms.Write(buff, 0, bytSize);

                        // 受信したメッセージを文字列に変換
                        str = Encoding.UTF8.GetString(ms.ToArray());

                        // ここでは、<end-send>と送られるまで受信を続行する。
                        if (str.IndexOf("<end-send>") != -1)
                        {
                            // シーケンス制御文字を消去
                            str = str.Replace("<end-send>", "");
                            break;
                        }
                    }

                    // 受信メッセージを表示
                    this.SetResult_Client(
                        string.Format("({0})受信:{1}", managedThreadId, str));
                }

                // 全送信の終了
                str = "終わり<end-connect>";
                byt = Encoding.UTF8.GetBytes(str);
                s.Write(byt, 0, byt.Length);
                
                // 送信メッセージを表示
                this.SetResult_Client(
                        string.Format("({0})送信:{1}", managedThreadId, str));
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
                    // TCPストリーム ソケットをクローズ
                    s.Close();
                }

                // 切断を表示
                this.SetResult_Client(
                    string.Format(
                        "切断 - ThreadId:{0}" , managedThreadId));
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
            try
            {
                // サーバ停止（最低限サーバスレッド数の実行が必要）
                for (int i = 0; i < 10; i++)
                {
                    this.button2_Click(null, null);
                }
            }
            catch
            {
                // タイムアウトの場合
                // 全てのサーバが止まった場合
            }
        }
    }
}
