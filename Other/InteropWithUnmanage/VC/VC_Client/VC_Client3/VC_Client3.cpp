// VC_Client3.cpp : コンソール アプリケーションのエントリ ポイントを定義します。

// C++でCOMクライアント
// http://www.asahi-net.or.jp/~kv8s-yjm/another/yja106.htm
// http://www.asahi-net.or.jp/~kv8s-yjm/another/yja107.htm
// http://www.asahi-net.or.jp/~kv8s-yjm/another/yja108.htm
// http://www.asahi-net.or.jp/~kv8s-yjm/another/yja109.htm
// http://www.asahi-net.or.jp/~kv8s-yjm/another/yja110.htm

// Interface 研究室
// http://www.sol.dti.ne.jp/~yoshinor/ni/ni0003.html

#include "stdafx.h"

#include "stdio.h"
#include "windows.h"
#include "objbase.h"

// 下記のインクルードではなく、tlhを自動生成
//#include "../../VC_COM/VC_COM_i.h"

//VC_COM.dllを元に、必要な定義ファイルを生成する
#import "../../VC_COM/Debug/VC_COM.dll" no_namespace named_guids raw_interfaces_only

int _tmain(int argc, _TCHAR* argv[])
{
    HRESULT hr;             // COM 戻り値用変数
    IClassTest* pClassTest; // COMインターフェイスポインタ

    // COMの初期化 ◆◆追加
	hr = ::CoInitialize(NULL);
    if(FAILED(hr)){
        printf("CoInitialize 失敗\n");
        return 0;
    }

	// ----------------------------------------------------------------------------------------------------
	// ----------------------------------------------------------------------------------------------------

	// <以下のコードは、アーリーバインディング方式での呼び出し>

	// インスタンスの作成(CLSID:[CLSID_ClassTest]とIID:[IID_IClassTest]を指定して、ポインタを取得)
    hr = ::CoCreateInstance((REFCLSID) CLSID_ClassTest, 0, CLSCTX_INPROC_SERVER,
							(REFIID) IID_IClassTest, (LPVOID*)&pClassTest);

    if(FAILED(hr)){
        printf("CoCreateInstance 失敗\n");
        return 0;
    }

	// BSTRを処理する場合は、_bstr_tが楽（解放など）
	// http://mzs184.blogspot.com/2008/04/bstrbstrt.html

	// _bstr_t Class
	// http://msdn.microsoft.com/ja-jp/library/zthfhkd6.aspx
	// _bstr_t::operator =
	// http://msdn.microsoft.com/ja-jp/library/7bh2f8sk.aspx
	_bstr_t bstrText =  L"だいすけ";
	_bstr_t bstrCaption = L"にしの";
	_bstr_t bstrRetVal;

	// メソッド呼び出し

	// _bstr_t::wchar_t*, _bstr_t::char*
	// BSTRはOLECHAR（= WCHAR）のポインタなので適用可能。
	// http://msdn.microsoft.com/ja-jp/library/btdzb8eb.aspx
	// _bstr_t::GetAddress
	// http://msdn.microsoft.com/ja-jp/library/t2x13207.aspx
	hr = pClassTest->MethodTest(bstrText, bstrCaption, bstrRetVal.GetAddress());

	if(FAILED(hr)){
        printf("MethodTest 失敗\n");
        return 0;
    }

	MessageBox(NULL, bstrRetVal, L"戻り値", MB_OK);

	// bstrRetValをクリア（DetachしてSysFreeString）

	// _bstr_t::Detach
    // http://msdn.microsoft.com/en-us/library/3c73x1sf.aspx
	// SysFreeString
	// http://msdn.microsoft.com/ja-jp/site/ms221481

	BSTR bstr = bstrRetVal.Detach();

	if(bstr != NULL)
	{
		SysFreeString(bstr);
	}

	// bstrCaptionの再設定
	bstrCaption = L"";

	// メソッド呼び出し（同上）
	hr = pClassTest->MethodTest(bstrText, bstrCaption, bstrRetVal.GetAddress());
	
	// Dr.GUI Online
	// Dr.GUI と COM オートメーション、
	// 第 3 部：続 COM のすばらしきデータ型
	// http://msdn.microsoft.com/ja-jp/library/cc482694.aspx
	// ISupportErrorInfo、IErrorInfoでエラー情報を取得

	if(FAILED(hr))
	{
		// IID_ISupportErrorInfoインターフェイスを取得
		ISupportErrorInfo *pSupport;
		hr = pClassTest->QueryInterface(IID_ISupportErrorInfo, (void**)&pSupport);

		if (SUCCEEDED(hr)) {

			hr = pSupport->InterfaceSupportsErrorInfo(IID_IClassTest);

			if (hr == S_OK) { // can't use SUCCEEDED here! S_FALSE succeeds!

				IErrorInfo *pErrorInfo;
				hr = GetErrorInfo(0, &pErrorInfo);

				if (SUCCEEDED(hr)) {
					// FINALLY can call methods on pErrorInfo! ...and handle the error!

					_bstr_t bstrErrorDescription;
					pErrorInfo->GetDescription(bstrErrorDescription.GetAddress());

					// エラー情報
					MessageBox(NULL, bstrErrorDescription, L"ErrorDescription", MB_OK);

					// don't forget to release!
					pErrorInfo->Release();
				}
			}

			// don't forget to release!
			pSupport->Release();
		}
	}

	// don't forget to release!
	pClassTest->Release();

	// ----------------------------------------------------------------------------------------------------
	// ----------------------------------------------------------------------------------------------------

	// <以下のコードは、レイトバインディング方式での呼び出し>

	// CLSID の取得
    CLSID clsid;
    hr = CLSIDFromProgID(L"VC_COM.ClassTest", &clsid);

    if(FAILED(hr)){
        printf("CLSIDFromProgID 失敗\n");
        return 0;
    }

	// インスタンスの作成(CLSIDとIID:[IID_IDispatch]を指定して、ポインタを取得)
    IDispatch* pDisp = NULL;
    hr = ::CoCreateInstance(clsid, NULL, CLSCTX_ALL, IID_IDispatch, (void**)&pDisp);

    if(FAILED(hr)){
        printf("CoCreateInstance 失敗\n");
        return 0;
    }
	
	// COMディスパッチ識別子の取得
	DISPID dispID;
	OLECHAR* wszName = L"MethodTest";
	hr = pDisp->GetIDsOfNames(IID_NULL, &wszName, 1, LOCALE_USER_DEFAULT, &dispID);

	if(FAILED(hr)){
        printf("GetIDsOfNames 失敗\n");
        exit(1);
    }
	
	// CComVariant クラス（CComVariant は VARIANT 型から派生）
	// http://msdn.microsoft.com/ja-jp/library/ac97df2h.aspx

	// 引数を VARIANT 配列に設定
    CComVariant pvArgs[2];
	pvArgs[0] = L"だいすけ";
	pvArgs[1] = L"にしの";

	// 戻り値を VARIANT変数
	CComVariant pvResult;

	// DISPPARAMS の設定
    DISPPARAMS dispParams;

    dispParams.rgvarg = pvArgs;
    dispParams.rgdispidNamedArgs = NULL;
    dispParams.cArgs = 2;
    dispParams.cNamedArgs = 0;

	// メソッドにレイトバインド（Invoke）
	hr = pDisp->Invoke(dispID, IID_NULL,
		LOCALE_USER_DEFAULT, DISPATCH_METHOD,
		&dispParams, &pvResult, NULL, NULL);

	if(FAILED(hr)){
        printf("MethodTest 失敗\n");
        return 0;
    }

	// BSTRで格納されている時、tagVARIANTのメンバ、bstrValで取得可能。
	// BSTRはOLECHAR（= WCHAR）のポインタなのでLPWSTRにキャスト可能。
	MessageBox(NULL, (LPWSTR)pvResult.bstrVal, L"戻り値", MB_OK);

	// don't forget to release!
	pDisp->Release();

	// ----------------------------------------------------------------------------------------------------
	// ----------------------------------------------------------------------------------------------------

    // COMの終了処理 ◆◆追加
    ::CoUninitialize();

    return 0;
}