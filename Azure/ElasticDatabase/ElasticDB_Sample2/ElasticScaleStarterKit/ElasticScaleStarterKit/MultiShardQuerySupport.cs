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
//* クラス名        ：MultiShardQuerySupport
//* クラス日本語名  ：MultiShardQuerySupport
//*
//* 作成者          ：生技 西野
//* 更新履歴        ：
//*
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  20xx/xx/xx  ＸＸ ＸＸ         新規作成
//*  2016/04/22  Supragyan        Created MultiShardQuerySupport class to execute MultiShard Query
//**********************************************************************************

// System
using System;
using System.Data;
using System.Linq;
// Microsoft
using Microsoft.Azure.SqlDatabase.ElasticScale.ShardManagement;
// Touryo
using Touryo.Infrastructure.Public.Db;
using Touryo.Infrastructure.Business.Util;
using Touryo.Infrastructure.Framework.Common;
// MyType
using MyType;

namespace ElasticScaleStarterKit
{
    /// <summary>
    /// MultiShardQuerySupport class
    /// </summary>
    public static class MultiShardQuerySupport
    {
        /// <summary>
        /// ExecuteMultiShardQuery method to get the database records using MultiShardConnection,
        /// MultiShardCommand,multiShardDataReader class
        /// </summary>
        /// <param name="shardMap"></param>
        /// <param name="credentialsConnectionString"></param>
        public static void ExecuteMultiShardQuery(RangeShardMap<int> shardMap)
        {
            // Get the Shards from Shard Map manager
            MultiShardConfiguration.Shards = shardMap.GetShards();
            TestParameterValue testParameterValue
           = new TestParameterValue(
               "MultiShard", "ExecMultiShard", "SelectAll_DR",
               "SqlDbWithMultiShard" + "%"
               + "individual" + "%"
               + "-",
             new MyUserInfo("MultiShard", "MultiShard"));

            // 分離レベルの設定
            DbEnum.IsolationLevelEnum iso = DbEnum.IsolationLevelEnum.NoTransaction;

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
    }
}
