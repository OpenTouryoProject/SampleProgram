using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Data;
using System.ComponentModel;

namespace WpfApplication1
{
    /// <summary>全チェック用バリデーション ルール</summary>
    public class ValidationRuleNothing : ValidationRule
    {
        public override ValidationResult Validate(object value,
          System.Globalization.CultureInfo cultureInfo)
        {
            foreach (object o in (((BindingGroup)(value))).Items)
            {
                System.Diagnostics.Debug.WriteLine(o.ToString());
            }

            return new ValidationResult(true, null);
        }
    }
}
