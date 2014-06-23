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
    class TestClass
    {
        // スレッドID
        public int ID = 0;

        // 排他用のオブジェクト（クリティカルセクションで利用）
        private static readonly object _lock = new object();

        // 同期用のオブジェクト（開始処理の調整）
        private EventWaitHandle _evt = null;

        // カウンタ
        public static int i = 0;

        // ハッシュテーブル
        public static Hashtable ht = new Hashtable();

        /// <summary>コンストラクタ</summary>
        /// <param name="id">処理（スレッド）を識別するためのID</param>
        /// <param name="evt">同期用のオブジェクト（開始処理の調整）</param>
        public TestClass(int id, EventWaitHandle evt)
        {
            this._evt = evt;
            this.ID = id;
        }

        /// <summary>カウンタにマルチスレッドアクセス</summary>
        public void ThFunc_Counter()
        {
            // 待機（手動リセット）
            this._evt.WaitOne();

            for (int cnt = 0; cnt < 1000; cnt++)
            {
                // コンテキストスイッチを発生
                // させることでエラー発生頻度を高める。
                Thread.Sleep(10); // 一桁だとエラー発生頻度はあまり高くならない。

                int a, b;

                // 以下のロックコードブロックをコメントアウトして実行するとエラーが出る。
                // ロックコードブロックを有効にして、クリティカルセクションとすると、エラーは出ない。

                //lock (_lock)
                //{
                    a = i;

                    // 更新処理注意
                    i++;
                    
                    b = i;
                //}

                // 検証処理
                // 「マルチスレッドアクセスの場合、
                // カウンタが壊れてエラーになる」
                if ((b - a) == 1)
                {
                    Console.WriteLine("INFO:" + ID.ToString() + ":" + a.ToString() + "→" + b.ToString());
                }
                else
                {
                    Console.WriteLine("ERR:" + ID.ToString() + ":" + a.ToString() + "→" + b.ToString());
                    throw new Exception("ERR:" + ID.ToString() + ":" + a.ToString() + "→" + b.ToString());
                }
            }
        }

        /// <summary>ハッシュテーブルにマルチスレッドアクセス</summary>
        public void ThFunc_Hash() 
        {
            // 待機（手動リセット）
            this._evt.WaitOne();

            for (int cnt = 0; cnt < 1000; cnt++)
            {
                // コンテキストスイッチを発生
                // させることでエラー発生頻度を高める。
                Thread.Sleep(10); // 一桁だとエラー発生頻度はあまり高くならない。

                // マルチスレッド環境下での
                // ハッシュテーブルの読み書きの動作検証
                string str = this.ID + " - " + cnt.ToString();

                // 以下のロックコードブロックをコメントアウトして実行するとエラーが出る。
                // ロックコードブロックを有効にして、クリティカルセクションとすると、エラーは出ない。

                //lock (_lock)
                //{
                    // 更新処理注意
                    ht.Add(str, str);
                //}

                // 検証処理
                // 「マルチスレッドアクセスの場合、
                // ハッシュテーブルが壊れてエラーになる」
                if (str == (string)ht[str])
                {
                    Console.WriteLine("INFO:" + "[" + str + "] Is OK");
                }
                else
                {
                    // さらにチェック
                    if (null == (string)ht[str])
                    {
                        Console.WriteLine("ERR:" + "[" + str + "] Is Null");
                        throw new Exception("ERR:" + "[" + str + "] Is Null");
                    }
                    else
                    {
                        Console.WriteLine("ERR:" + "[" + str + "] Is Different");
                        throw new Exception("ERR:" + "[" + str + "] Is Different");
                    }
                }   
            }
        }
    }
}
