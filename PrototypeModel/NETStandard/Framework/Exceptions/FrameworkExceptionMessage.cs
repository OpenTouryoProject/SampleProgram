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
//* クラス名        ：FrameworkExceptionMessage
//* クラス日本語名  ：フレームワーク層の例外のメッセージＩＤ、メッセージに使用する
//*                   文字列定数を定義する定数クラス（フレームワーク用）
//*
//* 作成者          ：生技 西野
//* 更新履歴        ：
//*
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  2007/xx/xx  西野 大介         新規作成
//*  2009/04/21  西野 大介         FrameworkExceptionの追加に伴い、名称変更
//*  2009/06/02  西野 大介         sln - IR版からの修正
//*                                ・#7, 8, 9 ： 「エラーメッセージ：」
//*                                ・#14 ： XMLチェック処理追加
//*                                ・#15 ： XML要素のリテラル化
//*  2009/07/21  西野 大介         コントロール取得処理の仕様変更
//*  2009/07/21  西野 大介         マスタ ページのネストに対応
//*  2009/07/31  西野 大介         セッション情報の自動削除機能を追加
//*  2009/07/31  西野 大介         不正操作の検出機能を追加
//*  2010/09/24  西野 大介         ジェネリック対応（XMLのDictionary化）
//*                                nullチェック方法、Contains → ContainsKeyなどに注意
//*  2010/10/29  西野 大介         RichClientフレームワークの分割によりアクセス修飾子を変更
//*  2010/11/15  西野 大介         RichClientフレームワーク（非同期呼び出し）のメッセージを追加
//*  2010/12/21  西野 大介         RichClientフレームワーク（非同期イベント）のメッセージを追加
//*  2011/01/14  西野 大介         GetPropsFromPropStringをPubCmnFunctionに移動
//*  2011/10/09  西野 大介         国際化対応
//*  2013/12/23  西野 大介         アクセス修飾子をすべてpublicに変更した。
//*  2014/01/18  Rituparna.Biswas  国際化対応の見直し。
//*  2014/01/22  Rituparna.Biswas  Changes from ConfigurationManager.AppSettings to GetConfigParameter.GetConfigValue in CmnFunc
//*  2014/02/03  西野 大介         取り込み：リソースファイル名とスイッチ名の変更、#pragma warning disableの追加。
//**********************************************************************************

using System;
using System.Resources;
using System.Globalization;

using Touryo.Infrastructure.Framework.Resources;
using Touryo.Infrastructure.Framework.Util;
using Touryo.Infrastructure.Public.Util;

namespace Touryo.Infrastructure.Framework.Exceptions
{
    /// <summary>
    /// フレームワーク層の例外のメッセージＩＤ、メッセージに使用する
    /// 文字列定数を定義する定数クラス（フレームワーク用）
    /// </summary>
    public class FrameworkExceptionMessage
    {
        #region 定義不正

        #region メッセージ定義ファイルの不正

        /// <summary>メッセージ定義ファイルの不正</summary>
        public static string[] MESSAGE_XML_FORMAT_ERROR
        {
            get
            {
                string temp = "";
                // Get current property name.
                //string key = PubCmnFunction.GetCurrentMethodName();
                string key = PubCmnFunction.GetCurrentPropertyName();

                // Stores the specified string resource for the specified culture or current UI culture.
                temp = FrameworkExceptionMessage.CmnFunc(key);
                //Returns the specified string resource for the specified culture or current UI culture.
                return new string[] { "MessageXMLFormatError", temp };
            }
        }

        /// <summary>メッセージ定義ファイルの不正（メッセージ補足）</summary>
        public static string MESSAGE_XML_FORMAT_ERROR_ATTR
        {
            get
            {

                // Get current property name.
                //string key = PubCmnFunction.GetCurrentMethodName();
                string key = PubCmnFunction.GetCurrentPropertyName();

                // Returns the specified string resource for the specified culture or current UI culture.
                return FrameworkExceptionMessage.CmnFunc(key);
            }
        }

        #endregion

        #region 共有情報定義ファイルの不正

        /// <summary>共有情報定義ファイルの不正</summary>
        public static string[] SHAREDPROPERTY_XML_FORMAT_ERROR
        {
            get
            {
                string temp = "";
                // Get current property name.
                //string key = PubCmnFunction.GetCurrentMethodName();
                string key = PubCmnFunction.GetCurrentPropertyName();

                // Stores the specified string resource for the specified culture or current UI culture.
                temp = FrameworkExceptionMessage.CmnFunc(key);

                // Returns the specified string resource for the specified culture or current UI culture.
                return new string[] { "SharedPropertyXMLFormatError", temp };
            }
        }

        /// <summary>共有情報定義ファイルの不正（メッセージ補足）</summary>
        public static string SHAREDPROPERTY_XML_FORMAT_ERROR_ATTR
        {
            get
            {
                // Get current property name.
                //string key = PubCmnFunction.GetCurrentMethodName();
                string key = PubCmnFunction.GetCurrentPropertyName();

                // Returns the specified string resource for the specified culture or current UI culture.
                return FrameworkExceptionMessage.CmnFunc(key);
            }
        }

        #endregion

        #endregion

        #region 共通

        #region 初回起動時チェックエラー

        /// <summary>フレームワークで必要なHIDDENタグが実装されていない場合。</summary>
        public static string[] NO_FX_HIDDEN
        {
            get
            {
                string temp = "";
                // Get current property name.
                //string key = PubCmnFunction.GetCurrentMethodName();
                string key = PubCmnFunction.GetCurrentPropertyName();

                // Stores the specified string resource for the specified culture or current UI culture.
                temp = FrameworkExceptionMessage.CmnFunc(key);

                // Returns the specified string resource for the specified culture or current UI culture.
                return new string[] { "NoFxHidden", temp };
            }
        }

        /// <summary>フレームワークの数値情報が正しく設定されていない場合。</summary>/*34/
        public static string[] ERROR_IN_WRITING_OF_FX_NUMVAL
        {
            get
            {
                string temp = "";
                // Get current property name.
                //string key = PubCmnFunction.GetCurrentMethodName();
                string key = PubCmnFunction.GetCurrentPropertyName();

                // Returns the specified string resource for the specified culture or current UI culture.
                temp = FrameworkExceptionMessage.CmnFunc(key);

                // Returns the specified string resource for the specified culture or current UI culture.
                return new string[] { "ErrorInWritingOfFxNumVal", temp };
            }
        }

        /// <summary>フレームワークのパス情報が設定されていない場合。</summary>
        public static string[] ERROR_IN_WRITING_OF_FX_PATH1
        {
            get
            {
                string temp = "";
                // Get current property name.
                //string key = PubCmnFunction.GetCurrentMethodName();
                string key = PubCmnFunction.GetCurrentPropertyName();

                // Returns the specified string resource for the specified culture or current UI culture.
                temp = FrameworkExceptionMessage.CmnFunc(key);

                return new string[] { "ErrorInWritingOfFxPath1", temp };
            }
        }

        /// <summary>フレームワークのパス情報が間違っている場合。</summary>
        public static string[] ERROR_IN_WRITING_OF_FX_PATH2
        {
            get
            {
                string temp = "";
                // Get current property name.
                //string key = PubCmnFunction.GetCurrentMethodName();
                string key = PubCmnFunction.GetCurrentPropertyName();

                // Stores the specified string resource for the specified culture or current UI culture.
                temp = FrameworkExceptionMessage.CmnFunc(key);

                // Returns the specified string resource for the specified culture or current UI culture.
                return new string[] { "ErrorInWritingOfFxPath2", temp };
            }
        }

        /// <summary>フレームワークのスイッチが正しく設定されていない場合（on or off）。</summary>
        public static string[] ERROR_IN_WRITING_OF_FX_SWITCH1
        {
            get
            {
                string temp = "";
                // Get current property name.
                //string key = PubCmnFunction.GetCurrentMethodName();
                string key = PubCmnFunction.GetCurrentPropertyName();

                // Stores the specified string resource for the specified culture or current UI culture.
                temp = FrameworkExceptionMessage.CmnFunc(key);

                // Returns the specified string resource for the specified culture or current UI culture.
                return new string[] { "ErrorInWritingOfFxSwitch1", temp };
            }
        }

        /// <summary>フレームワークのスイッチが正しく設定されていない場合（true or false）。</summary>
        public static string[] ERROR_IN_WRITING_OF_FX_SWITCH2
        {
            get
            {
                string temp = "";
                // Get current property name.
                //string key = PubCmnFunction.GetCurrentMethodName();
                string key = PubCmnFunction.GetCurrentPropertyName();

                // Stores the specified string resource for the specified culture or current UI culture.
                temp = FrameworkExceptionMessage.CmnFunc(key);

                // Returns the specified string resource for the specified culture or current UI culture.
                return new string[] { "ErrorInWritingOfFxSwitch2", temp };
            }
        }

        #endregion

        #region パラメタのチェックエラー

        #region 汎用

        /// <summary>パラメタのチェックエラー</summary>
        public static string[] PARAMETER_CHECK_ERROR
        {
            get
            {
                string temp = "";
                // Get current property name.
                //string key = PubCmnFunction.GetCurrentMethodName();
                string key = PubCmnFunction.GetCurrentPropertyName();

                // Stores the specified string resource for the specified culture or current UI culture.
                temp = FrameworkExceptionMessage.CmnFunc(key);

                // Returns the specified string resource for the specified culture or current UI culture.
                return new string[] { "ParameterCheckError", temp };
            }
        }

        /// <summary>パラメタのチェックエラー（メッセージ補足１）</summary>
        public static string PARAMETER_CHECK_ERROR_null
        {
            get
            {
                // Get current property name.
                //string key = PubCmnFunction.GetCurrentMethodName();
                string key = PubCmnFunction.GetCurrentPropertyName();

                // Returns the specified string resource for the specified culture or current UI culture.
                return FrameworkExceptionMessage.CmnFunc(key);
            }
        }

        /// <summary>パラメタのチェックエラー（メッセージ補足２）</summary>
        public static string PARAMETER_CHECK_ERROR_empty
        {
            get
            {
                // Get current property name.
                //string key = PubCmnFunction.GetCurrentMethodName();
                string key = PubCmnFunction.GetCurrentPropertyName();

                // Returns the specified string resource for the specified culture or current UI culture.
                return FrameworkExceptionMessage.CmnFunc(key);
            }
        }

        /// <summary>パラメタのチェックエラー（メッセージ補足３）</summary>
        public static string PARAMETER_CHECK_ERROR_length
        {
            get
            {
                // Get current property name.
                //string key = PubCmnFunction.GetCurrentMethodName();
                string key = PubCmnFunction.GetCurrentPropertyName();

                // Returns the specified string resource for the specified culture or current UI culture.
                return FrameworkExceptionMessage.CmnFunc(key);
            }
        }

        #endregion

        #region Rich

        /// <summary>非同期呼び出しフレームワークの引数エラー</summary>
        public static string[] ASYNC_FUNC_CHECK_ERROR
        {
            get
            {
                string temp = "";

                // Get current property name.
                //string key = PubCmnFunction.GetCurrentMethodName();
                string key = PubCmnFunction.GetCurrentPropertyName();

                // Stores the specified string resource for the specified culture or current UI culture.
                temp = FrameworkExceptionMessage.CmnFunc(key);

                // Returns the specified string resource for the specified culture or current UI culture.
                return new string[] { "AsyncFunc", temp };
            }
        }

        /// <summary>非同期呼び出しフレームワークの利用API不整合</summary>
        public static string[] ASYNC_MSGBOX_ERROR
        {
            get
            {
                string temp = "";

                // Get current property name.
                //string key = PubCmnFunction.GetCurrentMethodName();
                string key = PubCmnFunction.GetCurrentPropertyName();

                // Stores the specified string resource for the specified culture or current UI culture.
                temp = FrameworkExceptionMessage.CmnFunc(key);
                // Returns the specified string resource with corrected value for the specified culture or current UI culture.
                return new string[] { "AsyncMsgBox", temp };
            }
        }

        /// <summary>非同期イベント フレームワークの引数エラー（Control）</summary>
        public static string[] ASYNC_EVENT_ENTRY_CHECK_ERROR
        {
            get
            {
                string temp = "";

                // Get current property name.
                //string key = PubCmnFunction.GetCurrentMethodName();
                string key = PubCmnFunction.GetCurrentPropertyName();

                // Stores the specified string resource for the specified culture or current UI culture.
                temp = FrameworkExceptionMessage.CmnFunc(key);
                // Returns the specified string resource  for the specified culture or current UI culture.
                return new string[] { "AsyncEventEntry", temp };
            }
        }

        /// <summary>非同期イベント フレームワークの引数エラー（Control）</summary>
        public static string[] ASYNC_EVENT_ENTRY_CONTROL_CHECK_ERROR
        {
            get
            {
                string temp = "";

                // Get current property name.
                //string key = PubCmnFunction.GetCurrentMethodName();
                string key = PubCmnFunction.GetCurrentPropertyName();

                // Stores the specified string resource for the specified culture or current UI culture.
                temp = FrameworkExceptionMessage.CmnFunc(key);
                // Returns the specified string resource  for the specified culture or current UI culture.
                return new string[] { "AsyncEventEntry_Control", temp };
            }
        }

        /// <summary>非同期イベント フレームワークの引数エラー（Callback）</summary>
        public static string[] ASYNC_EVENT_ENTRY_CALLBACK_CHECK_ERROR
        {
            get
            {
                string temp = "";

                // Get current property name.
                //string key = PubCmnFunction.GetCurrentMethodName();
                string key = PubCmnFunction.GetCurrentPropertyName();

                // Stores the specified string resource for the specified culture or current UI culture.
                temp = FrameworkExceptionMessage.CmnFunc(key);
                // Returns the specified string resource  for the specified culture or current UI culture.
                return new string[] { "AsyncEventEntry_Callback", temp };
            }
        }

        #endregion

        #endregion

        #endregion

        #region Ｂ層

        /// <summary>Ｂ層呼び出しチェック</summary>
        public static string[] LB_ILLEGAL_CALL_CHECK_ERROR
        {
            get
            {
                string temp = "";

                // Get current property name.
                //string key = PubCmnFunction.GetCurrentMethodName();
                string key = PubCmnFunction.GetCurrentPropertyName();

                // Stores the specified string resource for the specified culture or current UI culture.
                temp = FrameworkExceptionMessage.CmnFunc(key);

                // Returns the specified string resource  for the specified culture or current UI culture.
                return new string[] { "LayerBIllegalCallCheckError", temp };
            }
        }

        #endregion

        #region CmnFunc
        /// <summary>Returns the specified string resource for the specified culture or current UI culture. </summary> 
        /// <param name="key">resource key</param> 
        /// <returns>resource string</returns>
        private static string CmnFunc(string key)
        {
            // We acquire ResourceManager.
            ResourceManager rm = FrameworkExceptionMessageResource.ResourceManager;

            // We acquire a value from App.Config.
            string FxUICulture = GetConfigParameter.GetConfigValue(PubLiteral.EXCEPTIONMESSAGECULTUER); 

            if (string.IsNullOrEmpty(FxUICulture))
            {
                // When the key is not set to App.Config, we use a default culture. 
                return rm.GetString(key);
            }
            else
            {
                // When the key is set to App.Config, we use the specified culture.
                try
                {
                    CultureInfo culture = new CultureInfo(FxUICulture);
                    return rm.GetString(key, culture);
                }
#pragma warning disable
                catch (Exception ex) // There is not CultureNotFoundException in .NET3.5.
                {
#pragma warning restore
                    // When the specified culture is not an effective name, we use a default culture.
                    return rm.GetString(key);
                }
            }
        }
        #endregion

    }
}
