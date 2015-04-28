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
//* クラス名        ：LayerB
//*
//* 作成日時        ：－
//* 作成者          ：－
//* 更新履歴        ：－
//*
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  2015/04/26  Sandeep           Implemented CRUD operation of business layer 
//**********************************************************************************

// system
using System;
using System.IO;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// SPA_WebAPI_EF
using SPA_WebAPI_EF.Models;
using SPA_WebAPI_EF.Codes.Dao;
using SPA_WebAPI_EF.Codes.Dao.EntityFrameWork;

namespace SPA_WebAPI_EF.Codes.Business
{
    /// <summary>
    ///  LayerB class
    /// </summary>
    public class LayerB
    {
        #region Select

        #region SelectCount

        /// <summary>
        ///  Counts the total number of records from shippers table
        /// </summary>
        /// <param name="param">param</param>
        public void SelectCount(WebApiParams param)
        {
            DaoLayer daoObj = new DaoLayer();
            daoObj.SelectCount(param);
        }

        #endregion

        #region SelectAll_List

        /// <summary>
        ///  Gets the list of records from shipper table.
        /// </summary>
        /// <param name="param"></param>
        public void SelectAll_List(WebApiParams param)
        {
            DaoLayer daoObj = new DaoLayer();
            daoObj.SelectAll_List(param);
        }

        #endregion        

        #region SelectByCondition

        /// <summary>
        ///  Gets the shipper table based on input conditions
        /// </summary>
        /// <param name="param"></param>
        public void SelectByCondition(WebApiParams param)
        {
            DaoLayer daoObj = new DaoLayer();
            daoObj.SelectByCondition(param);
        }

        #endregion

        #endregion

        #region Insert

        /// <summary>
        ///  Inserts data to the shipper table
        /// </summary>
        /// <param name="param"></param>
        public void Insert(WebApiParams param)
        {
            DaoLayer daoObj = new DaoLayer();
            daoObj.Insert(param);
        }

        #endregion

        #region Update

        /// <summary>
        ///  Updates data to the shipper table
        /// </summary>
        /// <param name="param"></param>
        public void Update(WebApiParams param)
        {
            DaoLayer daoObj = new DaoLayer();
            daoObj.Update(param);
        }

        #endregion

        #region Delete

        /// <summary>
        ///  Deletes data from the shipper table based on shipper id
        /// </summary>
        /// <param name="param"></param>
        public void Delete(WebApiParams param)
        {
            DaoLayer daoObj = new DaoLayer();
            daoObj.Delete(param);
        }

        #endregion      
    }
}