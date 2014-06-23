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
    /// DataErrorValidationRuleWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class DataErrorValidationRuleWindow : Window
    {
        public DataErrorValidationRuleWindow()
        {
            InitializeComponent();

            // ソースクラス（バリデーション付）をバインド
            this.DataContext = new SourceClassWithValid(-1, -1, -1);
        }
    }
}
