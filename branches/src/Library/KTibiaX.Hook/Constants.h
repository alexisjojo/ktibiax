#if MSC_VER > 100
#pragma once
#endif

#ifndef _CONSTANTS_H_
#define _CONSTANTS_H_

namespace Consts {
}

/* DLL Injection Related Stuff */
extern HINSTANCE hMod;
extern bool HookInjected;

/* Pipes */
extern std::string PipeName;
extern bool PipeConnected;
extern HANDLE PipeThread;
extern BYTE Buffer[1024];
extern CRITICAL_SECTION PipeReadCriticalSection;

enum PipeConstantType : BYTE
{
        PrintName = 0x01,
        PrintFPS = 0x02,
        ShowFPS = 0x03,
        PrintTextFunc = 0x04,
        NopFPS = 0x05,
        AddContextMenuFunc = 0x06,
        OnClickContextMenu = 0x07,
        SetOutfitContextMenu = 0x08,
        PartyActionContextMenu = 0x09,
        CopyNameContextMenu = 0x0A,
		OnClickContextMenuVf = 0x0B,
		TradeWithContextMenu = 0x0C,
};

#endif
