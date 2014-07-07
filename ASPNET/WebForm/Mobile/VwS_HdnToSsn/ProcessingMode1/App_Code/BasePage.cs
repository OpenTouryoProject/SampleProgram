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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using System.Collections;
using System.Configuration;

/// <summary>
/// BasePage の概要の説明です
/// </summary>
public class BasePage : System.Web.UI.Page 
{
    // 画面を一意に識別するためのGUID
    private string Guid = "";

    // ID管理機能を持つキュー（CustQueue）
    private CustQueue _CustQueue = null;

    /// <summary>コンストラクタ</summary>
	public BasePage()
	{
        // Page_Loadイベントハンドラを追加する。
        this.Load += new EventHandler(this.Page_Load);
    }

    /// <summary>初回ロード時のページロードでHiddenにGUIDを裁判する</summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // CustQueueを初期化する。
        this._CustQueue = new CustQueue("VwS_HdnToSsn");

        if (this.IsPostBack)
        {
            // ポストバック

            // ここで、HiddenFieldからGUIDを取得しようとしたが、
            // LoadPageStateFromPersistenceMediumイベントが先に動く。
        }

        // ViewState保存用のGUIDを生成
        Guid guid = System.Guid.NewGuid();
        this.Guid = guid.ToString();

        // HiddenFieldにViewState保存用のGUIDを保存する。
        // HiddenFieldはMasterPageに置いてある為このコードとなる。
        // （マスタ ページのネストなどに対応する場合は少々複雑になる。）
        ((HiddenField)this.Master.FindControl("hdnViewStateGuid")).Value = this.Guid;

        // CustQueueにIDを追加する。
        Hashtable delIds = this._CustQueue.EnQandDeQ(this.Guid);

        // 削除されたIDをSessionから消去する。
        foreach (string delID in delIds.Keys)
        {
            Session.Remove("VIEWSTATE:" + delID);
        }
    }

    /// <summary>
    /// LoadPageStateFromPersistenceMediumのオーバーライドは、
    /// ViewStateデータをロードする処理をカスタムできる。
    /// </summary>
    protected override object LoadPageStateFromPersistenceMedium()
    {
        if (ConfigurationManager.AppSettings["VSMode"].ToUpper() == "S")
        {
            // SessionからViewStateデータをロードする。

            // この時点で、まだ、ASP.NET HiddenFieldが初期化されて
            // いないのでViewState保存用のGUIDを取得できない。

            // 故に、Request.Formから直接取得する
            // （マスタページ使用時のIDに注意）。
            this.Guid = Request.Form["ctl00$hdnViewStateGuid"];

            // Sessionのキーを生成する。
            string key = "VIEWSTATE:" + this.Guid;

            // Session値を返す。
            return Session[key];
        }
        else if (ConfigurationManager.AppSettings["VSMode"].ToUpper() == "H")
        {
            // HiddenからViewStateデータをロードする。

            // baseのLoadPageStateFromPersistenceMediumを呼ぶ。
            // （ = 標準のViewState処理、Hiddenから値をロードする処理を行う。）
            return base.LoadPageStateFromPersistenceMedium();
        }
        else
        {
            throw new Exception("「VSMode」の設定値が不正です。");
        }
    }

    /// <summary>
    /// SavePageStateToPersistenceMediumのオーバーライドは、
    /// ViewStateのデータをセーブする処理をカスタムできる。
    /// </summary>
    protected override void SavePageStateToPersistenceMedium(object viewState)
    {
        if (ConfigurationManager.AppSettings["VSMode"].ToUpper() == "S")
        {
            // SessionにViewStateデータをセーブする。

            // Sessionのキーを生成
            string key = "VIEWSTATE:" + this.Guid;
            Session[key] = viewState;
        }
        else if (ConfigurationManager.AppSettings["VSMode"].ToUpper() == "H")
        {
            // HiddenにViewStateデータをセーブする。

            // baseのLoadPageStateFromPersistenceMediumを呼ぶ。
            // （ = 標準のViewState処理、HiddenにViewStateデータをセーブする処理を行う。）
            base.SavePageStateToPersistenceMedium(viewState);
        }
        else
        {
            throw new Exception("「VSMode」の設定値が不正です。");
        }
    }
}
