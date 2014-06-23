﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Media;
using System.Windows.Controls;
using System.ComponentModel;

namespace WpfApplication1
{
    /// <summary>カスタム バリデーション</summary>
    public class NumericValidationRule : ValidationRule
    {
        /// <summary>最大値（メンバ）</summary>
        private int _max = 0;

        /// <summary>最大値（アクセッサ）</summary>
        public int Max
        {
            get { return this._max; }
            set { this._max = value; }
        }

        /// <summary>カスタム バリデーション メソッド</summary>
        /// <param name="value">入力値</param>
        /// <param name="cultureInfo">カルチャ</param>
        /// <returns>ValidationResult</returns>
        public override ValidationResult Validate(object value,
          System.Globalization.CultureInfo cultureInfo)
        {
            int intVal;

            if (int.TryParse(value.ToString(), out intVal))
            {
                if (intVal <= this._max)
                {
                    return new ValidationResult(true, null);
                }
                else
                {
                    return new ValidationResult(false, "最大値[" + this._max.ToString() + "]を超えた");
                }
            }
            else
            {
                return new ValidationResult(false, "数値でない。");
            }
        }
    }
}
