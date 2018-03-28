//**********************************************************************************
//* Copyright (C) 2007,2016 Hitachi Solutions,Ltd.
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

//**********************************************************************************
//* クラス名        ：PerformanceRecorder
//* クラス日本語名  ：パフォーマンス測定クラス
//*
//* 作成者          ：生技 西野
//* 更新履歴        ：
//*
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  2007/xx/xx  西野 大介         新規作成
//*  2010/10/26  西野 大介         場所移動（Win32置き場新設）
//*  2018/03/28  西野 大介         .NET Standard対応で、高分解能PCからSystem.Diagnostics.Stopwatchへ。
//**********************************************************************************

using System;
using System.Diagnostics;

namespace Touryo.Infrastructure.Public.Util
{
    /// <summary>パフォーマンス測定クラス</summary>
    /// <remarks>
    /// 自由に利用できる。
    /// .NET Standard対応で、高分解能パフォーマンスカウンタを
    /// System.Diagnostics.Stopwatchに変更した。
    /// </remarks>
	public class PerformanceRecorder
    {
        #region 測定結果の保存用メンバ変数

        /// <summary>System.Diagnostics.Stopwatch</summary>
        private Stopwatch _stopwatch = null;

        /// <summary>
        /// 実行時間
        /// </summary>
        private string _ExecTime = "";

        /// <summary>
        /// CPU時間
        /// </summary>
        private string _CpuTime = "";

        /// <summary>
        /// CPUカーネル モード時間
        /// </summary>
        private string _CpuKernelTime = "";

        /// <summary>
        /// CPUユーザ モード時間
        /// </summary>
        private string _CpuUserTime = "";

        #endregion

        #region 測定開始メソッド

        /// <summary>測定開始メソッド</summary>
        /// <returns>処理が成功した場合：True、失敗した場合：False</returns>
        /// <remarks>自由に利用できる。</remarks>
        public bool StartsPerformanceRecord()
        {
            this._stopwatch = new Stopwatch();
            this._stopwatch.Start();

            return true;
        }

        #endregion

        #region 測定終了メソッド

        /// <summary>測定終了メソッド</summary>
        /// <returns>処理が成功した場合：結果文字列、失敗した場合：エラーメッセージ</returns>
        /// <remarks>自由に利用できる。</remarks>
        public string EndsPerformanceRecord()
        {
            this._stopwatch.Stop();

            this._ExecTime = this._stopwatch.ElapsedMilliseconds.ToString();

            return
                    "ExT:" + this._ExecTime + "[msec]" +
                    ", CT: - [msec]" +
                    ", KT: - [msec]" +
                    ", UT: - [msec]";
        }

        #endregion

        #region プロパティ

        /// <summary>実行時間（ミリ秒）</summary>
        /// <remarks>自由に利用できる。</remarks>
        public string ExecTime
        {
            get
            {
                return this._ExecTime;
            }
        }

        /// <summary>CPU時間（ミリ秒）</summary>
        /// <remarks>自由に利用できる。</remarks>
        public string CpuTime
        {
            get
            {
                return this._CpuTime;
            }
        }

        /// <summary>CPUカーネル モード時間（ミリ秒）</summary>
        /// <remarks>自由に利用できる。</remarks>
        public string CpuKernelTime
        {
            get
            {
                return this._CpuKernelTime;
            }
        }

        /// <summary>CPUユーザ モード時間（ミリ秒）</summary>
        /// <remarks>自由に利用できる。</remarks>
        public string CpuUserTime
        {
            get
            {
                return this._CpuUserTime;
            }
        }

        #endregion
    }
}
