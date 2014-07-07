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

using System.Collections;
using System.Web.SessionState;

/// <summary>
/// ID管理機能を持つキュー
/// </summary>
public class CustQueue
{
    /// <summary>セッション</summary>
    HttpSessionState Session = null;

    /// <summary>キュー</summary>
    Queue _Queue = null;

    /// <summary>キューのキャパシティ</summary>
    int Capacity = 0;

    /// <summary>コンストラクタ</summary>
    /// <param name="HashtableQueueName">キュー名</param>
    public CustQueue(string HashtableQueueName)
	{
        // セッションからキューを取り出す。
        this.Session = HttpContext.Current.Session;

        // セッションが空の場合は新規作成する。
        if (Session[HashtableQueueName] == null)
        {
            Session[HashtableQueueName] = new Queue();
        }

        // メンバに保持する。
        this._Queue = (Queue)Session[HashtableQueueName];
        
        // キューのキャパシティ
        this.Capacity = int.Parse(ConfigurationManager.AppSettings["QueueCapacity"]);
	}

    /// <summary>IDを追加（Enqueue）、削除（Dequeue）する</summary>
    /// <param name="ID">ID</param>
    /// <returns>削除（Dequeue）したIDのハッシュテーブル</returns>
    public Hashtable EnQandDeQ(string ID)
    {
        // IDの追加（Enqueue）
        this._Queue.Enqueue(ID);

        // 戻り値：削除（Dequeue）したID
        Hashtable ids = new Hashtable();

        // キャパシティをチェック
        while (this.Capacity < this._Queue.Count)
        {
            // IDの削除（Dequeue）
            string oldestID = "";
            oldestID = (string)this._Queue.Dequeue();

            // 戻り値のハッシュテーブルへ削除したIDを設定する。
            ids.Add(oldestID, "");
        }

        // 戻り値：削除（Dequeue）したID
        return ids;
    }
}
