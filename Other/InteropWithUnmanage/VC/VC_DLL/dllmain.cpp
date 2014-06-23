// dllmain.cpp : DLL アプリケーションのエントリ ポイントを定義します。

#include "stdafx.h"
#include <windows.h>
#include <stdio.h>

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
					 )
{
	switch (ul_reason_for_call)
	{
		case DLL_PROCESS_ATTACH:
			printf("DLL_PROCESS_ATTACH\r\n");
			break;
		case DLL_THREAD_ATTACH:
			printf("DLL_THREAD_ATTACH\r\n");
			break;
		case DLL_THREAD_DETACH:
			printf("DLL_THREAD_DETACH\r\n");
			break;
		case DLL_PROCESS_DETACH:
			printf("DLL_PROCESS_DETACH\r\n");
			break;
	}
	return TRUE;
}

