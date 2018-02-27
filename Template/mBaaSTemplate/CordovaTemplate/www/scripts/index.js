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

// Visual Studio Tools for Apache Cordovaにおける開発方法につては、次のドキュメントを参照してください:
// http://go.microsoft.com/fwlink/?LinkID=397704
// ページ上のコードをデバッグするには、
// 1. Ripple で読み込むか、Android デバイス/エミュレーターで読み込みます。
// 2. アプリを起動し、ブレークポイントを設定します。
// 3. 次に、JavaScript コンソールで "window.location.reload()" を実行します。

var SignInEndPoint = "http://10.0.2.2:81/HOME/OAuth2Starter";
var SignUpEndPoint = "http://10.0.2.2/MultiPurposeAuthSite/Account/Register";
var ConvertCodeToTokenEndPoint = "http://10.0.2.2:81/HOME/ConvertCodeToToken";
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
    document.addEventListener('deviceready', onDeviceReady.bind(this), false);

    // ---------------------------------------------------------------
    // devicereadyイベントのハンドラ
    // ---------------------------------------------------------------
    function onDeviceReady() {
        // Cordova の一時停止を処理し、イベントを再開します。

        MyAlert("onDeviceReady 1");

        // pauseイベントのハンドラ登録
        document.addEventListener('pause', onPause.bind(this), false);
        // resumeイベントのハンドラ登録
        document.addEventListener('resume', onResume.bind(this), false);

        MyAlert("onDeviceReady 2");

        // Cordova の読込が完了するので、
        // 以降のステップで、Deviceプラグインのテストなどが行える。
        // alert(device.platform);
  
        // 初期化
        InitCordova();

        MyAlert("onDeviceReady 3");

        InitStatus();

        MyAlert("onDeviceReady 4");

        // tokenチェック
        var token = localStorage.getItem('token');

        // tokenが、
        if (token) {
            MyAlert('onDeviceReady token is not null');
            MyAlert('onDeviceReady token: ' + token);

            // ある場合、/userinfoにリクエストして、サインイン
            CallUserInfo(token); // → SignIn()

            MyAlert("onDeviceReady 5");
        }
        else {
            MyAlert('onDeviceReady token is null');

            // ない場合、サインアウト
            SignOut();

            MyAlert("onDeviceReady 6");
        }
    }

    // ---------------------------------------------------------------
    // pauseイベントのハンドラ
    // ---------------------------------------------------------------
    function onPause() {
        // TODO: このアプリケーションは中断されました。ここで、アプリケーションの状態を保存します。
    }

    // ---------------------------------------------------------------
    // resumeイベントのハンドラ
    // ---------------------------------------------------------------
    function onResume() {
        // TODO: このアプリケーションが再アクティブ化されました。ここで、アプリケーションの状態を復元します。
    }

})();

// ---------------------------------------------------------------
// MyAlert
// ---------------------------------------------------------------
function MyAlert(msg) {

    if (msg.indexOf('onDeviceReady') !== -1) {
    }
    else if (msg.indexOf('handleOpenURL') !== -1) {
    }
    else if (msg.indexOf('CallConvertCodeToToken') !== -1) {
    }
    else if (msg.indexOf('CallUserInfo') !== -1) {
    }
    else {
        alert(msg);
    }

    //alert(msg);
}

// ---------------------------------------------------------------
// Cordovaの初期化
// ---------------------------------------------------------------
// 引数    －
// 戻り値  －
// ---------------------------------------------------------------
function InitCordova() {
    // ここで、Cordova を必要とする初期化を実行します。
    var parentElement = document.getElementById('deviceready');
    var listeningElement = parentElement.querySelector('.listening');
    var receivedElement = parentElement.querySelector('.received');
    listeningElement.setAttribute('style', 'display:none;');
    receivedElement.setAttribute('style', 'display:block;');
}

// ---------------------------------------------------------------
// 状態の初期化
// ---------------------------------------------------------------
// 引数    －
// 戻り値  －
// ---------------------------------------------------------------
function InitStatus()
{
    localStorage.removeItem('state');
    localStorage.removeItem('code_verifier');

    // state
    var state = GetRandomString(12);
    localStorage.setItem('state', state);

    // code_verifier
    var code_verifier = GetRandomString(64);
    localStorage.setItem('code_verifier', code_verifier);

    // Starterの初期化
    document.getElementById('signin').href = SignInEndPoint
        + "?state=" + state
        + "&code_verifier=" + code_verifier;

    document.getElementById('signup').href = SignUpEndPoint;
}

// ---------------------------------------------------------------
// cordova-plugin-customurlschemeのコールバック
// ---------------------------------------------------------------
// 引数    url
// 戻り値  －
// ---------------------------------------------------------------
function handleOpenURL(url) {

    setTimeout(function () {

        MyAlert("handleOpenURL 1");

        MyAlert("handleOpenURL received url: " + url);

        // 逆だとNG
        var state = url.substring(
            url.indexOf('&state=') + '&state='.length);
        MyAlert("handleOpenURL state: " + state);

        var code = url.substring(
            url.indexOf('?code=') + '?code='.length, url.indexOf('&state='));
        MyAlert("handleOpenURL code: " + code);

        var code_verifier = localStorage.getItem('code_verifier');
        MyAlert("handleOpenURL code_verifier: " + code_verifier);

        MyAlert("handleOpenURL 2");

        if (localStorage.getItem('state') === state) {
            // 一致する。
            CallConvertCodeToToken(code, code_verifier);

            MyAlert("handleOpenURL 3");
        }
        else {
            // 一致しない。
            MyAlert("handleOpenURL 4");
        }
    }, 0);
}

// ---------------------------------------------------------------
// /ConvertCodeToTokenにリクエスト
// ---------------------------------------------------------------
// 引数    code, code_verifier
// 戻り値  －
// ---------------------------------------------------------------
function CallConvertCodeToToken(code, code_verifier) {

    $.ajax({
        type: 'post',
        url: ConvertCodeToTokenEndPoint,
        crossDomain: true,
        headers: null,
        data: {
            "code": code,
            "code_verifier": code_verifier
        },
        xhrFields: {
            withCredentials: true
        },
        success: function (responseData, textStatus, jqXHR) {
            
            var token = responseData.token;

            MyAlert("CallConvertCodeToToken token: " + token);

            CallUserInfo(token);
        },
        error: function (responseData, textStatus, errorThrown) {
            MyAlert("CallConvertCodeToToken " + textStatus + ', ' + errorThrown.message);
        }
    });
}

// ---------------------------------------------------------------
// /userinfoにリクエスト
// ---------------------------------------------------------------
// 引数    token
// 戻り値  －
// ---------------------------------------------------------------
function CallUserInfo(token) {

    $.ajax({
        type: 'get',
        url: UserInfoEndPoint,
        crossDomain: true,
        headers: {
            'Authorization': 'Bearer ' + token
        },
        data: null,
        xhrFields: {
            withCredentials: true
        },
        success: function (responseData, textStatus, jqXHR) {

            var userinfo = responseData;

            MyAlert("CallUserInfo userinfo: " + JSON.stringify(userinfo));

            if (userinfo.sub) {
                MyAlert("CallUserInfo sub: " + userinfo.sub);
                SignIn(token, userinfo.sub);
            }
            else {
                MyAlert("CallUserInfo sub is null");
                SignOut();
            }
        },
        error: function (responseData, textStatus, errorThrown) {
            MyAlert("CallUserInfo " + textStatus + ', ' + errorThrown.message);
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
    // 初期化
    InitStatus();
    
    // サインイン、サインアップボタンを表示
    document.getElementById('signin').style.visibility = "visible";
    document.getElementById('signup').style.visibility = "visible";
    document.getElementById('signout').style.visibility = "hidden";
    document.getElementById('sub').style.visibility = "hidden";
    document.getElementById('sub').innerHTML = "";
}

// ---------------------------------------------------------------
// ランダム文字列を取得する。
// ---------------------------------------------------------------
// 引数    l(len)
// 戻り値  ランダム文字列
// ---------------------------------------------------------------
function GetRandomString(l) {
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

// ↓ 要らなくなったけど、取っておく予定。
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