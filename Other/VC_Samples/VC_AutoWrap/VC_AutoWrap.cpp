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

// VC_AutoWrap.cpp : コンソール アプリケーションのエントリ ポイントを定義します。

#include "stdafx.h"
#include "atlstr.h"

#include <locale.h> 

//////////////////////////////////////////////////////////////////////////////////////////////////
// AutoWrap() - Automation helper function...（IDispatch::Invokeでレイトバインドする）
////////////////////////////////////////////////////////////////////////////////////////////////// 
// [HOWTO] Visual C++ を使用してオートメーションで文書プロパティにアクセスする方法
// http://support.microsoft.com/kb/238393/ja
////////////////////////////////////////////////////////////////////////////////////////////////// 
HRESULT AutoWrap(int autoType, VARIANT *pvResult, IDispatch *pDisp, LPOLESTR ptName, int cArgs...) 
{

	// Begin variable-argument list...
	va_list marker;
	va_start(marker, cArgs);

	if(!pDisp) {
		printf("NULL IDispatch passed to AutoWrap\n");
		//MessageBox(NULL, "NULL IDispatch passed to AutoWrap()", L"Error", 0x10010);
		_exit(0);
	}

	// Variables used...
	DISPPARAMS dp = { NULL, NULL, 0, 0 };
	DISPID dispidNamed = DISPID_PROPERTYPUT;
	DISPID dispID;
	HRESULT hr;
	char buf[200];
	char szName[200];
   
	// Convert down to ANSI
	WideCharToMultiByte(CP_ACP, 0, ptName, -1, szName, 256, NULL, NULL);
   
	// Get DISPID for name passed...
	hr = pDisp->GetIDsOfNames(IID_NULL, &ptName, 1, LOCALE_USER_DEFAULT, &dispID);

	if(FAILED(hr)) {
		sprintf(buf, "IDispatch::GetIDsOfNames(\"%s\") failed w/err0x%08lx", szName, hr);
		//MessageBox(NULL, buf, "AutoWrap()", 0x10010);
		_exit(0);
		return hr;
	}
   
	// Allocate memory for arguments...
	VARIANT *pArgs = new VARIANT[cArgs+1];
	// Extract arguments...
	for(int i=0; i<cArgs; i++) {
		pArgs[i] = va_arg(marker, VARIANT);
	}
   
	// Build DISPPARAMS
	dp.cArgs = cArgs;
	dp.rgvarg = pArgs;
   
	// Handle special-case for property-puts!
	if(autoType & DISPATCH_PROPERTYPUT) {
		dp.cNamedArgs = 1;
		dp.rgdispidNamedArgs = &dispidNamed;
	}
   
	// Make the call!
	hr = pDisp->Invoke(dispID, IID_NULL, LOCALE_SYSTEM_DEFAULT, autoType, &dp, pvResult, NULL, NULL);
	
	if(FAILED(hr)) {
		sprintf(buf, "IDispatch::Invoke(\"%s\"=%08lx) failed w/err 0x%08lx", szName, dispID, hr);
		//MessageBox(NULL, buf, "AutoWrap()", 0x10010);
		_exit(0);
		return hr;
	}

	// End variable-argument section...
	va_end(marker);
   
	delete [] pArgs;
   
	return hr;
}

// 
//////////////////////////////////////////////////////////////////////////////////////////////////
// _tmain()
////////////////////////////////////////////////////////////////////////////////////////////////// 
// VC_AutoWrap.cpp : コンソール アプリケーションのエントリ ポイントを定義します。
//////////////////////////////////////////////////////////////////////////////////////////////////
int _tmain(int argc, _TCHAR* argv[])
{
	// TODO: ここに実装コードを追加してください。

	// http://cx5software.com/article_vcpp_unicode/
	// OSのデフォルトロケール
	_tsetlocale(LC_ALL, _T(""));
	
	// printfのテスト
	printf("Hello World!\n"); 
	printf("こんにちは世界!\n");
	printf("%s\n", argv[1]); 
	wprintf(L"%s\n", argv[1]); 
	_tprintf(_T("%s\n"), argv[1]);

	// Initialize COM for this thread...
    CoInitialize(NULL);

    // Get CLSID for Word.Application...
    CLSID clsid;
    HRESULT hr = CLSIDFromProgID(L"Excel.Application", &clsid);
    
	if(FAILED(hr)) {
      //::MessageBox(NULL, "CLSIDFromProgID() failed", "Error", 0x10010);
      return -1;
    }
    
	// Start Excel and get IDispatch...
    IDispatch *pDisp;
    hr = CoCreateInstance(clsid, NULL, CLSCTX_LOCAL_SERVER, IID_IDispatch, (void **)&pDisp);

    if(FAILED(hr)) {
      //::MessageBox(NULL, "Word not registered properly", "Error", 0x10010);
      return -2;
    }

	//----------------------------------------------------------------------------------------------------

	// プロパティ（set）呼び出し（Excel.Application.DisplayAlerts）
    {
		// 引数
		VARIANT x;
		x.vt = VT_BOOL;
		x.boolVal = VARIANT_FALSE;
		
		// DisplayAlertsプロパティの設定
		AutoWrap(DISPATCH_PROPERTYPUT, NULL, pDisp, L"DisplayAlerts", 1, x);
	}
	
	// プロパティ（get）呼び出し（Excel.Application.Workbooks）
	IDispatch *pWorkbooks;
    {
		// 戻り値
		VARIANT result;
		VariantInit(&result);
		
		// Workbooksプロパティの取得
		AutoWrap(DISPATCH_PROPERTYGET, &result, pDisp, L"Workbooks", 0);
		pWorkbooks = result.pdispVal;
    }

	// メソッド呼び出し（Excel.Application.Workbooks.Open）
	{
		// 引数
		VARIANT x;
		x.vt = VT_BSTR;
		BSTR _bstr = SysAllocString((OLECHAR *)argv[1]);
		x.bstrVal = _bstr;

		// 戻り値
		VARIANT result;
		VariantInit(&result);

		// Openメソッドの呼び出し
		AutoWrap(DISPATCH_METHOD, &result, pWorkbooks, L"Open", 1, x);

		SysFreeString(_bstr);
	}

	// プロパティ呼び出し（Excel.Application.ActiveWorkbook）
	IDispatch *pActiveWorkbook;
    {
		// 戻り値
		VARIANT result;
		VariantInit(&result);

		// ActiveWorkbookプロパティの取得
		AutoWrap(DISPATCH_PROPERTYGET, &result, pDisp, L"ActiveWorkbook", 0);
		pActiveWorkbook = result.pdispVal;
    }

	// メソッド呼び出し（Excel.Application.ActiveWorkbook.SaveAs）
	{
		// 引数
		VARIANT x1;
		x1.vt = VT_BSTR;
		BSTR _bstr = SysAllocString((OLECHAR *)argv[2]);
		x1.bstrVal = _bstr;
		
		VARIANT x2;
		x2.vt = VT_INT;
		x2.intVal = -4143;

		VARIANT x3;
		x3.vt = VT_BSTR;
		x3.bstrVal = L"test";

		// 戻り値
		VARIANT result;
		VariantInit(&result);

		// SaveAsメソッドの呼び出し
		AutoWrap(DISPATCH_METHOD, &result, pActiveWorkbook, L"SaveAs", 3, x3, x2, x1);

		SysFreeString(_bstr);
	}

	// プロパティ呼び出し（Excel.Application.DisplayAlerts）
    {
		// 引数
		VARIANT x;
		x.vt = VT_BOOL;
		x.boolVal = VARIANT_TRUE;

		// DisplayAlertsプロパティの設定
		AutoWrap(DISPATCH_PROPERTYPUT, NULL, pDisp, L"DisplayAlerts", 1, x);
    }

	// メソッド呼び出し（Excel.Application.Quit）
	{
		// 引数
		VARIANT result;
		VariantInit(&result);

		// Quitメソッドの呼び出し
		AutoWrap(DISPATCH_METHOD, &result, pDisp, L"Quit", 0);
    }

	//----------------------------------------------------------------------------------------------------

	// don't forget to release!
	pDisp->Release();

    // COMの終了処理 ◆◆追加
    ::CoUninitialize();

	return 0;
}

