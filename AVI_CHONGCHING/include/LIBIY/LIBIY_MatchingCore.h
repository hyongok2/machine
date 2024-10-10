#pragma once
#ifdef LIBIY_EXT
#include "../LIBIY_CommonObject/LIBIY_CommonObject.h"
#else
#include "./LIBIY/LIBIY_CommonObject.h"

#endif


namespace LIBIY
{
	extern void    (*fpCropImage8UToReal)(ImageInfo8U &Img1, ImageInfoReal &DstImg, bool bZeroSet);
	extern void    (*fpPowerToReal)(ImageInfoComplex &ComplexImage, ImageInfoReal &PowSpectrum, float fnorm);
	extern void    (*fpPhaseCorrelation)(ImageInfoComplex &Img1, ImageInfoComplex &Img2, ImageInfoComplex &Dst);
	extern __int64 (*fpSobelAMP)(ImageInfo8U &SrcImage, ImageInfoReal &DstImage);
	extern float   (*fpFindMaxVal)(float* Array, size_t nSize);
	int LIBIY_EXT_API _fft_R2C(ImageInfoReal &_Model, ImageInfoComplex &PrepareTemplate);
	int LIBIY_EXT_API ComplexImageGen(ImageInfoComplex &PrepareTemplate, _rect _roi);
	int LIBIY_EXT_API GetOptimalDFTSize(int nSize);
	void    LIBIY_EXT_API CropImage8UToReal(ImageInfo8U &Img1, ImageInfoReal &DstImg, bool bZeroSet);
	void    LIBIY_EXT_API PowerToReal(ImageInfoComplex &ComplexImage, ImageInfoReal &PowSpectrum, float fnorm);
	void    LIBIY_EXT_API PhaseCorrelation(ImageInfoComplex &Img1, ImageInfoComplex &Img2, ImageInfoComplex &Dst);
	__int64 LIBIY_EXT_API SobelAMP(ImageInfo8U &SrcImage, ImageInfoReal &DstImage);
	__int64 LIBIY_EXT_API SobelAMP_SSE(ImageInfo8U &SrcImage, ImageInfoReal &DstImage);
	__int64 LIBIY_EXT_API CropImageRealTo8U(ImageInfoReal &SrcImage, ImageInfo8U &DstImage);

	enum FFTAlgorithms {
		FFT_PHASE_CORRELATION = 0, FFT_PHASE_CORRELATION_FILTER, FFT_PHASE_CORRELATION_SOBEL_AMP, DOGFILTER, FFT_PHASE_CORRELATION_FILTER_CENTER};

	__int64 LIBIY_EXT_API LIBIY_FFTPhaseCorrelationMatching(ImageInfo8U &DataImage, ImageInfo8U &ModelImage, float& Tx, float& Ty, float& Score);
	__int64 LIBIY_EXT_API GenPrepareTemplateFFT(ImageInfo8U &ModelImage, ImageInfo8U &TargetImage, FFTAlgorithms Algorithms, ImageInfoComplex &PrepareTemplate, ImageInfoReal &_PowSpectrum);
	__int64 LIBIY_EXT_API GenPrepareTemplateFFT(ImageInfo8U &ModelImage, Point2D TargetSize, FFTAlgorithms Algorithms, ImageInfoComplex &PrepareTemplate, ImageInfoReal &_PowSpectrum);
	__int64 LIBIY_EXT_API FFTPhaseCorrelationPrepareTemplateMatching(ImageInfoComplex &PrepareTemplate, ImageInfoReal &_PowSpectrum, ImageInfo8U &TargetImage, FFTAlgorithms Algorithms, float& Tx, float& Ty, float& Score, float PhaseThresh = 5.0f);
	__int64 LIBIY_EXT_API FFTPhaseCorrelationMatching(ImageInfo8U &ModelImage, ImageInfo8U &TargetImage, FFTAlgorithms Algorithms, float& Tx, float& Ty, float& Score, float PhaseThresh = 5.0f);
	__int64 LIBIY_EXT_API FFTPhaseCorrelationMatchingWithoutBorder(ImageInfo8U &ModelImage, ImageInfo8U &TargetImage, FFTAlgorithms Algorithms, float& Tx, float& Ty, float& Score, float PhaseThresh = 5.0f);
	
	void LIBIY_EXT_API Gen_GaussFiter(ImageInfoReal &DstImg, float Sigma);
	__int64 LIBIY_EXT_API ConvertImageTypeRealTo8U(ImageInfoReal &SrcImage, ImageInfo8U &DstImage);
	__int64 LIBIY_EXT_API GaussFilterGenerate(ImageInfoComplex &FilterImg, float Sigma, int nWidth, int nHeight);
	__int64 LIBIY_EXT_API GaussFilterGenerate_Inv(ImageInfoComplex &FilterImg, float Sigma, int nWidth, int nHeight);
	__int64 LIBIY_EXT_API ConvolFFTFilter(ImageInfo8U &SrcImage, ImageInfoComplex &FilterImg, ImageInfoReal &DstImage, ImageInfo8U &SubImage, int nSubImageMult, int nSubImageAdd);
	__int64 LIBIY_EXT_API ConvolFFTFilter(ImageInfo8U &SrcImage, ImageInfoComplex &FilterImg, ImageInfoReal &DstImage, int nSubImageMult, int nSubImageAdd);
}

