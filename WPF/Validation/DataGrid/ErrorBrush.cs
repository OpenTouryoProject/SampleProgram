using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Media;

namespace WpfApplication1
{
    /// <summary>
    /// エラーのSolidColorBrushの処理
    /// </summary>
    public class ErrorBrush
    {
        /// <summary>エラーのSolidColorBrushを生成し返す</summary>
        /// <returns>エラーのSolidColorBrush</returns>
        public static SolidColorBrush GetErrorSolidColorBrush()
        {
            return new SolidColorBrush(Colors.Red);
        }

        /// <summary>背景色から問題を確認</summary>
        /// <param name="b">Background（Brush）を想定</param>
        /// <returns>結果：true = 問題なし、false = 問題あり</returns>
        public static bool CheckErrorAtBgBrush(Brush b)
        {
            SolidColorBrush scb = new SolidColorBrush(Colors.Red);

            if (b.GetType() == scb.GetType())
            {
                if (((SolidColorBrush)b).Color == scb.Color) { return false; }
                else { return true; }
            }
            else { return true; }

        }
    }
}
