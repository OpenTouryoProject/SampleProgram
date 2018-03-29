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
//* クラス名        ：FxHttpSessionIndex
//* クラス日本語名  ：Sessionのインデックスに使用する文字列定数を定義する定数クラス
//*
//* 作成者          ：生技 西野
//* 更新履歴        ：
//*
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  2007/xx/xx  西野 大介         新規作成
//*  2009/03/13  西野 大介         ラベル名の変更、ラベルの追加
//*                                ・・・
//*  2015/10/29  Sai               Added constant string to store dummy key and value.
//*  2018/03/29  西野 大介         .NET Standard対応で、削除機能に関連するリテラルを削除
//**********************************************************************************

namespace Touryo.Infrastructure.Framework.Util
{
    /// <summary>Sessionのインデックスに使用する文字列定数を定義する</summary>
    public static class FxHttpSessionIndex
    {
        /// <summary>認証ユーザ情報を格納するSessionキー</summary>
        public const string AUTHENTICATION_USER_INFORMATION = "AuthenticationUserInformation";

        /// <summary>サブシステム情報を格納するSessionキー</summary>
        public const string SUB_SYSTEM_INFORMATION = "SubSystemInformation";

        /// <summary> Session key that contains the dummy value. </summary>
        public const string DUMMY = "dummy";
    }
}
