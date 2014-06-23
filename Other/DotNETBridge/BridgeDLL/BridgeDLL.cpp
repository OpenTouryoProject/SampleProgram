// 簡単な方式（明示的ロードが必要）
// BridgeDLL.cpp : DLL アプリケーション用にエクスポートされる関数を定義します。
//

// ■ char⇔String型変換
// http://homepage1.nifty.com/MADIA/vc/vc_bbs/200604/200604_06040016.html

// C++-CLI Tips  文字列操作
// http://vene.wankuma.com/prog/CppCli_strings.aspx

// 方法: System::String を wchar_t* または char* に変換する
// http://msdn.microsoft.com/ja-jp/library/d1ae6tz5.aspx

// Visual C++ で SystemString から char に変換する方法
// http://support.microsoft.com/kb/311259/ja

#include "stdafx.h"

// convert_string_to_wchar.cpp
// compile with: /clr
#include < vcclr.h > // managed C++ 時代の vcclr.h

// VisualStudio 2008 になって、C++/CLI では marshal_as<> が利用できるようになりました。
// これは組み込み関数である marshal_as 演算子とマーシャリング・オブジェクト marshal_context を作成し、
// ネイティブ・データの生存時間を管理する marshal オブジェクトによって構成されています。

// marshal_as
// http://msdn.microsoft.com/ja-jp/library/bb384859.aspx
// marshal_context クラス
// http://msdn.microsoft.com/ja-jp/library/bb384854.aspx

// marshal_as, marshal_context - Schimaの日記
// http://d.hatena.ne.jp/Schima/20090422#1240359965

using namespace System;
using namespace TargetAssembly;

extern "C"{

	// 明示的ロードでVC、DOTNETから使用するのでヘッダーファイルは不要
	// なお、__cdeclでもDOTNETから正しく呼び出せることを確認している。
	// 通常、__stdcallを適用する(__stdcall = WINAPI)。
	__declspec(dllexport) LPCWSTR __stdcall TestMethod(LPCWSTR message);

	LPCWSTR __stdcall TestMethod(LPCWSTR message){

		// LPCWSTR→System::String（GC対象）
		String^ gc_message = gcnew String( message );

		// .NETのクラスを生成する。
		TargetClass^ obj = gcnew TargetClass();

		// .NETのメソッドを呼び出す。
		String^ gc_return = obj->MessageBox_Show(gc_message);
		
		// 在来の関数が呼ばれる間に、GCがそれを
		// 移動させることができないように、メモリをピンで留め。
		pin_ptr<const wchar_t> wc_return = PtrToStringChars(gc_return);

		// pin_ptrが解放されれば、ピンが外れてgc_returnはGCされる筈。
		return wc_return;
	}
}