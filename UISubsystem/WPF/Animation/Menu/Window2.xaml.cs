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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Windows.Media.Animation;

namespace WpfApplication1
{
    /// <summary>
    /// Window2.xaml の相互作用ロジック
    /// </summary>
    public partial class Window2 : Window
    {
        /// <summary>定数</summary>
        private double SpHeight1Ini = 28.0;
        /// <summary></summary>
        private double SpHeight2Ini = 28.0;
        /// <summary></summary>
        private double SpHeight1To = 150.0;
        /// <summary></summary>
        private double SpHeight2To = 150.0;
        /// <summary></summary>
        private Duration D = new Duration(new TimeSpan(0, 0, 0, 0, 300));

        /// <summary>コンストラクタ</summary>
        public Window2()
        {
            InitializeComponent();
        }
        
        /// <summary>Window_Loaded</summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            stackPanel1.Height = this.SpHeight1Ini;
            stackPanel2.Height = this.SpHeight2Ini;
        }

        /// <summary>フラグ</summary>
        private bool flg = false;

        /// <summary>アニメーションを切り替え</summary>
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (this.flg)
            {
                this.SpHeight1To = 150;
                this.SpHeight2To = 150;
                this.D = new Duration(new TimeSpan(0, 0, 0, 0, 300));
            }
            else
            {
                this.SpHeight1To = 130;
                this.SpHeight2To = 130;
                this.D = new Duration(new TimeSpan(0, 0, 0, 1));
            }

            this.flg = !this.flg;
        }

        /// <summary>ReturnDoubleAnimation</summary>
        /// <param name="spHeightFrom">DoubleAnimation.From</param>
        /// <param name="spHeightTo">DoubleAnimation.To</param>
        /// <returns>DoubleAnimation</returns>
        private DoubleAnimation ReturnDoubleAnimation(double spHeightFrom, double spHeightTo)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = spHeightFrom;
            da.To = spHeightTo;
            da.Duration = this.D;
            
            return da;
        }

        #region BeginAnimation

        /// <summary>stackPanel1_MouseLeftButtonDown</summary>
        private void stackPanel1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.stackPanel1.Height == this.SpHeight1Ini)
            {
                this.stackPanel1.BeginAnimation(
                    FrameworkElement.HeightProperty,
                    this.ReturnDoubleAnimation(this.SpHeight1Ini, this.SpHeight1To));
            }
        }

        /// <summary>stackPanel1_MouseRightButtonDown</summary>
        private void stackPanel1_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {   
            this.stackPanel1.BeginAnimation(
                    FrameworkElement.HeightProperty, null);

            this.stackPanel1.Height = this.SpHeight1Ini;
        }

        /// <summary>stackPanel2_MouseLeftButtonDown</summary>
        private void stackPanel2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.stackPanel2.Height == this.SpHeight2Ini)
            {
                this.stackPanel2.BeginAnimation(
                    FrameworkElement.HeightProperty,
                    this.ReturnDoubleAnimation(this.SpHeight2Ini, this.SpHeight2To));
            }
        }

        /// <summary>stackPanel2_MouseRightButtonDown</summary>
        private void stackPanel2_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.stackPanel2.BeginAnimation(
                FrameworkElement.HeightProperty, null);

            this.stackPanel2.Height = this.SpHeight2Ini;
        }
        
        #endregion

    }
}
