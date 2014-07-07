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

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Web.UI.Adapters;

/// <summary>
/// CustomPageAdapter の概要の説明です
/// </summary>
public class CustomPageAdapter : PageAdapter
{
    /// <summary>PageStatePersisterを選択して返す。</summary>
    /// <returns>PageStatePersister</returns>
    public override PageStatePersister GetStatePersister()
    {
        if (ConfigurationManager.AppSettings["VSMode"].ToUpper() == "H")
        {
            // HIDDENを保存先とする場合

            // HiddenFieldPageStatePersisterを返す。
            return new HiddenFieldPageStatePersister(this.Page);
        }
        else if (ConfigurationManager.AppSettings["VSMode"].ToUpper() == "S")
        {
            // SESSIONを保存先とする場合

            // SessionPageStatePersisterを返す。
            return new SessionPageStatePersister(this.Page);
        }
        else
        {
            throw new Exception("「VSMode」の設定値が不正です。");
        }
    }
}