#define EXPORTDLL extern "C" __declspec ( dllexport )

#pragma data_seg ( "shared" )
HHOOK	hMesHook	= NULL ;
HWND	hWnd		= NULL ;
#pragma data_seg ()
#pragma comment ( linker, "/section:shared,rws" )


EXPORTDLL BOOL WINAPI SetHook ( HWND hWnd, DWORD dwTreadID, BOOL isInstall )  ; 
