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
//* クラス名        ：DaoLayer
//*
//* 作成日時        ：－
//* 作成者          ：－
//* 更新履歴        ：－
//*
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  2015/03/15  Sandeep Nayak     Implemented code to perform INSERT/UPDATE/DELETE operation using Entity Framework
//*  2015/04/26  Sandeep           Implemented select operation based static or dynamic execution 
//*  2015/05/07  Sandeep           Implemented code of existence check for Update and Delete 
//*  2015/05/07  Sandeep           Implemented code sort the list based on the values of ddlOrderColumn and ddlOrderSequence
//**********************************************************************************

// system
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.EntityClient;
using System.Linq;
using System.Web;

// SPA_WebAPI_EF
using SPA_WebAPI_EF.Models;

namespace SPA_WebAPI_EF.Codes.Dao.EntityFrameWork
{
    /// <summary>
    /// DaoLayer class
    /// </summary>
    public partial class DaoLayer
    {
        #region Insert

        /// <summary>
        /// Insert record in Shippers table
        /// </summary>
        /// <param name="param">param</param>
        /// <param name="param">param</param>
        public void Insert(WebApiParams param)
        {
            using (NorthwindEntities context = new NorthwindEntities())
            {
                // Creating object to store the result set. 
                object dynObj;

                // Creating Shipper object.
                Shipper objShipper = new Shipper();

                // Set vlaues to the Shipper Entity.
                objShipper.CompanyName = param.CompanyName;
                objShipper.Phone = param.Phone;

                // Adds Shipper Entity to the context object.
                context.Shippers.Add(objShipper);

                // Saves all changes made in the context object to the database.
                dynObj = context.SaveChanges();

                param.Obj = dynObj;
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Update records in shippers table
        /// </summary>
        /// <param name="param">param</param>
        /// <param name="param">param</param>
        public void Update(WebApiParams param)
        {
            using (NorthwindEntities context = new NorthwindEntities())
            {                
                // Data existence check
                bool isExists = context.Shippers.Any(x => x.ShipperID == param.ShipperId);

                if (!isExists)
                {
                    // If data does not exists
                    // return 0
                    param.Obj = Convert.ToInt32(isExists);
                }
                else
                {
                    // If data exists

                    // Creating object to store the result set.
                    object dynObj;

                    // Creating Shipper object.
                    Shipper objShipper = new Shipper();

                    // Gets the Shipper record using Linq to Entities based on ShipperID
                    objShipper = context.Shippers.First(x => x.ShipperID == param.ShipperId);

                    // Set vlaues to the Shipper Entity.
                    if (!string.IsNullOrWhiteSpace(param.CompanyName))
                    {
                        objShipper.CompanyName = param.CompanyName;
                    }
                    if (!string.IsNullOrWhiteSpace(param.Phone))
                    {
                        objShipper.Phone = param.Phone;
                    }

                    // Saves all changes made in the context object to the database.      
                    dynObj = context.SaveChanges();

                    param.Obj = dynObj;
                }                
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete records in shippers table
        /// </summary>
        /// <param name="param">param</param>
        /// <param name="param">param</param>
        public void Delete(WebApiParams param)
        {
            using (NorthwindEntities context = new NorthwindEntities())
            {
                // Data existence check
                bool isExists = context.Shippers.Any(x => x.ShipperID == param.ShipperId);

                if (!isExists)
                {
                    // If data does not exists
                    // return 0
                    param.Obj = Convert.ToInt32(isExists);
                }
                else
                {
                    // If data exists

                    // Creating object to store the result set.
                    object dynObj;

                    // Creating Shipper object.
                    Shipper objShipper = new Shipper();

                    // Gets the Shipper record using Linq to Entities based on ShipperID
                    objShipper = context.Shippers.First(x => x.ShipperID == param.ShipperId);

                    // Deletes Shipper entity from the context object.
                    context.Shippers.Remove(objShipper);

                    // Saves all changes made in the context object to the database.
                    dynObj = context.SaveChanges();

                    param.Obj = dynObj;
                }
            }
        }

        #endregion

        #region Select

        /// <summary>
        /// Count the total number of records from shippers table
        /// </summary>
        /// <param name="param">param</param>
        public void SelectCount(WebApiParams param)
        {
            using (NorthwindEntities context = new NorthwindEntities())
            {
                switch (param.ddlMode2)
                {
                    // 静的SQL
                    case "static":
                        int total = (from m in context.Shippers select m).Count();
                        param.Obj = total;
                        break;

                    case "dynamic":
                        param.Obj = context.Shippers.Count();
                        break;
                }
            }
        }

        /// <summary>
        /// Select records from shippers table based on various conditions
        /// </summary>
        /// <param name="param"></param>
        public void SelectByCondition(WebApiParams param)
        {
            List<Shipper> shipperList = new List<Shipper>();

            // Get Shipper data from database based on condition.
            IQueryable<Shipper> parentQuery = null;
            using (NorthwindEntities context = new NorthwindEntities())
            {
                // Get Shipper data if ShipperID, CompanyName and Phone number are entered
                if (param.ShipperId != 0 && !string.IsNullOrEmpty(param.CompanyName) && !string.IsNullOrEmpty(param.Phone))
                {
                    parentQuery = context.Shippers;
                    parentQuery = parentQuery.Where(c => c.ShipperID == param.ShipperId && c.CompanyName == param.CompanyName && c.Phone == param.Phone).OrderByDescending(o => o.ShipperID);
                    var dynQuery = parentQuery.ToList();
                    foreach (var item in dynQuery)
                    {
                        Shipper shipperItem = new Shipper();
                        shipperItem.ShipperID = item.ShipperID;
                        shipperItem.CompanyName = item.CompanyName;
                        shipperItem.Phone = item.Phone;
                        shipperList.Add(shipperItem);
                    }
                }
                else
                {
                    if (param.ShipperId != 0)
                    {
                        parentQuery = context.Shippers;

                        // If only ShipperID is entered
                        if (string.IsNullOrEmpty(param.CompanyName) && string.IsNullOrEmpty(param.Phone))
                        {
                            parentQuery = parentQuery.Where(c => c.ShipperID == param.ShipperId);
                        }
                        // If only ShipperID and CompanyName are entered
                        else if (!string.IsNullOrEmpty(param.CompanyName) && string.IsNullOrEmpty(param.Phone))
                        {
                            parentQuery = parentQuery.Where(c => c.ShipperID == param.ShipperId && c.CompanyName == param.CompanyName);
                        }
                        // If only ShipperID and Phone number are entered
                        else if (string.IsNullOrEmpty(param.CompanyName) && !string.IsNullOrEmpty(param.Phone))
                        {
                            parentQuery = parentQuery.Where(c => c.ShipperID == param.ShipperId && c.Phone == param.Phone);
                        }

                        // Execute the query and Assign Shipper data to the data table
                        var dynQuery = parentQuery.ToList();
                        foreach (var item in dynQuery)
                        {
                            Shipper shipperItem = new Shipper();
                            shipperItem.ShipperID = item.ShipperID;
                            shipperItem.CompanyName = item.CompanyName;
                            shipperItem.Phone = item.Phone;
                            shipperList.Add(shipperItem);
                        }
                    }
                    else if (!string.IsNullOrEmpty(param.CompanyName))
                    {
                        parentQuery = context.Shippers;

                        // If CompanyName and Phone number are entered
                        if (param.Phone != null)
                        {
                            parentQuery = parentQuery.Where(c => c.CompanyName == param.CompanyName && c.Phone == param.Phone);
                        }
                        // If only CompanyName is entered
                        else
                        {
                            parentQuery = parentQuery.Where(c => c.CompanyName == param.CompanyName).OrderBy(o => o.ShipperID);
                        }

                        // Execute the query and Assign Shipper data to the data table
                        var dynQuery = parentQuery.ToList();
                        foreach (var item in dynQuery)
                        {
                            Shipper shipperItem = new Shipper();
                            shipperItem.ShipperID = item.ShipperID;
                            shipperItem.CompanyName = item.CompanyName;
                            shipperItem.Phone = item.Phone;
                            shipperList.Add(shipperItem);
                        }
                    }
                    // If only Phone number is entered.
                    else if (!string.IsNullOrEmpty(param.Phone))
                    {
                        parentQuery = context.Shippers;
                        parentQuery = parentQuery.Where(c => c.Phone == param.Phone).OrderBy(o => o.ShipperID);
                        var dynQuery = parentQuery.ToList();
                        foreach (var item in dynQuery)
                        {
                            Shipper shipperItem = new Shipper();
                            shipperItem.ShipperID = item.ShipperID;
                            shipperItem.CompanyName = item.CompanyName;
                            shipperItem.Phone = item.Phone;
                            shipperList.Add(shipperItem);
                        }
                    }
                }
            }
            param.Obj = shipperList;
        }

        /// <summary>
        /// SelectAll_List method
        /// </summary>
        /// <param name="param">param</param>
        /// <param name="param">param</param>
        public void SelectAll_List(WebApiParams param)
        {
            List<Shipper> shipperList = new List<Shipper>();
            using (NorthwindEntities context = new NorthwindEntities())
            {
                switch (param.ddlMode2)
                {                    
                    case "static":
                        // Execute static SQL

                        List<Shipper> staticQuery = null;
                        switch (param.ddlOrderColumn)
                        {
                            case "c1":
                                if (param.ddlOrderSequence == "A")
                                {
                                    staticQuery = (from shipper in context.Shippers
                                                   orderby shipper.ShipperID
                                                   select shipper).ToList();
                                }
                                else
                                {
                                    staticQuery = (from shipper in context.Shippers
                                                   orderby shipper.ShipperID descending
                                                   select shipper).ToList();
                                }
                                break;
                            case "c2":
                                if (param.ddlOrderSequence == "A")
                                {
                                    staticQuery = (from shipper in context.Shippers
                                                   orderby shipper.CompanyName
                                                   select shipper).ToList();
                                }
                                else
                                {
                                    staticQuery = (from shipper in context.Shippers
                                                   orderby shipper.CompanyName descending
                                                   select shipper).ToList();
                                }
                                break;
                            case "c3":
                                if (param.ddlOrderSequence == "A")
                                {
                                    staticQuery = (from shipper in context.Shippers
                                                   orderby shipper.Phone
                                                   select shipper).ToList();
                                }
                                else
                                {
                                    staticQuery = (from shipper in context.Shippers
                                                   orderby shipper.Phone descending
                                                   select shipper).ToList();
                                }
                                break;
                        }

                        foreach (var item in staticQuery)
                        {
                            Shipper shipperItem = new Shipper();
                            shipperItem.ShipperID = item.ShipperID;
                            shipperItem.CompanyName = item.CompanyName;
                            shipperItem.Phone = item.Phone;
                            shipperList.Add(shipperItem);
                        }
                        param.Obj = shipperList;
                        break;                       
                    case "dynamic":
                        // Execute dynamic SQL

                        IQueryable<Shipper> dynQuery = null;
                        switch (param.ddlOrderColumn)
                        {
                            case "c1":
                                if (param.ddlOrderSequence == "A")
                                {
                                    dynQuery = context.Shippers.OrderBy(o => o.ShipperID);
                                }
                                else
                                {
                                    dynQuery = context.Shippers.OrderByDescending(o => o.ShipperID);
                                }
                                break;
                            case "c2":
                                if (param.ddlOrderSequence == "A")
                                {
                                    dynQuery = context.Shippers.OrderBy(o => o.CompanyName);
                                }
                                else
                                {
                                    dynQuery = context.Shippers.OrderByDescending(o => o.CompanyName);
                                }
                                break;
                            case "c3":
                                if (param.ddlOrderSequence == "A")
                                {
                                    dynQuery = context.Shippers.OrderBy(o => o.Phone);
                                }
                                else
                                {
                                    dynQuery = context.Shippers.OrderByDescending(o => o.Phone);
                                }
                                break;
                        }
                        foreach (var item in dynQuery)
                        {
                            Shipper shipperItem = new Shipper();
                            shipperItem.ShipperID = item.ShipperID;
                            shipperItem.CompanyName = item.CompanyName;
                            shipperItem.Phone = item.Phone;
                            shipperList.Add(shipperItem);
                        }

                        param.Obj = shipperList;
                        break;
                }
            }
        }

        #endregion
    }
}