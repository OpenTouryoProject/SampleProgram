// VC_Client2.cpp : コンソール アプリケーションのエントリ ポイントを定義します。
//

#include "stdafx.h"
#include <windows.h>

int _tmain(int argc, _TCHAR* argv[])
{
	// DLLのハンドル
	HMODULE hModule;

	// コールバック関数の定義
	void (__stdcall *func) (LPCWSTR, LPCWSTR);

	// LoadLibraryを使用したDLL関数の呼び出し方法。

	// LoadLibraryでDLLをロード
	hModule = LoadLibrary( _T("VC_DLL.dll") );

	if( hModule == NULL )
	{
		// ロードに失敗（DLLが存在しない）
		MessageBox(NULL, L"VC_DLL.dllが見つかりません", L"エラー", MB_OK);
	}
	else
	{
		// DLL内の関数のアドレスを取得する（DEFファイルに書いた名前で呼べる）
		func = (void (__stdcall *)(LPCWSTR, LPCWSTR))GetProcAddress( hModule, "Test_MYLIBAPI" );
		
		if( func != NULL )
		{
			// 取得したアドレスを経由して呼び出す
			func(L"text", L"caption");
		}
		
		// ロードしたDLLを解放
		FreeLibrary( hModule );
	}

	return 0;
}

