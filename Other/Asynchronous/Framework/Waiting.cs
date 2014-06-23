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
