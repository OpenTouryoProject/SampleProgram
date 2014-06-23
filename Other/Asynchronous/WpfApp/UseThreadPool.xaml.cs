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

using System.Threading;
using System.Diagnostics;

using Framework;

namespace WpfApp
{
    /// <summary>
    /// UseThreadPool1.xaml の相互作用ロジック
    /// </summary>
    public partial class UseThreadPool : Window
    {
        public UseThreadPool()
        {
            InitializeComponent();

            ThreadPool.SetMinThreads(10, 10); // 待機状態スレッド数
            ThreadPool.SetMaxThreads(10, 10); // 最大スレッド起動数
        }

        // スレッド数を監視する。
        private int ThreadCount = 0;

        /// <summary>スレッド関数を実行</summary>
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // 実行時間を生成
            int execTime = 0;
            int maxExecTime = 0;

            if (Int32.TryParse(this.textBox1.Text, out maxExecTime) == false)
            {
                MessageBox.Show("実行時間が認識できない！");
                return;
            }
            else
            {
                Random random = new Random();
                execTime = random.Next(maxExecTime);
            }

            // Threadに渡すThread関数付クラスを生成
            Waiting wainting = new Waiting();
            wainting.SetResult = new Waiting.SetResultDelegate(this.SetResult);
            wainting.SleepTime = execTime;

            if (this.ThreadCount >= 10)
            {
                // これ以上はキューイングできない。
                MessageBox.Show("これ以上はキューイングできない！");
            }
            else
            {
                // Thread数情報をインクリメント
                this.ThreadCount++;
                this.label1.Content = this.ThreadCount.ToString();

                // Thread関数をThreadPoolに追加して実行
                ThreadPool.QueueUserWorkItem(new WaitCallback(wainting.Callback_UseThreadPool));

                // デバック情報（主スレッド）
                Debug.WriteLine("主スレッド＠button1_Click："
                    + Thread.CurrentThread.ManagedThreadId.ToString());
            }
        }

        /// <summary>
        /// Thread関数付クラスから結果を反映する。
        /// </summary>
        /// <remarks>
        /// Dispatcher.CheckAccess,Dispatcher.Invokeを
        /// 使用して主スレッドから実行するようにする。
        /// </remarks>
        public void SetResult(string result)
        {
            // this.Dispatcherを使用する前にthis.CheckAccessを確認

            // 呼び出し元のスレッドがこの Dispatcher に関連付けられた
            // スレッドである場合は true。それ以外の場合は false。 

            if (this.CheckAccess())
            {
                // Dispatcherを利用しなくてもいい場合（主スレッドから実行）

                this.ThreadCount--;
                this.label1.Content = this.ThreadCount.ToString();

                this.textBox2.Text += result + "\r\n";

                // デバック情報（主スレッド）
                Debug.WriteLine("主スレッド＠SetResult："
                    + Thread.CurrentThread.ManagedThreadId.ToString());
            }
            else
            {
                // Dispatcherを利用する必要がある場合（副スレッドから実行）
                this.Dispatcher.Invoke(new Waiting.SetResultDelegate(this.SetResult), result);

                // デバック情報（副スレッド）
                Debug.WriteLine("副スレッド＠SetResult："
                    + Thread.CurrentThread.ManagedThreadId.ToString());
            }
        }
    }
}
