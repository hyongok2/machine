#pragma once
#ifndef LIBIY_COMMONOBJECT
#define LIBIY_COMMONOBJECT
#ifndef LIBIY_API_EXPORT
#define LIBIY_API_EXPORT __declspec(dllexport)
#define LIBIY_API_IMPORT __declspec(dllimport)
#define LIBIY_CLASS_EXPORT __declspec(dllexport)
#define LIBIY_CLASS_IMPORT __declspec(dllimport)
#define LIBIY_DATA_EXPORT __declspec(dllexport)
#define LIBIY_DATA_IMPORT __declspec(dllimport)
#ifdef LIBIY_EXT
#define LIBIY_EXT_API LIBIY_API_EXPORT
#define LIBIY_EXT_DATA LIBIY_DATA_EXPORT
#define LIBIY_EXT_CLASS LIBIY_CLASS_EXPORT
#else
#define LIBIY_EXT_API LIBIY_API_IMPORT
#define LIBIY_EXT_DATA LIBIY_DATA_IMPORT
#define LIBIY_EXT_CLASS LIBIY_CLASS_IMPORT
#endif
#endif

#include "Structures.h"

//#define  uniqueSortInt uniqueSort<int>;

#endif
