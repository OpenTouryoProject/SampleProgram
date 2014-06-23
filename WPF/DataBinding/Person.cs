using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;

namespace DataBinding.CalcBMI
{
    /// <summary>バインド ソース（Person）</summary>
    public class Person : DependencyObject
    {
        public double Height { get; set; }
        public double Weight { get; set; }

        /// <summary>Bmiプロパティを依存関係プロパティとして登録</summary>
        public static readonly DependencyProperty BmiProperty =
          DependencyProperty.Register("Bmi", typeof(double), typeof(Person),
              new UIPropertyMetadata(0.0));

        /// <summary>BmiプロパティのCLRプロパティ</summary>
        public double Bmi
        {
            get { return (double)this.GetValue(Person.BmiProperty); }
            set { this.SetValue(Person.BmiProperty, value); }
        }

        /// <summary>計算処理</summary>
        public void Calculate()
        {
            this.Bmi = Weight / Math.Pow(Height, 2);
        }
    }
}
