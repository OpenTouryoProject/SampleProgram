// エクスポートとインポートの切り替え
#ifdef MYLIBAPI // VC_DLL_EXPORTS でもいいかも

// DLLのPJではMYLIBAPIを使用して関数をエクスポート。

#else

// EXEの(DLLを利用する)PJではMYLIBAPIをインポート。
// extern "C" は、他の言語から呼ぶ場合に必要。
#define MYLIBAPI extern "C" __declspec(dllimport)

#endif

// 以下、エクスポート関数のプロトタイプ宣言
// 通常、__stdcallを適用する(__stdcall = WINAPI)。
MYLIBAPI void __stdcall Test_MYLIBAPI(LPCWSTR lpText, LPCWSTR lpCaption);

// この方式だと、C4603、C4273の警告が出るが、問題はない。

// ・C4603：'<identifier>': マクロが定義されていないか、
//          プリコンパイル済みヘッダーが使用している定義とは異なります。

// ・C4273：dll リンクが一貫していません。
