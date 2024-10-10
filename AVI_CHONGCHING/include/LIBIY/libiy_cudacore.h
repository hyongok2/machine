#pragma once
#ifndef LIBIY_CUDACORE
#define LIBIY_CUDACORE
typedef unsigned short USHORT;
#define LIBIY_TIMER
#ifdef LIBIY_EXT

#include "../LIBIY_Core/LIBIY_Core.h"

#else
#include "../LIBIY/LIBIY_CommonObject.h"
#include "../LIBIY/LIBIY_Utility.h"
#include "../LIBIY/InspectionStructures.h"

#endif 

class CSharedDefectResult;
namespace LIBIY
{
	struct ScuBBKBuffer
	{
		float *cuResultData;
		float *cuResultSum;
	};

	struct SCuBlobFeatures
	{
		short sDefectArea;
		float fDefectPixelX;
		float fDefectPixelY;
		short sDefectLeft;
		short sDefectRight;
		short sDefectTop;
		short sDefectBottom;
		float fDefectGrayAvg;
		short sDefectGrayMin;
		short sDefectGrayMax;
		float fDefectPeakAvg;
		short sDefectPeakMin;
		short sDefectPeakMax;
		float fDefectLengthMax;
		float fDefectLengthMin;
		float fM20;
		float fM02;
		float fM11;
	};

	struct ScuFeatureInfo
	{

	};
	int LIBIY_EXT_API cuCreateStream(void** p);
	int LIBIY_EXT_API cuDestroyStream(void *p);
	int LIBIY_EXT_API cuCudaReset();
	int LIBIY_EXT_API cuGetTemperature(unsigned int &temp);
	int LIBIY_EXT_API cuMemoryAlloc(void** cuBuff, __int64 size);
	int LIBIY_EXT_API cuMemoryFree(void* cuBuff);
	int LIBIY_EXT_API cuMemorySet(void* cuBuff, BYTE setVal, __int64 size);
	int LIBIY_EXT_API cuMemoryGetInfo(size_t& totalByte, size_t &usedByte, size_t &freeByte);
	int LIBIY_EXT_API cpuMemoryGetInfo(size_t& totalByte, size_t &usedByte, size_t &freeByte);
	int LIBIY_EXT_API cuMemoryCopyHostToDevice(void* cuBuff,const void* memBuff, __int64 size, void* cuStream);
	int LIBIY_EXT_API cuMemoryCopyDeviceToHost(void* cuBuff, void* memBuff, __int64 size, void* cuStream);
	int LIBIY_EXT_API cuMemoryCopy2DHostToDevice(ImageInfo8U cuDst, ImageInfo8U src, void* cuStream);
	int LIBIY_EXT_API cuMemoryCopy2DDeviceToHost(ImageInfo8U & cuSrc, ImageInfo8U & dst, void* cuStream);
	void LIBIY_EXT_API BetweenVectorAngle(Point2Df v1, Point2Df v2, float &rad, float &deg);

	void LIBIY_EXT_API RadialDistortionCorrect(CamParam c, unsigned char * p, unsigned char* dst);

	void LIBIY_EXT_API Coeff2DIdentity(double rtMat[2][3]);
	void LIBIY_EXT_API Coeff2D2DTranslate(double inMat[2][3], double tx, double ty, double rtMat[2][3]);
	void LIBIY_EXT_API Coeff2D2DRotate(double inMat[2][3], double rad, double cx, double cy, double rtMat[2][3]);
	void LIBIY_EXT_API CuAffineTransImageHostImage(ImageInfo8U &src, ImageInfo8U &dst, double tx, double ty);
	void LIBIY_EXT_API CuAffineTransImageHostImage(ImageInfo8U &src, ImageInfo8U &dst, double rad, double cx, double cy);
	void LIBIY_EXT_API CuAffineTransImageHostImage(ImageInfo8U &src, ImageInfo8U &dst, double aCoeffs[2][3]);
	bool LIBIY_EXT_API CuAffineTransImageDeviceImage(ImageInfo8U &cuSrc, ImageInfo8U &cuDst, double aCoeffs[2][3]);
	bool LIBIY_EXT_API CuAffineTransImageDeviceImage(ImageInfo8U &cuSrc, ImageInfo8U &cuDst, double rad, double cx, double cy);
	bool LIBIY_EXT_API CuResizeImageHostImage(ImageInfo8U &src, ImageInfo8U &dst, double sx, double sy);
	bool LIBIY_EXT_API CuResizeImageDeviceImage(ImageInfo8U cuSrc, ImageInfo8U &cuDst, double sx, double sy);

	__int64 LIBIY_EXT_API LIBIY_cuMatching(ImageInfo8U &SrcImage, ImageInfo8U &DstImage, float& Tx, float& Ty, float& Score);
	__int64 LIBIY_EXT_API cuPrepareTemplateFFT(ImageInfo8U &SrcImage, ImageInfoComplex &PrepareTemplate, Point2D BorderSize, float fThreshold);
	__int64 LIBIY_EXT_API LIBIY_cuMatching_PreProcessTemplate(ImageInfoComplex &TemplateFFT, ImageInfo8U &DstImage, float& Tx, float& Ty, float& Score);

	void LIBIY_EXT_API GencuBBKBuffer(ScuBBKBuffer &buffer, int Width, int Height, int ntdx = 9, int ndty = 9);
	void LIBIY_EXT_API DestroycuBBKBuffer(ScuBBKBuffer &buffe);
	float LIBIY_EXT_API cuBBK_multi(ImageInfo8U cuSrc, ImageInfo8U cuRef, float xShift, float yShift, int ndt, float dt, float&tx, float &ty, ScuBBKBuffer &buf, void* cuStream);

	int   LIBIY_EXT_API cuAbsDiffImage(ImageInfo8U cuSrc, ImageInfo8U cuRef, ImageInfo8U cuDiff, float tx, float ty);
	__int64 LIBIY_EXT_API PreiodInspection(ImageInfo8U &SrcImage, ImageInfo8U &DstImage);

#ifndef __STL_ARRAY_NOT_SUPPORT__	
	bool LIBIY_EXT_API CuReferenceInspectionHostImage(ImageInfo8U &src, ImageInfo8U &ref, ImageInfo8U &mask, SReferenceInspectionParam& inspectParam, SInspectionOutput& outputParam, bool bSaveDiffImage = false);
	bool LIBIY_EXT_API CuReferenceInspectionDeviceImage(ImageInfo8U &cuSrc, ImageInfo8U cuRef, ImageInfo8U &cuMask, SReferenceInspectionParam& inspectParam, SInspectionOutput& outputParam, bool bSaveDiffImage = false);
	void LIBIY_EXT_API CuLabelingHostImage(ImageInfo8U src, int maxDefect, std::vector<Point2D> *blob, int &nBlob);
	void LIBIY_EXT_API CuLabelingDeviceImage(ImageInfo8U cuSrc, int maxDefect, std::vector<Point2D> *blob, int &nBlob);
	void LIBIY_EXT_API CuCalcBlobFeatures(ArrPoint3D *blob, int &nBlob, SDefectResult* arrInputDefect, SCuBlobFeatures *arrOutputDefect);
	
	size_t LIBIY_EXT_API GetCuPeriodInspParamBufferSize(int maxDefect);
	size_t LIBIY_EXT_API GetCuRefInspParamBufferSize(int maxDefect);


	void LIBIY_EXT_API CuInspectionHostImage(ImageInfo8U &src, ImageInfo8U &ref, Point2Dlf matchOffset, int threshold, int maxDefect);
	void LIBIY_EXT_API CuInspectionDeviceImage(ImageInfo8U &cuSrc, ImageInfo8U &cuRef, Point2Dlf matchOffset, int threshold, int maxDefect);
	int LIBIY_EXT_API CuPeriodInspectionVert(ImageInfo8U &cuSrcImage, const SPeriodInspectionParam &Param, SInspectionOutput &OutData, void* cuParamBuffer = 0, void* cuStream = 0);
	int LIBIY_EXT_API CuPeriodInspectionHori(ImageInfo8U &cuSrcImage, const SPeriodInspectionParam &Param, SInspectionOutput &OutData, void* cuParamBuffer = 0, void* cuStream = 0);
	bool LIBIY_EXT_API CuDiffOfGaussHostImage(ImageInfo8U &src, ImageInfo8U &dst);
	bool LIBIY_EXT_API CuReferenceInspectionHostImage(ImageInfo8U src, ImageInfo8U ref, ImageInfo8U mask, ImageInfo8U sub, SReferenceInspectionParam& inspectParam, SInspectionOutput& outputParam, bool bSaveDiffImage = false);
	bool LIBIY_EXT_API CuReferenceInspectionDeviceImage(ImageInfo8U &cuSrc, ImageInfo8U cuRef, ImageInfo8U &cuMask, ImageInfo8U cuSub, SReferenceInspectionParam& inspectParam, SInspectionOutput& outputParam, bool bSaveDiffImage = false);
	bool LIBIY_EXT_API CuReferenceInspectionDeviceImageRevA(ImageInfo8U cuSrc, ImageInfo8U cuRef, ImageInfo8U cuMask, ImageInfo8U cuSub, SReferenceInspectionParam& inspectParam, SInspectionOutput& outputParam);
	bool LIBIY_EXT_API CuNearReferenceInspectionDeviceImage(ImageInfo8U cuSrc, ImageInfo8U cuRef, ImageInfo8U cuRef2, ImageInfo8U cuMask, ImageInfo8U cuSub, SReferenceInspectionParam& inspectParam, SInspectionOutput& outputParam, void* cuParamBuffer = 0, void* cuStream = 0);
	bool LIBIY_EXT_API CuNearReferenceInspectionHostImage(ImageInfo8U src, ImageInfo8U ref, ImageInfo8U ref2, ImageInfo8U mask, ImageInfo8U sub, SReferenceInspectionParam& inspectParam, SInspectionOutput& outputParam);
	bool LIBIY_EXT_API CuBinalizeInspectionHostImage(ImageInfo8U src, int minTh, int maxTh, SInspectionOutput &output);
	bool LIBIY_EXT_API CuBinalizeInspectionDeviceImage(ImageInfo8U cuSrc, int minTh, int maxTh, SInspectionOutput &output);
	bool LIBIY_EXT_API MasterReferenceFilter(const ImageInfo8U &masterRef, const SThreshold &threshold, float scale, SInspectionOutput &output);
	bool LIBIY_EXT_API CuImageWrite(ImageInfo8U& cuImage, char* path);
#endif

#pragma region CuImageProcessing
	bool LIBIY_EXT_API CuGaussImageDeviceImage(ImageInfo8U cuSrc, ImageInfo8U &cuDst, int kernelSize = 5);
	bool LIBIY_EXT_API CuGaussImageHostImage(ImageInfo8U src, ImageInfo8U &dst, int kernelSize = 5);
	bool LIBIY_EXT_API CuBinningGaussFilterDeviceImage(ImageInfo8U cuSrc, ImageInfo8U &cuDst, int binningSize, int gaussKernelSize);
	bool LIBIY_EXT_API CuBinningGaussFilterHostImage(ImageInfo8U src, ImageInfo8U &dst, int binningSize, int gaussKernelSize);
	bool LIBIY_EXT_API CuScaleImageHostImage(ImageInfo8U src, ImageInfo8U &dst, float mult, float add);
	bool LIBIY_EXT_API CuScaleImageDeviceImage(ImageInfo8U cuSrc, ImageInfo8U &cuDst, float mult, float add);
	bool LIBIY_EXT_API CuScaleImageMaxDeviceImage(ImageInfo8U cuSrc, ImageInfo8U &cuDst);
	bool LIBIY_EXT_API CuScaleImageMaxHostImage(ImageInfo8U src, ImageInfo8U &dst);


#pragma endregion

	template <typename T>
	bool GenCuImageFromImageInfo(ImgInfo<T> Src, ImgInfo<T> &cuDst)
	{
		if (Src.imgPtr == nullptr)
			return false;

		cuDst.GenImageConst(Src.Width, Src.Height, Src.WStep, cuMemoryAlloc, cuMemoryFree);
		cuDst.SetRoi(Src.roi);

		return cuMemoryCopyHostToDevice(cuDst.imgPtr, Src.imgPtr, Src.GetImageBufferSize(), 0);
	}
}

#endif
