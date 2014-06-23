using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Data;

namespace DataBinding.Slider
{
    public class ValConverter : IValueConverter
    {
        #region IValueConverter メンバ

        /// <summary>変換メソッド（ソースからターゲット）</summary>
        public object Convert(object value, Type targetType,
          object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string)
            {
                if (string.IsNullOrEmpty((string)value))
                {
                    return 0;
                }
            }

            return double.Parse(value.ToString()) / 100;
        }

        /// <summary>変換メソッド（ターゲットからソース）</summary>
        public object ConvertBack(object value, Type targetType,
          object parameter, System.Globalization.CultureInfo culture)
        {
            return ((double)value) * 100;
        }

        #endregion
    }
}
