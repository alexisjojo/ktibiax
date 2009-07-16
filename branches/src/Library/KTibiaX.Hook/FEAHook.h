#ifndef _FEAHook_H
#define _FEAHook_H

class CFEAHook {
public:
	BOOL	isSuccess ;						
	PSTR	pModuleName, pFunName ;			
	LPVOID	pOldFunEntry, pNewFunEntry ;	
	BYTE	bOldByte[5], bNewByte[5] ;		

public:
	CFEAHook () {	isSuccess	= false ;	}
	~CFEAHook() {	UnHook() ;		}
	void Hook ( PSTR szModuleName, PSTR szFunName, FARPROC pFun )
	{	
		HMODULE	hMod = GetModuleHandle ( szModuleName ) ;
		if ( hMod != NULL )
		{
			isSuccess		= true ;
			pModuleName		= szModuleName ;
			pFunName		= szFunName ;
			pNewFunEntry	= (LPVOID)pFun ;
			pOldFunEntry	= (LPVOID)GetProcAddress ( hMod, pFunName ) ;
			bNewByte[0]		= 0xE9 ;
			*((PDWORD)(&(bNewByte[1])))	= (DWORD)pNewFunEntry - (DWORD)pOldFunEntry - 5 ; 

			DWORD   dwProtect, dwWriteByte, dwReadByte ; 
			VirtualProtect ( (LPVOID)pOldFunEntry, 5, PAGE_READWRITE, &dwProtect );
			ReadProcessMemory	( GetCurrentProcess(), (LPVOID)pOldFunEntry, bOldByte, 5, &dwReadByte ) ;		
			WriteProcessMemory	( GetCurrentProcess(), (LPVOID)pOldFunEntry, bNewByte, 5, &dwWriteByte ) ;
			VirtualProtect ( (LPVOID)pOldFunEntry, 5, dwProtect, NULL ) ;
		}
	}
	void ReHook ()
	{
		DWORD	dwProtect, dwWriteByte ;
		VirtualProtect ( pOldFunEntry, 5, PAGE_READWRITE, &dwProtect );
		WriteProcessMemory ( GetCurrentProcess(), pOldFunEntry, bNewByte, 5, &dwWriteByte ) ;
		VirtualProtect ( pOldFunEntry, 5, dwProtect, NULL ) ;
	}
	void UnHook ()
	{
		DWORD	dwProtect, dwWriteByte ;
		VirtualProtect ( pOldFunEntry, 5, PAGE_READWRITE, &dwProtect );
		WriteProcessMemory ( GetCurrentProcess(), pOldFunEntry, bOldByte, 5, &dwWriteByte ) ;
		VirtualProtect ( pOldFunEntry, 5, dwProtect, NULL ) ;
	}
} ;

#endif