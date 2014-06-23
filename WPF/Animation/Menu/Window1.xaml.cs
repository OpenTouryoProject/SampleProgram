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

        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            stackPanel1.Height = 28;
            stackPanel2.Height = 28;
        }

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
