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

namespace InputField
{
    /// <summary>MyValidationRuleBgWindow.xaml の相互作用ロジック</summary>
    public partial class MyValidationRuleBgWindow : Window
    {
        /// <summary>コンストラクタ</summary>
        public MyValidationRuleBgWindow()
        {
            InitializeComponent();

            // ソースクラスをバインド
            this.DataContext = new SourceClass();
        }

        /// <summary>Validation.Errorに設定するエラーハンドラ</summary>
        private void textBox2_Error(object sender, ValidationErrorEventArgs e)
        {
            MessageBox.Show("エラー");
        }

        /// <summary>button1_Click</summary>
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (!this.stackPanel.BindingGroup.ValidateWithoutUpdate())
            {
                // 処理中止
                MessageBox.Show("処理中止");
            }
            else
            {
                // 処理続行
                MessageBox.Show("処理続行");
            }
        }
    }
}
