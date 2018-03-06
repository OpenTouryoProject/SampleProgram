
//**********************************************************************************
//* サンプル アプリ・コントローラ
//**********************************************************************************

// テスト用クラスなので、必要に応じて流用 or 削除して下さい。

//**********************************************************************************
//* クラス名        ：HomeController
//* クラス日本語名  ：認証用サンプル アプリ・コントローラ
//*
//* 作成日時        ：－
//* 作成者          ：生技
//* 更新履歴        ：
//*
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  20xx/xx/xx  ＸＸ ＸＸ         ＸＸＸＸ
//**********************************************************************************

using CordovaBackend.Models;

using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;

using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

namespace CordovaBackend.Controllers
{
    /// <summary>認証用サンプル アプリ・コントローラ</summary>
    public class HomeController : Controller
    {
        /// <summary>OAuthHttpClient</summary>
        private static HttpClient OAuthHttpClient = new HttpClient();

        /// <summary>ClientId</summary>
        public string ClientId = "40319c0100f94ff3aab3004c8bdb5e52";

        /// <summary>ClientSecret</summary>
        public string ClientSecret = "m7VUuKLCK1nODl3xrLSoitw1x8N7sike9d5cXWa9_lg";

        /// <summary>RedirectUri</summary>
        public string RedirectUri = "CordovaTemplate://";
        
        /// <summary>Index</summary>
        /// <returns>IActionResult</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>OAuth2Starter</summary>
        /// <param name="state">string</param>
        /// <param name="code_verifier">string</param>
        /// <returns>IActionResult</returns>
        public IActionResult OAuth2Starter(string state, string code_verifier)
        {
            string code_challenge = code_verifier;

            return Redirect(
                "http://10.0.2.2/MultiPurposeAuthSite/Account/OAuthAuthorize"
                + "?client_id=" + ClientId
                + "&response_type=code"
                + "&scope=profile%20email%20phone%20address%20userid"
                + "&state=" + state
                + "&code_challenge=" + code_challenge
                + "&code_challenge_method=plain");

            //return View();
        }

        /// <summary>ConvertCodeToToken</summary>
        /// <param name="code">string</param>
        /// <param name="code_verifier">string</param>
        /// <returns>IActionResult</returns>
        public async Task<IActionResult> ConvertCodeToToken(string code, string code_verifier)
        {
            try
            {
                // PKCEのアクセストークン・リクエスト（素描）

                // Cordova経由にならないので、ここでは、10.0.2.2ではなく、localhostを使用。
                string tokenEndpointUri = "http://localhost/MultiPurposeAuthSite/OAuthBearerToken";

                // 通信用の変数
                HttpRequestMessage httpRequestMessage = null;
                HttpResponseMessage httpResponseMessage = null;

                // HttpRequestMessage (Method & RequestUri)
                httpRequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(tokenEndpointUri),
                };

                // HttpRequestMessage (Headers & Content)
                httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(
                        Encoding.GetEncoding(20127).GetBytes(
                            string.Format("{0}:{1}", this.ClientId, this.ClientSecret))));

                // OAuth PKCEのアクセストークン・リクエスト
                httpRequestMessage.Content = new FormUrlEncodedContent(
                    new Dictionary<string, string>
                    {
                        { "grant_type", "authorization_code" },
                        { "code", code },
                        { "code_verifier", code_verifier },
                        { "redirect_uri", HttpUtility.HtmlEncode(this.RedirectUri) },
                    });

                // HttpResponseMessage
                httpResponseMessage = await OAuthHttpClient.SendAsync(httpRequestMessage);

                string response = await httpResponseMessage.Content.ReadAsStringAsync();
                Dictionary<string, string> dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);

                return new JsonResult(new Dictionary<string, string>() { { "token", dic["access_token"] } });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Dictionary<string, string>() { { "token", ex.ToString() } });
            }
        }

        /// <summary>Error</summary>
        /// <returns>IActionResult</returns>
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
