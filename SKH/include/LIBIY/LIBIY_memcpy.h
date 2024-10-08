#pragma once
#ifndef __LIBIY_MEMCPY__
#define __LIBIY_MEMCPY__

#ifdef LIBIY_AVX512_SUPPORT
#define LIBIY_memcpy LIBIY_MEMCPY_AVX512
#define LIBIY_memset LIBIY_MEMSET_AVX
#else
#ifdef LIBIY_AVX_SUPPORT
#define LIBIY_memcpy LIBIY_MEMCPY_AVX
#define LIBIY_memset LIBIY_MEMSET_AVX
#else 
#define LIBIY_memcpy memcpy
#define LIBIY_memset memset
#endif
#endif

extern "C"
{
	void LIBIY_MEMCPY_AVX(void* dst, void* src, __int64 length);
	void LIBIY_MEMCPY_AVX512(void* dst, void* src, __int64 length);
	void LIBIY_MEMSET_AVX(void* dst, int val, __int64 length);
	void LIBIY_MEMSET_SSE(void* dst, int val, __int64 length);
}
#endif