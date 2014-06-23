// ClassTest.cpp : CClassTest の実装

#include "stdafx.h"
#include "ClassTest.h"

// ＜参考サイト＞
// COMを作って使う（超簡単例：VCで作成、VBSで実行）
// http://www.dinop.com/vc/com003.html
// 【VC++】 COM の作成の基本
// http://maglog.jp/lightbox/Article437075.html
// ATL で作成した Active X コントロールのサンプル
// http://www.koutou-software.co.jp/junk/atl-actxctrl-sample.html
// COM - Hirotake Itoh's memo by PukiWiki
// http://aki.issp.u-tokyo.ac.jp/itoh/PukiWiki/pukiwiki.php?COM

// ATL で作成した Active X コントロールのサンプル
// http://www.koutou-software.co.jp/junk/atl-actxctrl-sample.html
// （コントロールが安全であるとマーキング：IObjectSafety）

#include "atlstr.h"

// CClassTest

STDMETHODIMP CClassTest::InterfaceSupportsErrorInfo(REFIID riid)
{
	static const IID* arr[] = 
	{
		&IID_IClassTest
	};

	for (int i=0; i < sizeof(arr) / sizeof(arr[0]); i++)
	{
		if (InlineIsEqualGUID(*arr[i],riid))
			return S_OK;
	}
	return S_FALSE;
}

STDMETHODIMP CClassTest::MethodTest(BSTR bstrText, BSTR bstrCaption, BSTR* rtnval)
{
	// TODO: ここに実装コードを追加してください。

	// BSTRはOLECHAR（= WCHAR）のポインタ
	// typedef OLECHAR FAR * BSTR;
	// http://msdn.microsoft.com/ja-jp/site/ms221069

	// Web/DB プログラミング徹底解説
	// Windows 徹底解説 > COM+ の話題 > BSTR とは？
	// http://keicode.com/com/bstr-alloc-free.php

	// CAtlString：CSimpleStringを継承した
	// テンプレートクラス CStringT を実体化した物
	// http://myoga.web.fc2.com/prog/cpplib/shared01.htm

	CAtlString casText;
	CAtlString casCaption;
	
	// CStringT::operator =
	// http://msdn.microsoft.com/ja-jp/library/wxeexhs7.aspx
	casText = (WCHAR*)bstrText;
	casCaption = (WCHAR*)bstrCaption;

	// VBのErr.Raise的な。
	if(casCaption == L"")
	{
		return Error(TEXT("bstrCaptionが空です。"));
	}

	// CAtlString → LPTSTR
	// CSimpleStringT::operator PCXSTR
	// http://msdn.microsoft.com/ja-jp/library/tk1z2hd9.aspx
	MessageBox(NULL, casText, casCaption, MB_OK);
	
	// rtnvalはCOMのretval

	// How to pass BSTR* as the return value
	// from the ATL\COM application to vb6.0 application.
	// http://social.msdn.microsoft.com/Forums/en/vclanguage/thread/0cbb0f96-5d2d-43e9-a9c4-86af905cfe0f

	// SysAllocStringはOLECHAR（= WCHAR）のポインタを返す。
	// http://msdn.microsoft.com/ja-jp/site/ms221458
	*rtnval = SysAllocString( L"成功！" );

	return S_OK;
}
