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
//* クラス名        ：LayerD
//* クラス日本語名  ：LayerD
//*
//* 作成者          ：生技 西野
//* 更新履歴        ：
//*
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  20xx/xx/xx  ＸＸ ＸＸ         新規作成
//*  2016/04/22  Supragyan        Created LayerD class to support Azure SQL functionality
//**********************************************************************************

// System
using System;
using System.Data;
// MyType
using MyType;
// 業務フレームワーク
using Touryo.Infrastructure.Business.Dao;
// 部品
using Touryo.Infrastructure.Public.Db;

/// <summary>
/// LayerD の概要の説明です
/// </summary>
public class LayerD : MyBaseDao
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public LayerD(BaseDam dam) : base(dam) { }

    #region 追加

    /// <summary>Insertクエリを実行する</summary>
    /// <param name="testParameter">引数クラス</param>
    /// <param name="testReturn">戻り値クラス</param>
    public void Insert(TestParameterValue testParameter, TestReturnValue testReturn)
    {
        // ↓DBアクセス-----------------------------------------------------       

        //   -- ファイルから読み込む場合。
        this.SetSqlByFile2("Customers_Orders_DDRInsert.sql");

        // パラメタ ライズド クエリのパラメタに対して、動的に値を設定する。
        this.SetParameter("P1", testParameter.CustomerId);
        this.SetParameter("P2", testParameter.Name);
        this.SetParameter("P3", testParameter.RegionId);

        this.SetParameter("P4", testParameter.CustomerId);
        this.SetParameter("P5", testParameter.ProductId);
        this.SetParameter("P6", DateTime.Now.Date);

        object obj;

        //   -- 追加（件数を確認できる）
        obj = this.ExecInsUpDel_NonQuery();

        // ↑DBアクセス-----------------------------------------------------

        // 戻り値を設定
        testReturn.Obj = obj;
    }

    #endregion

    #region 一覧取得（SelectAll）

    /// <summary>一覧を返すSELECTクエリを実行する（DR）</summary>
    /// <param name="testParameter">引数クラス</param>
    /// <param name="testReturn">戻り値クラス</param>
    public void SelectAll_DR(TestParameterValue testParameter, TestReturnValue testReturn)
    {
        // ↓DBアクセス-----------------------------------------------------

        string commandText = "";

        if ((testParameter.ActionType.Split('%'))[2] == "static")
        {
            // 静的SQL
            commandText = "SELECT c.CustomerId,c.Name AS CustomerName,COUNT(o.OrderID) AS OrderCount FROM dbo.Customers AS c INNER JOIN dbo.Orders AS o ON c.CustomerID = o.CustomerID GROUP BY c.CustomerId,c.Name ORDER BY OrderCount";
        }
        else if ((testParameter.ActionType.Split('%'))[2] == "dynamic")
        {
            // 動的SQL
            commandText =
                "<?xml version=\"1.0\" encoding=\"shift_jis\" ?><ROOT>SELECT c.CustomerId,c.Name AS CustomerName,COUNT(o.OrderID) AS OrderCount FROM dbo.Customers AS c INNER JOIN dbo.Orders AS o ON c.CustomerID = o.CustomerID GROUP BY c.CustomerId,c.Name ORDER BY OrderCount</ROOT>";
            // 通常、動的SQLをSetSqlByCommandで直接指定するような使い方はしない。
        }

        //   -- 直接指定する場合。
        this.SetSqlByCommand(commandText);

        // 戻り値 dt
        DataTable dt = new DataTable();

        // ３列生成
        dt.Columns.Add("c1", System.Type.GetType("System.String"));
        dt.Columns.Add("c2", System.Type.GetType("System.String"));
        dt.Columns.Add("c3", System.Type.GetType("System.String"));

        //   -- 一覧を返すSELECTクエリを実行する
        IDataReader idr = (IDataReader)this.ExecSelect_DR();

        while (idr.Read())
        {
            // DRから読む
            object[] objArray = new object[3];
            idr.GetValues(objArray);

            // DTに設定する。
            DataRow dr = dt.NewRow();
            dr.ItemArray = objArray;
            dt.Rows.Add(dr);
        }

        // 終了したらクローズ
        idr.Close();

        // ↑DBアクセス-----------------------------------------------------

        // 戻り値を設定
        testReturn.Obj = dt;
    }

    /// <summary>Selects the Customer data by Id</summary>
    /// <param name="testParameter">testParameter</param>
    /// <param name="testReturn">testReturn</param>
    public void SelectByCustomer(TestParameterValue testParameter, TestReturnValue testReturn)
    {
        // ↓DBアクセス-----------------------------------------------------

        string filename = "";

        if ((testParameter.ActionType.Split('%'))[2] == "static")
        {
            // 静的SQL
            filename = "CustomersSelectDDR.sql";
        }
        else if ((testParameter.ActionType.Split('%'))[2] == "dynamic")
        {
            // 動的SQL
            filename = "CustomersSelectDDR.sql";
        }

        //   -- ファイルから読み込む場合。
        this.SetSqlByFile2(filename);

        // パラメタ ライズド クエリのパラメタに対して、動的に値を設定する。
        this.SetParameter("P1", testParameter.CustomerId);

        // 戻り値 dt
        DataTable dt = new DataTable();
        // ３列生成
        dt.Columns.Add("c1", System.Type.GetType("System.String"));
        dt.Columns.Add("c2", System.Type.GetType("System.String"));
        dt.Columns.Add("c3", System.Type.GetType("System.String"));

        //   -- 一覧を返すSELECTクエリを実行する
        IDataReader idr = (IDataReader)this.ExecSelect_DR();

        while (idr.Read())
        {
            // DRから読む
            object[] objArray = new object[3];
            idr.GetValues(objArray);

            // DTに設定する。
            DataRow dr = dt.NewRow();
            dr.ItemArray = objArray;
            dt.Rows.Add(dr);
        }

        // 終了したらクローズ
        idr.Close();

        // ↑DBアクセス-----------------------------------------------------

        // 戻り値を設定
        testReturn.Obj = dt;
    }
    #endregion

    #region 削除

    /// <summary>Deleteクエリを実行する</summary>
    /// <param name="testParameter">引数クラス</param>
    /// <param name="testReturn">戻り値クラス</param>
    public void Delete(TestParameterValue testParameter, TestReturnValue testReturn)
    {
        // ↓DBアクセス-----------------------------------------------------

        string filename = "";

        if ((testParameter.ActionType.Split('%'))[2] == "static")
        {
            // 静的SQL
            filename = "CustomersDeleteDDR.sql";
        }
        else if ((testParameter.ActionType.Split('%'))[2] == "dynamic")
        {
            // 動的SQL
            filename = "CustomersDeleteDDR.sql";
        }

        //   -- ファイルから読み込む場合。
        this.SetSqlByFile2(filename);

        // パラメタ ライズド クエリのパラメタに対して、動的に値を設定する。
        this.SetParameter("P1", testParameter.CustomerId);

        object obj;

        //   -- 追削除（件数を確認できる）
        obj = this.ExecInsUpDel_NonQuery();

        // ↑DBアクセス-----------------------------------------------------

        // 戻り値を設定
        testReturn.Obj = obj;
    }

    #endregion
}
