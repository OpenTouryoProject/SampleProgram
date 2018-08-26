//**********************************************************************************
//* Copyright (C) 2007,2014 Hitachi Solutions,Ltd.
//**********************************************************************************

#region Apache License

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
//**********************************************************************************
// * Class name            : Startup.Auth.cs
// * Class name class name :
// *
// * Author                : Sandeep
// * Class Japanese name   :
// * Change log
// * Date:       Author:          Comments:
// * ----------  ---------------  -------------------------------------------------
// * 2016/02/11  Sandeep          Implemented OWIN middleware funtionality to sign-in
// *                              users from a single Azure AD tenant
//**********************************************************************************

// System
using System;
using System.Configuration;
using System.Globalization;
using System.Threading.Tasks;

// Microsoft OWIN
using Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;

namespace OpenIDConnect_sample
{
    /// <summary>
    /// Startup Class
    /// </summary>
    public partial class Startup {

        //  ClientID is used by the application to uniquely identify itself to Azure AD.
        private static string clientId = ConfigurationManager.AppSettings["FxClientId"];

        // The aadInstance is a variable to get the microsoft online login url.
        private static string aadInstance = ConfigurationManager.AppSettings["FxAzureADInstance"];

        // Tenant Name
        private static string tenant = ConfigurationManager.AppSettings["FxTenant"];

        // The postLogoutRedirectUri is to get localhost url.
        private static string postLogoutRedirectUri = ConfigurationManager.AppSettings["FxPostLogoutRedirectUri"];

        string authority = String.Format(CultureInfo.InvariantCulture, aadInstance, tenant);

        /// <summary>
        /// ConfigureAuth Methode
        /// </summary>
        /// <param name="app">Owin.IAppBuilder Interface </param>
        public void ConfigureAuth(IAppBuilder app)
        {
            // Sets the sign-in authentication type
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            
            // Sets the authentication cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            // Downloads the Azure AD metadata, finds the signing keys, and finds the issuer name for the tenant
            // Processes OpenID Connect sign-in responses by validating the signature and issuer in an incoming JWT
            // Extracts the user's claims, and putting them on ClaimsPrincipal.Current
            // Integrats with the session cookie ASP.Net OWIN middleware to establish a session for the user
            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = clientId,
                    Authority = authority,
                    PostLogoutRedirectUri = postLogoutRedirectUri,
                    Notifications = new OpenIdConnectAuthenticationNotifications
                    {
                        AuthenticationFailed = context =>
                        {
                            context.HandleResponse();
                            context.Response.Redirect("/Error.aspx?message=" + context.Exception.Message);
                            return Task.FromResult(0);
                        }
                    }
                });
        }
    }
}
