#pragma once
#ifndef LIBIY_FFTCORE
#define LIBIY_FFTCORE

namespace LIBIY
{
	int  LIBIY_EXT_API getOptimalDFTSize(int size0);
	void LIBIY_EXT_API fft_R2C(ImageInfoReal &Reallmg, ImageInfoComplex &CompImg);
	void LIBIY_EXT_API ifft_C2R(ImageInfoComplex CompImg, ImageInfoReal Reallmg);
}

#endif
 