//**********************************************************************************
//* Copyright (C) 2007,2014 Hitachi Solutions,Ltd.
//**********************************************************************************

//#region Apache License
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
//#endregion

// ClassTest.h : CClassTest の宣言

#pragma once
#include "resource.h"       // メイン シンボル

#include "VC_COM_i.h"


#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "DCOM の完全サポートを含んでいない Windows Mobile プラットフォームのような Windows CE プラットフォームでは、単一スレッド COM オブジェクトは正しくサポートされていません。ATL が単一スレッド COM オブジェクトの作成をサポートすること、およびその単一スレッド COM オブジェクトの実装の使用を許可することを強制するには、_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA を定義してください。ご使用の rgs ファイルのスレッド モデルは 'Free' に設定されており、DCOM Windows CE 以外のプラットフォームでサポートされる唯一のスレッド モデルと設定されていました。"
#endif

// ATL で作成した Active X コントロールのサンプル
// http://www.koutou-software.co.jp/junk/atl-actxctrl-sample.html
// （コントロールが安全であるとマーキング：IObjectSafety）

// CClassTest

class ATL_NO_VTABLE CClassTest :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CClassTest, &CLSID_ClassTest>,
	public ISupportErrorInfo,
	public IDispatchImpl<IClassTest, &IID_IClassTest, &LIBID_VC_COMLib, /*wMajor =*/ 1, /*wMinor =*/ 0>,
	public IObjectSafetyImpl<CClassTest, INTERFACESAFE_FOR_UNTRUSTED_CALLER | INTERFACESAFE_FOR_UNTRUSTED_DATA> // IObjectSafety対応で追加
{
public:
	CClassTest()
	{
	}

DECLARE_REGISTRY_RESOURCEID(IDR_CLASSTEST)


BEGIN_COM_MAP(CClassTest)
	COM_INTERFACE_ENTRY(IClassTest)
	COM_INTERFACE_ENTRY(IDispatch)
	COM_INTERFACE_ENTRY(ISupportErrorInfo)
	COM_INTERFACE_ENTRY(IObjectSafety) // IObjectSafety対応で追加
END_COM_MAP()

// ISupportsErrorInfo
	STDMETHOD(InterfaceSupportsErrorInfo)(REFIID riid);


	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct()
	{
		return S_OK;
	}

	void FinalRelease()
	{
	}

public:

	STDMETHOD(MethodTest)(BSTR bstrText, BSTR bstrCaption, BSTR* rtnval);
};

OBJECT_ENTRY_AUTO(__uuidof(ClassTest), CClassTest)
