using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Media;
using System.Windows.Controls;
using System.ComponentModel;

namespace WpfApplication1
{
    /// <summary>カスタム バリデーション</summary>
    public class LengthValidationRule : ValidationRule
    {
        /// <summary>最大値（メンバ）</summary>
        private int _len = 0;

        /// <summary>最大値（アクセッサ）</summary>
        public int Length
        {
            get { return this._len; }
            set { this._len = value; }
        }

        /// <summary>カスタム バリデーション メソッド</summary>
        /// <param name="value">入力値</param>
        /// <param name="cultureInfo">カルチャ</param>
        /// <returns>ValidationResult</returns>
        public override ValidationResult Validate(object value,
          System.Globalization.CultureInfo cultureInfo)
        {
            if (value.ToString().Length == 0 || value.ToString().Length == this._len)
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "長さが一致しない");
            }
        }
    }
}
