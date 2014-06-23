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

