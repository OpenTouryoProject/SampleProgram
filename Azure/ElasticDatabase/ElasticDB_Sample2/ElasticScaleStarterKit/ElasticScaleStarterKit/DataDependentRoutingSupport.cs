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
using System.Data.Common;
using System.Collections.Generic;
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

        //randShard instance
        private static Random randShard = new Random();

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
            int customerId = GetCustomerId(currentMaxHighKey);
            MultiShardConfiguration.customerId = GetCustomerId(currentMaxHighKey);
            string customerName = shardCustomerNames[randShard.Next(shardCustomerNames.Length)];
            int regionId = 0;
            int productId = 0;

            TestParameterValue testParameterValue
           = new TestParameterValue(
               "DataDependentRouting", "ExecuteDataDependentRouting", "Insert",
               "SqlDbWithDataDependent" + "%"
               + "individual" + "%"
               + "static" + "%"
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

            Console.WriteLine("Inserted order for customer ID: {0}", testParameterValue.CustomerId);
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
               + "static" + "%"
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

            DataTable dt = (DataTable)testReturnValue.Obj;
            DbDataReader reader = dt.CreateDataReader();
            int rows = 0;
            // Get the column names
            TableFormatter formatter = new TableFormatter(GetColumnNames(reader).ToArray());

            while (reader.Read())
            {
                // Read the values using standard DbDataReader methods
                object[] values = new object[reader.FieldCount];
                reader.GetValues(values);

                // Extract just database name from the $ShardLocation pseudocolumn to make the output formater cleaner.
                // Note that the $ShardLocation pseudocolumn is always the last column
                int shardLocationOrdinal = values.Length - 1;
                values[shardLocationOrdinal] = ExtractDatabaseName(values[shardLocationOrdinal].ToString());

                // Add values to output formatter
                formatter.AddRow(values);

                rows++;
            }
            Console.WriteLine(formatter.ToString());
            Console.WriteLine("({0} rows returned)", rows);
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
               + "static" + "%"
               + "-" ,
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
            return randShard.Next(0, maxid);
        }

        /// <summary>
        /// Gets the column names from a data reader.
        /// </summary>
        private static IEnumerable<string> GetColumnNames(DbDataReader reader)
        {
            List<string> columnNames = new List<string>();
            foreach (DataRow r in reader.GetSchemaTable().Rows)
            {
                columnNames.Add(r[SchemaTableColumn.ColumnName].ToString());
            }

            return columnNames;
        }

        /// <summary>
        /// Extracts the database name from the provided shard location string.
        /// </summary>
        private static string ExtractDatabaseName(string shardLocationString)
        {
            string[] pattern = new[] { "[", "DataSource=", "Database=", "]" };
            string[] matches = shardLocationString.Split(pattern, StringSplitOptions.RemoveEmptyEntries);
            return matches[0];
        }
    }
}
