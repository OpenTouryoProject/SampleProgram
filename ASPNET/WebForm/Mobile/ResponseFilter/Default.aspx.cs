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
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Text;

/// <summary>
/// Response.Filterテスト画面
/// </summary>
public partial class _Default : System.Web.UI.Page 
{
    /// <summary>Page_Load</summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("Page_Load：ウウウ<br/>");
    }

    /// <summary>Button1_Click</summary>
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Write("Button1_Click：エエエ<br/>");
    }

    /// <summary>Button2_Clickでフィルタ</summary>
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Write("Button2_Click：オオオ<br/>");

        // 【方法１】コードビハインドでフィルタを設定する。
        Response.Filter = new ToHankakuFilterStream(Response.Filter, Encoding.GetEncoding("utf-8"));

        Response.Write("Button2_Click：カカカ<br/>");
    }

    /// <summary>Button3_Clickでフラッシュ＋フィルタ</summary>
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Write("Button3_Click：キキキ<br/>");

        // フィルタを設定する前にフラッシュする。
        Response.Flush();

        // 【方法１】コードビハインドでフィルタを設定する。
        Response.Filter = new ToHankakuFilterStream(Response.Filter, Encoding.GetEncoding("utf-8"));

        Response.Write("Button3_Click：ククク<br/>");
    }
}
