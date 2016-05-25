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
//* クラス名        ：TestParameterValue
//* クラス日本語名  ：TestParameterValue
//*
//* 作成者          ：生技 西野
//* 更新履歴        ：
//*
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  20xx/xx/xx  ＸＸ ＸＸ         新規作成
//*  2016/04/22  Supragyan        Created TestParameterValue class to pass Azure SQL parameters
//**********************************************************************************

// ベースクラス
using Touryo.Infrastructure.Business.Util;
using Touryo.Infrastructure.Business.Common;

namespace MyType
{
    /// <summary>
    /// TestParameterValue の概要の説明です
    /// </summary>
    public class TestParameterValue : MyParameterValue
    {
        /// <summary>汎用エリア</summary>
        public object Obj;

        /// <summary>CustomerId</summary>
        public int CustomerId;

        /// <summary>Name</summary>
        public string Name;

        /// <summary>RegionId</summary>
        public int RegionId;

        /// <summary>ProductId</summary>
        public int ProductId;

        #region コンストラクタ

        /// <summary>コンストラクタ</summary>
        public TestParameterValue(string screenId, string controlId, string methodName, string actionType, MyUserInfo user)
            : base(screenId, controlId, methodName, actionType, user)
        {
            // Baseのコンストラクタに引数を渡すために必要。
        }
        #endregion
    }
}
