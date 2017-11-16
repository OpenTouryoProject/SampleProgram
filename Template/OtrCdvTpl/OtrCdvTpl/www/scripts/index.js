//**********************************************************************************
//* index.js
//**********************************************************************************

// 必要に応じて流用 or 削除して下さい。

//**********************************************************************************
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  2017/11/08  西野 大介         Cordova関連のJSコード
//*
//**********************************************************************************

// 空白のテンプレートの概要については、次のドキュメントを参照してください:
// http://go.microsoft.com/fwlink/?LinkID=397704
// ページ上のコードをデバッグするには、
// 1. Ripple で読み込むか、Android デバイス/エミュレーターで読み込みます。
// 2. アプリを起動し、ブレークポイントを設定します。
// 3. 次に、JavaScript コンソールで "window.location.reload()" を実行します。

var SignInEndPoint = "http://10.0.2.2:81/HOME/OAuth2Starter";
var UserInfoEndPoint = "http://10.0.2.2/MultiPurposeAuthSite/userinfo";

// ---------------------------------------------------------------
// Cordovaのテンプレート実装
// ---------------------------------------------------------------
// 引数    －
// 戻り値  －
// ---------------------------------------------------------------
(function () {
    "use strict";

    // devicereadyイベントのハンドラ登録
    document.addEventListener( 'deviceready', onDeviceReady.bind( this ), false );

    // devicereadyイベントのハンドラ
    function onDeviceReady() {
        // Cordova の一時停止を処理し、イベントを再開します

        // pauseイベントのハンドラ登録
        document.addEventListener('pause', onPause.bind(this), false);
        // resumeイベントのハンドラ登録
        document.addEventListener( 'resume', onResume.bind( this ), false );
        
        // TODO: Cordova が読み込まれました。
        // ここで、Cordova を必要とする初期化を実行します。
        var parentElement = document.getElementById('deviceready');
        var listeningElement = parentElement.querySelector('.listening');
        var receivedElement = parentElement.querySelector('.received');
        listeningElement.setAttribute('style', 'display:none;');
        receivedElement.setAttribute('style', 'display:block;');

        // 開発中
        //localStorage.removeItem('token');

        // 初期化
        document.getElementById('signin').href = SignInEndPoint;
        document.getElementById('signup').href = SignInEndPoint;

        // localStorageのtokenを確認する。
        var token = localStorage.getItem('token');

        if (token)
        {
            //alert('token is not null');
            //alert('token: ' + token);

            // tokenがある場合、/userinfoにリクエスト。
            CallOAuthAPI(UserInfoEndPoint, token, 'get', null);
        }
        else
        {
            //alert('token is null');

            // tokenがない場合、サインイン、サインアップボタンを表示
            document.getElementById('signin').style.visibility = "visible";
            document.getElementById('signup').style.visibility = "visible";
            document.getElementById('signout').style.visibility = "hidden";
        }
    }

    // pauseイベントのハンドラ
    function onPause() {
        // TODO: このアプリケーションは中断されました。ここで、アプリケーションの状態を保存します。
    }

    // resumeイベントのハンドラ
    function onResume() {
        // TODO: このアプリケーションが再アクティブ化されました。ここで、アプリケーションの状態を復元します。
    }

})();

/*
// ---------------------------------------------------------------
// cordova-plugin-inappbrowser適用後、
// 外部サイト（http://, https:// ）を内部（WebView）で開くための実装
// ---------------------------------------------------------------
// 引数    －
// 戻り値  －
// ---------------------------------------------------------------
function OpenInternally() {
    window.open(SignInEndPoint, '_self');
}
*/

// ---------------------------------------------------------------
// SignOut
// ---------------------------------------------------------------
// 引数    －
// 戻り値  －
// ---------------------------------------------------------------
function SignOut() {
    localStorage.removeItem('token');
    document.getElementById('signin').style.visibility = "visible";
    document.getElementById('signup').style.visibility = "visible";
    document.getElementById('signout').style.visibility = "hidden";
}
// ---------------------------------------------------------------
// cordova-plugin-customurlschemeのコールバック
// ---------------------------------------------------------------
// 引数    url
// 戻り値  －
// ---------------------------------------------------------------
function handleOpenURL(url) {
    setTimeout(function () {

        //alert("received url: " + url);
        var token = url.substring(url.indexOf('://') + 3);
        //alert("token: " + token);

        CallOAuthAPI(UserInfoEndPoint, token, 'get', null);
    }, 0);
}

// ---------------------------------------------------------------
// cordova-plugin-customurlschemeのコールバック
// ---------------------------------------------------------------
// 引数    url
// 戻り値  －
// ---------------------------------------------------------------
function CallOAuthAPI(url, token, httpMethod, postdata) {
    //alert(
    //    '<httpMethod>' + '\n' + httpMethod + '\n' +
    //    '<url>' + '\n' + url + '\n' +
    //    '<token>' + '\n' + token);

    $.ajax({
        type: httpMethod,
        url: url,
        crossDomain: true,
        headers: {
            'Authorization': 'Bearer ' + token
        },
        data: postdata,
        xhrFields: {
            withCredentials: true
        },
        success: function (responseData, textStatus, jqXHR) {
            //alert(textStatus + ', ' + JSON.stringify(responseData));

            var userinfo = responseData;
            //alert("userinfo: " + JSON.stringify(userinfo));

            if (userinfo.sub) {
                alert('sub is ' + userinfo.sub);
                localStorage.setItem("token", token);
                document.getElementById('signin').style.visibility = "hidden";
                document.getElementById('signup').style.visibility = "hidden";
                document.getElementById('signout').style.visibility = "visible";
            }
            else {
                //alert('sub is null');
                localStorage.removeItem('token');
                document.getElementById('signin').style.visibility = "visible";
                document.getElementById('signup').style.visibility = "visible";
                document.getElementById('signout').style.visibility = "hidden";
            }
        },
        error: function (responseData, textStatus, errorThrown) {
            alert(textStatus + ', ' + errorThrown.message);
        }
    });
}
