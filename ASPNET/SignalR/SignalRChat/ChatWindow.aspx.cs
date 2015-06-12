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
// * Class name: ChatWindow
// *
// * Author: Supragyan
// * Update history:
// *
// * Date and time update's content
// * ---------- ---------------- --------------------- -------------------------------
//*  2015/05/21  Supragyan       Created Signin,Signout method authenticate users. 
//**********************************************************************************
//system
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SignalRChat
{
    public partial class ChatWindow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.FindControl("chatWindow").Visible = false;
            }
        }

        /// <summary>
        /// To login to the chat screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (this.txtUserName.Value != "")
            {
                login.Attributes.Add("style", "display: none;");
                Page.FindControl("chatWindow").Visible = true;
                displayname.Value = txtUserName.Value;
            }
        }

        /// <summary>
        /// To logout the Chat.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            login.Attributes.Add("style", "display: inline;");
            Page.FindControl("chatWindow").Visible = false;
        }
    }
}