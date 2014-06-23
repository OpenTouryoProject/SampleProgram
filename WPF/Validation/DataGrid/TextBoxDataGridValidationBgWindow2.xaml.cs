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

using System.Data;

namespace WpfApplication1
{
    /// <summary>
    /// TextBoxDataGridValidationBindingGroupWindow2.xaml の相互作用ロジック
    /// </summary>
    public partial class TextBoxDataGridValidationBindingGroupWindow2 : Window
    {
        public TextBoxDataGridValidationBindingGroupWindow2()
        {
            InitializeComponent();

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("count", typeof(Int32)));
            dt.Columns.Add(new DataColumn("name", typeof(System.String)));
            DataRow dr=null;
            
            dr = dt.NewRow();
            dr["count"] = 100;
            dr["name"] = "あああ";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["count"] = 200;
            dr["name"] = "いいい";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["count"] = 300;
            dr["name"] = "ううう";
            dt.Rows.Add(dr);

            // ここまでの変更をコミット
            dt.AcceptChanges();

            this.dataGrid1.DataContext = dt;

            // ソースクラスをバインド
            this.DataContext = new SourceClass();
        }

        /// <summary>実行ボタン</summary>
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (!this.stackPanel1.BindingGroup.ValidateWithoutUpdate())
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
