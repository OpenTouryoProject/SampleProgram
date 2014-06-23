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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.Diagnostics;

using Framework;

namespace WinFormApp
{
    public partial class MultiThread : Form
    {
        public MultiThread()
        {
            InitializeComponent();
        }

        // スレッド数を監視する。
        private int ThreadCount = 0;

        /// <summary>スレッド関数を実行</summary>
        private void button1_Click(object sender, EventArgs e)
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
                // 10Thread以上は同時起動しない。
                MessageBox.Show("10Thread以上は同時起動しない！");
            }
            else
            {
                // Thread数情報をインクリメント
                this.ThreadCount++;
                this.label5.Text = this.ThreadCount.ToString();

                // Threadを生成してThread関数を実行
                Thread th = new Thread(new ThreadStart(wainting.Callback_MultiThread));
                th.Start();

                // 循環参照にならないように、
                // Threadオブジェクトは投げっぱなす。

                // デバック情報（主スレッド）
                Debug.WriteLine("主スレッド＠button1_Click："
                    + Thread.CurrentThread.ManagedThreadId.ToString());
            }
        }

        /// <summary>
        /// Thread関数付クラスから結果を反映する。
        /// </summary>
        /// <remarks>
        /// Control.InvokeRequired,Control.Invokeを
        /// 使用して主スレッドから実行するようにする。
        /// </remarks>
        public void SetResult(string result)
        {
            // Invokeを使用する前にthis.InvokeRequiredを確認

            // 呼び出し元のスレッドがこの Dispatcher に関連付けられた
            // スレッドである場合は true。それ以外の場合は false。 

            if (!this.InvokeRequired)
            {
                // Invokeを利用しなくてもいい場合（主スレッドから実行）

                this.ThreadCount--;
                this.label5.Text = this.ThreadCount.ToString();

                this.textBox2.Text += result + "\r\n";

                // デバック情報（主スレッド）
                Debug.WriteLine("主スレッド＠SetResult："
                    + Thread.CurrentThread.ManagedThreadId.ToString());
            }
            else
            {
                // Invokeを利用する必要がある場合（副スレッドから実行）
                this.Invoke(new Waiting.SetResultDelegate(this.SetResult), result);

                // デバック情報（副スレッド）
                Debug.WriteLine("副スレッド＠SetResult："
                    + Thread.CurrentThread.ManagedThreadId.ToString());
            }
        }
    }
}
