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
//* クラス名        ：SampleEFController
//*
//* 作成日時        ：－
//* 作成者          ：－
//* 更新履歴        ：－
//*
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  2015/04/25  Sandeep           Modified the code of SELECT/INSERT/UPDATE/DELETE controller
//**********************************************************************************

// system
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;


// SPA_WebAPI_EF
using SPA_WebAPI_EF.Codes.Business;
using SPA_WebAPI_EF.Codes.Dao.EntityFrameWork;
using SPA_WebAPI_EF.Models;
using System;

namespace SPA_WebAPI_EF.Controllers
{
    /// <summary>
    ///  SampleEFController controller class
    /// </summary>
    public class SampleEFController : Controller
    {
        // GET: /SampleEF/
        public ActionResult Index()
        {
            return View();
        }
    }

    #region API Controller class

    /// <summary>
    /// Count the number of records in Shippers table
    /// </summary>
    public class GetCountEFController : ApiController
    {
        // POST api/GetCount
        public HttpResponseMessage Post(WebApiParams param)
        {
            // 結果表示するメッセージ
            string message = "";
            Dictionary<string, string> dic = new Dictionary<string, string>();

            // Ｂ層呼出し＋都度コミット
            LayerB layerB = new LayerB();
            layerB.SelectCount(param);            

            // 結果（正常系）
            message = param.Obj.ToString() + "件のデータがあります";
            dic.Add("Message", message);

            return Request.CreateResponse(HttpStatusCode.OK, dic);
        }
    }

    /// <summary>
    /// Select all records in Shippers table and return it in DataTable
    /// </summary>
    public class SelectListEFController : ApiController
    {
        // POST api/SelectListEF
        public HttpResponseMessage Post(WebApiParams param)
        {
            // Ｂ層呼出し＋都度コミット
            LayerB layerB = new LayerB();
            layerB.SelectAll_List(param);

            // 結果（正常系）
            List<Shipper> shipperData = new List<Shipper>();
            shipperData = (List<Shipper>)param.Obj;
            return Request.CreateResponse(HttpStatusCode.OK, shipperData);            
        }
    }    

    /// <summary>
    /// Select records from Shippers table based on various condition
    /// </summary>
    public class SearchEFController : ApiController
    {
        // POST api/Select
        public HttpResponseMessage Post(WebApiParams param)
        {
            // Ｂ層呼出し＋都度コミット
            LayerB layerB = new LayerB();
            layerB.SelectByCondition(param);

            // 結果（正常系）
            List<Shipper> shipperData = new List<Shipper>();
            shipperData = (List<Shipper>)param.Obj;

            if (shipperData.Count == 0)
            {
                // 結果表示するメッセージ
                string message = "";
                Dictionary<string, string> dic = new Dictionary<string, string>();

                // 結果（正常系）
                message = "No records found";
                dic.Add("NoRecords", message);
                return Request.CreateResponse(HttpStatusCode.OK, dic);
            }
            else 
            {
                return Request.CreateResponse(HttpStatusCode.OK, shipperData);
            }
        }
    }

    /// <summary>
    /// Insert records in the Shippers table
    /// </summary>
    public class InsertEFController : ApiController
    {
        // POST api/Insert
        public HttpResponseMessage Post(WebApiParams param)
        {
            // 結果表示するメッセージ
            string message = "";
            Dictionary<string, string> dic = new Dictionary<string, string>();            

            // Ｂ層呼出し＋都度コミット
            LayerB layerB = new LayerB();
            layerB.Insert(param);

            // 結果（正常系）
            message = param.Obj.ToString() + "件追加";
            dic.Add("Message", message);

            return Request.CreateResponse(HttpStatusCode.OK, dic);
        }
    }

    /// <summary>
    /// Update records in Shippers table
    /// </summary>
    public class UpdateEFController : ApiController
    {
        // POST api/Update
        public HttpResponseMessage Post(WebApiParams param)
        {
           // 結果表示するメッセージ
            string message = "";
            Dictionary<string, string> dic = new Dictionary<string, string>();            

            // Ｂ層呼出し＋都度コミット
            LayerB layerB = new LayerB();
            layerB.Update(param);

            // 結果（正常系）
            message = param.Obj.ToString() + "件更新";
            dic.Add("Message", message);

            return Request.CreateResponse(HttpStatusCode.OK, dic);
        }
    }

    /// <summary>
    /// Delete records from Shippers table
    /// </summary>
    public class DeleteEFController : ApiController
    {
        // POST api/Delete
        public HttpResponseMessage Post(WebApiParams param)
        {
            // 結果表示するメッセージ
            string message = "";
            Dictionary<string, string> dic = new Dictionary<string, string>();            

            // Ｂ層呼出し＋都度コミット
            LayerB layerB = new LayerB();
            layerB.Delete(param);

            // 結果（正常系）
            message = param.Obj.ToString() + "件削除";
            dic.Add("Message", message);

            return Request.CreateResponse(HttpStatusCode.OK, dic);
        }
    }

    #endregion
}
