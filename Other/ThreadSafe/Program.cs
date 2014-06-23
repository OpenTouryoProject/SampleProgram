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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;

namespace ThreadSafe
{
    class Program
    {
        /// <summary>メイン関数（EXEのエントリポイント）</summary>
        /// <param name="args">コマンドライン引数</param>
        static void Main(string[] args)
        {
            // 手動リセットイベントをノンシグナル状態で作成
            EventWaitHandle evt = new EventWaitHandle(false, EventResetMode.ManualReset);

            evt.Reset(); // ★ ノンシグナル状態

            // スレッドと、スレッドに実行させるクラスを準備
            Thread th1 = null;
            Thread th2 = null;
            Thread th3 = null;
            Thread th4 = null;
            Thread th5 = null;

            TestClass c1 = new TestClass(1, evt);
            TestClass c2 = new TestClass(2, evt);
            TestClass c3 = new TestClass(3, evt);
            TestClass c4 = new TestClass(4, evt);
            TestClass c5 = new TestClass(5, evt);

            #region カウンタへのマルチスレッドアクセス

            th1 = new Thread(new ThreadStart(c1.ThFunc_Counter));
            th2 = new Thread(new ThreadStart(c2.ThFunc_Counter));
            th3 = new Thread(new ThreadStart(c3.ThFunc_Counter));
            th4 = new Thread(new ThreadStart(c4.ThFunc_Counter));
            th5 = new Thread(new ThreadStart(c5.ThFunc_Counter));

            // （一斉に）開始
            th1.Start();
            th2.Start();
            th3.Start();
            th4.Start();
            th5.Start();

            evt.Set(); // ★ シグナル状態に

            // 終了を待つ
            th1.Join();
            th2.Join();
            th3.Join();
            th4.Join();
            th5.Join();

            #endregion

            evt.Reset(); // ★ ノンシグナル状態

            #region  ハッシュテーブルへのマルチスレッドアクセス

            th1 = new Thread(new ThreadStart(c1.ThFunc_Hash));
            th2 = new Thread(new ThreadStart(c2.ThFunc_Hash));
            th3 = new Thread(new ThreadStart(c3.ThFunc_Hash));
            th4 = new Thread(new ThreadStart(c4.ThFunc_Hash));
            th5 = new Thread(new ThreadStart(c5.ThFunc_Hash));

            // （一斉に）開始
            th1.Start();
            th2.Start();
            th3.Start();
            th4.Start();
            th5.Start();

            evt.Set(); // ★ シグナル状態に

            // 終了を待つ
            th1.Join();
            th2.Join();
            th3.Join();
            th4.Join();
            th5.Join();

            #endregion

            // htの結果を出力
            Hashtable ht = TestClass.ht;

            foreach (string key in ht.Keys)
            {
                if (key == (string)ht[key])
                {
                    Console.WriteLine("INFO:" + "[" + key + "],[" + (string)ht[key] + "]");
                }
                else
                {
                    Console.WriteLine("ERR:" + "[" + key + "],[" + (string)ht[key] + "]");
                }
            }
        }
    }
}
