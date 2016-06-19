//**********************************************************************************
//* Copyright (C) 2007,2016 Hitachi Solutions,Ltd.
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

//**********************************************************************************
//* クラス名        ：DataDependentRoutingSupport
//* クラス日本語名  ：DataDependentRoutingSupport設定
//*
//* 作成者          ：生技 西野
//* 更新履歴        ：
//*
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  20xx/xx/xx  ＸＸ ＸＸ         新規作成
//*  2016/04/27  Supragyan        Created DataDependentRoutingSupport class to insert data to shards
//**********************************************************************************

// System
using System;
using System.Linq;
using System.Data;
// Microsoft
using Microsoft.Azure.SqlDatabase.ElasticScale.ShardManagement;
// MyType
using MyType;
// Touryo
using Touryo.Infrastructure.Business.Util;
using Touryo.Infrastructure.Public.Db;
using Touryo.Infrastructure.Framework.Common;

namespace ElasticScaleStarterKit
{
    /// <summary>
    /// DataDependentRoutingSupport class
    /// </summary>
    internal static class DataDependentRoutingSupport
    {
        //Stores customer names
        private static string[] shardCustomerNames = new[]
        {
            "Customer1",
            "Customer2",
            "Customer3",
            "Customer4",
            "Customer5"
        };

        //randCustomer instance
        private static Random randCustomer = new Random();

        /// <summary>
        /// To create database records
        /// </summary>
        /// <param name="shardMap"></param>
        /// <param name="credentialsConnectionString"></param>
        public static void ExecuteDataDependentRoutingQuery(RangeShardMap<int> shardMap)
        {
            // we just choose a random key out of the range that is mapped. Here we assume that the ranges
            // start at 0, are contiguous, and are bounded (i.e. there is no range where HighIsMax == true)
            int currentMaxHighKey = shardMap.GetMappings().Max(m => m.Value.High);
            MultiShardConfiguration.customerId = GetCustomerId(currentMaxHighKey);
            string customerName = shardCustomerNames[randCustomer.Next(shardCustomerNames.Length)];
            int regionId = 0;
            int productId = 0;

            TestParameterValue testParameterValue
           = new TestParameterValue(
               "DataDependentRouting", "ExecuteDataDependentRouting", "Insert",
               "SqlDbWithDataDependent" + "%"
               + "individual" + "%"
               + "-",
             new MyUserInfo("DataDependentRouting", "DataDependentRouting"));

            // 情報の設定
            testParameterValue.CustomerId = MultiShardConfiguration.customerId;
            testParameterValue.Name = customerName;
            testParameterValue.RegionId = regionId;
            testParameterValue.ProductId = productId;

            // 分離レベルの設定
            DbEnum.IsolationLevelEnum iso = DbEnum.IsolationLevelEnum.DefaultTransaction;

            // B層を生成
            LayerB myBusiness = new LayerB();

            // 業務処理を実行
            TestReturnValue testReturnValue =
                (TestReturnValue)myBusiness.DoBusinessLogic(
                    (BaseParameterValue)testParameterValue, iso);

            string strErrorMsg = "";
            if (testReturnValue.ErrorFlag == true)
            {
                // 結果（業務続行可能なエラー）
                strErrorMsg = "ErrorMessageID:" + testReturnValue.ErrorMessageID + "\r\n";
                strErrorMsg += "ErrorMessage:" + testReturnValue.ErrorMessage + "\r\n";
                strErrorMsg += "ErrorInfo:" + testReturnValue.ErrorInfo + "\r\n";

                Console.WriteLine("Inserted failed for customer ID: {0},{1}", testParameterValue.CustomerId, strErrorMsg);
            }
            else
            {
                // 結果（正常系）
                Console.WriteLine("Inserted order for customer ID: {0}", testParameterValue.CustomerId);
            }
        }

        /// <summary>
        /// To Select database records
        /// </summary>
        /// <param name="shardMap"></param>
        /// <param name="credentialsConnectionString"></param>
        public static void ExecuteDataDependentRoutingQueryForSelect(RangeShardMap<int> shardMap)
        {
            TestParameterValue testParameterValue
           = new TestParameterValue(
               "DataDependentRouting", "ExecuteDataDependentRouting", "SelectByCustomer",
               "SqlDbWithDataDependent" + "%"
               + "individual" + "%"
               + "-",
             new MyUserInfo("DataDependentRouting", "DataDependentRouting"));

            // 情報の設定
            testParameterValue.CustomerId = MultiShardConfiguration.customerId;

            // 分離レベルの設定
            DbEnum.IsolationLevelEnum iso = DbEnum.IsolationLevelEnum.DefaultTransaction;

            // B層を生成
            LayerB myBusiness = new LayerB();

            // 業務処理を実行
            TestReturnValue testReturnValue =
                (TestReturnValue)myBusiness.DoBusinessLogic(
                    (BaseParameterValue)testParameterValue, iso);

            Console.WriteLine("Selected order for customer ID: {0}", testParameterValue.CustomerId);

            string strErrorMsg = "";
            if (testReturnValue.ErrorFlag == true)
            {
                // 結果（業務続行可能なエラー）
                strErrorMsg = "ErrorMessageID:" + testReturnValue.ErrorMessageID + "\r\n";
                strErrorMsg += "ErrorMessage:" + testReturnValue.ErrorMessage + "\r\n";
                strErrorMsg += "ErrorInfo:" + testReturnValue.ErrorInfo + "\r\n";

                Console.WriteLine("Inserted failed for Error message : {0}", strErrorMsg);
            }
            else
            {
                //Converts Return value object to dataTable data to display the data in screen
                DataTable dtTable = (DataTable)testReturnValue.Obj;

                int rows = 0;

                // Get the column names
                TableFormatter formatter = new TableFormatter(ShardManagementUtils.GetColumnNames(dtTable).ToArray());

                foreach (DataRow dr in dtTable.Rows)
                {
                    // Extract just database name from the $ShardLocation pseudocolumn to make the output formater cleaner.
                    // Note that the $ShardLocation pseudocolumn is always the last column
                    int shardLocationOrdinal = dr.ItemArray.Length - 1;
                    dr.ItemArray[shardLocationOrdinal] = ShardManagementUtils.ExtractDatabaseName(dr.ItemArray[shardLocationOrdinal].ToString());

                    // Add values to output formatter
                    formatter.AddRow(dr.ItemArray);

                    rows++;
                }
                Console.WriteLine(formatter.ToString());
                Console.WriteLine("({0} rows returned)", rows);
            }
        }

        /// <summary>
        /// To Delete database records
        /// </summary>
        /// <param name="shardMap"></param>
        /// <param name="credentialsConnectionString"></param>
        public static void ExecuteDataDependentRoutingQueryForDelete(RangeShardMap<int> shardMap)
        {
            TestParameterValue testParameterValue
           = new TestParameterValue(
               "DataDependentRouting", "ExecuteDataDependentRouting", "Delete",
               "SqlDbWithDataDependent" + "%"
               + "individual" + "%"
               + "-",
             new MyUserInfo("DataDependentRouting", "DataDependentRouting"));

            // 情報の設定
            testParameterValue.CustomerId = MultiShardConfiguration.customerId;

            // 分離レベルの設定
            DbEnum.IsolationLevelEnum iso = DbEnum.IsolationLevelEnum.DefaultTransaction;

            // B層を生成
            LayerB myBusiness = new LayerB();

            // 業務処理を実行
            TestReturnValue testReturnValue =
                (TestReturnValue)myBusiness.DoBusinessLogic(
                    (BaseParameterValue)testParameterValue, iso);

            Console.WriteLine("Deleted order for customer ID: {0}", testParameterValue.CustomerId);
        }

        /// <summary>
        /// Gets a customer ID to insert into the customers table.
        /// </summary>
        private static int GetCustomerId(int maxid)
        {
            // To create a random customer ID. 
            return randCustomer.Next(0, maxid);
        }
    }
}
