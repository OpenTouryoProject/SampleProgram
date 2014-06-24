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
