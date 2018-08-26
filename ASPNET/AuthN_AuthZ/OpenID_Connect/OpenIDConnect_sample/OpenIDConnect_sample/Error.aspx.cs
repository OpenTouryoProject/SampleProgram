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
// * Class name            : Error.aspx.cs
// * Class name class name :
// *
// * Author                : Sandeep
// * Class Japanese name   :
// * Change log
// *  Date:       Author:        Comments:
// * ----------  --------------  -------------------------------------------------
// * 2016/02/11  Sandeep         Implemented code to display error message
//**********************************************************************************
// System
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OpenIDConnect_sample
{
    /// <summary>
    ///  Error class
    /// </summary>
    public partial class Error : System.Web.UI.Page
    {
        /// <summary>
        /// Page_Load event
        /// </summary>
        /// <param name="sender">sender Variable </param>
        /// <param name="e">EventArgs Variable</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // In page load assigning the Error message to the label.
                this.lblErrorMessage.Text = Request.QueryString["message"].ToString();
            }
            catch(Exception ex)
            {
                // Assigning exception message to the label
                this.lblErrorMessage.Text = ex.Message;
            }            
        }
    }
}