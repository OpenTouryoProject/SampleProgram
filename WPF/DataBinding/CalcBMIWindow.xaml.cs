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
