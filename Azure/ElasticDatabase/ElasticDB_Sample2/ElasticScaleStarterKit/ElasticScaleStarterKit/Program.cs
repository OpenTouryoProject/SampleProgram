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
//* クラス名        ：Program
//* クラス日本語名  ：Program
//*
//* 作成者          ：生技 西野
//* 更新履歴        ：
//*
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  20xx/xx/xx  ＸＸ ＸＸ         新規作成
//*  2016/04/27  Supragyan        Created Program to execute main method
//**********************************************************************************

// System
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
// Microsoft
using Microsoft.Azure.SqlDatabase.ElasticScale.ShardManagement;
using Microsoft.Azure.SqlDatabase.ElasticScale.ShardManagement.Schema;
using Microsoft.Azure.SqlDatabase.ElasticScale.Query;
// Touryo
using Touryo.Infrastructure.Public.Db;

namespace ElasticScaleStarterKit
{
    /// <summary>
    /// Program class
    /// </summary>
    internal class Program
    {
        public static void Main()
        {
            // Welcome screen
            Console.WriteLine("***********************************************************");
            Console.WriteLine("***    Welcome to Elastic Database Tools Starter Kit    ***");
            Console.WriteLine("***********************************************************");
            Console.WriteLine();


            MultiShardConfiguration.ShardMapName = "CustomerShardMap";
            MultiShardConfiguration.ShardMapManagerDatabaseName = "ElasticScaleStarterKit_ShardMapManagerDb";
            MultiShardConfiguration.MasterDatabaseName = "master";

            // Verify that we can connect to the Sql Database that is specified in App.config settings
            if (!SqlDatabaseUtils.TryConnectToSqlDatabase())
            {
                // Connecting to the server failed - please update the settings in App.Config

                // Give the user a chance to read the mesage, if this program is being run
                // in debug mode in Visual Studio
                if (Debugger.IsAttached)
                {
                    Console.WriteLine("Press ENTER to continue...");
                    Console.ReadLine();
                }

                // Exit
                return;
            }

            // Connection succeeded. Begin interactive loop
            MenuLoop();
        }

        #region Program control flow

        /// <summary>
        /// Main program loop.
        /// </summary>
        private static void MenuLoop()
        {
            // Get the shard map manager, if it already exists.
            // It is recommended that you keep only one shard map manager instance in
            // memory per AppDomain so that the mapping cache is not duplicated.
            MultiShardConfiguration.objShardMapManager = ShardManagementUtils.TryGetShardMapManager();

            // Loop until the user chose "Exit".
            bool continueLoop;
            do
            {
                PrintShardMapState();
                Console.WriteLine();

                PrintMenu();
                Console.WriteLine();

                continueLoop = GetMenuChoiceAndExecute();
                Console.WriteLine();
            }
            while (continueLoop);
        }

        /// <summary>
        /// Writes the shard map's state to the console.
        /// </summary>
        private static void PrintShardMapState()
        {
            Console.WriteLine("Current Shard Map state:");
            RangeShardMap<int> shardMap = MultiShardConfiguration.TryGetShardMap();
            if (shardMap == null)
            {
                return;
            }

            // Get all shards
            IEnumerable<Shard> allShards = shardMap.GetShards();

            // Get all mappings, grouped by the shard that they are on. We do this all in one go to minimise round trips.
            ILookup<Shard, RangeMapping<int>> mappingsGroupedByShard = shardMap.GetMappings().ToLookup(m => m.Shard);

            if (allShards.Any())
            {
                // The shard map contains some shards, so for each shard (sorted by database name)
                // write out the mappings for that shard
                foreach (Shard shard in shardMap.GetShards().OrderBy(s => s.Location.Database))
                {
                    IEnumerable<RangeMapping<int>> mappingsOnThisShard = mappingsGroupedByShard[shard];

                    if (mappingsOnThisShard.Any())
                    {
                        string mappingsString = string.Join(", ", mappingsOnThisShard.Select(m => m.Value));
                        Console.WriteLine("\t{0} contains key range {1}", shard.Location.Database, mappingsString);
                    }
                    else
                    {
                        Console.WriteLine("\t{0} contains no key ranges.", shard.Location.Database);
                    }
                }
            }
            else
            {
                Console.WriteLine("\tShard Map contains no shards");
            }
        }

        // color for items that are expected to succeed
        private const ConsoleColor EnabledColor = ConsoleColor.White;

        // color for items that are expected to fail
        private const ConsoleColor DisabledColor = ConsoleColor.DarkGray;

        /// <summary>
        /// Writes the program menu.
        /// </summary>
        private static void PrintMenu()
        {
            ConsoleColor createSmmColor; // color for create shard map manger menu item
            ConsoleColor otherMenuItemColor; // color for other menu items
            if (MultiShardConfiguration.objShardMapManager == null)
            {
                createSmmColor = EnabledColor;
                otherMenuItemColor = DisabledColor;
            }
            else
            {
                createSmmColor = DisabledColor;
                otherMenuItemColor = EnabledColor;
            }

            ConsoleUtils.WriteColor(createSmmColor, "1. Create shard map manager, and add a couple shards");
            ConsoleUtils.WriteColor(otherMenuItemColor, "2. Add another shard");
            ConsoleUtils.WriteColor(otherMenuItemColor, "3. Insert sample rows using Data-Dependent Routing");
            ConsoleUtils.WriteColor(otherMenuItemColor, "4. Select sample rows using Data-Dependent Routing");
            ConsoleUtils.WriteColor(otherMenuItemColor, "5. Delete sample rows using Data-Dependent Routing");
            ConsoleUtils.WriteColor(otherMenuItemColor, "6. Execute sample Multi-Shard Query");
            ConsoleUtils.WriteColor(otherMenuItemColor, "7. Drop shard map manager database and all shards");
            ConsoleUtils.WriteColor(EnabledColor, "8. Exit");
        }

        /// <summary>
        /// Gets the user's chosen menu item and executes it.
        /// </summary>
        /// <returns>true if the program should continue executing.</returns>
        private static bool GetMenuChoiceAndExecute()
        {
            while (true)
            {
                int inputValue = ConsoleUtils.ReadIntegerInput("Enter an option [1-6] and press ENTER: ");

                switch (inputValue)
                {
                    case 1: // Create shard map manager
                        Console.WriteLine();
                        CreateShardMapManagerAndShard();
                        return true;
                    case 2: // Add shard
                        Console.WriteLine();
                        AddShard();
                        return true;
                    case 3: // Data Dependent Routing-insert
                        Console.WriteLine();
                        DataDepdendentRoutingForInsert();
                        return true;
                    case 4: // Data Dependent Routing-select
                        Console.WriteLine();
                        DataDepdendentRoutingForSelect();
                        return true;
                    case 5: // Data Dependent Routing-Delete
                        Console.WriteLine();
                        DataDepdendentRoutingForDelete();
                        return true;
                    case 6: // Multi-Shard Query
                        Console.WriteLine();
                        MultiShardQuery();
                        return true;
                    case 7: // Drop all
                        Console.WriteLine();
                        DropAll();
                        return true;
                    case 8: // Exit
                        return false;
                }
            }
        }

        #endregion

        #region Menu item implementations

        /// <summary>
        /// Creates a shard map manager, creates a shard map, and creates a shard
        /// with a mapping for the full range of 32-bit integers.
        /// </summary>
        private static void CreateShardMapManagerAndShard()
        {
            if (MultiShardConfiguration.objShardMapManager != null)
            {
                ConsoleUtils.WriteWarning("Shard Map Manager already exists");
                return;
            }

            // Create shard map manager database
            if (!SqlDatabaseUtils.ExistsDatabase(MultiShardConfiguration.ShardMapManagerServerName, MultiShardConfiguration.ShardMapManagerDatabaseName))
            {
                SqlDatabaseUtils.CreateDatabase(MultiShardConfiguration.ShardMapManagerServerName, MultiShardConfiguration.ShardMapManagerDatabaseName);
            }

            // Create shard map manager
            string shardMapManagerConnectionString = MultiShardConfiguration.GetConnectionString();

            MultiShardConfiguration.objShardMapManager = ShardManagementUtils.CreateOrGetShardMapManager(shardMapManagerConnectionString);

            // Create shard map
            RangeShardMap<int> shardMap = ShardManagementUtils.CreateOrGetRangeShardMap<int>(
                MultiShardConfiguration.objShardMapManager, MultiShardConfiguration.ShardMapName);

            // Create schema info so that the split-merge service can be used to move data in sharded tables
            // and reference tables.
            CreateSchemaInfo(shardMap.Name);

            // If there are no shards, add two shards: one for [0,100) and one for [100,+inf)
            if (!shardMap.GetShards().Any())
            {
                CreateShardSample.CreateShard(shardMap, new Range<int>(0, 100));
                CreateShardSample.CreateShard(shardMap, new Range<int>(100, 200));
            }
        }

        /// <summary>
        /// Creates schema info for the schema defined in InitializeShard.sql.
        /// </summary>
        private static void CreateSchemaInfo(string shardMapName)
        {
            // Create schema info
            SchemaInfo schemaInfo = new SchemaInfo();
            schemaInfo.Add(new ReferenceTableInfo("Regions"));
            schemaInfo.Add(new ReferenceTableInfo("Products"));
            schemaInfo.Add(new ShardedTableInfo("Customers", "CustomerId"));
            schemaInfo.Add(new ShardedTableInfo("Orders", "CustomerId"));

            // Register it with the shard map manager for the given shard map name
            MultiShardConfiguration.objShardMapManager.GetSchemaInfoCollection().Add(shardMapName, schemaInfo);
        }

        /// <summary>
        /// Reads the user's choice of a split point, and creates a new shard with a mapping for the resulting range.
        /// </summary>
        private static void AddShard()
        {
            RangeShardMap<int> shardMap = MultiShardConfiguration.TryGetShardMap();
            if (shardMap != null)
            {
                // Here we assume that the ranges start at 0, are contiguous, 
                // and are bounded (i.e. there is no range where HighIsMax == true)
                int currentMaxHighKey = shardMap.GetMappings().Max(m => m.Value.High);
                int defaultNewHighKey = currentMaxHighKey + 100;

                Console.WriteLine("A new range with low key {0} will be mapped to the new shard.", currentMaxHighKey);
                int newHighKey = ConsoleUtils.ReadIntegerInput(
                    string.Format("Enter the high key for the new range [default {0}]: ", defaultNewHighKey),
                    defaultNewHighKey,
                    input => input > currentMaxHighKey);

                Range<int> range = new Range<int>(currentMaxHighKey, newHighKey);

                Console.WriteLine();
                Console.WriteLine("Creating shard for range {0}", range);
                CreateShardSample.CreateShard(shardMap, range);
            }
        }

        /// <summary>
        /// Executes the Data-Dependent Routing sample.
        /// </summary>
        private static void DataDepdendentRoutingForInsert()
        {
            RangeShardMap<int> shardMap = MultiShardConfiguration.TryGetShardMap();
            if (shardMap != null)
            {
                DataDependentRoutingSupport.ExecuteDataDependentRoutingQuery(shardMap);
            }

        }

        /// <summary>
        /// Executes the Data-Dependent Routing sample.
        /// </summary>
        private static void DataDepdendentRoutingForSelect()
        {
            RangeShardMap<int> shardMap = MultiShardConfiguration.TryGetShardMap();
            if (shardMap != null)
            {
                DataDependentRoutingSupport.ExecuteDataDependentRoutingQueryForSelect(shardMap);
            }

        }

        /// <summary>
        /// Executes the Data-Dependent Routing sample.
        /// </summary>
        private static void DataDepdendentRoutingForDelete()
        {
            RangeShardMap<int> shardMap = MultiShardConfiguration.TryGetShardMap();
            if (shardMap != null)
            {
                DataDependentRoutingSupport.ExecuteDataDependentRoutingQueryForDelete(shardMap);
            }

        }

        /// <summary>
        /// Executes the Multi-Shard Query sample.
        /// </summary>
        private static void MultiShardQuery()
        {
            RangeShardMap<int> shardMap = MultiShardConfiguration.TryGetShardMap();
            if (shardMap != null)
            {
                MultiShardQuerySupport.ExecuteMultiShardQuery(shardMap);
            }
        }

        /// <summary>
        /// Drops all shards and the shard map manager database (if it exists).
        /// </summary>
        private static void DropAll()
        {
            RangeShardMap<int> shardMap = MultiShardConfiguration.TryGetShardMap();
            if (shardMap != null)
            {
                // Drop shards
                foreach (Shard shard in shardMap.GetShards())
                {
                    SqlDatabaseUtils.DropDatabase(shard.Location.DataSource, shard.Location.Database);
                }
            }

            if (SqlDatabaseUtils.ExistsDatabase(MultiShardConfiguration.ShardMapManagerServerName, MultiShardConfiguration.ShardMapManagerDatabaseName))
            {
                // Drop shard map manager database
                SqlDatabaseUtils.DropDatabase(MultiShardConfiguration.ShardMapManagerServerName, MultiShardConfiguration.ShardMapManagerDatabaseName);
            }

            // Since we just dropped the shard map manager database, this shardMapManager reference is now non-functional.
            // So set it to null so that the program knows that the shard map manager is gone.
            MultiShardConfiguration.objShardMapManager = null;
        }

        #endregion
    }
}
