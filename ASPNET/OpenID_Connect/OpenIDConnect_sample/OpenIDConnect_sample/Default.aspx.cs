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
// * Class name            : Default.aspx.cs
// * Class name class name :
// *
// * Author                : Sandeep
// * Class Japanese name   :
// * Change log
// * Date:       Author  :        Comments:
// * ----------  ---------------  -------------------------------------------------
// * 2016/02/11  Sandeep          Implemented code to redirect to Azure STS using OWIN middleware
//**********************************************************************************

// System
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// Microsoft OWIN
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.Owin.Security;

namespace OpenIDConnect_sample
{
    /// <summary>
    /// Default class
    /// </summary>
    public partial class Default : System.Web.UI.Page
    {
        /// <summary>
        /// Page_Load event
        /// </summary>
        /// <param name="sender">sender Variable</param>
        /// <param name="e">EventArgs Variable</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                // Send an OpenID Connect sign-in request.
                Context.GetOwinContext().Authentication.Challenge(new AuthenticationProperties { RedirectUri = "/" }, OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }
            else
            {
                // Assigning user Identity name to the label
                this.lblLoggedInUser.Text = Context.User.Identity.Name;

                // To do
                // ...
            }
        }
    }
}