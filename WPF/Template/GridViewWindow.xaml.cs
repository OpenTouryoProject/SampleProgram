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

namespace Template
{
    /// <summary>
    /// GridViewWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class GridViewWindow : Window
    {
        public GridViewWindow()
        {
            InitializeComponent();

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Image"));
            dt.Columns.Add(new DataColumn("Text"));

            DataRow dr = null;
            
            dr = dt.NewRow();
            dr["Image"] = @".\jpg\Blue hills.jpg";
            dr["Text"] = "Blue hills";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Image"] = @".\jpg\Sunset.jpg";
            dr["Text"] = "Sunset";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Image"] = @".\jpg\Water lilies.jpg";
            dr["Text"] = "Water lilies";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Image"] = @".\jpg\Winter.jpg";
            dr["Text"] = "Winter";
            dt.Rows.Add(dr);

            this.stackPanel1.DataContext = dt;
        }
    }
}
