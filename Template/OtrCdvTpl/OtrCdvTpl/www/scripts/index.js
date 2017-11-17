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

var SignInEndPoint = "http://10.0.2.2:81/HOME/OAuth2Starter?state=";
var SignUpEndPoint = "http://10.0.2.2/MultiPurposeAuthSite/Account/Register";
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
        //alert('開発中');
        localStorage.removeItem('state');
        localStorage.removeItem('token');

        // state
        //alert('state');
        var state = localStorage.getItem('state');

        // stateが・・・
        if (state)
        {
            // ある場合
        }
        else
        {
            // ない場合
            state = GetState(12);
            //alert("state: " + state);
            localStorage.setItem('state', state);
        }

        // Starterの初期化
        document.getElementById('signin').href = SignInEndPoint + state;
        document.getElementById('signup').href = SignUpEndPoint;

        // token
        //alert('token');
        var token = localStorage.getItem('token');

        // tokenが・・・
        if (token)
        {
            alert('token is not null');
            alert('token: ' + token);

            // ある場合、/userinfoにリクエスト。
            CallOAuthAPI(UserInfoEndPoint, token, 'get', null); // → SignIn()
        }
        else
        {
            //alert('token is null');

            // ない場合、
            SignOut();
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

        var payload = DecBase64(token.split('.')[1]);
        //alert("payload: " + payload);

        payload = JSON.parse(payload);
        var state = localStorage.getItem('state');

        //alert('nonce: ' + payload.nonce);
        //alert('state: ' + state);

        if (payload.nonce == state) {
            // 一致する。
            CallOAuthAPI(UserInfoEndPoint, token, 'get', null);
        }
        else {
            // 一致しない。
            SignOut();
        }

    }, 0);
}

// ---------------------------------------------------------------
// /userinfoにリクエスト
// ---------------------------------------------------------------
// 引数    url, token, httpMethod, postdata
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
                //alert('sub is ' + userinfo.sub);
                SignIn(token, userinfo.sub);
            }
            else {
                //alert('sub is null');
                SignOut();
            }
        },
        error: function (responseData, textStatus, errorThrown) {
            //alert(textStatus + ', ' + errorThrown.message);
            SignOut();
        }
    });
}

// ---------------------------------------------------------------
// SignIn
// ---------------------------------------------------------------
// 引数    －
// 戻り値  －
// ---------------------------------------------------------------
function SignIn(token, sub) {
    // tokenを保持
    localStorage.setItem("token", token);

    // サインイン、サインアップボタンを非表示
    document.getElementById('signin').style.visibility = "hidden";
    document.getElementById('signup').style.visibility = "hidden";
    document.getElementById('signout').style.visibility = "visible";
    document.getElementById('sub').style.visibility = "visible";
    document.getElementById('sub').innerHTML = sub;
}

// ---------------------------------------------------------------
// SignOut
// ---------------------------------------------------------------
// 引数    －
// 戻り値  －
// ---------------------------------------------------------------
function SignOut() {
    // tokenを破棄
    localStorage.removeItem('token');

    // stateの事前更新・保存
    var state = GetState(12);
    localStorage.setItem('state', state);
    document.getElementById('signin').href = SignInEndPoint + state;

    // サインイン、サインアップボタンを表示
    document.getElementById('signin').style.visibility = "visible";
    document.getElementById('signup').style.visibility = "visible";
    document.getElementById('signout').style.visibility = "hidden";
    document.getElementById('sub').style.visibility = "hidden";
    document.getElementById('sub').innerHTML = "";
}

// ---------------------------------------------------------------
// stateを取得する。
// ---------------------------------------------------------------
// 引数    l(len)
// 戻り値  ランダム文字列
// ---------------------------------------------------------------
function GetState(l) {
    // https://qiita.com/ryounagaoka/items/4736c225bdd86a74d59c
    // 生成する文字列に含める文字セット
    var c = "abcdefghijklmnopqrstuvwxyz0123456789";
    var cl = c.length;
    var r = "";
    for (var i = 0; i < l; i++) {
        r += c[Math.floor(Math.random() * cl)];
    }
    return r;
}

// ---------------------------------------------------------------
// Base64/Base64Urlのどちらでもデコード
// https://www.g200kg.com/archives/2014/12/base64encdec.html
//   Licensed under WTFPLv2
// ---------------------------------------------------------------
// 引数    x : Base64/Base64Url String
// 戻り値  Decoded String
// ---------------------------------------------------------------
function DecBase64(x) {
    x = x.split("=")[0];
    var tab = "-_ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
    var r = [], d = 0, bits = 0, l = x.length;
    for (var i = 0; i < l; ++i) {
        d = (d << 6) + ((tab.indexOf(x[i]) - 2) & 0x3f);
        if ((bits += 6) >= 8)
            r.push((d >> (bits -= 8)) & 0xff);
    }

    return String.fromCharCode.apply(null, r);
}