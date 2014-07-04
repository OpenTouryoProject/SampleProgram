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

namespace Trigger
{
    /// <summary>ItemsControlWindow.xaml の相互作用ロジック</summary>
    public partial class ItemsControlWindow : Window
    {
        /// <summary>コンストラクタ</summary>
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

        /// <summary>button1_Click</summary>
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
