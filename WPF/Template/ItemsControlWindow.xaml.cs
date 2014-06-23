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

namespace Template
{
    /// <summary>
    /// ItemsControlWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class ItemsControlWindow : Window
    {
        public ItemsControlWindow()
        {
            InitializeComponent();

            List<Dictionary<string, string>> lst
              = new List<Dictionary<string, string>>();

            Dictionary<string, string> dic = null;

            dic = new Dictionary<string, string>();
            dic["Image"] = @".\jpg\Blue hills.jpg";
            dic["Text"] = "Blue hills";
            lst.Add(dic);

            dic = new Dictionary<string, string>();
            dic["Image"] = @".\jpg\Sunset.jpg";
            dic["Text"] = "Sunset";
            lst.Add(dic);

            dic = new Dictionary<string, string>();
            dic["Image"] = @".\jpg\Water lilies.jpg";
            dic["Text"] = "Water lilies";
            lst.Add(dic);

            dic = new Dictionary<string, string>();
            dic["Image"] = @".\jpg\Winter.jpg";
            dic["Text"] = "Winter";
            lst.Add(dic);

            this.stackPanel1.DataContext = lst;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> dic = (Dictionary<string, string>)this.ListBox1.SelectedValue;

            if (dic != null)
            {
                MessageBox.Show(
                    "Text\t: " + (string)dic["Text"] + "\r\n"
                    +  "Image\t: " + (string)dic["Image"],
                    "選択", MessageBoxButton.OK);
            }
        }
    }
}
