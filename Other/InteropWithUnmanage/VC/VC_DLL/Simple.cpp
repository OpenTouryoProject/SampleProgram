// 簡単な方式（明示的ロードが必要）
// VC_DLL.cpp : DLL アプリケーション用にエクスポートされる関数を定義します。

#include "stdafx.h"
#include <stdio.h>

// 以下は、
// The try, catch, and throw Statements
// http://msdn.microsoft.com/en-us/library/6dekhbbc(VS.80).aspx
// から引用

class CTest {
public:
   CTest() {};
   ~CTest() {};
   const char *ShowReason() const { 
      return "Exception in CTest class."; 
   }
};

extern "C"{
	typedef struct _A{
		int m1;
		char m2_in[12]; // 11文字まで (MarshalAs(UnmanagedType.ByValTStr, SizeConst=12)
	 	char m3_out[48]; // 47文字まで (MarshalAs(UnmanagedType.ByValTStr, SizeConst=48)
	} A;

	// 明示的ロードでVC、DOTNETから使用するのでヘッダーファイルは不要
	// なお、__cdeclでもDOTNETから正しく呼び出せることを確認している。
	// 通常、__stdcallを適用する(__stdcall = WINAPI)。
	__declspec(dllexport) int __stdcall TestArrayMethod(A a, A *pa, int len, A aa[]);

	int __stdcall TestArrayMethod(A a, A *pa, int len, A aa[]){

		// 配列の長さが0未満はありえないので
		// 例外を発行する(C++の例外を発行してみる)
		if(len < 0){
			throw CTest();
		} // extern "c" で警告が表示される（__cdeclでも__stdcallでも）。

		// 値渡しの構造体 a
		char *p_inputmessage = a.m2_in;
		int counter = a.m1;

		// 参照渡しの構造体 *pa（構造体 a からコピー）
		pa->m1 = ++counter; // 先に ++ してから代入
		sprintf_s(pa->m3_out, 47, "%s by pa->", p_inputmessage);

		// 参照渡しの構造体配列 aa[]（構造体 a からコピー）
		int i;
		for (i = 0; i < len; i++){
			aa[i].m1 = ++counter; // 先に ++ してから代入
			sprintf_s(aa[i].m3_out, 47, "%s by aa[%d].", p_inputmessage, i);
		}

		return counter;
	}
}
