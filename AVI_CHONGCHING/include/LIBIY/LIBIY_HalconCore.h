#pragma once
#ifndef LIBIY_HALCONCORE
#define LIBIY_HALCONCORE

class CSharedDefectResult;
namespace LIBIY
{
	struct SDefect
	{
		int x;
		int y;
		int srcGray;
		int dstGray;
		int DefectNum;
	};

	void LIBIY_EXT_API CreateShapeModelID(ImageInfo8U Img, _rect roi, __int64 &ModelID);
	void LIBIY_EXT_API FindShapeModel(ImageInfo8U Img, _rect roi, __int64 ModelID, double &tx, double &ty);

	void LIBIY_EXT_API FindAlignMarker(ImageInfo8U Img, _rect roi, void* pAlignMarkerID, double &tx, double &ty);
	
	int LIBIY_EXT_API ImageTranslationWithHalcon(ImageInfo8U src, ImageInfo8U &dst, float tx, float ty);
	int LIBIY_EXT_API ImageRotationWithHalcon(ImageInfo8U src, ImageInfo8U &dst, double rad, float tx, float ty);


	int LIBIY_EXT_API GenTiledImageHalcon(int nImages, ImageInfo8U *Images, int RowOffset, int ColOffset, double *txs, double *tys, ImageInfo8U DstImage, int stratX = 0, int startY = 0);
	int LIBIY_EXT_API ImageWriteWithHalcon(ImageInfo8U &Img, const char* FileType, const char* FileName);
	int LIBIY_EXT_API ImageWriteWithHalcon(ImageInfoComplex &Img, const char* FileType, const char* FileName);
	int LIBIY_EXT_API ImageWriteWithHalcon(ImageInfoReal &Img, const char* FileType, const char* FileName);
	int LIBIY_EXT_API ImageWriteWithHalcon(ImageInfo32S &Img, const char* FileType, const char* FileName);
	void LIBIY_EXT_API action();
	int LIBIY_EXT_API InitGPUHalcon();
	ImageInfo8U LIBIY_EXT_API HImgToImageInfo8U(void* ObjPtr);
	/*int LIBIY_EXT_API BlobDefect(ImageInfo8U Src, ImageInfo8U Dst, _rect *SrcRoi, int *threshold, int nRoi, _rect *DeadRoi, int nDeadRoi, CSharedDefectResult *defect, int maxDefect, int nDieIndex);
	int LIBIY_EXT_API DiffImage(ImageInfo8U Src, ImageInfo8U Dst, _rect *SrcRoi, int *threshold, int nRoi, _rect *DeadRoi, int nDeadRoi, Point2Dlf match, CSharedDefectResult *defect, int maxDefect, int nDieIndex, bool bSaveDefectImage = false);*/
	
}

#endif // !LIBIY_HALCONCORE