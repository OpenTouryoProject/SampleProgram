
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    /// <summary>認証用サンプル アプリ・コントローラ</summary>
    public class HomeController : Controller
    {
        /// <summary>Index</summary>
        /// <returns>IActionResult</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>OAuth2Starter</summary>
        /// <param name="state">string</param>
        /// <param name="code_challenge">string</param>
        /// <returns>IActionResult</returns>
        public IActionResult OAuth2Starter(string state, string code_challenge)
        {
            return Redirect(
                "http://10.0.2.2/MultiPurposeAuthSite/Account/OAuthAuthorize"
                + "?client_id=40319c0100f94ff3aab3004c8bdb5e52&response_type=code"
                + "&scope=profile%20email%20phone%20address%20userid"
                + "&state=" + state
                + "&code_challenge=" + code_challenge
                + "&code_challenge_method=plain");

            //return View();
        }

        /// <summary>ConvertCodeToToken</summary>
        /// <param name="code">string</param>
        /// <param name="code_challenge">string</param>
        /// <returns>IActionResult</returns>
        public IActionResult ConvertCodeToToken(string code, string code_challenge)
        {
            // PKCEのアクセストークン・リクエスト

            return new JsonResult(new Dictionary<string, string>(){ { "token", "hogehoge" } });
        }

        /// <summary>Error</summary>
        /// <returns>IActionResult</returns>
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
