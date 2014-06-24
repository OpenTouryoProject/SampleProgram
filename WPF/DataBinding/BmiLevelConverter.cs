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
