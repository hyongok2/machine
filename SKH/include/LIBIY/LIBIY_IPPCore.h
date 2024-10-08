#pragma once
#ifdef LIBIY_EXT
#include "../LIBIY_Utility/LIBIY_Utility.h"
#include "../LIBIY_CommonObject/LIBIY_CommonObject.h"
#else
#include "./LIBIY/LIBIY_CommonObject.h"
#include "./LIBIY/LIBIY_Utility.h"
#endif
namespace LIBIY
{
	typedef enum {
		BayerBGGR,
		BayerRGGB,
		BayerGBRG,
		BayerGRBG
	} BayerGrid;

	int LIBIY_EXT_API BayerCFAToRGB_ipp(ImageInfo8U &SrcImg, ImageInfo8UC3 &Dst, BayerGrid Grid);

}
