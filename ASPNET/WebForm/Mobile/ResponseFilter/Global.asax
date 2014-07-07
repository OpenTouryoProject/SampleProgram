<%@ Application Language="C#" %>

<script runat="server">

    /// <summary>Application_Start</summary>
    void Application_Start(object sender, EventArgs e) 
    {
        // アプリケーションのスタートアップで実行するコードです

    }
    /// <summary>Application_End</summary>
    void Application_End(object sender, EventArgs e) 
    {
        //  アプリケーションのシャットダウンで実行するコードです

    }
    /// <summary>Application_Error</summary>
    void Application_Error(object sender, EventArgs e) 
    { 
        // ハンドルされていないエラーが発生したときに実行するコードです

    }
    /// <summary>Session_Start</summary>
    void Session_Start(object sender, EventArgs e) 
    {
        // 新規セッションを開始したときに実行するコードです

    }
    /// <summary>Session_End</summary>
    void Session_End(object sender, EventArgs e) 
    {
        // セッションが終了したときに実行するコードです 
        // メモ: Web.config ファイル内で sessionstate モードが InProc に設定されているときのみ、
        // Session_End イベントが発生します。session モードが StateServer か、または SQLServer に 
        // 設定されている場合、イベントは発生しません。

    }

    /////////////////////////////////////////////////////////////////////////////////
    // ASP.NETパイプライン処理のイベント ハンドラ
    /////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////

    // Global.asaxが対応しているASP.NETパイプライン処理のイベント ハンドラの一覧
    // -----------------------------------------------------------------------------------
    // ① Application_OnBeginRequest                :リクエスト処理を開始する前に発生 
    // ② Application_OnAuthenticateRequest         :認証の直前に発生 
    // ③ Application_OnAuthorizeRequest            :認証が完了したタイミングで発生 
    // ④ Application_OnResolveRequestCache         :リクエストをキャッシングするタイミングで発生 
    // ⑤ Application_OnAcquireRequestState         :セッション状態などを取得するタイミングで発生 
    // ⑥ Application_OnPreRequestHandlerExecute    :ページの実行を開始する直前に発生 
    // ⑦ Application_OnPostRequestHandlerExecute   :ページの実行を完了した直後に発生 
    // ⑧ Application_OnReleaseRequestState         :すべての処理を完了したタイミングで発生 
    // ⑨ Application_OnUpdateRequestCache          :出力キャッシュを更新したタイミングで発生 
    // ⑩ Application_OnEndRequest                  :すべてのリクエスト処理が完了したタイミングで発生 
    // ⑪ Application_OnPreSendRequestHeaders       :ヘッダをクライアントに送信する直前に発生 
    // ⑫ Application_OnPreSendRequestContent       :コンテンツをクライアントに送信する直前に発生 

    // イベント・ハンドラはこの表の順番で呼び出される。

    // ただし、Application_OnPreSendRequestHeadersメソッドや
    // Application_OnPreSendRequestContentメソッドは
    // バッファ処理（HTTP応答バッファリング）が有効かどうかによって
    // 呼び出されるタイミングが異なるので注意すること。

    // バッファ処理が有効である場合には、上記表の順番で発生するが、
    // バッファ処理が無効である場合には最初のページ出力が開始される
    // 任意のタイミングで呼び出される。

    // なお、それぞれのイベント・ハンドラの名前から「Application_On」を
    // 取り除いた部分がGlobal.asaxで発生するイベントの名前である。
    // Global.asaxではイベント名に「Application_On」あるいは「Application_」を付けた
    // イベント・ハンドラが事前に定義されており、イベントの発生時に呼び出される。     

    /// <summary>Application_OnBeginRequest</summary>
    void Application_OnBeginRequest(object sender, EventArgs e)
    {
        // ① リクエスト処理を開始する前に発生

        //// 【方法２】Global.asaxのASP.NETイベント ハンドラでフィルタを設定する。
        //Response.Filter = new ToHankakuFilterStream(Response.Filter, Encoding.GetEncoding("utf-8"));
    }
    /// <summary>Application_OnAuthenticateRequest</summary>
    void Application_OnAuthenticateRequest(object sender, EventArgs e)
    {
        // ② 認証の直前に発生

        //// 【方法２】Global.asaxのASP.NETイベント ハンドラでフィルタを設定する。
        //Response.Filter = new ToHankakuFilterStream(Response.Filter, Encoding.GetEncoding("utf-8"));
    }
    /// <summary>Application_OnAuthorizeRequest</summary>
    void Application_OnAuthorizeRequest(object sender, EventArgs e)
    {
        // ③ 認証が完了したタイミングで発生

        //// 【方法２】Global.asaxのASP.NETイベント ハンドラでフィルタを設定する。
        //Response.Filter = new ToHankakuFilterStream(Response.Filter, Encoding.GetEncoding("utf-8"));
    }
    /// <summary>Application_OnResolveRequestCache</summary>
    void Application_OnResolveRequestCache(object sender, EventArgs e)
    {
        // ④ リクエストをキャッシングするタイミングで発生

        //// 【方法２】Global.asaxのASP.NETイベント ハンドラでフィルタを設定する。
        //Response.Filter = new ToHankakuFilterStream(Response.Filter, Encoding.GetEncoding("utf-8"));
    }
    /// <summary>Application_OnAcquireRequestState</summary>
    void Application_OnAcquireRequestState(object sender, EventArgs e)
    {
        // ⑤ セッション状態などを取得するタイミングで発生

        //// 【方法２】Global.asaxのASP.NETイベント ハンドラでフィルタを設定する。
        //Response.Filter = new ToHankakuFilterStream(Response.Filter, Encoding.GetEncoding("utf-8"));
    }
    /// <summary>Application_OnPreRequestHandlerExecute</summary>
    void Application_OnPreRequestHandlerExecute(object sender, EventArgs e)
    {
        // ⑥ ページの実行を開始する直前に発生

        //// 【方法２】Global.asaxのASP.NETイベント ハンドラでフィルタを設定する。
        //Response.Filter = new ToHankakuFilterStream(Response.Filter, Encoding.GetEncoding("utf-8"));
    }

    ///////////////////////////////////////////////////////////////////
    // ページの実行が⑥～⑦の間に入る。
    ///////////////////////////////////////////////////////////////////

    /// <summary>Application_OnPostRequestHandlerExecute</summary>
    void Application_OnPostRequestHandlerExecute(object sender, EventArgs e)
    {
        // ⑦ ページの実行を完了した直後に発生

        //// 【方法２】Global.asaxのASP.NETイベント ハンドラでフィルタを設定する。
        //Response.Filter = new ToHankakuFilterStream(Response.Filter, Encoding.GetEncoding("utf-8"));
    }
    /// <summary>Application_OnReleaseRequestState</summary>
    void Application_OnReleaseRequestState(object sender, EventArgs e)
    {
        // ⑧ すべての処理を完了したタイミングで発生

        //// 【方法２】Global.asaxのASP.NETイベント ハンドラでフィルタを設定する。
        //Response.Filter = new ToHankakuFilterStream(Response.Filter, Encoding.GetEncoding("utf-8"));
    }
    /// <summary>Application_OnUpdateRequestCache</summary>
    void Application_OnUpdateRequestCache(object sender, EventArgs e)
    {
        // ⑨ 出力キャッシュを更新したタイミングで発生

        //// 【方法２】Global.asaxのASP.NETイベント ハンドラでフィルタを設定する。
        //Response.Filter = new ToHankakuFilterStream(Response.Filter, Encoding.GetEncoding("utf-8"));
    }
    /// <summary>Application_OnEndRequest</summary>
    void Application_OnEndRequest(object sender, EventArgs e)
    {
        // ⑩ すべてのリクエスト処理が完了したタイミングで発生

        //// 【方法２】Global.asaxのASP.NETイベント ハンドラでフィルタを設定する。
        //Response.Filter = new ToHankakuFilterStream(Response.Filter, Encoding.GetEncoding("utf-8"));
    }
    /// <summary>Application_OnPreSendRequestHeaders</summary>
    void Application_OnPreSendRequestHeaders(object sender, EventArgs e)
    {
        // ⑪ ヘッダをクライアントに送信する直前に発生

        //// 【方法２】Global.asaxのASP.NETイベント ハンドラでフィルタを設定する。
        //Response.Filter = new ToHankakuFilterStream(Response.Filter, Encoding.GetEncoding("utf-8"));
    }
    /// <summary>Application_OnPreSendRequestContent</summary>
    void Application_OnPreSendRequestContent(object sender, EventArgs e)
    {
        // ⑫ コンテンツをクライアントに送信する直前に発生

        //// 【方法２】Global.asaxのASP.NETイベント ハンドラでフィルタを設定する。
        //Response.Filter = new ToHankakuFilterStream(Response.Filter, Encoding.GetEncoding("utf-8"));
    }    
</script>
