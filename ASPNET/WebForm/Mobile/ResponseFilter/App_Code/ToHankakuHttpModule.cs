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
/// ToHankakuFilterStream
/// （半角化フィルタ ストリーム）
/// の設定処理ハンドラ。
/// </summary>
/// <remarks>
/// 次のWebページを参考にして作成
/// 
/// チュートリアル : カスタム HTTP モジュールを作成および登録する
/// http://msdn.microsoft.com/ja-jp/library/0e7ykf56.aspx
/// 
/// IISのバージョンによって登録方法が異なるので注意が必要
/// </remarks>
public class ToHankakuHttpModule : IHttpModule
{
    /// <summary>Init（実装必須）</summary>
    /// <remarks>
    /// ・BeginRequest               ：要求処理の開始前に発生
    /// ・AuthenticateRequest        ：呼び出し元を認証
    /// ・AuthorizeRequest           ：アクセスチェックを実行
    /// ・ResolveRequestCache        ：キャッシュから応答を取得
    /// ・AcquireRequestState        ：セッション状態をロード
    /// ・PreRequestHandlerExecute   ：要求をハンドラオブジェクトに送信する直前に発生
    /// ・PostRequestHandlerExecute  ：要求をハンドラオブジェクトに送信した直後に発生
    /// ・ReleaseRequestState        ：セッション状態を保存
    /// ・UpdateRequestCache         ：応答キャッシュを更新
    /// ・EndRequest                 ：処理の終了後に発生
    /// ・PreSendRequestHeaders      ：バッファ内の応答ヘッダーを送信する前に発生
    /// ・PreSendRequestContent      ：バッファ内の応答本体を送信する前に発生
    /// </remarks>
    public void Init(HttpApplication application)
    {
        

        // ASP.NETイベント ハンドラを設定

        application.BeginRequest += (new EventHandler(this.Filter));

        //application.AuthenticateRequest += (new EventHandler(this.Filter));

        //application.AuthorizeRequest += (new EventHandler(this.Filter));

        //application.ResolveRequestCache += (new EventHandler(this.Filter));

        //application.AcquireRequestState += (new EventHandler(this.Filter));

        //application.PreRequestHandlerExecute += (new EventHandler(this.Filter));

        //application.PostRequestHandlerExecute += (new EventHandler(this.Filter));

        //application.ReleaseRequestState += (new EventHandler(this.Filter));

        //application.UpdateRequestCache += (new EventHandler(this.Filter));

        //application.EndRequest += (new EventHandler(this.Filter));

        //application.PreSendRequestHeaders += (new EventHandler(this.Filter));

        //application.PreSendRequestContent += (new EventHandler(this.Filter));
    }

    #region イベント ハンドラ

    /// <summary>イベント時、Filter処理を行う</summary>
    private void Filter(Object source, EventArgs e)
    {
        HttpContext context = ((HttpApplication)source).Context;

        // 【方法３】カスタム HTTP モジュールのASP.NETイベント ハンドラでフィルタを設定する。
        context.Response.Filter = new ToHankakuFilterStream(context.Response.Filter, Encoding.GetEncoding("utf-8"));
    }

    #endregion

    /// <summary>Dispose（実装必須）</summary>
    public void Dispose() { }
}
