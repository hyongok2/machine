#pragma once

namespace M_ImageControls
{
	enum ImageConvertType	{ConvertTypeGray2RGB = 0, ConvertTypeRGB2Gray, ConvertTypeGray2RGBA, ConvertTypeRGBA2Gray, ConvertTypeRGB2RGBA, ConvertTypeRGBA2RGB };
	enum ImageFlipType		{ FlipTypeBoth=-1, FlipTypeVert=0, FlipTypeHoriz=1 };
	enum ImageRotateType	{ RotateTypeRight=0, RotateTypeLeft };
	enum ProcessResultType	{ ResultSuccess=1, ResultImageNULL=0, ResultImageNoExist=-1, ResultFail=-2 };
}

#include <list>
#include <vector>
#include <algorithm>
#include "CHImageData.h"

struct _IplConvKernel;

struct PointDouble
{
	PointDouble()
	{
		Reset();
	}

	PointDouble(double xx, double yy)
	{
		x = xx;
		y = yy;
	}

	void Reset()
	{
		x = 0.0;
		y = 0.0;
	}

	double x;
	double y;
};
typedef std::vector<PointDouble>			VectorPointDouble;
typedef std::vector<PointDouble>::iterator	VectorPointDoubleIt;

struct SDefectBlob
{
	SDefectBlob()
	{
		Reset();
	}

	SDefectBlob(int left, int top, int right, int bottom)
	{
		nLeft = left;
		nTop = top;
		nRight = right;
		nBottom = bottom;
		nPeak = 0;
		nCount = 0;
		vectorPoint.clear();
	}

	void Reset()
	{
		nLeft = INT_MAX;
		nTop = INT_MAX;
		nRight = INT_MIN;
		nBottom = INT_MIN;
		nPeak = 0;
		nGray = 0;
		nCount = 0;
		vectorPoint.clear();
	}

	int		GetSquareSize()	const { return (nRight - nLeft + 1) * (nBottom - nTop + 1); }
	int		GetWidth() const { return (nRight - nLeft + 1); }
	int		GetHeight() const { return (nBottom - nTop + 1); }
	int		GetCenterX() const { return nLeft + (nRight - nLeft + 1) / 2; }
	int		GetCenterY() const { return nTop + (nBottom - nTop + 1) / 2; }
	int		GetMaxSize() const { return (GetWidth() < GetHeight()) ? GetHeight() : GetWidth(); }
	int		GetMinSize() const { return (GetWidth() > GetHeight()) ? GetHeight() : GetWidth(); }
	size_t	GetPixelCount()	const { return nCount; }
	int		GetAvgPeak() const { return (nCount == 0) ? 0 : nPeak / nCount; }
	int		GetAvgGray() const { return (nCount == 0) ? 0 : nGray / nCount; }

	int					nLeft;
	int					nTop;
	int					nRight;
	int					nBottom;
	int					nPeak;
	int					nGray;
	int					nCount;

	std::vector<CPoint> vectorPoint;
};
typedef std::vector<SDefectBlob>			VectorDefectBlob;
typedef std::vector<SDefectBlob>::iterator	VectorDefectBlobIt;

class AFX_EXT_CLASS CCHImageProcess
{
public:
	class CPolynomial
	{
	public:

		CPolynomial()		{ Reset(); }

		CPolynomial(int degree, double coef)
		{
			nDegree = degree;
			dCoef	= coef;
		}

		~CPolynomial()	{ Reset(); }

		void Reset()
		{
			nDegree = 0;
			dCoef	= 0.0;
		}

		int		nDegree;
		double	dCoef;
	};

	typedef std::list<CPolynomial*>				ListPolynomial;
	typedef std::list<CPolynomial*>::iterator	ListPolynomialIt;

	typedef std::vector<double>						VectorDouble;
	typedef std::vector<double>::iterator			VectorDoubleIt;

	static BOOL	CalculateLagrange(VectorDouble& vectorX, VectorDouble& vectorY, ListPolynomial& listPolynomial);

public:
	CCHImageProcess(void);
	virtual ~CCHImageProcess(void);

	static int ImageMerge(CCHImageData* pResultImage, const CCHImageData* pImage1, const CCHImageData* pImage2 = NULL, const CCHImageData* pImage3 = NULL, const CCHImageData* pImage4 = NULL);

	static int ImageConvert(const CCHImageData* pSourceImage, CCHImageData* pResultImage, int nConvertCode);
	static int ImageAverage(const CCHImageData* pSourceImage, double& dValue);
	static int ImageMinMax(const CCHImageData* pSourceImage, double& dMinValue, double& dMaxValue);

	static int ImageResize(const CCHImageData* pSourceImage, CCHImageData* pResultImage, int nNewWidth, int nNewHeight, int nInterpolation=1);
	static int ImageResize(const CCHImageData* pSourceImage, CCHImageData* pResultImage, double dScale, int nInterpolation=1);
	static int ImageFlip(const CCHImageData* pSourceImage, CCHImageData* pResultImage, int nFlipMode);

	static int ImageErode(const CCHImageData* pSourceImage, CCHImageData* pResultImage, _IplConvKernel* pElement=NULL, int nIterations=1);
	static int ImageDilate(const CCHImageData* pSourceImage, CCHImageData* pResultImage, _IplConvKernel* pElement=NULL, int nIterations=1);
	static int ImageOpening(const CCHImageData* pSourceImage, CCHImageData* pResultImage, _IplConvKernel* pElement=NULL, int nIterations=1);
	static int ImageClosing(const CCHImageData* pSourceImage, CCHImageData* pResultImage, _IplConvKernel* pElement=NULL, int nIterations=1);

	static int ImageRotate(const CCHImageData* pSourceImage, CCHImageData* pResultImage, double dAngle, int nFlag=9, UCHAR nValue=0);
	static int ImageRotate(const CCHImageData* pSourceImage, CCHImageData* pResultImage, int nRotateType);

	static int ImageSmooth(const CCHImageData* pSourceImage, CCHImageData* pResultImage, int nSmoothType=2, int nSize1=3, int nSize2=0, double dSigma1=1.0, double dSigma2=1.0);
	static int ImageNormalize(const CCHImageData* pSourceImage, CCHImageData* pResultImage, double dMin, double dMax, int nType=32);
	static int ImageAbsDiff(const CCHImageData* pAImage, const CCHImageData* pBImage, CCHImageData* pCImage);

	static int ImageAdd(const CCHImageData* pAImage, const CCHImageData* pBImage, CCHImageData* pCImage);
	static int ImageAdd(const CCHImageData* pAImage, double dA, const CCHImageData* pBImage, double dB, double dC, CCHImageData* pCImage );
	static int ImageAnd(const CCHImageData* pAImage, const CCHImageData* pBImage, CCHImageData* pCImage);
	static int ImageEqualizeHist(const CCHImageData* pSourceImage, CCHImageData* pResultImage);
	static int ImagePreCornerDetect(const CCHImageData* pSourceImage, CCHImageData* pResultImage, int nApertureSize=3);
	static int ImageSobel(const CCHImageData* pSourceImage, CCHImageData* pResultImage, int nXOrder, int nYOrder, int nApertureSize=3);
	static int ImageSobel(const CCHImageData* pSourceImage, CCHImageData* pXImage, CCHImageData* pYImage, int nApertureSize=0);
	static int ImageCanny(const CCHImageData* pSourceImage, CCHImageData* pResultImage, double dThreshold1, double dThreshold2, int nApertureSize=3);
	static int ImageAdaptiveThreshold(const CCHImageData* pSourceImage, CCHImageData* pResultImage, double dMaxValue, int nMethodType=0, int nThresholdType=0, int nBlockSize=3, double dParam1=5);
	static int ImageThreshold(const CCHImageData* pSourceImage, CCHImageData* pResultImage, double dThresholdValue, double dMaxValue, int nThresholdType);
	static int ImageThreshold(const CCHImageData* pSourceImage, CCHImageData* pResultImage, double dThresholdValue);
	static int ImageMul(const CCHImageData* pSorceImg, const CCHImageData* pMaskImg, CCHImageData* pResultImg );
		 
	static int ImageFFT(CCHImageData* pSourceImage, CCHImageData* pResultImage, int nFlags, CCHImageData* pNormalImage=NULL);
	static int ImageInverseFFT(CCHImageData* pSourceImage, CCHImageData* pResultImage, int nWidth, int nHeight);

	static int ImageLowPassFilter(CCHImageData* pSourceImage, CCHImageData* pResultImage, int D0, int N, int nFlags=1, CCHImageData *pNormalImage=NULL);
	static int ImageHighPassFilter(CCHImageData* pSourceImage, CCHImageData* pResultImage, int D0, int N, int nFlags=1, CCHImageData *pNormalImage=NULL);

	static int MatrixSolve(double *A, double* B, double* X, int nSize, int nMethod=0);

	static int ImageDCT(CCHImageData* pSourceImage, CCHImageData* pResultImage);
	static int ImageInvertDCT(CCHImageData* pSourceImage, CCHImageData* pResultImage);
	static void	RemovePolynomial(ListPolynomial& listPolynomial);

	static	BOOL	ImageSmooth_HybridMedianFilter(CCHImageData* pSourceImage, CCHImageData* pResultImage, int nFilterSize);
	static	BOOL	ImageSmooth_HybridMedianFilter2(CCHImageData* pSourceImage, CCHImageData* pResultImage, int nFilterSize);
	static	int		GetHybridMedianFilterValue(BYTE* pKernelValue, int nFilterSize);
	static	void	Swap(int* pA, int* pB);
	static	void	QuickSort(int* nArr, int nStart, int nEnd);
	static	void	ImageDiffusion(BYTE *pBuffer, int w, int h, int iter, float lambda=0.25f, float k=4.0f);
	static int		ImageLocalBinary(CCHImageData* pSourceImage, CCHImageData* pResultImage);
	static int		ImageReplace(CCHImageData* pSourceImage, CCHImageData* pResultImage, int nMin, int nMax, int nSet);

	// pixel
	static int ImageMatching(const CCHImageData* pSourceImage, const CCHImageData* pTemplateImage, CCHImageData* pResultImage, int nMethod=5);
	static int ImageMatching(const CCHImageData* pSourceImage, const CCHImageData* pTemplateImage, CRect& rtROIRegion,  CCHImageData* pResultImage, int nMethod=5);
	static int ImageMatching(const CCHImageData* pSourceImage, const CCHImageData* pTemplateImage, int& nResultX, int& nResultY, double& dResultValue, int nMethod=5);
	static int ImageMatching(const CCHImageData* pSourceImage, const CCHImageData* pTemplateImage, int& nResultX, int& nResultY, double& dResultValue, CCHImageData* pResultImage, int nMethod=5);
	static int ImageMatching(const CCHImageData* pSourceImage, const CCHImageData* pTemplateImage, CRect& rtROIRegion, int& nResultX, int& nResultY, double& dResultValue, int nMethod=5);
	static int ImageMatching(const CCHImageData* pSourceImage, const CCHImageData* pTemplateImage, CRect& rtROIRegion, int& nResultX, int& nResultY, double& dResultValue, CCHImageData* pResultImage, int nMethod=5);

	// sub pixel
	static int ImageMatching(const CCHImageData* pSourceImage, const CCHImageData* pTemplateImage, double& dResultX, double& dResultY, double& dResultValue, int nMethod=5, int nSubPixel=0);
	static int ImageMatching(const CCHImageData* pSourceImage, const CCHImageData* pTemplateImage, double& dResultX, double& dResultY, double& dResultValue, CCHImageData* pResultImage, int nMethod=5, int nSubPixel=0);
	static int ImageMatching(const CCHImageData* pSourceImage, const CCHImageData* pTemplateImage, CRect& rtROIRegion, double& dResultX, double& dResultY, double& dResultValue, int nMethod=5, int nSubPixel=0);
	static int ImageMatching(const CCHImageData* pSourceImage, const CCHImageData* pTemplateImage, CRect& rtROIRegion, double& dResultX, double& dResultY, double& dResultValue, CCHImageData* pResultImage, int nMethod=5, int nSubPixel=0);
	

	// pixel
	static int ImageConvolution(CCHImageData* pSourceImage, CCHImageData* pResultImage, int nLeft, int nTop, int nRight, int nBottom, int nKernelX, int nKernelY, int nPitch, int nThres);
	static int ImageLocalConvolution(CCHImageData* pSourceImage, CCHImageData* pVertImage, CCHImageData* pHoriImage, int nPitchX, int nPitchY, int& nPosX, int& nPosY);
	static int ImageIntegral(CCHImageData* pSourceImage, CCHImageData* pResultImage);
	static int ImageKernelMean(CCHImageData* pSourceImage, CCHImageData* pResultImage, int nKernelSizeW, int nKernelSizeH);
	
	// Blob
	static int ImageBlobAnalysis_Binary(BYTE *pImage, int nWidth, int nHeight, int nThreshold, int nBlobMargin, int nStartX, int nStartY, VectorDefectBlob& vectorBlob);

	static int CornerDetect_Harris(CCHImageData* pSourceImage, CCHImageData* pResultImage);

	static int	EllipseFitting(std::vector<CPoint>& points, VectorDouble &vecResult);
	static int ImageScaleMax(CCHImageData* pSourceImage, CCHImageData* pResultImage);

protected:
	static void	InsertPolynomial(ListPolynomial &listPolynomial, int degree, double coef);
	static double	Combi(VectorDouble& vectorX, VectorDouble& vectorY, int n, int r);

};

