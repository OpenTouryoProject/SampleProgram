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
//* クラス名        ：FxLiteral
//* クラス日本語名  ：Framework層のリテラル クラス
//*
//* 作成者          ：生技 西野
//* 更新履歴        ：
//*
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  2009/04/22  西野 大介         新規作成
//*  2009/06/02  西野 大介         sln - IR版からの修正
//*                                ・・・
//*  2017/08/28  西野 大介         非同期メソッドのリテラルを追加した。
//*  2018/03/29  西野 大介         .NET Standard対応で、削除機能に関連するリテラルを削除
//**********************************************************************************

namespace Touryo.Infrastructure.Framework.Util
{
    /// <summary>Framework層のリテラル クラス</summary>
    public class FxLiteral
    {
        #region app.configのキー（とデフォルト値）

        #region 各機能

        /// <summary>共有情報定義のXML</summary>
        public const string XML_SP_DEFINITION = "FxXMLSPDefinition";

        /// <summary>メッセージ定義のXML</summary>
        public const string XML_MSG_DEFINITION = "FxXMLMSGDefinition";
        
        /// <summary>国際化対応のスイッチ（業務メッセージ）</summary>
        public const string BUSINESSMESSAGECULTUER = "FxBusinessMessageCulture";

        #endregion

        #endregion
        
        #region 設定のリテラル

        #region 汎用定義

        /// <summary>ON / OFFのON</summary>
        public const string ON = "ON";

        /// <summary>ON / OFFのOFF</summary>
        public const string OFF = "OFF";

        /// <summary>拒否/許可の拒否</summary>
        public const string DENY = "DENY";

        /// <summary>拒否/許可の許可</summary>
        public const string ALLOW = "ALLOW";

        #endregion

        #endregion

        #region 値文字列

        /// <summary>Boolean：true</summary>
        public const string VALUE_STR_TRUE = "TRUE";

        /// <summary>Boolean：false</summary>
        public const string VALUE_STR_FALSE = "FALSE";

        /// <summary>null</summary>
        public const string VALUE_STR_NULL = "NULL";

        /// <summary>DUMMY文字列</summary>
        public const string VALUE_STR_DUMMY_STRING = "dummy";

        #endregion

        #region XML定義のリテラル

        #region タグ
        
        #region メッセージ、共有情報定義

        /// <summary>Messageタグ</summary>
        public const string XML_MSG_TAG_MESSAGE = "Message";

        /// <summary>SharedPropタグ</summary>
        public const string XML_SP_TAG_SHARED_PROPERTY = "SharedProp";

        #endregion

        #endregion

        #region 属性

        #region 汎用属性

        /// <summary>汎用属性（id）</summary>
        public const string XML_CMN_ATTR_ID = "id";

        /// <summary>汎用属性（key）</summary>
        public const string XML_CMN_ATTR_KEY = "key";

        /// <summary>汎用属性（value）</summary>
        public const string XML_CMN_ATTR_VALUE = "value";

        #endregion
        
        #region メッセージ、共有情報定義

        /// <summary>メッセージ内容</summary>
        public const string XML_MSG_ATTR_DESCRIPTION = "description";

        #endregion

        #endregion

        #endregion
    }
}
