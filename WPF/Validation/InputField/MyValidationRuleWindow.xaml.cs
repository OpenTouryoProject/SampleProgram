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
    /// MyValidationRuleWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MyValidationRuleWindow : Window
    {
        public MyValidationRuleWindow()
        {
            InitializeComponent();

            // ソースクラスをバインド
            this.DataContext = new SourceClass();
        }

        /// <summary>
        /// Validation.Errorに設定するエラーハンドラ
        /// </summary>
        private void textBox2_Error(object sender, ValidationErrorEventArgs e)
        {
            MessageBox.Show("エラー");
        }
    }
}
