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
using System.Threading;

namespace Framework
{
    /// <summary>Thread関数付クラス</summary>
    public class Waiting
    {
        /// <summary>SetResultのDelegate型</summary>
        public delegate void SetResultDelegate(string result);

        /// <summary>各画面を更新するDelegate</summary>
        public SetResultDelegate SetResult { get; set; }

        // メンバ変数は、Threadに渡すために利用
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public int SleepTime { get; set; }

        /// <summary>Thread関数</summary>
        public void Callback_MultiThread()
        {
            this.StartTime = DateTime.Now;

            Thread.Sleep(this.SleepTime * 1000);

            this.FinishTime = DateTime.Now;

            // 結果を返す。
            this.SetResult(string.Format("{0}～{1} = {2}秒待機",
                this.StartTime.ToString("HH:mm:ss"),
                this.FinishTime.ToString("HH:mm:ss"),
                this.SleepTime));
        }

        /// <summary>Thread関数</summary>
        public void Callback_UseThreadPool(object stateInfo)
        {
            this.StartTime = DateTime.Now;

            Thread.Sleep(this.SleepTime * 1000);

            this.FinishTime = DateTime.Now;

            // 結果を返す。
            this.SetResult(string.Format("{0}～{1} = {2}秒待機",
                this.StartTime.ToString("HH:mm:ss"),
                this.FinishTime.ToString("HH:mm:ss"),
                this.SleepTime));
        }
    }
}
