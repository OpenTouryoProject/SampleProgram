//**********************************************************************************
//* クラス名        ：NorthwindEntities
//*
//* 作成日時        ：－
//* 作成者          ：－
//* 更新履歴        ：－
//*
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  2015/03/15  Sandeep Nayak     Implemented code to perform INSERT/UPDATE/DELETE operation using Entity Framework 
//**********************************************************************************

// system
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.EntityClient;
using System.Linq;
using System.Web;

// SPA_WebAPI_EF
using SPA_WebAPI_EF.Codes.Common;

namespace SPA_WebAPI_EF.Codes.Dao.EntityFrameWork
{
    /// <summary>
    /// NorthwindEntities class
    /// </summary>
    public partial class NorthwindEntities
    {
        #region Insert

        /// <summary>
        /// Insert record in Shippers table
        /// </summary>
        /// <param name="testParameter">testParameter</param>
        /// <param name="testReturn">testReturn</param>
        public void Insert(TestParameterValue testParameter, TestReturnValue testReturn)
        {
            using (NorthwindEntities context = new NorthwindEntities())
            {
                // Creating object to store the result set. 
                object dynObj;

                // Creating Shipper object.
                Shipper objShipper = new Shipper();

                // Set vlaues to the Shipper Entity.
                objShipper.CompanyName = testParameter.CompanyName;
                objShipper.Phone = testParameter.Phone;

                // Adds Shipper Entity to the context object.
                context.Shippers.Add(objShipper);

                // Saves all changes made in the context object to the database.
                dynObj = context.SaveChanges();

                testReturn.Obj = dynObj;
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Update records in shippers table
        /// </summary>
        /// <param name="testParameter">testParameter</param>
        /// <param name="testReturn">testReturn</param>
        public void Update(TestParameterValue testParameter, TestReturnValue testReturn)
        {
            using (NorthwindEntities context = new NorthwindEntities())
            {
                // Creating object to store the result set.
                object dynObj;

                // Creating Shipper object.
                Shipper objShipper = new Shipper();

                // Gets the Shipper record using Linq to Entities based on ShipperID
                objShipper = context.Shippers.First(x => x.ShipperID == testParameter.ShipperID);

                // Set vlaues to the Shipper Entity.
                objShipper.CompanyName = testParameter.CompanyName;
                objShipper.Phone = testParameter.Phone;

                // Saves all changes made in the context object to the database.      
                dynObj = context.SaveChanges();

                testReturn.Obj = dynObj;
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete records in shippers table
        /// </summary>
        /// <param name="testParameter">testParameter</param>
        /// <param name="testReturn">testReturn</param>
        public void Delete(TestParameterValue testParameter, TestReturnValue testReturn)
        {
            using (NorthwindEntities context = new NorthwindEntities())
            {
                // Creating object to store the result set.
                object dynObj;

                // Creating Shipper object.
                Shipper objShipper = new Shipper();

                // Gets the Shipper record using Linq to Entities based on ShipperID
                objShipper = context.Shippers.First(x => x.ShipperID == testParameter.ShipperID);

                // Deletes Shipper entity from the context object.
                context.Shippers.Remove(objShipper);

                // Saves all changes made in the context object to the database.
                dynObj = context.SaveChanges();

                testReturn.Obj = dynObj;
            }
        }

        #endregion

        #region Select

        /// <summary>
        /// Count the total number of records from shippers table
        /// </summary>
        /// <param name="testParameter">testParameter</param>
        /// <param name="testReturn">testReturn</param>
        public void SelectCount(TestParameterValue testParameter, TestReturnValue testReturn)
        {
            using (NorthwindEntities context = new NorthwindEntities())
            {
                switch ((testParameter.ActionType.Split('%'))[2])
                {
                    // 静的SQL
                    case "static":
                        int total = (from m in context.Shippers select m).Count();
                        testReturn.Obj = total;
                        break;

                    case "dynamic":
                        testReturn.Obj = context.Shippers.Count();
                        break;
                }
            }
        }

        /// <summary>
        /// Select records from shippers table based on various conditions
        /// </summary>
        /// <param name="testParameter"></param>
        /// <param name="testReturn"></param>
        public void Select(TestParameterValue testParameter, TestReturnValue testReturn)
        {
            // Create Shipper data table using entity model
            DataTable dt = new DataTable();
            using (NorthwindEntities context = new NorthwindEntities())
            {
                var colNames = typeof(Shipper).GetProperties().Select(a => a.Name).ToList();
                for (int i = 0; i < colNames.Count; i++)
                {
                    DataColumn dc = new DataColumn(colNames[i]);
                    dt.Columns.Add(dc);
                }
            }

            // Get Shipper data from database based on condition.
            IQueryable<Shipper> parentQuery = null;
            using (NorthwindEntities context = new NorthwindEntities())
            {
                // Get Shipper data if ShipperID, CompanyName and Phone number are entered
                if (testParameter.ShipperID != 0 && !string.IsNullOrEmpty(testParameter.CompanyName) && !string.IsNullOrEmpty(testParameter.Phone))
                {
                    parentQuery = context.Shippers;
                    parentQuery = parentQuery.Where(c => c.ShipperID == testParameter.ShipperID && c.CompanyName == testParameter.CompanyName && c.Phone == testParameter.Phone).OrderByDescending(o => o.ShipperID);
                    var dynQuery = parentQuery.ToList();
                    foreach (var item in dynQuery)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = item.ShipperID;
                        dr[1] = item.CompanyName;
                        dr[2] = item.Phone;
                        dt.Rows.Add(dr);
                    }
                }
                else
                {
                    if (testParameter.ShipperID != 0)
                    {
                        parentQuery = context.Shippers;

                        // If only ShipperID is entered
                        if (string.IsNullOrEmpty(testParameter.CompanyName) && string.IsNullOrEmpty(testParameter.Phone))
                        {
                            parentQuery = parentQuery.Where(c => c.ShipperID == testParameter.ShipperID);
                        }
                        // If only ShipperID and CompanyName are entered
                        else if (!string.IsNullOrEmpty(testParameter.CompanyName) && string.IsNullOrEmpty(testParameter.Phone))
                        {
                            parentQuery = parentQuery.Where(c => c.ShipperID == testParameter.ShipperID && c.CompanyName == testParameter.CompanyName);
                        }
                        // If only ShipperID and Phone number are entered
                        else if (string.IsNullOrEmpty(testParameter.CompanyName) && !string.IsNullOrEmpty(testParameter.Phone))
                        {
                            parentQuery = parentQuery.Where(c => c.ShipperID == testParameter.ShipperID && c.Phone == testParameter.Phone);
                        }

                        // Execute the query and Assign Shipper data to the data table
                        var dynQuery = parentQuery.ToList();
                        foreach (var item in dynQuery)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = item.ShipperID;
                            dr[1] = item.CompanyName;
                            dr[2] = item.Phone;
                            dt.Rows.Add(dr);
                        }
                    }
                    else if (!string.IsNullOrEmpty(testParameter.CompanyName))
                    {
                        parentQuery = context.Shippers;

                        // If CompanyName and Phone number are entered
                        if (testParameter.Phone != null)
                        {
                            parentQuery = parentQuery.Where(c => c.CompanyName == testParameter.CompanyName && c.Phone == testParameter.Phone);
                        }
                        // If only CompanyName is entered
                        else
                        {
                            parentQuery = parentQuery.Where(c => c.CompanyName == testParameter.CompanyName).OrderBy(o => o.ShipperID);
                        }

                        // Execute the query and Assign Shipper data to the data table
                        var dynQuery = parentQuery.ToList();
                        foreach (var item in dynQuery)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = item.ShipperID;
                            dr[1] = item.CompanyName;
                            dr[2] = item.Phone;
                            dt.Rows.Add(dr);
                        }
                    }
                    // If only Phone number is entered.
                    else if (!string.IsNullOrEmpty(testParameter.Phone))
                    {
                        parentQuery = context.Shippers;
                        parentQuery = parentQuery.Where(c => c.Phone == testParameter.Phone).OrderBy(o => o.ShipperID);
                        var dynQuery = parentQuery.ToList();
                        foreach (var item in dynQuery)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = item.ShipperID;
                            dr[1] = item.CompanyName;
                            dr[2] = item.Phone;
                            dt.Rows.Add(dr);
                        }
                    }
                }
            }
            testReturn.Obj = dt;
        }

        /// <summary>
        /// SelectAll_DT method
        /// </summary>
        /// <param name="testParameter">testParameter</param>
        /// <param name="testReturn">testReturn</param>
        public void SelectAll_DT(TestParameterValue testParameter, TestReturnValue testReturn)
        {
            DataTable dt = new DataTable();
            using (NorthwindEntities context = new NorthwindEntities())
            {
                var colNames = typeof(Shipper).GetProperties().Select(a => a.Name).ToList();
                for (int i = 0; i < colNames.Count; i++)
                {
                    DataColumn dc = new DataColumn(colNames[i]);
                    dt.Columns.Add(dc);
                }
                switch ((testParameter.ActionType.Split('%'))[2])
                {
                    // 静的SQL
                    case "static":
                        // 静的SQL
                        var query = (from shipper in context.Shippers
                                     select new
                                     {
                                         ShipperID = shipper.ShipperID,
                                         CompanyName = shipper.CompanyName,
                                         Phone = shipper.Phone
                                     }).ToList();

                        foreach (var item in query)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = item.ShipperID;
                            dr[1] = item.CompanyName;
                            dr[2] = item.Phone;
                            dt.Rows.Add(dr);
                        }
                        testReturn.Obj = dt;
                        break;
                    case "dynamic":
                        var dynQuery = context.Shippers;
                        // 戻り値を設定
                        foreach (var item in dynQuery)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = item.ShipperID;
                            dr[1] = item.CompanyName;
                            dr[2] = item.Phone;
                            dt.Rows.Add(dr);
                        }
                        testReturn.Obj = dt;
                        break;
                }
            }
        }

        /// <summary>
        /// SelectAll_DS method
        /// </summary>
        /// <param name="testParameter">testParameter</param>
        /// <param name="testReturn">testReturn</param>
        public void SelectAll_DS(TestParameterValue testParameter, TestReturnValue testReturn)
        {
            DataSet ds = new DataSet();
            using (NorthwindEntities context = new NorthwindEntities())
            {
                DataTable dt = new DataTable();
                var colNames = typeof(Shipper).GetProperties().Select(a => a.Name).ToList();
                for (int i = 0; i < colNames.Count; i++)
                {
                    DataColumn dc = new DataColumn(colNames[i]);
                    dt.Columns.Add(dc);
                }
                switch ((testParameter.ActionType.Split('%'))[2])
                {
                    // 静的SQL
                    case "static":
                        var query = (from shipper in context.Shippers
                                     select new
                                     {
                                         ShipperID = shipper.ShipperID,
                                         CompanyName = shipper.CompanyName,
                                         Phone = shipper.Phone
                                     }).ToList();
                        foreach (var item in query)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = item.ShipperID;
                            dr[1] = item.CompanyName;
                            dr[2] = item.Phone;
                            dt.Rows.Add(dr);
                        }

                        ds.Tables.Add(dt);
                        // 戻り値を設定
                        testReturn.Obj = ds;
                        break;
                    case "dynamic":
                        var dynQuery = context.Shippers;
                        foreach (var item in dynQuery)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = item.ShipperID;
                            dr[1] = item.CompanyName;
                            dr[2] = item.Phone;
                            dt.Rows.Add(dr);
                        }
                        ds.Tables.Add(dt);
                        // 戻り値を設定
                        testReturn.Obj = ds;
                        break;
                }
            }
            // 戻り値を設定
            testReturn.Obj = ds;
        }

        /// <summary>
        /// SelectAll_DR method
        /// </summary>
        /// <param name="testParameter">testParameter</param>
        /// <param name="testReturn">testReturn</param>
        public void SelectAll_DR(TestParameterValue testParameter, TestReturnValue testReturn)
        {
            // But pure ADO.NET way is much easier and faster because this example still uses mapping of query to SQL query
            DataTable dt = new DataTable();

            using (NorthwindEntities context = new NorthwindEntities())
            {
                var colNames = typeof(Shipper).GetProperties().Select(a => a.Name).ToList();
                for (int i = 0; i < colNames.Count; i++)
                {
                    DataColumn dc = new DataColumn(colNames[i]);
                    dt.Columns.Add(dc);
                }
                using (var con = new EntityConnection("name=NorthwindEntities"))
                {
                    con.Open();
                    EntityCommand cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT VALUE st FROM NorthwindEntities.Shippers as st";
                    using (EntityDataReader rdr = cmd.ExecuteReader(CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection))
                    {
                        while (rdr.Read())
                        {
                            // DRから読む
                            object[] objArray = new object[3];
                            rdr.GetValues(objArray);

                            // DTに設定する。
                            DataRow dr = dt.NewRow();
                            dr.ItemArray = objArray;
                            dt.Rows.Add(dr);
                        }
                    }
                    testReturn.Obj = dt;
                }
            }
        }

        /// <summary>
        /// SelectAll_DSQL method
        /// </summary>
        /// <param name="testParameter">testParameter</param>
        /// <param name="testReturn">testReturn</param>
        public void SelectAll_DSQL(TestParameterValue testParameter, TestReturnValue testReturn)
        {
            DataTable dt = new DataTable();
            using (NorthwindEntities context = new NorthwindEntities())
            {
                var colNames = typeof(Shipper).GetProperties().Select(a => a.Name).ToList();
                for (int i = 0; i < colNames.Count; i++)
                {
                    DataColumn dc = new DataColumn(colNames[i]);
                    dt.Columns.Add(dc);
                }

                switch ((testParameter.ActionType.Split('%'))[2])
                {
                    // 静的SQL
                    case "static":
                        // 静的SQL
                        var query = (from shipper in context.Shippers
                                     where shipper.CompanyName != testParameter.CompanyName
                                     select new
                                     {
                                         ShipperID = shipper.ShipperID,
                                         CompanyName = shipper.CompanyName,
                                         Phone = shipper.Phone
                                     }).ToList();

                        if (testParameter.OrderColumn == "c1")
                        {
                            switch (testParameter.OrderSequence)
                            {
                                case "A":
                                    query = query.OrderBy(o => o.ShipperID).ToList();
                                    break;
                                case "D":
                                    query = query.OrderByDescending(o => o.ShipperID).ToList();
                                    break;
                            }
                        }
                        else if (testParameter.OrderColumn == "c2")
                        {
                            switch (testParameter.OrderSequence)
                            {
                                case "A":
                                    query = query.OrderBy(o => o.CompanyName).ToList();
                                    break;
                                case "D":
                                    query = query.OrderByDescending(o => o.CompanyName).ToList();
                                    break;
                            }
                        }
                        else
                        {
                            switch (testParameter.OrderSequence)
                            {
                                case "A":
                                    query = query.OrderBy(o => o.Phone).ToList();
                                    break;
                                case "D":
                                    query = query.OrderByDescending(o => o.Phone).ToList();
                                    break;
                            }
                        }
                        foreach (var item in query)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = item.ShipperID;
                            dr[1] = item.CompanyName;
                            dr[2] = item.Phone;
                            dt.Rows.Add(dr);
                        }
                        testReturn.Obj = dt;
                        break;
                    case "dynamic":
                        IQueryable<Shipper> parentQuery = null;
                        parentQuery = context.Shippers;
                        if (!string.IsNullOrEmpty(testParameter.CompanyName))
                        {
                            if (testParameter.OrderColumn == "c1")
                            {
                                switch (testParameter.OrderSequence)
                                {
                                    case "A":
                                        parentQuery = parentQuery.Where(c => c.CompanyName != testParameter.CompanyName).OrderBy(o => o.ShipperID);
                                        break;
                                    case "D":
                                        parentQuery = parentQuery.Where(c => c.CompanyName != testParameter.CompanyName).OrderByDescending(o => o.ShipperID);
                                        break;
                                }
                            }
                            else if (testParameter.OrderColumn == "c2")
                            {
                                switch (testParameter.OrderSequence)
                                {
                                    case "A":
                                        parentQuery = parentQuery.Where(c => c.CompanyName != testParameter.CompanyName).OrderBy(o => o.CompanyName);
                                        break;
                                    case "D":
                                        parentQuery = parentQuery.Where(c => c.CompanyName != testParameter.CompanyName).OrderByDescending(o => o.CompanyName);
                                        break;
                                }
                            }
                            else
                            {
                                switch (testParameter.OrderSequence)
                                {
                                    case "A":
                                        parentQuery = parentQuery.Where(c => c.CompanyName != testParameter.CompanyName).OrderBy(o => o.Phone);
                                        break;
                                    case "D":
                                        parentQuery = parentQuery.Where(c => c.CompanyName != testParameter.CompanyName).OrderByDescending(o => o.Phone);
                                        break;
                                }
                            }

                            foreach (var item in parentQuery)
                            {
                                DataRow dr = dt.NewRow();
                                dr[0] = item.ShipperID;
                                dr[1] = item.CompanyName;
                                dr[2] = item.Phone;
                                dt.Rows.Add(dr);
                            }
                            testReturn.Obj = dt;
                        }
                        break;
                }
            }
        }

        #endregion
    }
}