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
//* クラス名        ：LayerB
//* クラス日本語名  ：LayerB
//*
//* 作成者          ：生技 西野
//* 更新履歴        ：
//*
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  20xx/xx/xx  ＸＸ ＸＸ         新規作成
//*  2016/04/22  Supragyan        Created LayerB class to support Azure SQL functionality
//**********************************************************************************

// Touryo
using Touryo.Infrastructure.Business.Business;

// MyType
using MyType;

/// <summary>
/// LayerB の概要の説明です
/// </summary>
public class LayerB : MyFcBaseLogic
{
    #region Insert

    /// <summary>業務処理を実装</summary>
    /// <param name="testParameter">引数クラス</param>
    private void UOC_Insert(TestParameterValue testParameter)
    {
        // 戻り値クラスを生成して、事前に戻り地に設定しておく。
        TestReturnValue testReturn = new TestReturnValue();
        this.ReturnValue = testReturn;

        // ↓業務処理-----------------------------------------------------
        // 個別Daoを使用する。
        LayerD myDao = new LayerD(this.GetDam());
        myDao.Insert(testParameter, testReturn);
    }

    #endregion

    #region SelectAll_DR

    /// <summary>業務処理を実装</summary>
    /// <param name="testParameter">引数クラス</param>
    private void UOC_SelectAll_DR(TestParameterValue testParameter)
    {
        // 戻り値クラスを生成して、事前に戻り地に設定しておく。
        TestReturnValue testReturn = new TestReturnValue();
        this.ReturnValue = testReturn;

        LayerD myDao = new LayerD(this.GetDam());
        myDao.SelectAll_DR(testParameter, testReturn);
    }

    #endregion

    #region SelectByCustomer

    /// <summary>Select data by customerId</summary>
    /// <param name="testParameter">testParameter</param>
    private void UOC_SelectByCustomer(TestParameterValue testParameter)
    {
        TestReturnValue testReturn = new TestReturnValue();
        this.ReturnValue = testReturn;

        LayerD myDao = new LayerD(this.GetDam());
        myDao.SelectByCustomer(testParameter, testReturn);
    }

    #endregion

    #region Delete

    /// <summary>Delete the specific customers data</summary>
    /// <param name="testParameter">testParameter</param>
    private void UOC_Delete(TestParameterValue testParameter)
    {
        TestReturnValue testReturn = new TestReturnValue();
        this.ReturnValue = testReturn;

        LayerD myDao = new LayerD(this.GetDam());
        myDao.Delete(testParameter, testReturn);
    }

    #endregion
}
