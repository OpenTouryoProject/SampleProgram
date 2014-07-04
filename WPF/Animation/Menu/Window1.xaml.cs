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
    /// Window1.xaml の相互作用ロジック
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }


        /// <summary>Window_Loaded</summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            stackPanel1.Height = 28;
            stackPanel2.Height = 28;
        }

        /// <summary>stackPanel_PreviewMouseLeftButtonDown</summary>
        private void stackPanel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // 縮んでいるとき（スタックパネルのHeightが28なら）
            // 拡張する（Heightを28-200までのアニメーションを実行）
            if (((StackPanel)(sender)).Height == 28) { }
            else
            {
                // ルーティング イベントを止める

                // イベントを止めないコントロールをここに記述する。
                if (e.Source.GetType() == typeof(Button)){ }
                else
                {
                    // ルーティング イベントを止める
                    e.Handled = true;
                }                
            }
        }
    }
}
