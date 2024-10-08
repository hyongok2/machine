#pragma once
#ifndef LIBIY_UTILIYY
#define LIBIY_UTILIYY

//#define LIBIY_AVX512_SUPPORT
#define LIBIY_AVX_SUPPORT
#include "LIBIY_memcpy.h"



#if 0
#ifdef LIBIY_AVX512_SUPPORT
#define LIBIY_memcpy LIBIY_memcpyAVX512
#else
	#ifdef LIBIY_AVX_SUPPORT
	#define LIBIY_memcpy memcpy_generic
	#else
	#define LIBIY_memcpy memcpy
	#endif
#endif
#endif


#ifdef LIBIY_TIMER
#include "tchar.h"
#include "windows.h"
inline void __cdecl MyTrace(const TCHAR* strMsg, ...)
{
	TCHAR strBuffer[512];
	va_list args;
	va_start(args, strMsg);
	_vsntprintf_s(strBuffer, 512, strMsg, args);
	va_end(args);
	OutputDebugString(strBuffer);
}

#define FUNTIMECHECK(A,SS) \
{   LARGE_INTEGER startingTime, endingTime, elapsed;\
	LARGE_INTEGER frequency;\
	QueryPerformanceFrequency(&frequency);\
	QueryPerformanceCounter(&startingTime);\
	A;\
	QueryPerformanceCounter(&endingTime);\
	elapsed.QuadPart = endingTime.QuadPart - startingTime.QuadPart;\
	elapsed.QuadPart *= 1000000;\
	elapsed.QuadPart /= frequency.QuadPart;\
	MyTrace(_T("%s Time(microseconds) : %I64d\n"),SS, elapsed);\
}
#define FUNTIMECHECK_UP_BLOCK \
{   LARGE_INTEGER startingTime, endingTime, elapsed;\
	LARGE_INTEGER frequency;\
	QueryPerformanceFrequency(&frequency);\
	QueryPerformanceCounter(&startingTime);

#define FUNTIMECHECK_BOTTOM_BLOCK(SS) \
	QueryPerformanceCounter(&endingTime);\
	elapsed.QuadPart = endingTime.QuadPart - startingTime.QuadPart;\
	elapsed.QuadPart *= 1000000;\
	elapsed.QuadPart /= frequency.QuadPart;\
	MyTrace(_T("%s Time(microseconds) : %I64d\n"),SS, elapsed);\
}
#define FUNTIMECHECK_BOTTOM_BLOCK_(TT) \
	QueryPerformanceCounter(&endingTime);\
	elapsed.QuadPart = endingTime.QuadPart - startingTime.QuadPart;\
	elapsed.QuadPart *= 1000000;\
	elapsed.QuadPart /= frequency.QuadPart;\
	TT = elapsed.QuadPart;\
}
#endif

namespace LIBIY
{

}
extern "C"
{
	void LIBIY_MEMORY_REVERSE(void* dst, void* src, __int64 length);
}
#endif