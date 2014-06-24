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
