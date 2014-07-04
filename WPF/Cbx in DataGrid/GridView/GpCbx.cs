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
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;

using System.Data;
using System.Collections.ObjectModel;

namespace GridView
{
    /// <summary>汎用コンボボックス コレクション データクラス</summary>
    public class GpCbxCollectionData : Collection<GpCbxItemData>
    {
        /// <summary>コンストラクタ</summary>
        public GpCbxCollectionData(string key)
            : base()
        {
            DataTable dt = (DataTable)Application.Current.Properties[key];

            if (dt != null) // デザイナでエラーにならないよう対策
            {
                foreach (DataRow dr in dt.Rows)
                {
                    this.Add(new GpCbxItemData((int)dr["value"], (string)dr["display"]));
                }
            }
        }
    }

    /// <summary>汎用コンボボックス アイテム データクラス</summary>
    public class GpCbxItemData
    {
        /// <summary>値</summary>
        private int _value;
        /// <summary>表示</summary>
        private string _display;

        /// <summary>コンストラクタ</summary>
        public GpCbxItemData(int value, string display)
        {
            this._value = value;
            this._display = display;
        }

        /// <summary>値</summary>
        public int Value
        {
            get { return this._value; }
            set { this._value = value; }
        }
        /// <summary>表示</summary>
        public string Display
        {
            get { return this._display; }
            set { this._display = value; }
        }
    }
}
