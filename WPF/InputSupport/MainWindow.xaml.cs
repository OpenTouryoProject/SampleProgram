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

        // 【情報メニュー＋コマンド】UIElementにRoutedCommandをバインド：カスタム動作（実行可否）
        private void AboutCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        // 【情報メニュー＋コマンド】UIElementにRoutedCommandをバインド：カスタム動作（実行可否）
        private void AboutCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // 情報表示処理
        }

        // 【保存メニュー＋コマンド】UIElementにRoutedCommandをバインド：カスタム動作（実行可否）
        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        // 【保存メニュー＋コマンド】UIElementにRoutedCommandをバインド：カスタム動作（イベント処理）
        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // 保存処理
        }

        // 【終了メニュー＋コマンド】UIElementにRoutedCommandをバインド：カスタム動作（実行可否）
        private void CloseCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        // 【終了メニュー＋コマンド】UIElementにRoutedCommandをバインド：カスタム動作（イベント処理）
        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        // 【終了メニュー】メニューのクリックイベントのみの実装で済ます場合。
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
