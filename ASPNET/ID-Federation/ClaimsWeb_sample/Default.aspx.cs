//**********************************************************************************
//* Copyright (C) 2007,2014 Hitachi Solutions,Ltd.
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
// * Class name: ProcessCopyJob
// *
// * Author: Sandeep
// * Update history:
// *
// * Date & time update's       content
// * ----------  -------------- --------------------- -------------------------------
// * 2015/04/16  Sandeep        Implemented code to access the claims and display it
//**********************************************************************************

// System
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;

// Microsoft
using Microsoft.IdentityModel.Claims;

namespace ClaimsWeb_sample
{
    /// <summary>Default class</summary>
    public partial class Default : System.Web.UI.Page
    {
        /// <summary>All claims</summary>
        private ArrayList alClaims = new ArrayList();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Gets the thread's current principal
            IClaimsPrincipal claimsPrincipal = Thread.CurrentPrincipal as IClaimsPrincipal;

            // Access the interface IClaimsIdentity, which contains claims
            IClaimsIdentity claimsIdentity = (IClaimsIdentity)claimsPrincipal.Identity;

            // Access claims that associated with claims identity object 
            foreach (Claim claim in claimsIdentity.Claims)
            {
                // Display claim type and value
                //Response.Write("Claim Type: " + claim.ClaimType + "</br>");
                //Response.Write("Claim Value: " + claim.Value + "</br>");

                // Get claims and store it in array list 
                switch (claim.ClaimType)
                {
                    case ClaimTypes.Email:
                    case ClaimTypes.Name:
                    case ClaimTypes.GivenName:
                    case ClaimTypes.Role:
                    case ClaimTypes.Prip.CommonName:
                        alClaims.Add(new ClaimInfo(claim.ClaimType, claim.Value));
                        break;
                }

                // TODO
                //...                 
            }
            this.Repeater1.DataSource = alClaims;
            this.Repeater1.DataBind();
        }
    }
}

# region ClaimInfo

/// <summary>Holds claim information</summary>
public class ClaimInfo
{
    /// <summary>key</summary>
    private string _key;

    /// <summary>value</summary>
    private string _value;

    /// <summary>claim information</summary>
    public ClaimInfo(string key, string value)
    {
        this._key = key;
        this._value = value;
    }

    /// <summary>key</summary>
    public string key
    {
        get
        {
            return _key;
        }
    }

    /// <summary>value</summary>
    public string value
    {
        get
        {
            return _value;
        }
    }
}

# endregion