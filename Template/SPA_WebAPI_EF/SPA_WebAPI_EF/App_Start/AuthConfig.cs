//**********************************************************************************
//* クラス名        ：AuthConfig
//*
//* 作成日時        ：－
//* 作成者          ：－
//* 更新履歴        ：－
//*
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  20xx/xx/xx  ＸＸ ＸＸ         ＸＸＸＸ
//**********************************************************************************

// system
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using SPA_WebAPI_EF.Models;

namespace SPA_WebAPI_EF
{
    /// <summary>
    ///  AuthConfig Class
    /// </summary>
    public static class AuthConfig
    {
        /// <summary>
        ///  Register Authentication
        /// </summary>
        public static void RegisterAuth()
        {
            // このサイトのユーザーが、Microsoft、Facebook、および Twitter などの他のサイトのアカウントを使用してログインできるようにするには、
            // このサイトを更新する必要があります。詳細については、http://go.microsoft.com/fwlink/?LinkID=252166 を参照してください

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "",
            //    appSecret: "");

            //OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
