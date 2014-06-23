using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Data;
using System.Windows.Media;

namespace DataBinding.CalcBMI
{
    /// <summary>BMIレベルに合った背景色を返す</summary>
    public class BmiLevelConverter : IValueConverter
    {
        #region IValueConverter メンバ

        /// <summary>変換メソッド（ソースからターゲット）</summary>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // BMIレベルに合った背景色を返す
            double target = (double)value;
            SolidColorBrush level;

            if (target < 18.5)
                level = new SolidColorBrush(Colors.Blue);
            else if (target > 25)
                level = new SolidColorBrush(Colors.Red);
            else
                level = new SolidColorBrush(Colors.White);

            return level;
        }

        /// <summary>変換メソッド（ターゲットからソース）</summary>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
