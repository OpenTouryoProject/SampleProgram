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
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Default1 : System.Web.UI.Page
{
    /// <summary>Page_Load</summary>
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// データ テーブルをグリッドにデータバインドしてViewStatewを生成する。
    /// </summary>
    protected void btnDataBind_Click(object sender, EventArgs e)
    {
        // データ テーブルの生成
        DataTable dt = new DataTable();

        dt.Columns.Add(new DataColumn("col1", Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("col2", Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("col3", Type.GetType("System.String")));

        DataRow dr = null;

        for (int i = 1; i < 4; i++)
        {
            dr = dt.NewRow();

            dr["col1"] = i.ToString() + "1：" + this.TextBox1.Text;
            dr["col2"] = i.ToString() + "2：" + this.TextBox1.Text;
            dr["col3"] = i.ToString() + "3：" + this.TextBox1.Text;

            dt.Rows.Add(dr);
        }

        // 生成したデータ テーブルをグリッドにデータバインド
        this.GridView1.DataSource = dt;
        this.GridView1.DataBind();
    }

    /// <summary>空のポストバックなので何もしない。</summary>
    protected void btnPostBack_Click(object sender, EventArgs e)
    {

    }

    /// <summary>次画面へ画面遷移する。</summary>
    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (ConfigurationManager.AppSettings["Transition"].ToUpper() == "T")
        {
            Server.Transfer("Default2.aspx");
        }
        else if (ConfigurationManager.AppSettings["Transition"].ToUpper() == "R")
        {
            Response.Redirect("Default2.aspx");
        }
        else
        {
            throw new Exception("「Transition」の設定値が不正です。");
        }
    }
}
