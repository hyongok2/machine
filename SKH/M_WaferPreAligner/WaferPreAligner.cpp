#include "pch.h"
#include "WaferPreAligner.h"

#include "CHImageData.h"
#include "CHImageProcess.h"

#ifdef _DEBUG
#pragma comment(lib,"M_ImageControls_Debug.lib")
#pragma comment(lib,"LIBIY_CoreD.lib")
#pragma comment(lib,"LIBIY_UtilityD.lib")
#pragma comment(lib,"LIBIY_MatchingD.lib")
#pragma comment(lib,"LIBIY_ImageCodecD.lib")
#else
#pragma comment(lib,"M_ImageControls_Release.lib")
#pragma comment(lib,"LIBIY_Core.lib")
#pragma comment(lib,"LIBIY_Utility.lib")
#pragma comment(lib,"LIBIY_Matching.lib")
#pragma comment(lib,"LIBIY_ImageCodec.lib")
#endif // !_DEBUG

CWaferPreAlignerParam::CWaferPreAlignerParam()
{
	m_bSaveResult = 0;
	m_nEdgeThreshold = 10;
	m_bUseInspect = 1;
}

CWaferPreAlignerParam::~CWaferPreAlignerParam()
{

}

void CWaferPreAlignerParam::Reset()
{
	m_bSaveResult = 0;
	m_bSetParam = 0;
	m_bUseGrayScaleMax = 0;			// 영상의 밝기 차 보정 전처리 사용 유무
	m_nEdgeThreshold = 100;			// 영상 이진화 문턱값(0~255)

	m_dNotchFindThreshold = 12;		// Notch 탐색을 위한 Wafer 외접 타원에서 실제 Edge 까지의 거리 문턱값
	m_nNotchFindROILeft = 0;		// Notch 탐색 영역 시작 X 좌표(Pixel)
	m_nNotchFindROIRight = 0;		// Notch 탐색 영역 끝 X 좌표(Pixel)
	m_nNotchFindROITop = 0;			// Notch 탐색 영역 시작 Y 좌표(Pixel)
	m_nNotchFindROIBottom = 0;		// Notch 탐색 영역 끝 Y 좌표(Pixel)

	m_dRefDiameter = 2600;			// Wafer 기준 지름 크기(Pixel)
	m_dRefCenterX = 0;				// Wafer 기준 중심 X 좌표(Pixel)
	m_dRefCenterY = 0;				// Wafer 기준 중심 Y 좌표(Pixel)
	m_dRefNotchDegree = 0.0;		// Wafer 기준 Notch Angle (Degree)

	m_bUseInspect = 1;
}

CWaferPreAlignerResult::CWaferPreAlignerResult() : CWaferPreAlignerParam()
{
	m_nCode = WaferPreAlignerResult_Fail;
	m_dDx = 0.0;
	m_dDy = 0.0;
	m_dDTheta = 0.0;

	m_dCenterX = 0.0;
	m_dCenterY = 0.0;

	m_dMajorLength = 0.0;
	m_dMajorStartX = 0.0;
	m_dMajorStartY = 0.0;
	m_dMajorEndX = 0.0;
	m_dMajorEndY = 0.0;
	m_dMajorRadian = 0.0;
	m_dMajorDegree = 0.0;

	m_dMinorLength = 0.0;
	m_dMinorStartX = 0.0;
	m_dMinorStartY = 0.0;
	m_dMinorEndX = 0.0;
	m_dMinorEndY = 0.0;

	m_dNotchX = 0.0;
	m_dNotchY = 0.0;
	m_dNotchRadian = 0.0;
	m_dNotchDegree = 0.0;

	m_nDefectCount = 0;
}

CWaferPreAlignerResult::~CWaferPreAlignerResult()
{

}

void CWaferPreAlignerResult::Reset()
{
	CWaferPreAlignerParam::Reset();

	m_nCode = WaferPreAlignerResult_Fail;
	m_dDx = 0.0;
	m_dDy = 0.0;
	m_dDTheta = 0.0;

	m_dCenterX = 0.0;
	m_dCenterY = 0.0;

	m_dMajorLength = 0.0;
	m_dMajorStartX = 0.0;
	m_dMajorStartY = 0.0;
	m_dMajorEndX = 0.0;
	m_dMajorEndY = 0.0;
	m_dMajorRadian = 0.0;
	m_dMajorDegree = 0.0;

	m_dMinorLength = 0.0;
	m_dMinorStartX = 0.0;
	m_dMinorStartY = 0.0;
	m_dMinorEndX = 0.0;
	m_dMinorEndY = 0.0;

	m_dNotchX = 0.0;
	m_dNotchY = 0.0;
	m_dNotchRadian = 0.0;
	m_dNotchDegree = 0.0;

	m_nDefectCount = 0;
}

CWaferPreAligner::CWaferPreAligner()
{
	m_AlignResult.Reset();
	m_pImageData = new CCHImageData();
	m_pArrDefect = nullptr;
}

CWaferPreAligner::~CWaferPreAligner()
{
	if (m_pImageData) delete m_pImageData;
	m_pImageData = nullptr;
	m_AlignResult.Reset();
	m_pArrDefect = nullptr;
}

const CWaferPreAlignerResult* CWaferPreAligner::GetPreAlignerResult() const
{
	return &m_AlignResult;
}

void CWaferPreAligner::SetPreAlignerParam(const CWaferPreAlignerResult& param, PreAlignDefect* pArrDefect)
{
	m_AlignResult.Reset();

	// param copy
	m_AlignResult = param;
	
	if (m_pArrDefect) m_pArrDefect = nullptr;
	if (pArrDefect) m_pArrDefect = pArrDefect;
	
	m_AlignResult.SetParam(true);
}

int CWaferPreAligner::Process_PreAlign(const char* pImageBuffer, int nImageWidth, int nImageHeight, const CWaferPreAlignerResult& param)
{
	// set param
	SetPreAlignerParam(param);

	// process
	return Process_PreAlign(pImageBuffer, nImageWidth, nImageHeight);
}

int CWaferPreAligner::Process_PreAlign(const char* pImageBuffer, int nImageWidth, int nImageHeight, const CWaferPreAlignerResult& param, PreAlignDefect* pArrDefect)
{
	// set param
	SetPreAlignerParam(param, pArrDefect);

	// process
	return Process_PreAlign(pImageBuffer, nImageWidth, nImageHeight);
}

int CWaferPreAligner::Process_PreAlign(const char* pImageBuffer, int nImageWidth, int nImageHeight)
{
	// error check
	if (m_pImageData == nullptr) return WaferPreAlignerResult_ImageNull;
	if (pImageBuffer == NULL || nImageWidth < 1 || nImageHeight < 0) return WaferPreAlignerResult_ImageNull;

	// image copy
	if (m_pImageData->CreateImage(nImageWidth, nImageHeight, 8, 1) == FALSE) return WaferPreAlignerResult_ImageNull;
	memcpy(m_pImageData->GetImageBuffer(), pImageBuffer, m_pImageData->GetImageSize());
	//m_pImageData->SaveImage(_T("d:\\work\\checkImage.bmp"));

	if (m_AlignResult.GetParam_IsSetParam() == false) return WaferPreAlignerResult_ParamNull;

	// align process
	//return PreAlignProcess();
	return PreAlignProcessByStep();
}

int CWaferPreAligner::PreAlignProcess()
{
	if (m_pImageData == nullptr || !m_pImageData->GetImageExist() || m_pImageData->GetChannels()!=1) 
		return WaferPreAlignerResult_ImageNull;
	
	// Parameters ///////////////////////////////////////////////////////////////////////////////////////
	double dThreshold = m_AlignResult.GetParam_EdgeThreshold();
	double dErrorThreshold = m_AlignResult.GetParam_EllipseSizeThreshold();
	double dNotchFindThreshold = m_AlignResult.GetParam_NotchFindThreshold();
	double dRefDiameter = m_AlignResult.GetParam_RefDiameter();
	
	CRect rtROI;
	rtROI.left = m_AlignResult.GetParam_NotchFindROILeft();
	rtROI.right = m_AlignResult.GetParam_NotchFindROIRight();
	rtROI.top = m_AlignResult.GetParam_NotchFindROITop();
	rtROI.bottom = m_AlignResult.GetParam_NotchFindROIBottom();
	
	// Processing Image ///////////////////////////////////////////////////////////////////////////////////////
	CCHImageData imgResult;
	CCHImageData imgThreshold;
	CCHImageData imgScale;
	CCHImageData imgEdge;
	CCHImageData imgEdgeClone;
	CCHImageData imgBlob;

	// Process Variables ///////////////////////////////////////////////////////////////////////////////////////
	CCHImageProcess::VectorDouble vecResult;
	VectorDefectBlob vecBlob;
	SDefectBlob* pResultBlob = nullptr;
	VectorDefectBlobIt itResult;
	VectorPointDouble vecPointDouble;
	VectorPointDouble vecPointDoubleThinning;

	CString strSaveResult = _T("ResultImage.bmp");
	CString strSaveBlob = _T("BlobImage.bmp");
	CString strSaveBinary = _T("BinaryImage.bmp");
	CString strSaveEdge = _T("EdgeImage.bmp");

	__int64 iPixel = 0;
	bool   bMajorAxisX = false; // 장축방향
	double dMinError = INT_MAX;
	
	double dCenterX, dCenterY;
	double dMajorLength, dMinorLength;
	double dMajorDegree;
	double dMajorRadian;
	double dNotchX, dNotchY;
	double dMajorStartX, dMajorStartY;
	double dMajorEndX, dMajorEndY;
	double dMinorStartX, dMinorStartY;
	double dMinorEndX, dMinorEndY;
	double dFocalX1, dFocalY1;
	double dFocalX2, dFocalY2;
	double dMajorVertexDistance;
	double dDistance = 0.0;
	//double dRadius, dTheta;
	int nCount = 0;
	double dSizeError = 0.0;
	BYTE* pBuffer = nullptr;

	try
	{
		// 0. Prepare Process
		if (rtROI.IsRectEmpty()) throw 0;

		// Result Image Convert Gray to RGB	
		if (m_AlignResult.GetParam_IsSaveResult())
			CCHImageProcess::ImageConvert(m_pImageData, &imgResult, 0);

		// 0.1 Gray Scale Max (선택적 전처리 : 영상의 밝기 향상)
		if (m_AlignResult.GetParam_UseGrayScaleMax())
		{
			if (CCHImageProcess::ImageScaleMax(m_pImageData, &imgScale) != 1) throw 1;
			memcpy(m_pImageData->GetImageBuffer(), imgScale.GetImageBuffer(), m_pImageData->GetImageSize());
			imgScale.ReleaseImage();
		}

		// 1. 영상 이진화(외곽선 추출을 위한 전처리)
		if (CCHImageProcess::ImageThreshold(m_pImageData, &imgThreshold, dThreshold, 255.0, 0) != 1) throw 2;

		// 2. Edge 탐색(Sobel Mask 이용)
		if (CCHImageProcess::ImageSobel(&imgThreshold, &imgEdge, 1, 1) != 1) throw 3;

		// 3. Blob 탐색을 위한 준비 (Blob 입력 영상은 변경됨)
		if (imgEdge.CopyImageTo(&imgEdgeClone) != TRUE) throw 4;
		if (imgBlob.CreateImage(imgEdge.GetWidthStep(), imgEdge.GetHeight()) != TRUE) throw 5;
		pBuffer = imgBlob.GetImageBuffer();

		// 4. 외곽선 Blobbing
		if (CCHImageProcess::ImageBlobAnalysis_Binary(imgEdgeClone.GetImageBuffer(), imgEdgeClone.GetWidth(), imgEdgeClone.GetHeight(), 128, 1, 0, 0, vecBlob) != 1)
			throw 6;
		if (vecBlob.size() == 0) throw 7;

		// 5. 입력된 Ref 정보와 Minimum Error 타원 선택
		dMinError = INT_MAX;
		itResult = vecBlob.end();
		for (VectorDefectBlobIt it = vecBlob.begin(); it != vecBlob.end(); ++it)
		{
			if (CCHImageProcess::EllipseFitting(it->vectorPoint, vecResult))
			{
				dCenterX = vecResult[0];
				dCenterY = vecResult[1];
				dMajorLength = (vecResult[2] >= vecResult[3]) ? vecResult[2] : vecResult[3];
				dMinorLength = (vecResult[2] < vecResult[3]) ? vecResult[2] : vecResult[3];
				dMajorDegree = vecResult[4];
				dMajorRadian = dMajorDegree * PI / 180;

				dSizeError = sqrt(((dMajorLength - dRefDiameter) * (dMajorLength - dRefDiameter)) + ((dMinorLength - dRefDiameter) * (dMinorLength - dRefDiameter)));
				if (dSizeError < dMinError)
				{
					itResult = it;
					dMinError = dSizeError;
					m_AlignResult.SetResult_CenterX(dCenterX);
					m_AlignResult.SetResult_CenterY(dCenterY);
					m_AlignResult.SetResult_MajorLength(vecResult[2]);
					m_AlignResult.SetResult_MinorLength(vecResult[3]);
					m_AlignResult.SetResult_MajorDegree(dMajorDegree);
					m_AlignResult.SetResult_MajorRadian(dMajorRadian);
				}
			}
			else
				throw 8;
		}
		if (itResult == vecBlob.end() || itResult->vectorPoint.size() == 0) throw 9;
		
		// Minimum Error가 Threhold보다 높으면 잘못 찾은걸로 간주한다.
		if (dMinError > dErrorThreshold) throw 9;

		// 6. 선택된 타원의 특성 산출 (중심위치, 초점위치)
		if (m_AlignResult.GetResult_MajorLength() > 0 && m_AlignResult.GetResult_MinorLength() > 0)
		{
			// 6.0. Set Variable for Calculation
			dCenterX = m_AlignResult.GetResult_CenterX();
			dCenterY = m_AlignResult.GetResult_CenterY();
			dMajorLength = m_AlignResult.GetResult_MajorLength();
			dMinorLength = m_AlignResult.GetResult_MinorLength();
			dMajorDegree = m_AlignResult.GetResult_MajorDegree();
			dMajorRadian = m_AlignResult.GetResult_MajorRadian();
			bMajorAxisX = (dMajorLength >= dMinorLength) ? true : false;

			if (!bMajorAxisX)
			{
				double dTemp = dMinorLength;
				dMinorLength = dMajorLength;
				dMajorLength = dTemp;
			}

			// 6.1. Calc Major Position
			dMajorStartX = bMajorAxisX ? dCenterX - dMajorLength / 2.0 : dCenterX;
			dMajorStartY = bMajorAxisX ? dCenterY : dCenterY - dMajorLength / 2.0;
			dMajorEndX = bMajorAxisX ? dCenterX + dMajorLength / 2.0 : dCenterX;
			dMajorEndY = bMajorAxisX ? dCenterY : dCenterY + dMajorLength / 2.0;
			RotatePointF(dMajorStartX, dMajorStartY, dMajorRadian, dCenterX, dCenterY);
			RotatePointF(dMajorEndX, dMajorEndY, dMajorRadian, dCenterX, dCenterY);
			m_AlignResult.SetResult_MajorPosStartX(dMajorStartX);
			m_AlignResult.SetResult_MajorPosStartY(dMajorStartY);
			m_AlignResult.SetResult_MajorPosEndX(dMajorEndX);
			m_AlignResult.SetResult_MajorPosEndY(dMajorEndY);

			// 6.2. Calc Minor Position
			dMinorStartX = !bMajorAxisX ? dCenterX - dMinorLength / 2.0 : dCenterX;
			dMinorStartY = !bMajorAxisX ? dCenterY : dCenterY - dMinorLength / 2.0;
			dMinorEndX = !bMajorAxisX ? dCenterX + dMinorLength / 2.0 : dCenterX;
			dMinorEndY = !bMajorAxisX ? dCenterY : dCenterY + dMinorLength / 2.0;
			RotatePointF(dMinorStartX, dMinorStartY, dMajorRadian, dCenterX, dCenterY);
			RotatePointF(dMinorEndX, dMinorEndY, dMajorRadian, dCenterX, dCenterY);
			m_AlignResult.SetResult_MinorPosStartX(dMinorStartX);
			m_AlignResult.SetResult_MinorPosStartY(dMinorStartY);
			m_AlignResult.SetResult_MinorPosEndX(dMinorEndX);
			m_AlignResult.SetResult_MinorPosEndY(dMinorEndY);

			// 6.3. Calc focal points
			double dA = dMajorLength / 2.0;
			double dB = dMinorLength / 2.0;
			dFocalX1 = bMajorAxisX ? (dCenterX + sqrt(dA * dA - dB * dB)) : dCenterX;
			dFocalY1 = bMajorAxisX ? dCenterY : (dCenterY + sqrt(dA * dA - dB * dB));
			dFocalX2 = bMajorAxisX ? (dCenterX - sqrt(dA * dA - dB * dB)) : dCenterX;
			dFocalY2 = bMajorAxisX ? dCenterY : (dCenterY - sqrt(dA * dA - dB * dB));
			RotatePointF(dFocalX1, dFocalY1, dMajorRadian, dCenterX, dCenterY);
			RotatePointF(dFocalX2, dFocalY2, dMajorRadian, dCenterX, dCenterY);

			// Draw Debug
			if (m_AlignResult.GetParam_IsSaveResult())
			{
				imgResult.DrawEllipse(CPoint(dCenterX, dCenterY), bMajorAxisX ? CPoint(dMajorLength, dMinorLength) : CPoint(dMinorLength, dMinorLength), dMajorDegree, RGB(255, 0, 0));
				imgResult.DrawLine(CPoint(dMajorStartX, dMajorStartY), CPoint(dMajorEndX, dMajorEndY), RGB(0, 255, 255));
				imgResult.DrawLine(CPoint(dMinorStartX, dMinorStartY), CPoint(dMinorEndX, dMinorEndY), RGB(255, 255, 0));
				imgResult.DrawLine(CPoint(dFocalX1 - 5, dFocalY1 - 5), CPoint(dFocalX1 + 5, dFocalY1 + 5), RGB(255, 0, 0), 1);
				imgResult.DrawLine(CPoint(dFocalX1 - 5, dFocalY1 + 5), CPoint(dFocalX1 + 5, dFocalY1 - 5), RGB(255, 0, 0), 1);
				imgResult.DrawLine(CPoint(dFocalX2 - 5, dFocalY2 - 5), CPoint(dFocalX2 + 5, dFocalY2 + 5), RGB(255, 0, 0), 1);
				imgResult.DrawLine(CPoint(dFocalX2 - 5, dFocalY2 + 5), CPoint(dFocalX2 + 5, dFocalY2 - 5), RGB(255, 0, 0), 1);
			}

			// 6.4. 기준거리 계산 (두 초점과 Major 꼭지점까지의 거리의 합)
			dMajorVertexDistance = CalcEllipseDistance(dMajorStartX, dMajorStartY, dFocalX1, dFocalY1, dFocalX2, dFocalY2);
		}

		// 7. Find Notch
		//FindNotchByThinning(char* pBlobImage, VectorDefectBlob vecBlob, )
		// 7. Fitting 된 타원의 경계와 실제 Wafer 경계비교 (입력된 임계거리를 벗어나는 지점 탐색)
		vecPointDouble.clear();
		memset(pBuffer, 0, sizeof(char) * imgEdge.GetWidthStep() * imgEdge.GetHeight());
		for (int i = 0; i < itResult->vectorPoint.size(); ++i)
		{
			iPixel = (itResult->vectorPoint[i].y * imgBlob.GetWidthStep()) + itResult->vectorPoint[i].x;

			// 7.1. Calc distance from ellipse outline to edge points
			dDistance = CalcEllipseDistance(itResult->vectorPoint[i].x, itResult->vectorPoint[i].y, dFocalX1, dFocalY1, dFocalX2, dFocalY2);
			dDistance = abs(dDistance - dMajorVertexDistance);
			pBuffer[iPixel] = dDistance > dNotchFindThreshold ? 255 : 0;

			// 7.2. Notch 탐색 ROI의 Distance 다른 점들의 모음
			if (dDistance > dNotchFindThreshold && rtROI.PtInRect(CPoint(itResult->vectorPoint[i].x, itResult->vectorPoint[i].y)))
			{
				vecPointDouble.push_back(PointDouble(itResult->vectorPoint[i].x, itResult->vectorPoint[i].y));
			}
		}

		// 8. Notch 위치 산출
		// Notch가 3시방향이라는 가정에 최적화한 부분.
		if (vecPointDouble.size() == 0) throw 10;
		
		// 8.1. sorting by Y Pixel Position
		std::sort(vecPointDouble.begin(), vecPointDouble.end(), [](PointDouble a, PointDouble b) {
			return (a.y < b.y);
		});

		// 8.2. Thinning Points (동일한 Y좌표의 최우측 X만 선택한다.)
		double yy = vecPointDouble[0].y;
		vecPointDoubleThinning.clear();
		vecPointDoubleThinning.push_back(vecPointDouble[0]);
		int ii = 0;
		for (int i = 1; i < vecPointDouble.size(); ++i)
		{
			if (vecPointDoubleThinning[ii].y == vecPointDouble[i].y)
			{
				vecPointDoubleThinning[ii].x = vecPointDoubleThinning[ii].x > vecPointDouble[i].x ? vecPointDouble[i].x : vecPointDoubleThinning[ii].x;
			}
			else
			{
				vecPointDoubleThinning.push_back(vecPointDouble[i]);
				ii++;
			}
		}

		// 8.3. Get Median Position
		int iMedian = vecPointDoubleThinning.size() / 2 + (vecPointDoubleThinning.size() % 2);
		dNotchX = vecPointDoubleThinning[iMedian].x;
		dNotchY = vecPointDoubleThinning[iMedian].y;

		// Not Use : Old Notch Find Methods >> Blob Center
		//int iMedian = vecPointDouble.size() / 2 + (vecPointDouble.size() % 2);
		//ConvertToRectCoord(vecPointDouble[iMedian].y, vecPointDouble[iMedian].x, dCenterX, dCenterY, dNotchX, dNotchY);

		// Not Use : Old Notch Find Methods >> Mean Fitting
		//double peakPos;
		//double peakValue;
		//double dPeakX, dPeakY;
		//MeanFitting(&vecPointDoubleThinning, peakPos, peakValue, 3);
		//dPeakX = peakValue;
		//dPeakY = peakPos;
		//MeanFitting(&vecPointDouble, peakPos, peakValue, 3);
		//ConvertToRectCoord(peakValue, peakPos, dCenterX, dCenterY, dPeakX, dPeakY);
		//imgResult.DrawLine(CPoint(dPeakX - 5, dPeakY - 5), CPoint(dPeakX + 5, dPeakY + 5), RGB(0, 0, 255), 1);
		//imgResult.DrawLine(CPoint(dPeakX - 5, dPeakY + 5), CPoint(dPeakX + 5, dPeakY - 5), RGB(0, 0, 255), 1);
		//dNotchX = (dNotchX + dPeakX) / 2.0;
		//dNotchY = (dNotchY + dPeakY) / 2.0;

		if (m_AlignResult.GetParam_IsSaveResult())
		{
			// Draw Notch
			imgResult.DrawLine(CPoint(dNotchX - 5, dNotchY - 5), CPoint(dNotchX + 5, dNotchY + 5), RGB(255, 0, 0), 1);
			imgResult.DrawLine(CPoint(dNotchX - 5, dNotchY + 5), CPoint(dNotchX + 5, dNotchY - 5), RGB(255, 0, 0), 1);

			// Draw Center to Notch
			imgResult.DrawLine(CPoint(dCenterX, dCenterY), CPoint(dNotchX, dNotchY), RGB(255, 0, 0), 1);
		}

		// 8.4. Calc Wafer Theta
		double dDistX = dNotchX - dCenterX;
		double dDistY = dNotchY - dCenterY;
		double dWaferRadian = -atan2(dDistY, dDistX);
		double dWaferDegree = dWaferRadian * 180.0 / PI;

		// 9. Set Align Result
		m_AlignResult.SetResult_Code(WaferPreAlignerResult_Success);
		m_AlignResult.SetResult_NotchPosX(dNotchX);
		m_AlignResult.SetResult_NotchPosY(dNotchY);
		m_AlignResult.SetResult_NotchDegree(dWaferDegree);
		m_AlignResult.SetResult_NotchRadian(dWaferRadian);
		m_AlignResult.SetResult_Dx(m_AlignResult.GetParam_RefCenterX() - dCenterX);
		m_AlignResult.SetResult_Dy(m_AlignResult.GetParam_RefCenterY() - dCenterY);
		m_AlignResult.SetResult_Dt(m_AlignResult.GetParam_RefNotchDegree() - dWaferDegree);

	}
	catch (int nCode)
	{
		switch (nCode)
		{
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
		case 7:
			m_AlignResult.SetResult_Code(WaferPreAlignerResult_Fail_ImageProcess);
			break;
		case 8:
		case 9:
			m_AlignResult.SetResult_Code(WaferPreAlignerResult_Fail_FindEllipse);
			if (m_AlignResult.GetParam_IsSaveResult())
			{
				imgThreshold.SaveImage(strSaveBinary);
				imgEdge.SaveImage(strSaveEdge);
			}
			break;
		case 10:
			m_AlignResult.SetResult_Code(WaferPreAlignerResult_Fail_FindNotch);
			break;
		default:
			m_AlignResult.SetResult_Code(WaferPreAlignerResult_Fail);
			break;
		}
	}

	if (m_AlignResult.GetParam_IsSaveResult())
	{
		imgBlob.SaveImage(strSaveBlob);
		imgResult.SaveImage(strSaveResult);
	}

	imgResult.ReleaseImage();
	imgThreshold.ReleaseImage();
	imgScale.ReleaseImage();
	imgEdge.ReleaseImage();
	imgEdgeClone.ReleaseImage();
	imgBlob.ReleaseImage();

	return m_AlignResult.GetResult_Code();
}

BOOL CWaferPreAligner::RotatePointF(double &dX, double &dY, double rad, double dCX, double dCY)
{
	double cosR = cos(rad);
	double sinR = sin(rad);

	double dRX, dRY;

	dRX = (int)((dX - dCX) * cosR - (dY - dCY) * sinR + dCX);
	dRY = (int)((dX - dCX) * sinR + (dY - dCY) * cosR + dCY);

	dX = dRX;
	dY = dRY;
	return TRUE;
}

double CWaferPreAligner::CalcEllipseDistance(double dX, double dY, double dRefX1, double dRefY1, double dRefX2, double dRefY2)
{
	double dDistance = 0.0;
	dDistance = (sqrt((dRefX1 - dX)*(dRefX1 - dX) + (dRefY1 - dY)*(dRefY1 - dY)))
		+ (sqrt((dRefX2 - dX)*(dRefX2 - dX) + (dRefY2 - dY)*(dRefY2 - dY)));

	return dDistance;
}

void CWaferPreAligner::ConvertToPolarCoord(double dX, double dY, double dCenterX, double dCenterY, double &dRadius, double &dTheta)
{
	dRadius = sqrt((dCenterX - dX)*(dCenterX - dX) + (dCenterY - dY)*(dCenterY - dY));
	dTheta = atan2((dY - dCenterY), (dX - dCenterX));
}

void CWaferPreAligner::ConvertToRectCoord(double dRadius, double dTheta, double dCenterX, double dCenterY, double &dX, double &dY)
{
	dX = (dRadius * cos(dTheta)) + dCenterX;
	dY = (dRadius * sin(dTheta)) + dCenterY;
}

int CWaferPreAligner::MeanFitting(VectorPointDouble* pVecPolarCoord, double &peakPos, double &peakValue, int nIteration)
{
	//int i, p1, p2, gbuf_len;
	//double pix = 0, maxi = 0;
	//double sumix = 0, sumi = 0;
	//double thresh, thresh1, maxinten;
	//double radius = 0, center, r2 = 0., oldr2 = 0;
	//double *temp;

	VectorPointDouble vecTemp1, vecTemp2;
	VectorPointDouble* pVector = pVecPolarCoord;
	VectorPointDouble* pVectorResult = &vecTemp1;

	double dMeanTheta, dMeanRadius;
	int nIter = 0;
	while (TRUE)
	{
		for (size_t i = 1; i < pVector->size() - 1; i++)
		{
			dMeanTheta = ((*pVector)[i - 1].x + (*pVector)[i].x + (*pVector)[i + 1].x) / 3.0;
			dMeanRadius = ((*pVector)[i - 1].y + (*pVector)[i].y + (*pVector)[i + 1].y) / 3.0;
			pVectorResult->push_back(PointDouble(dMeanTheta, dMeanRadius));
		}
		nIter++;

		if (nIter >= nIteration) break;
		if (pVectorResult->size() < 9) break;

		// Swap
		pVector = pVectorResult;
		pVectorResult = (pVectorResult == &vecTemp1) ? &vecTemp2 : &vecTemp1;
		pVectorResult->clear();
	}

	// 최소 거리 지점 탐색
	int iMin = -1;
	double dMin = INT_MAX;
	//for (size_t i = 1; i < pVectorResult->size()-1; ++i)
	//{
	//	if (dMin > abs((*pVectorResult)[i].y))
	//	{
	//		dMin = abs((*pVectorResult)[i].y);
	//		iMin = i;
	//	}
	//}
	for (size_t i = 1; i < pVectorResult->size()-1; ++i)
	{
		if (dMin > abs((*pVectorResult)[i].x))
		{
			dMin = abs((*pVectorResult)[i].x);
			iMin = i;
		}
	}

	// 거리 변화량 최대위치에서 SubPixel 위치 계산
	// 세점을 지나는 곡선(극좌표상 Theta 변화는 일단 무시... ㅠㅠ)
	//double a = (*pVectorResult)[iMin].y;
	//double b = (*pVectorResult)[iMin + 1].y;
	//double c = (*pVectorResult)[iMin - 1].y;

	//double f1 = ((b - c) / 2.0);
	//double f2 = (b + c - (2*a));
	//double f = -f1 / f2;
	//// 곡선의 최대 위치값 x를 풀고
	//double x = (-1 * ((b - c) / 2.0)) / (((c - a) + (b - a)) / 2.0);

	//// x에서의 해를 구한다.
	//peakValue = a + ((b - c) / 2.0 * x) + (((c - a) + (b - a)) / 2.0 * x * x);

	//// Theta의 단위를 구한다.
	////double dThetaRatio = atan2(1.0, peakValue);
	//peakPos = (*pVectorResult)[iMin].x + (x);

	double a = (*pVectorResult)[iMin].x;
	double b = (*pVectorResult)[iMin + 1].x;
	double c = (*pVectorResult)[iMin - 1].x;

	double f1 = ((b - c) / 2.0);
	double f2 = (b + c - (2 * a));
	double f = -f1 / f2;
	// 곡선의 최대 위치값 x를 풀고
	double x = (-1 * ((b - c) / 2.0)) / (((c - a) + (b - a)) / 2.0);

	// x에서의 해를 구한다.
	peakValue = a + ((b - c) / 2.0 * x) + (((c - a) + (b - a)) / 2.0 * x * x);

	// y 좌표를 구한다
	peakPos = (*pVectorResult)[iMin].y + x;
	return 1;
}

int CWaferPreAligner::PreAlignProcessByStep()
{
	if (m_pImageData == nullptr || !m_pImageData->GetImageExist() || m_pImageData->GetChannels() != 1)
		return WaferPreAlignerResult_ImageNull;

	// Parameters ///////////////////////////////////////////////////////////////////////////////////////
	double dThreshold = m_AlignResult.GetParam_EdgeThreshold();
	double dErrorThreshold = m_AlignResult.GetParam_EllipseSizeThreshold();
	double dNotchFindThreshold = m_AlignResult.GetParam_NotchFindThreshold();
	double dRefDiameter = m_AlignResult.GetParam_RefDiameter();

	CRect rtROI;
	rtROI.left = m_AlignResult.GetParam_NotchFindROILeft();
	rtROI.right = m_AlignResult.GetParam_NotchFindROIRight();
	rtROI.top = m_AlignResult.GetParam_NotchFindROITop();
	rtROI.bottom = m_AlignResult.GetParam_NotchFindROIBottom();

	// Processing Image ///////////////////////////////////////////////////////////////////////////////////////
	CCHImageData imgResult;
	CCHImageData imgThreshold;
	CCHImageData imgErode;
	CCHImageData imgOpening;
	CCHImageData imgScale;
	CCHImageData imgEdge;
	CCHImageData imgEdgeClone;
	CCHImageData imgBlob;
	CCHImageData imgTemplate;
	CCHImageData imgMatchTarget;

	// Process Variables ///////////////////////////////////////////////////////////////////////////////////////
	CCHImageProcess::VectorDouble vecResult;
	VectorDefectBlob vecBlob;
	SDefectBlob* pResultBlob = nullptr;
	VectorDefectBlobIt itResult;
	VectorPointDouble vecPointDouble;
	VectorPointDouble vecPointDoubleThinning;

	CString strSaveResult		= _T("ResultImage.bmp");
	CString strSaveBlob			= _T("BlobImage.bmp");
	CString strSaveBinary		= _T("BinaryImage.bmp");
	CString strSavePreprocess	= _T("PreprocessImage.bmp");
	CString strSaveEdge			= _T("EdgeImage.bmp");
	CString strSaveMatch		= _T("MatchImage.bmp");

	__int64 iPixel = 0;
	bool   bMajorAxisX = false; // 장축방향
	bool   bLoadTemplate = false;
	double dMinError = INT_MAX;

	double dCenterX, dCenterY;
	double dMajorLength, dMinorLength;
	double dMajorDegree;
	double dMajorRadian;
	double dMatchPosX, dMatchPosY;
	double dNotchX, dNotchY;
	double dMajorStartX, dMajorStartY;
	double dMajorEndX, dMajorEndY;
	double dMinorStartX, dMinorStartY;
	double dMinorEndX, dMinorEndY;
	double dFocalX1, dFocalY1;
	double dFocalX2, dFocalY2;
	double dMajorVertexDistance;
	double dDistance = 0.0;
	//double dRadius, dTheta;
	int nCount = 0;
	double dSizeError = 0.0;
	BYTE* pBuffer = nullptr;
	double dMatchScore = 0.0;

	try
	{
		// 0. Prepare Process
		if (rtROI.IsRectEmpty()) throw 0;

		// 0.1 Load Notch Template Image.
		if (imgTemplate.LoadImage(_T("Notch_Template.bmp")))
		{
			bLoadTemplate = true;
		}

		// Result Image Convert Gray to RGB	
		if (m_AlignResult.GetParam_IsSaveResult())
			CCHImageProcess::ImageConvert(m_pImageData, &imgResult, 0);

		// 0.1 Gray Scale Max (선택적 전처리 : 영상의 밝기 향상)
		if (m_AlignResult.GetParam_UseGrayScaleMax())
		{
			if (CCHImageProcess::ImageScaleMax(m_pImageData, &imgScale) != 1) throw 1;
			memcpy(m_pImageData->GetImageBuffer(), imgScale.GetImageBuffer(), m_pImageData->GetImageSize());

			imgScale.SaveImage(_T("ScaleImage.bmp"));
			imgScale.ReleaseImage();
		}

		// 1. 영상 이진화(외곽선 추출을 위한 전처리)
		if (CCHImageProcess::ImageThreshold(m_pImageData, &imgThreshold, dThreshold, 255.0, 0) != 1) throw 2;
		//imgThreshold.SaveImage(strSaveBinary);
		CCHImageProcess::ImageErode(&imgThreshold, &imgErode, nullptr, 3);
		//imgErode.SaveImage(_T("ErodeImage.bmp"));
		CCHImageProcess::ImageDilate(&imgErode, &imgOpening, nullptr, 3);
		//imgOpening.SaveImage(_T("CloseImage.bmp"));

		// 2. Edge 탐색(Sobel Mask 이용)
		if (CCHImageProcess::ImageSobel(&imgOpening, &imgEdge, 1, 1) != 1) throw 3;

		// 3. Blob 탐색을 위한 준비 (Blob 입력 영상은 변경됨)
		if (imgEdge.CopyImageTo(&imgEdgeClone) != TRUE) throw 4;
		if (imgBlob.CreateImage(imgEdge.GetWidthStep(), imgEdge.GetHeight()) != TRUE) throw 5;
		pBuffer = imgBlob.GetImageBuffer();

		// 4. 외곽선 Blobbing
		if (CCHImageProcess::ImageBlobAnalysis_Binary(imgEdgeClone.GetImageBuffer(), imgEdgeClone.GetWidth(), imgEdgeClone.GetHeight(), 128, 1, 0, 0, vecBlob) != 1)
			throw 6;
		if (vecBlob.size() == 0) throw 7;

		// 5. 입력된 Ref 정보와 Minimum Error 타원 선택
		dMinError = INT_MAX;
		itResult = vecBlob.end();
		for (VectorDefectBlobIt it = vecBlob.begin(); it != vecBlob.end(); ++it)
		{
			if (CCHImageProcess::EllipseFitting(it->vectorPoint, vecResult))
			{
				dCenterX = vecResult[0];
				dCenterY = vecResult[1];
				dMajorLength = (vecResult[2] >= vecResult[3]) ? vecResult[2] : vecResult[3];
				dMinorLength = (vecResult[2] < vecResult[3]) ? vecResult[2] : vecResult[3];
				dMajorDegree = vecResult[4];
				dMajorRadian = dMajorDegree * PI / 180;

				dSizeError = sqrt(((dMajorLength - dRefDiameter) * (dMajorLength - dRefDiameter)) + ((dMinorLength - dRefDiameter) * (dMinorLength - dRefDiameter)));
				if (dSizeError < dMinError)
				{
					itResult = it;
					dMinError = dSizeError;
					m_AlignResult.SetResult_CenterX(dCenterX);
					m_AlignResult.SetResult_CenterY(dCenterY);
					m_AlignResult.SetResult_MajorLength(vecResult[2]);
					m_AlignResult.SetResult_MinorLength(vecResult[3]);
					m_AlignResult.SetResult_MajorDegree(dMajorDegree);
					m_AlignResult.SetResult_MajorRadian(dMajorRadian);
				}
			}
			else
				throw 8;
		}
		if (itResult == vecBlob.end() || itResult->vectorPoint.size() == 0) throw 9;

		// Minimum Error가 Threhold보다 높으면 잘못 찾은걸로 간주한다.
		if (dMinError > dErrorThreshold) throw 9;

		// 6. 선택된 타원의 특성 산출 (중심위치, 초점위치)
		if (m_AlignResult.GetResult_MajorLength() > 0 && m_AlignResult.GetResult_MinorLength() > 0)
		{
			// 6.0. Set Variable for Calculation
			dCenterX = m_AlignResult.GetResult_CenterX();
			dCenterY = m_AlignResult.GetResult_CenterY();
			dMajorLength = m_AlignResult.GetResult_MajorLength();
			dMinorLength = m_AlignResult.GetResult_MinorLength();
			dMajorDegree = m_AlignResult.GetResult_MajorDegree();
			dMajorRadian = m_AlignResult.GetResult_MajorRadian();
			bMajorAxisX = (dMajorLength >= dMinorLength) ? true : false;

			if (!bMajorAxisX)
			{
				double dTemp = dMinorLength;
				dMinorLength = dMajorLength;
				dMajorLength = dTemp;
			}

			// 6.1. Calc Major Position
			dMajorStartX = bMajorAxisX ? dCenterX - dMajorLength / 2.0 : dCenterX;
			dMajorStartY = bMajorAxisX ? dCenterY : dCenterY - dMajorLength / 2.0;
			dMajorEndX = bMajorAxisX ? dCenterX + dMajorLength / 2.0 : dCenterX;
			dMajorEndY = bMajorAxisX ? dCenterY : dCenterY + dMajorLength / 2.0;
			RotatePointF(dMajorStartX, dMajorStartY, dMajorRadian, dCenterX, dCenterY);
			RotatePointF(dMajorEndX, dMajorEndY, dMajorRadian, dCenterX, dCenterY);
			m_AlignResult.SetResult_MajorPosStartX(dMajorStartX);
			m_AlignResult.SetResult_MajorPosStartY(dMajorStartY);
			m_AlignResult.SetResult_MajorPosEndX(dMajorEndX);
			m_AlignResult.SetResult_MajorPosEndY(dMajorEndY);

			// 6.2. Calc Minor Position
			dMinorStartX = !bMajorAxisX ? dCenterX - dMinorLength / 2.0 : dCenterX;
			dMinorStartY = !bMajorAxisX ? dCenterY : dCenterY - dMinorLength / 2.0;
			dMinorEndX = !bMajorAxisX ? dCenterX + dMinorLength / 2.0 : dCenterX;
			dMinorEndY = !bMajorAxisX ? dCenterY : dCenterY + dMinorLength / 2.0;
			RotatePointF(dMinorStartX, dMinorStartY, dMajorRadian, dCenterX, dCenterY);
			RotatePointF(dMinorEndX, dMinorEndY, dMajorRadian, dCenterX, dCenterY);
			m_AlignResult.SetResult_MinorPosStartX(dMinorStartX);
			m_AlignResult.SetResult_MinorPosStartY(dMinorStartY);
			m_AlignResult.SetResult_MinorPosEndX(dMinorEndX);
			m_AlignResult.SetResult_MinorPosEndY(dMinorEndY);

			// 6.3. Calc focal points
			double dA = dMajorLength / 2.0;
			double dB = dMinorLength / 2.0;
			dFocalX1 = bMajorAxisX ? (dCenterX + sqrt(dA * dA - dB * dB)) : dCenterX;
			dFocalY1 = bMajorAxisX ? dCenterY : (dCenterY + sqrt(dA * dA - dB * dB));
			dFocalX2 = bMajorAxisX ? (dCenterX - sqrt(dA * dA - dB * dB)) : dCenterX;
			dFocalY2 = bMajorAxisX ? dCenterY : (dCenterY - sqrt(dA * dA - dB * dB));
			RotatePointF(dFocalX1, dFocalY1, dMajorRadian, dCenterX, dCenterY);
			RotatePointF(dFocalX2, dFocalY2, dMajorRadian, dCenterX, dCenterY);

			// Draw Debug
			if (m_AlignResult.GetParam_IsSaveResult())
			{
				imgResult.DrawEllipse(CPoint(dCenterX, dCenterY), bMajorAxisX ? CPoint(dMajorLength, dMinorLength) : CPoint(dMinorLength, dMinorLength), dMajorDegree, RGB(255, 0, 0));
				imgResult.DrawLine(CPoint(dMajorStartX, dMajorStartY), CPoint(dMajorEndX, dMajorEndY), RGB(0, 255, 255));
				imgResult.DrawLine(CPoint(dMinorStartX, dMinorStartY), CPoint(dMinorEndX, dMinorEndY), RGB(255, 255, 0));
				imgResult.DrawLine(CPoint(dFocalX1 - 5, dFocalY1 - 5), CPoint(dFocalX1 + 5, dFocalY1 + 5), RGB(255, 0, 0), 1);
				imgResult.DrawLine(CPoint(dFocalX1 - 5, dFocalY1 + 5), CPoint(dFocalX1 + 5, dFocalY1 - 5), RGB(255, 0, 0), 1);
				imgResult.DrawLine(CPoint(dFocalX2 - 5, dFocalY2 - 5), CPoint(dFocalX2 + 5, dFocalY2 + 5), RGB(255, 0, 0), 1);
				imgResult.DrawLine(CPoint(dFocalX2 - 5, dFocalY2 + 5), CPoint(dFocalX2 + 5, dFocalY2 - 5), RGB(255, 0, 0), 1);
			}

			// 6.4. 기준거리 계산 (두 초점과 Major 꼭지점까지의 거리의 합)
			dMajorVertexDistance = CalcEllipseDistance(dMajorStartX, dMajorStartY, dFocalX1, dFocalY1, dFocalX2, dFocalY2);
		}

		// 7. Find Notch
		// 이진화 된 영상에서 Notch Template 영상을 이용하여 Notch Matching
		CRect rtROIExt = rtROI;
		rtROIExt.InflateRect(32, 32);
		//if (bLoadTemplate && imgThreshold.GetImageExist())
		if (bLoadTemplate && imgOpening.GetImageExist())
		{
			//CCHImageProcess::ImageMatching(&imgThreshold, &imgTemplate, rtROIExt, dMatchPosX, dMatchPosY, dMatchScore, &imgMatchTarget);
			CCHImageProcess::ImageMatching(&imgOpening, &imgTemplate, rtROIExt, dMatchPosX, dMatchPosY, dMatchScore, &imgMatchTarget);
			dMatchScore = dMatchScore * 100.0;
			if (dMatchScore > dNotchFindThreshold)
			{
				dNotchX = dMatchPosX + rtROIExt.left + (imgTemplate.GetWidth() / 2);
				dNotchY = dMatchPosY + rtROIExt.top + (imgTemplate.GetHeight() / 2);
			}
			else
				throw 10;
		}
		else
		{
			//FindNotchByThinning(char* pBlobImage, VectorDefectBlob vecBlob, )
		// 7. Fitting 된 타원의 경계와 실제 Wafer 경계비교 (입력된 임계거리를 벗어나는 지점 탐색)
			vecPointDouble.clear();
			memset(pBuffer, 0, sizeof(char) * imgEdge.GetWidthStep() * imgEdge.GetHeight());
			for (int i = 0; i < itResult->vectorPoint.size(); ++i)
			{
				iPixel = (itResult->vectorPoint[i].y * imgBlob.GetWidthStep()) + itResult->vectorPoint[i].x;

				// 7.1. Calc distance from ellipse outline to edge points
				dDistance = CalcEllipseDistance(itResult->vectorPoint[i].x, itResult->vectorPoint[i].y, dFocalX1, dFocalY1, dFocalX2, dFocalY2);
				dDistance = abs(dDistance - dMajorVertexDistance);
				pBuffer[iPixel] = dDistance > dNotchFindThreshold ? 255 : 0;

				// 7.2. Notch 탐색 ROI의 Distance 다른 점들의 모음
				if (dDistance > dNotchFindThreshold && rtROI.PtInRect(CPoint(itResult->vectorPoint[i].x, itResult->vectorPoint[i].y)))
				{
					vecPointDouble.push_back(PointDouble(itResult->vectorPoint[i].x, itResult->vectorPoint[i].y));
				}
			}

			// 8. Notch 위치 산출
			// Notch가 3시방향이라는 가정에 최적화한 부분.
			if (vecPointDouble.size() == 0) throw 10;

			// 8.1. sorting by Y Pixel Position
			std::sort(vecPointDouble.begin(), vecPointDouble.end(), [](PointDouble a, PointDouble b) {
				return (a.y < b.y);
			});

			// 8.2. Thinning Points (동일한 Y좌표의 최우측 X만 선택한다.)
			double yy = vecPointDouble[0].y;
			vecPointDoubleThinning.clear();
			vecPointDoubleThinning.push_back(vecPointDouble[0]);
			int ii = 0;
			for (int i = 1; i < vecPointDouble.size(); ++i)
			{
				if (vecPointDoubleThinning[ii].y == vecPointDouble[i].y)
				{
					vecPointDoubleThinning[ii].x = vecPointDoubleThinning[ii].x > vecPointDouble[i].x ? vecPointDouble[i].x : vecPointDoubleThinning[ii].x;
				}
				else
				{
					vecPointDoubleThinning.push_back(vecPointDouble[i]);
					ii++;
				}
			}

			// 8.3. Get Median Position
			int iMedian = vecPointDoubleThinning.size() / 2 + (vecPointDoubleThinning.size() % 2);
			dNotchX = vecPointDoubleThinning[iMedian].x;
			dNotchY = vecPointDoubleThinning[iMedian].y;
			dMatchPosX = dNotchX;
			dMatchPosY = dNotchY;
		}

		if (m_AlignResult.GetParam_IsSaveResult())
		{
			// Draw Notch
			imgResult.DrawLine(CPoint(dNotchX - 5, dNotchY - 5), CPoint(dNotchX + 5, dNotchY + 5), RGB(255, 0, 0), 1);
			imgResult.DrawLine(CPoint(dNotchX - 5, dNotchY + 5), CPoint(dNotchX + 5, dNotchY - 5), RGB(255, 0, 0), 1);

			// Draw Center to Notch
			imgResult.DrawLine(CPoint(dCenterX, dCenterY), CPoint(dNotchX, dNotchY), RGB(255, 0, 0), 1);

			// Draw Match Pos
			imgResult.DrawLine(CPoint(dMatchPosX - 5, dMatchPosY - 5), CPoint(dMatchPosX + 5, dMatchPosY + 5), RGB(0, 255, 0), 1);
			imgResult.DrawLine(CPoint(dMatchPosX - 5, dMatchPosY + 5), CPoint(dMatchPosX + 5, dMatchPosY - 5), RGB(0, 255, 0), 1);
		}

		// 8.4. Calc Wafer Theta
		double dDistX = dNotchX - dCenterX;
		double dDistY = dNotchY - dCenterY;
		double dWaferRadian = -atan2(dDistY, dDistX);
		double dWaferDegree = dWaferRadian * 180.0 / PI;

		// 9. Set Align Result
		m_AlignResult.SetResult_Code(WaferPreAlignerResult_Success);
		m_AlignResult.SetResult_NotchPosX(dNotchX);
		m_AlignResult.SetResult_NotchPosY(dNotchY);
		m_AlignResult.SetResult_NotchDegree(dWaferDegree);
		m_AlignResult.SetResult_NotchRadian(dWaferRadian);
		m_AlignResult.SetResult_NotchScore(dMatchScore);
		m_AlignResult.SetResult_Dx(m_AlignResult.GetParam_RefCenterX() - dCenterX);
		m_AlignResult.SetResult_Dy(m_AlignResult.GetParam_RefCenterY() - dCenterY);
		m_AlignResult.SetResult_Dt(m_AlignResult.GetParam_RefNotchDegree() - dWaferDegree);

		if(m_AlignResult.GetParam_UseInspect())
			PreAlignProcess_Inspect(imgThreshold, imgResult, m_AlignResult, m_pArrDefect);
	}
	catch (int nCode)
	{
		switch (nCode)
		{
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
		case 7:
			m_AlignResult.SetResult_Code(WaferPreAlignerResult_Fail_ImageProcess);
			break;
		case 8:
		case 9:
			m_AlignResult.SetResult_Code(WaferPreAlignerResult_Fail_FindEllipse);
			if (m_AlignResult.GetParam_IsSaveResult())
			{
				//if (imgThreshold.GetImageExist())imgThreshold.SaveImage(strSaveBinary);
				if (imgEdge.GetImageExist())imgEdge.SaveImage(strSaveEdge);
			}
			break;
		case 10:
			m_AlignResult.SetResult_Code(WaferPreAlignerResult_Fail_FindNotch);
			break;
		default:
			m_AlignResult.SetResult_Code(WaferPreAlignerResult_Fail);
			break;
		}
	}

	if (m_AlignResult.GetParam_IsSaveResult())
	{
		if (imgBlob.GetImageExist())imgBlob.SaveImage(strSaveBlob);
		if (imgResult.GetImageExist())imgResult.SaveImage(strSaveResult);
		if (imgThreshold.GetImageExist())imgThreshold.SaveImage(strSaveBinary);
		if (imgOpening.GetImageExist())imgOpening.SaveImage(strSavePreprocess);
	}

	imgResult.ReleaseImage();
	imgThreshold.ReleaseImage();
	imgErode.ReleaseImage();
	imgOpening.ReleaseImage();
	imgScale.ReleaseImage();
	imgEdge.ReleaseImage();
	imgEdgeClone.ReleaseImage();
	imgBlob.ReleaseImage();

	return m_AlignResult.GetResult_Code();
}

int CWaferPreAligner::PreAlignProcess_Inspect(CCHImageData& imageThreshold, CCHImageData& imageResult, CWaferPreAlignerResult& result, PreAlignDefect* pArrDefect)
{
	if (!imageThreshold.GetImageExist() /*|| !imageResult.GetImageExist()*/)
		return FALSE;

	if (pArrDefect == nullptr) return FALSE;

	// 산출된 Wafer 타원 영역의 외부 제거
	int nOutterInsMargin = result.GetParam_InspectMargin() > 0 ? result.GetParam_InspectMargin() : 3;
	int nInnerInsMargin = 30;
	CCHImageData imageEllipseMask, imageMul;
	imageEllipseMask.CreateImage(imageThreshold.GetWidth(), imageThreshold.GetHeight());
	imageEllipseMask.DrawEllipse(CPoint(result.GetResult_CenterX(), result.GetResult_CenterY()), CPoint(result.GetResult_MajorLength() - nOutterInsMargin, result.GetResult_MinorLength() - nOutterInsMargin), result.GetResult_MajorRadian(), RGB(255, 255, 255), -1);
	imageEllipseMask.DrawEllipse(CPoint(result.GetResult_CenterX(), result.GetResult_CenterY()), CPoint(result.GetResult_MajorLength() - nInnerInsMargin, result.GetResult_MinorLength() - nInnerInsMargin), result.GetResult_MajorRadian(), RGB(0, 0, 0), -1);

	CCHImageProcess::ImageMul(&imageThreshold, &imageEllipseMask, &imageMul);
	if (imageMul.GetImageExist() && result.GetParam_IsSaveResult())
	{
		//imageEllipseMask.SaveImage(_T("MaskImage.bmp"));
		//imageThreshold.SaveImage(_T("ThresholdImage2.bmp"));
		imageMul.SaveImage(_T("MaskedBinary.bmp"));
	}
	
	VectorDefectBlob vecBlob;
	VectorDefectBlob vecBlobDefect;
	if (CCHImageProcess::ImageBlobAnalysis_Binary(imageMul.GetImageBuffer(), imageMul.GetWidth(), imageMul.GetHeight(), 128, 1, 0, 0, vecBlob) == 1)
	{
		double dRatio = 0;
		double dRectArea, dPixelCount;
		
		
		// Notch 결함 제거용
		double dDistance = 0;
		double dMinDistance = INT_MAX;
		int iMinDefect = -1;
		double dNotchX = result.GetResult_NotchPosX();
		double dNotchY = result.GetResult_NotchPosY();

		int nFilterAreaSize = result.GetParam_InspectFilterArea() > 0 ? result.GetParam_InspectFilterArea() : 10;
		int nFilterSize = 2;
		double dFilterAreaRatio = result.GetParam_InspectFilterRatio() > 0 ? result.GetParam_InspectFilterRatio() : 0.5;

		for (int iBlob = 0; iBlob < vecBlob.size(); ++iBlob)
		{
			if (vecBlob[iBlob].GetPixelCount() < nFilterAreaSize) continue;
			if (vecBlob[iBlob].GetWidth() < nFilterSize || vecBlob[iBlob].GetHeight() < nFilterSize) continue;

			dRectArea = vecBlob[iBlob].GetSquareSize();
			dPixelCount = vecBlob[iBlob].GetPixelCount();
			dRatio = dPixelCount / dRectArea;

			if (dRatio < dFilterAreaRatio) continue;

			vecBlobDefect.push_back(vecBlob[iBlob]);

			dDistance = sqrt(((dNotchX - vecBlob[iBlob].GetCenterX())*(dNotchX - vecBlob[iBlob].GetCenterX())) + ((dNotchY - vecBlob[iBlob].GetCenterY()) * (dNotchY - vecBlob[iBlob].GetCenterY())));
			if (dDistance < dMinDistance)
			{
				dMinDistance = dDistance;
				iMinDefect = vecBlobDefect.size() - 1;
			}

		}

		int iDefect = 0;
		for (int iBlob = 0; iBlob < vecBlobDefect.size(); ++iBlob)
		{
			if (iBlob == iMinDefect) continue;

			if (iDefect >= MAX_PREALIGN_DEFECT_COUNT) break;

			pArrDefect[iDefect].nIdx = iDefect;
			pArrDefect[iDefect].nType = TRUE;

			pArrDefect[iDefect].dPosX	= vecBlobDefect[iBlob].GetCenterX();
			pArrDefect[iDefect].dPosY	= vecBlobDefect[iBlob].GetCenterY();
			pArrDefect[iDefect].nArea	= vecBlobDefect[iBlob].GetPixelCount();
			pArrDefect[iDefect].nLeft	= vecBlobDefect[iBlob].nLeft;
			pArrDefect[iDefect].nTop	= vecBlobDefect[iBlob].nTop;
			pArrDefect[iDefect].nRight	= vecBlobDefect[iBlob].nRight;
			pArrDefect[iDefect].nBottom = vecBlobDefect[iBlob].nBottom;

			if(result.GetParam_IsSaveResult() && imageResult.GetImageExist())
				imageResult.DrawRectangle(CPoint(pArrDefect[iDefect].nLeft, pArrDefect[iDefect].nTop), CPoint(pArrDefect[iDefect].nRight, pArrDefect[iDefect].nBottom), RGB(0, 255, 0), 1);

			iDefect++;
		}
		vecBlob.clear();
		vecBlobDefect.clear();
		result.SetResult_DefectCount(iDefect);
	}	

	return TRUE;
}

