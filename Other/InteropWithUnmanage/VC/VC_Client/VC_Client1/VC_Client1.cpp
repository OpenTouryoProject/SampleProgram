// VC_Client1.cpp : コンソール アプリケーションのエントリ ポイントを定義します。
//

#include "stdafx.h"
#include <windows.h>
#include "../../VC_DLL/MYLIBAPI.h"

int _tmain(int argc, _TCHAR* argv[])
{
	// 通常のLibを使用したDLL関数の呼び出し方法。
	Test_MYLIBAPI(L"text", L"caption");

	return 0;
}