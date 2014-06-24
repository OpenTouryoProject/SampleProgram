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

namespace DataBinding.CalcBMI
{
    /// <summary>
    /// CalcBMIWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class CalcBMIWindow : Window
    {
        public CalcBMIWindow()
        {
            InitializeComponent();
        }

        /// <summary>初期化イベント</summary>
        private void MainPanel_Loaded(object sender, RoutedEventArgs e)
        {
            // バインド ソース（Person）
            MainPanel.DataContext = new Person();
        }

        /// <summary>計算イベント</summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((Person)MainPanel.DataContext).Calculate();
        }
    }
}
