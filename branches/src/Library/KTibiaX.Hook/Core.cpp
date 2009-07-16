#include "stdafx.h"
#include <windows.h>
#include <string>
#include <sstream>
#include <list>
#include <atlbase.h>
#include <assert.h>
#include "Constants.h"
#include "Core.h"
#include "Packet.h"

#ifdef _MANAGED
#pragma managed(push, off)
#endif

using namespace std;

//Asynchronisation variables
CHandle pipe;						//Holds the Pipe handle (CHandle is from ATL library)
OVERLAPPED overlapped = { 0 };		
DWORD errorStatus = ERROR_SUCCESS;

/*Addresses are loaded from Constants.xml file */

DWORD HookCall(DWORD dwAddress, DWORD dwFunction)
{   
	DWORD dwOldProtect, dwNewProtect, dwOldCall, dwNewCall;
	//CALL opcode = 0xE8 <4 byte for distance>
	BYTE callByte[5] = {0xE8, 0x00, 0x00, 0x00, 0x00};

	//Calculate the distance
	dwNewCall = dwFunction - dwAddress - 5;
	memcpy(&callByte[1], &dwNewCall, 4);
	
	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(dwAddress), 5, PAGE_READWRITE, &dwOldProtect); //Gain access to read/write
	memcpy(&dwOldCall, (LPVOID)(dwAddress+1), 4); //Get the old function address for unhooking
	memcpy((LPVOID)(dwAddress), &callByte, 5); //Hook the function
	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(dwAddress), 5, dwOldProtect, &dwNewProtect); //Restore access
	
	return dwOldCall; //Return old funtion address for unhooking
}

void UnhookCall(DWORD dwAddress, DWORD dwOldCall)
{
	DWORD dwOldProtect, dwNewProtect;
	BYTE callByte[5] = {0xE8, 0x00, 0x00, 0x00, 0x00};

	memcpy(&callByte[1], &dwOldCall, 4);
	
	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(dwAddress), 5, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)(dwAddress), &callByte, 5);
	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(dwAddress), 5, dwOldProtect, &dwNewProtect);
}

BYTE* Nop(DWORD dwAddress, int size)
{
	DWORD dwOldProtect, dwNewProtect;
	BYTE* OldBytes;
	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(dwAddress), size, PAGE_READWRITE, &dwOldProtect);
	OldBytes = new BYTE[size];
	memcpy(OldBytes, (LPVOID)(dwAddress), size);
	memset((LPVOID)(dwAddress), 0x90, size);
	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(dwAddress), size, dwOldProtect, &dwNewProtect);
	
	return OldBytes;
}

void UnNop(DWORD dwAddress, BYTE* OldBytes, int size)
{
	DWORD dwOldProtect, dwNewProtect;
	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(dwAddress), size, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)(dwAddress), OldBytes, size);
	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(dwAddress), size, dwOldProtect, &dwNewProtect);

	delete [] OldBytes;
	OldBytes = 0;
}

void __declspec(noreturn) UninjectSelf(HMODULE Module)
{
   __asm
   {
      push -2
      push 0
      push Module
      mov eax, TerminateThread
      push eax
      mov eax, FreeLibrary
      jmp eax
   }
}

inline void PipeOnRead()
{
	int position = 0;
	WORD len = Packet::ReadWord(Buffer, &position);
	BYTE PacketID = Packet::ReadByte(Buffer, &position);

	switch (PacketID){
		case 0x1: // Set Constant
			{
				

			}
			break;
		case 0x2: // DisplayText
			{
				
			}
			break;
		case 0xC:
			//TODO:Nothing here?the injected dll should send this packet to tibiaapi containing the eventid
			//and the matching contextmenu eventid would raise its event
			break;
		default:
			MessageBoxA(0, "Unknown PacketType!", "Error!", MB_ICONERROR);
			break;
	}
}

void PipeThreadProc(HMODULE Module)
{
	//Connect to Pipe
	if (WaitNamedPipeA(PipeName.c_str(), NMPWAIT_WAIT_FOREVER)) 
	{
		pipe.Attach(::CreateFileA(PipeName.c_str(), GENERIC_READ | GENERIC_WRITE , 0, NULL, OPEN_EXISTING, FILE_FLAG_OVERLAPPED, NULL));
		
		if (pipe == INVALID_HANDLE_VALUE)
		{
			errorStatus = ::GetLastError();
			MessageBoxA(0, "Pipe connection failed!", "TibiaAPI Injected DLL - Fatal Error", MB_ICONERROR);
			return;
		} 
		else 
		{
			//TODO: Remove!
			MessageBoxA(0, "Pipe ready and connected!", "KTibiaX", MB_ICONINFORMATION);

			//Pipe is ready. Let's start listening for incoming packets
			PipeConnected = true;
			if(!::ReadFileEx(pipe, Buffer, sizeof(Buffer), &overlapped, ReadFileCompleted))
			{
				errorStatus = ::GetLastError();
				MessageBoxA(0, "Pipe read error!", "TibiaAPI Injected DLL - Fatal Error", MB_ICONERROR);
				return;
			} 
			else 
			{
				while (errorStatus == ERROR_SUCCESS)
				{
					const DWORD sleepResult = ::SleepEx(INFINITE, TRUE);
					assert(WAIT_IO_COMPLETION == sleepResult);
				}
			}
		}
	} 
	else 
		MessageBoxA(0, "Failed waiting for pipe, maybe pipe is not ready?.", "TibiaAPI Injected DLL - Fatal Error", 0);
}

void CALLBACK ReadFileCompleted(DWORD errorCode, DWORD bytesCopied, OVERLAPPED* overlapped)
{
	errorStatus = errorCode;;

	if (errorStatus == ERROR_SUCCESS)
	{
		PipeOnRead();

		if (!::ReadFileEx(pipe, Buffer, sizeof(Buffer), overlapped, ReadFileCompleted))
		{
			errorStatus = ::GetLastError();
			MessageBoxA(0, "Pipe read error!", "TibiaAPI Injected DLL - Fatal Error", MB_ICONERROR);
		}
	}
	else
	{
		if(HookInjected) 
		{
			//remove all text
			HookInjected = false;
		}
		
		pipe.Detach();
		DeleteCriticalSection(&PipeReadCriticalSection);
		UninjectSelf(hMod);
	}
}

extern "C" bool APIENTRY DllMain (HMODULE hModule, DWORD reason, LPVOID reserved)
{
	switch (reason)
	{
		case DLL_PROCESS_ATTACH: //DLL was injected
        {
			hMod = hModule;
			/* Get Current Process ID and use it as Pipename (Pipe is named as TibiaAPI<processID> */
			DWORD CurrentPID = GetCurrentProcessId();
			std::stringstream sout;
			sout << "\\\\.\\pipe\\KTibiaX" << CurrentPID;
			PipeName =  sout.str();

			InitializeCriticalSection(&PipeReadCriticalSection);
			PipeConnected = false;

			//Start new thread for Pipe
			PipeThread = CreateThread(NULL, NULL, (LPTHREAD_START_ROUTINE)PipeThreadProc, hMod, NULL, NULL);
			
		}
        break;
		case DLL_PROCESS_DETACH: //DLL was uninjected
		{
			TerminateThread(PipeThread, EXIT_SUCCESS);
			DeleteCriticalSection(&PipeReadCriticalSection);
		}
		break;
    }

    return true;
}
