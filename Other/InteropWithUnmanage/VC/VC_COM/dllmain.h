// dllmain.h : モジュール クラスの宣言

class CVC_COMModule : public CAtlDllModuleT< CVC_COMModule >
{
public :
	DECLARE_LIBID(LIBID_VC_COMLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_VC_COM, "{A2AD8ACA-5BD9-4FD0-A448-97F8EDB9B66D}")
};

extern class CVC_COMModule _AtlModule;
