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

namespace WpfApplication1
{
    /// <summary>
    /// NumericValidationRuleBgWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class NumericValidationRuleBgWindow : Window
    {
        public NumericValidationRuleBgWindow()
        {
            InitializeComponent();

            // ソースクラスをバインド
            this.DataContext = new SourceClass();
        }

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
