#include "stdafx.h"
#include "IPPackLib.h"
#include "FEAHook.h"
#include "winsock2.h"
#pragma comment ( lib, "ws2_32.lib" )

HINSTANCE	hInst = NULL ;
CFEAHook	HOOK_send ;
CFEAHook	HOOK_recv ;
CFEAHook	HOOK_sendto ;
CFEAHook	HOOK_recvfrom ;

typedef struct CPACKDATA {
	DWORD	dwThreadId ;
	char	cType ;
	int		nDataLen ;
	char	pData[2048] ;
	CPACKDATA ()
	{
		dwThreadId = nDataLen = 0 ;
		memset ( pData, 0, sizeof(pData) ) ;
	}
} CPACKDATA ;


void TransDataToMonitor ( CPACKDATA* pData )
{
	HANDLE hPipe = CreateFile("\\\\.\\Pipe\\NamedPipe", GENERIC_READ | GENERIC_WRITE, \
		0, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL) ;
	if ( hPipe == INVALID_HANDLE_VALUE )
	{
		MessageBox ( 0, "打开管道失败", 0, 0 ) ;
		return ;
	}

	DWORD	dwWriteByte ;
	WriteFile ( hPipe, pData, sizeof(CPACKDATA), &dwWriteByte, NULL ) ;
	CloseHandle ( hPipe ) ;	
}


int WINAPI MY_send ( SOCKET s, const char FAR * buf, int len, int flags )
{
	CPACKDATA PackData ;
	PackData.dwThreadId = GetCurrentThreadId () ;
	PackData.cType		= 'S' ;
	PackData.nDataLen	= len ;
	memcpy ( PackData.pData, buf, len ) ;
	TransDataToMonitor ( &PackData ) ;

	HOOK_send.UnHook () ;
	int ret = send ( s, buf, len, flags ) ;
	HOOK_send.ReHook () ;

	return ret ;
}

int WINAPI MY_recv ( SOCKET s, char FAR* buf, int len, int flags )
{
	HOOK_recv.UnHook () ;
	int ret = recv ( s, buf, len, flags ) ;
	HOOK_recv.ReHook () ;
	
	CPACKDATA PackData ;
	PackData.dwThreadId = GetCurrentThreadId () ;
	PackData.cType		= 'R' ;
	PackData.nDataLen	= len ;
	memcpy ( PackData.pData, buf, len ) ;
	TransDataToMonitor ( &PackData ) ;

	return ret ;
}

int WINAPI MY_sendto ( SOCKET s, const char FAR * buf, int len, int flags, \
			   const struct sockaddr FAR * to, int tolen )
{
	CPACKDATA PackData ;
	PackData.dwThreadId = GetCurrentThreadId () ;
	PackData.cType		= 'S' ;
	PackData.nDataLen	= len ;
	memcpy ( PackData.pData, buf, len ) ;
	TransDataToMonitor ( &PackData ) ;

	HOOK_sendto.UnHook () ;
	int ret = sendto ( s, buf, len, flags, to, tolen ) ;
	HOOK_sendto.ReHook () ;
	return ret ;
}

int WINAPI MY_recvfrom ( SOCKET s, char FAR* buf, int len, int flags,                  
				 struct sockaddr FAR* from, int FAR* fromlen )
{
	HOOK_recvfrom.UnHook () ;
	int ret = recvfrom ( s, buf, len, flags, from, fromlen ) ;
	HOOK_recvfrom.ReHook () ;
	
	CPACKDATA PackData ;
	PackData.dwThreadId = GetCurrentThreadId () ;
	PackData.cType		= 'R' ;
	PackData.nDataLen	= len ;
	memcpy ( PackData.pData, buf, len ) ;
	TransDataToMonitor ( &PackData ) ;

	return ret ;
}

LRESULT CALLBACK GetMsgProc ( int code, WPARAM wParam, LPARAM lParam )
{
	return CallNextHookEx ( hMesHook, code, wParam, lParam ) ;
}

EXPORTDLL BOOL WINAPI SetHook ( HWND hWnd, DWORD dwThreadID, BOOL isInstall ) 
{
	if ( isInstall )
	{
		hMesHook = SetWindowsHookEx ( WH_GETMESSAGE, (HOOKPROC)GetMsgProc, hInst, dwThreadID ) ;
		if ( hMesHook != NULL )
			return true ;
	}
	else
	{
		UnhookWindowsHookEx ( hMesHook ) ;
		hMesHook	= NULL ;
		return true ;
	}
	return false ;
}

BOOL APIENTRY DllMain( HANDLE hModule, 
                       DWORD  ul_reason_for_call, 
                       LPVOID lpReserved
					 )
{
	hInst = (HINSTANCE)hModule ;
	switch (ul_reason_for_call) 
	{ 
	case DLL_PROCESS_ATTACH: 
		HOOK_send.Hook		( "wsock32.dll", "send",		(FARPROC)MY_send ) ;
		HOOK_recv.Hook		( "wsock32.dll", "recv",		(FARPROC)MY_recv ) ;
		HOOK_sendto.Hook	( "wsock32.dll", "sendto",		(FARPROC)MY_sendto ) ;
		HOOK_recvfrom.Hook	( "wsock32.dll", "recvfrom",	(FARPROC)MY_recvfrom ) ;
		break ;
	case DLL_THREAD_ATTACH: 
	case DLL_THREAD_DETACH: 
	case DLL_PROCESS_DETACH: 
		if ( HOOK_send.isSuccess )
			HOOK_send.UnHook () ;
		if ( HOOK_recv.isSuccess )
			HOOK_recv.UnHook () ;
		if ( HOOK_sendto.isSuccess )
			HOOK_sendto.UnHook () ;
		if ( HOOK_recvfrom.isSuccess )
			HOOK_recvfrom.UnHook () ;
		break ;
	} 
    return TRUE;
}