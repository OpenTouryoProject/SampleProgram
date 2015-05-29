//**********************************************************************************
//* Copyright (C) 2007,2015 Hitachi Solutions,Ltd.
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

// ************************************************ **********************************
// * Class name: MicrosoftTranslator_Translate.aspx.cs
// *
// * Author: Supragyan
// * Update history:
// *
// * Date and time update's content
// * ---------- ---------------- --------------------- -------------------------------
//*  2015/05/21  Supragyan       Created GetAccessToken method to get token based on clientId and clientSecret.
//**********************************************************************************
//system
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MicrosoftTranslator
{
    /// <summary>
    /// Translate class
    /// </summary>
    public partial class Translate : System.Web.UI.Page
    {       
        /// <summary>
        /// Gets AcessToken by clientId and client Secret.
        /// </summary>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static AdmAccessToken GetAccessToken()
        {
            //Get Client Id and Client Secret from https://datamarket.azure.com/developer/applications/
            AdmAuthentication admAuth = new AdmAuthentication("9a086c79-36a3-487a-9e87-8332ed05090d", "Tkpy6Rd682QCPaEvxKcDFe42ssMpMbL435yFJJkEjao=");

            //Gets acess token
            AdmAccessToken admToken = admAuth.GetAccessToken();

            return admToken;
        } 
    }
}