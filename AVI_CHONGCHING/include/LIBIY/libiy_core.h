#pragma once
#ifndef LIBIY_CORE
#define LIBIY_CORE

#ifdef LIBIY_EXT
#include "../LIBIY_Core/version.h"
#include "../LIBIY_CommonObject/LIBIY_CommonObject.h"
#include "../LIBIY_Utility/LIBIY_Utility.h"
#include "../LIBIY_HalconCore/LIBIY_HalconCore.h"
#include "../LIBIY_Core/InspectionStructures.h"
#include "../LIBIY_CUDACore/LIBIY_CUDACore.h"
#include "../LIBIY_ImageCodec/LIBIY_ImageCodec.h"
#include "../LIBIY_Matching/LIBIY_MatchingCore.h"
#include "../LIBIY_Core/LIBIY_Core.Internal.h"
#include "../LIBIY_Core/LIBIY_Core.define.h"
#include "../LIBIY_IPPCore/LIBIY_IPPCore.h"

#else
#include "./LIBIY/LIBIY_CommonObject.h"
#include "./LIBIY/LIBIY_Utility.h"
#include "./LIBIY/LIBIY_CUDACore.h"
#include "./LIBIY/LIBIY_HalconCore.h"
#include "./LIBIY/LIBIY_ImageCodec.h"
#include "./LIBIY/LIBIY_MatchingCore.h"
#include "./LIBIY/LIBIY_Core.define.h"
#include "./LIBIY/InspectionStructures.h"
#include "./LIBIY/Blobbing.h"
#include "./LIBIY/LIBIY_IPPCore.h"

#endif


namespace LIBIY
{
    const char LIBIY_EXT_API * LIBIY_Version();
	__int64 LIBIY_EXT_API CalcImageDiffSub(BYTE * srcBuffer, BYTE* dstBuffer, BYTE* SubImage, int srcW, int srcH, int WidthStep, int SubImageWidthStep, double xShift, double yShift);
	__int64 LIBIY_EXT_API CalcImageDiffSQRSum(BYTE * srcBuffer, BYTE* dstBuffer, BYTE* SubImage, int srcW, int srcH, int WidthStep, int dstWidthStep, int SubImageWidthStep, double xShift, double yShift);
	__int64 LIBIY_EXT_API CalcImageDiffSum(BYTE * srcBuffer, BYTE* dstBuffer, BYTE* SubImage, int srcW, int srcH, int WidthStep, int SubImageWidthStep, double xShift, double yShift);
	__int64 LIBIY_EXT_API CalcImageDiffSQRSumRevA(BYTE * srcBuffer, BYTE* dstBuffer, BYTE* SubImage, int srcW, int srcH, int srcWidthStep, int dstWidthStep, int SubImageWidthStep, double xShift, double yShift);
	__int64 LIBIY_EXT_API CalcImageDiffSQRSumRevASSE(BYTE * srcBuffer, BYTE* dstBuffer, BYTE* SubImage, int srcW, int srcH, int srcWidthStep, int dstWidthStep, int SubImageWidthStep, double xShift, double yShift);
	double LIBIY_EXT_API CalcImageZNCC(BYTE * srcBuffer, BYTE* dstBuffer, BYTE* SubImage, int srcW, int srcH, int srcWidthStep, int dstWidthStep, int SubImageWidthStep, double xShift, double yShift);
	double LIBIY_EXT_API CalcImageZNCCSuppress(BYTE * srcBuffer, BYTE* dstBuffer, BYTE* SubImage, int srcW, int srcH, int srcWidthStep, int dstWidthStep, int SubImageWidthStep, double xShift, double yShift, int SrcSuppress, int DstSuppress);

	__int64 LIBIY_EXT_API BBK_multi(BYTE * srcBuff, BYTE* dstBuff, ArrPoint2D arrSrcPoint, ArrPoint2D arrDstPoint, BYTE* dstBuffer,
		int srcW, int srcH, int WidthStep, double xShift, double yShift, int dtx, int dty, float di, double &fMinx, double &fMiny);

	__int64 LIBIY_EXT_API BBK_multiRevA(ImageInfo8U &SrcImg, ImageInfo8U &DstImg, ImageInfo8U &SubImg, ArrPoint2D arrSrcPoint, ArrPoint2D arrDstPoint,
		int srcW, int srcH, double xShift, double yShift, int dt, float di, double &fMinx, double &fMiny);
	__int64 LIBIY_EXT_API BBK_multi_Interpolation(ImageInfo8U &SrcImg, ImageInfo8U &DstImg, ImageInfo8U &SubImg, ArrPoint2D arrSrcPoint, ArrPoint2D arrDstPoint,
		int srcW, int srcH, double xShift, double yShift, int dt, float di, double &fMinx, double &fMiny);

	__int64 LIBIY_EXT_API AffineTranslateImage(BYTE * srcBuffer, BYTE* dstBuffer, int srcW, int srcH, int WidthStep, float Mat[6]);
	//void FindAlignMarker(BYTE * imgBuffer, _rect roi, int widthStep, HTuple AlignMarkerID, float &tx, float &ty);
	__int64 LIBIY_EXT_API CalcRotationFromPoint2D(double tx1, double ty1, double tx2, double ty2, double &Radi);
	__int64 LIBIY_EXT_API SubImageIntrin(ImageInfo8U SrcImage, ImageInfo8U RefImage, ImageInfo8U DstImage, float tx, float ty);
	void LIBIY_EXT_API FindAlignMarker(ImageInfo8U Img, _rect roi, void* pAlignMarkerID, double &tx, double &ty);

	int LIBIY_EXT_API GenTiledImage(int nImages, ImageInfo8U *Images, int RowOffset, int ColOffset, double *txs, double *tys, ImageInfo8U DstImage, int stratX = 0, int startY = 0, int MarginX = 0, int MarginY = 0);

	void LIBIY_EXT_API CropImageToDstImage_subpixel(ImageInfo8U &SrcImage, ImageInfo8U &DstImage, double tx, double ty);
	void LIBIY_EXT_API CropImageToDstImage_subpixelNotAligned_Merge(ImageInfo8U &SrcImage, ImageInfo8U &DstImage, double tx, double ty);
	void LIBIY_EXT_API CropImageToDstImage(ImageInfo8U &SrcImage, ImageInfo8U &DstImage);
	void LIBIY_EXT_API CropImageToDstImage(ImageInfo16U &SrcImage, ImageInfo16U &DstImage);
	void LIBIY_EXT_API CropRoiRect(ImageInfo8U &SrcImage, ImageInfo8U &DstImage);

	void LIBIY_EXT_API CalcMean(ImageInfo8U src, float &mean);
	void LIBIY_EXT_API CalcDeviation(ImageInfo8U src, double &mean, double &dev);
	int LIBIY_EXT_API MeanImage(int nImages, int margin, ImageInfo8U *Images, ImageInfo8U meanImage);
	int LIBIY_EXT_API MinMaxImage(ImageInfo8U src1, ImageInfo8U src2, ImageInfo8U minImage, ImageInfo8U maxImage);
	int LIBIY_EXT_API CreateImageMask(ImageInfo8U &maskImage, ArrRect &ActiveRects, ArrRect &RemoveRects);
	int LIBIY_EXT_API CreateImageMask(ImageInfo8U &maskImage, ArrRect &ActiveRects, ArrRect &RemoveRects, arrInt &arrZone);
	int LIBIY_EXT_API RemoveImageMask(ImageInfo8U &maskImage, ArrRect &RemoveRects);
	int LIBIY_EXT_API ImageWriteRoiRect(ImageInfo8U &SrcImage, const char* FileName);
	__int64 LIBIY_EXT_API MatchingSSD_1D(ImageInfo8U src, ImageInfo8U ref, int range, int &tx, int &ty);

#ifndef __STL_ARRAY_NOT_SUPPORT__
	int LIBIY_EXT_API PeriodInspectionVert(ImageInfo8U &SrcImage, const SPeriodInspectionParam &Param, SInspectionOutput &OutData);
	int LIBIY_EXT_API PeriodInspectionVert_AVX512(ImageInfo8U &SrcImage, const SPeriodInspectionParam &Param, SInspectionOutput &OutData);
	int LIBIY_EXT_API PeriodInspectionVertRevA_AVX512(ImageInfo8U &SrcImage, const SPeriodInspectionParam &Param, SInspectionOutputBuffer &OutData);
	int LIBIY_EXT_API PeriodInspectionVertRevA(ImageInfo8U &SrcImage, const SPeriodInspectionParam &Param, SInspectionOutputBuffer &OutData);
	int LIBIY_EXT_API PeriodInspectionVertRevT(ImageInfo8U &SrcImage, const SPeriodInspectionParam &Param, SInspectionOutputBuffer &OutData);
	int LIBIY_EXT_API PeriodInspectionDiagonal(ImageInfo8U &SrcImage, const SPeriodInspectionParam &Param, SInspectionOutputBuffer &OutData);
	int LIBIY_EXT_API PeriodInspectionVertRevDPCTilt(ImageInfo8U &SrcImage, const SPeriodInspectionParam &Param, SInspectionOutputBuffer &OutData);
	int LIBIY_EXT_API PeriodInspectionVertRevZ(ImageInfo8U &SrcImage, const SPeriodInspectionParam &Param, SInspectionOutputBuffer &OutData);
	int LIBIY_EXT_API PeriodInspectionVertRevZ5(ImageInfo8U &SrcImage, const SPeriodInspectionParam &Param, SInspectionOutputBuffer &OutData);
	int LIBIY_EXT_API PeriodInspectionVertFreeform(ImageInfo8U &SrcImage, const SPeriodInspectionParam &Param, SInspectionOutputBuffer &OutData, bool pairFilter = false);
	int LIBIY_EXT_API PeriodInspectionDiagonalFreeform(ImageInfo8U &SrcImage, const SPeriodInspectionParam &Param, SInspectionOutputBuffer &OutData);
	int LIBIY_EXT_API PeriodInspectionVertFreeformwithLabel(ImageInfo8U &SrcImage, const SPeriodInspectionParam &Param, SInspectionOutputBuffer &OutData, bool pairFilter = false);
	int LIBIY_EXT_API PeriodInspectionDiagonalFreeformwithLabel(ImageInfo8U &SrcImage, const SPeriodInspectionParam &Param, SInspectionOutputBuffer &OutData);
	void LIBIY_EXT_API GenBlobWidthKDTree(KDTreeArrayInt2 &InputData, ArrPoint3D *blobs, int &nBlobs, int nDist, int bDeduplicate = 0);
#endif

	int LIBIY_EXT_API FastestGaussianFilter(ImageInfo8U &SrcImage, ImageInfo8U &DstImage, GAUSSIAN_KERNEL_SIZE kernelSize);
	__int64 LIBIY_EXT_API CalcImageDiffSQRSum_Range(ImageInfo8U &SrcImage, ImageInfo8U &DstImage, int &tx, int &Ty, ImageInfo32S &DstImg);
	__int64 LIBIY_EXT_API CalcImageDiffSQRSum_RangeInt(ImageInfo8U &SrcImage, ImageInfo8U &DstImage, int &tx, int &Ty, ImageInfo32S &DstImg);
	__int64 LIBIY_EXT_API CalcImageDiffSQRSum_RangeCrop(ImageInfo8U &SrcImage, ImageInfo8U &DstImage, double &tx, double &Ty, ImageInfo32S &DstImg);
	
	__int64 LIBIY_EXT_API CalcImageDiffSQRSum_RangeCrop2(ImageInfo8U &SrcImage, ImageInfo8U &DstImage, int &tx, int &Ty, ImageInfo32S &DstImg);

	__int64 LIBIY_EXT_API CalcImageDiffSQRSum_RangeCropSuppress(ImageInfo8U &Template, ImageInfo8U &TargetImg, int suppressTemp, int suppressTarg, double &tx, double &ty, ImageInfo32S &DstImg);
	__int64 LIBIY_EXT_API Intensity(ImageInfo8U &Src, double &_Mean, double &Stdev);


	__int64 LIBIY_EXT_API SetImage(ImageInfoComplex &SrcImage, _float2 Val);
	__int64 LIBIY_EXT_API BayerDemosaicRG(ImageInfo8U &SrcImg, ImageInfo8U &R, ImageInfo8U &G, ImageInfo8U &B);
	__int64 LIBIY_EXT_API BayerDemosaicGB(ImageInfo8U &SrcImg, ImageInfo8U &R, ImageInfo8U &G, ImageInfo8U &B);

	__int64 LIBIY_EXT_API Calc_UF_DynamicPitch(ImageInfo8U &SrcImg, float fxPitch, float fyPitch, float dtx, float dty, int ntx, int nty, int interval, int RectWidth, int RectHeight, int* OutBuff, void* ExternalMemoryPool);
	__int64 LIBIY_EXT_API ScaleImage(ImageInfo8U &Src, ImageInfo8U &Dst, float fMult, float fadd);

	//////////////////////////////////////////////////////////////////////////
	/// Extern Memory Pool 은 Src Img Width*2 + 64 이상의 메모리를 전달, 0 일 경우 내부에서 할당
	__int64 LIBIY_EXT_API BayerCFAToRGB(ImageInfo8U &SrcImg, ImageInfo8UC3 &Dst, BayerGrid Grid, BYTE* ExternMemoryPool);
	
}
#endif
