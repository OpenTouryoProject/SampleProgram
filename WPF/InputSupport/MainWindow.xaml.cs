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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InputSupport
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // UIElementにRoutedCommandをバインドし、メニューに関連付ける。
            this.InitRoutedCommands_about();
        }

        // AboutCommand（カスタムのRoutedCommand）
        public static readonly RoutedCommand AboutCommand = new RoutedCommand();

        /// <summary>InitRoutedCommands_about</summary>
        private void InitRoutedCommands_about()
        {

            // CommandBindingの生成
            CommandBinding binding =
              new CommandBinding(AboutCommand, AboutCommand_Executed, AboutCommand_CanExecute);

            // UIElementにRoutedCommandをバインド
            this.CommandBindings.Add(binding);

            // メニューに関連付け
            About.Command = AboutCommand;
        }

        /// <summary>
        /// 【情報メニュー＋コマンド】UIElementにRoutedCommandをバインド：カスタム動作（実行可否）
        /// </summary>
        private void AboutCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        /// <summary>
        /// 【情報メニュー＋コマンド】UIElementにRoutedCommandをバインド：カスタム動作（実行可否）
        /// </summary>
        private void AboutCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // 情報表示処理
        }

        /// <summary>
        /// 【保存メニュー＋コマンド】UIElementにRoutedCommandをバインド：カスタム動作（実行可否）
        /// </summary>
        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        /// <summary>
        /// 【保存メニュー＋コマンド】UIElementにRoutedCommandをバインド：カスタム動作（イベント処理）
        /// </summary>
        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // 保存処理
        }

        /// <summary>
        /// 【終了メニュー＋コマンド】UIElementにRoutedCommandをバインド：カスタム動作（実行可否）
        /// </summary>
        private void CloseCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        /// <summary>
        /// 【終了メニュー＋コマンド】UIElementにRoutedCommandをバインド：カスタム動作（イベント処理）
        /// </summary>
        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 【終了メニュー】メニューのクリックイベントのみの実装で済ます場合。
        /// </summary>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
