#include "stdafx.h"
#include <string>
#include <windows.h>
#include "Constants.h"

namespace Consts {
}

/* DLL Injection Related Stuff */
HINSTANCE hMod = 0;
bool HookInjected = false;

/* Pipes */
std::string PipeName;
bool PipeConnected = false;
HANDLE PipeThread = 0;
BYTE Buffer[1024] = {0};
CRITICAL_SECTION PipeReadCriticalSection;

