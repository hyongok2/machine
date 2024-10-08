#pragma once
#ifdef LIBIY_EXT
#include "../LIBIY_CommonObject/LIBIY_CommonObject.h"
#else
#include "./LIBIY/LIBIY_CommonObject.h"
#endif

namespace LIBIY
{
	int LIBIY_EXT_API ImageRead(ImageInfo8U &Img, const char* FileName);
	int LIBIY_EXT_API ImageRead(ImageInfo8UC3 &Img, const char* FileName);

	int LIBIY_EXT_API ImageWrite(ImageInfo8U &Img, const char* FileName);
	int LIBIY_EXT_API ImageWrite(ImageInfo16U &Img, const char* FileName);
	int LIBIY_EXT_API ImageWrite(ImageInfo32S &Img, const char* FileName);
	int LIBIY_EXT_API ImageWrite(ImageInfoReal &Img, const char* FileName);
	int LIBIY_EXT_API ImageWrite(ImageInfo8UC3 &Img, const char* FileName);

	int LIBIY_EXT_API GaussFilterCVTest(ImageInfo8U &Img, const char* FileName);

	bool LIBIY_EXT_API ApproxyPolyPointFromMask(const ImageInfo8U & Img, int* ptsArray, const int &ptsMaxCount, int &retPtsCount);
}

