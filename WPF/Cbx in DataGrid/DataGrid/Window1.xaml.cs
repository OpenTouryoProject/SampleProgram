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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Data;
using System.Collections.ObjectModel;

namespace DataGrid
{
    /// <summary>
    /// Window1.xaml の相互作用ロジック
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            // リソースに定義したコンボ ボックスの初期化データをロード
            // Loadedイベントでは間に合わないので、コンストラクタで実行。
            this.LoadCbxData();
        }

        /// <summary>コンボボックスの初期化データをロード</summary>
        private void LoadCbxData()
        {
            DataTable dt = null;
            DataRow dr = null;

            // CbxData1（性別：Gender）
            dt = new DataTable();
            dt.Columns.Add(new DataColumn("value", typeof(Int32)));
            dt.Columns.Add(new DataColumn("display"));

            dr = dt.NewRow();
            dr["value"] = 1;
            dr["display"] = "男";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["value"] = 2;
            dr["display"] = "女";
            dt.Rows.Add(dr);

            // ここまでの変更をコミット
            dt.AcceptChanges();

            // Application.Current.Properties経由でコンストラクタに渡す
            Application.Current.Properties["CbxData1"] = dt;

            // CbxData2（出身：Homeland）
            dt = new DataTable();
            dt.Columns.Add(new DataColumn("value", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("display"));

            dr = dt.NewRow();
            dr["value"] = 1;
            dr["display"] = "日本";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["value"] = 2;
            dr["display"] = "米国";
            dt.Rows.Add(dr);

            // ここまでの変更をコミット
            dt.AcceptChanges();

            // Application.Current.Properties経由でコンストラクタに渡す
            Application.Current.Properties["CbxData2"] = dt;
        }

        /// <summary>Loadedイベント</summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // GridView用データをロードする

            DataTable dt = null;
            DataRow dr = null;

            dt = new DataTable();
            dt.Columns.Add(new DataColumn("ID", typeof(Int32)));
            dt.Columns.Add(new DataColumn("Last"));
            dt.Columns.Add(new DataColumn("First"));
            dt.Columns.Add(new DataColumn("Gender", typeof(Int32)));
            dt.Columns.Add(new DataColumn("Homeland", typeof(Int32)));

            #region サザエ

            dr = dt.NewRow();
            dr["ID"] = 1;
            dr["Last"] = "磯野";
            dr["First"] = "波平";
            dr["Gender"] = 1;
            dr["Homeland"] = 1;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["ID"] = 2;
            dr["Last"] = "磯野";
            dr["First"] = "フネ";
            dr["Gender"] = 2;
            dr["Homeland"] = 1;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["ID"] = 3;
            dr["Last"] = "フグ田";
            dr["First"] = "サザエ";
            dr["Gender"] = 2;
            dr["Homeland"] = 1;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["ID"] = 4;
            dr["Last"] = "磯野";
            dr["First"] = "カツオ";
            dr["Gender"] = 1;
            dr["Homeland"] = 1;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["ID"] = 5;
            dr["Last"] = "磯野";
            dr["First"] = "ワカメ";
            dr["Gender"] = 2;
            dr["Homeland"] = 1;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["ID"] = 6;
            dr["Last"] = "フグ田";
            dr["First"] = "マスオ";
            dr["Gender"] = 1;
            dr["Homeland"] = 1;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["ID"] = 6;
            dr["Last"] = "フグ田";
            dr["First"] = "タラオ";
            dr["Gender"] = 1;
            dr["Homeland"] = 1;
            dt.Rows.Add(dr);

            #endregion

            #region シンプソン

            dr = dt.NewRow();
            dr["ID"] = 1;
            dr["Last"] = "Simpson";
            dr["First"] = "Homer";
            dr["Gender"] = 1;
            dr["Homeland"] = 2;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["ID"] = 2;
            dr["Last"] = "Simpson";
            dr["First"] = "Marge";
            dr["Gender"] = 2;
            dr["Homeland"] = 2;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["ID"] = 3;
            dr["Last"] = "Simpson";
            dr["First"] = "Bart";
            dr["Gender"] = 1;
            dr["Homeland"] = 2;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["ID"] = 4;
            dr["Last"] = "Simpson";
            dr["First"] = "Lisa";
            dr["Gender"] = 2;
            dr["Homeland"] = 2;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["ID"] = 5;
            dr["Last"] = "Simpson";
            dr["First"] = "Maggie";
            dr["Gender"] = 2;
            dr["Homeland"] = 2;
            dt.Rows.Add(dr);

            #endregion

            // ここまでの変更をコミット
            dt.AcceptChanges();

            this.dataGrid1.DataContext = dt;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = (DataTable)this.dataGrid1.DataContext;
            foreach (DataRow dr in dt.Rows)
            {
                // 行の状態を確認する。
                System.Diagnostics.Debug.WriteLine(dr.RowState.ToString());
            }
            System.Diagnostics.Debug.WriteLine("---");
        }
    }

    /// <summary>
    /// CbxData1（性別：Gender）
    /// StaticResource参照するためコンボ毎のリソースとして定義
    /// なお、StaticResource参照のため、変更通知は不要
    /// </summary>
    public class CbxData1 : GpCbxCollectionData
    {
        public CbxData1() : base("CbxData1") { }
    }

    /// <summary>
    /// CbxData2（出身：Homeland）
    /// StaticResource参照するためコンボ毎のリソースとして定義
    /// なお、StaticResource参照のため、変更通知は不要
    /// </summary>
    public class CbxData2 : GpCbxCollectionData
    {
        public CbxData2() : base("CbxData2") { }
    }
}