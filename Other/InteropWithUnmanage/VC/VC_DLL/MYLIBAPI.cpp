// このソースコードの関数と変数をエクスポート
#define MYLIBAPI extern "C" __declspec(dllexport)

#include <stdafx.h>
#include <windows.h>
#include <stdio.h>

// エクスポート関数の宣言
#include "MYLIBAPI.h"

// 以下、エクスポート関数の実装
// 通常、__stdcallを適用する(__stdcall = WINAPI)。
void __stdcall Test_MYLIBAPI(LPCWSTR lpText, LPCWSTR lpCaption)
{
	// MessageBoxを呼び出すだけ。
	MessageBox(NULL, lpText, lpCaption, MB_OK);
}