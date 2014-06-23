// CallerEXE.cpp : コンソール アプリケーションのエントリ ポイントを定義します。
//

#include "stdafx.h"
#include <windows.h>

int _tmain(int argc, _TCHAR* argv[])
{
	// DLLのハンドル
	HMODULE hModule;

	// コールバック関数の定義
	LPCWSTR (__stdcall *func) (LPCWSTR);

	// LoadLibraryを使用したDLL関数の呼び出し方法。

	// LoadLibraryでDLLをロード
	hModule = LoadLibrary( _T("BridgeDLL.dll") );

	if( hModule == NULL )
	{
		// ロードに失敗（DLLが存在しない）
		MessageBox(NULL, L"BridgeDLL.dllが見つかりません", L"エラー", MB_OK);
	}
	else
	{
		// DLL内の関数のアドレスを取得する（DEFを使用していないので）
		func = (LPCWSTR (__stdcall *)(LPCWSTR))GetProcAddress( hModule, "_TestMethod@4" );

		if( func != NULL )
		{
			// 取得したアドレスを経由して呼び出す
			MessageBox(NULL, func(L"アンマネージからマネージ！！！"), L"戻り値", MB_OK);
		}
		
		// ロードしたDLLを解放
		FreeLibrary( hModule );
	}

	return 0;
}

