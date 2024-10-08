#pragma once
#include "CHImageProcess.h"
enum WaferPreAlignerResultCode 
{ 
	WaferPreAlignerResult_PtrNull=-7, 
	WaferPreAlignerResult_ImageNull, 
	WaferPreAlignerResult_ParamNull, 
	WaferPreAlignerResult_ROIEmpty,
	WaferPreAlignerResult_Fail_ImageProcess,
	WaferPreAlignerResult_Fail_FindEllipse,
	WaferPreAlignerResult_Fail_FindNotch,
	WaferPreAlignerResult_Fail, 
	WaferPreAlignerResult_Success 
};

class CCHImageData;

#ifndef PI
#define PI   3.1415926535897932384626433832795
#endif // !PI

#define MAX_PREALIGN_DEFECT_COUNT 100

struct PreAlignDefect
{
	int nIdx;
	int nType;
	int nArea;
	int nLeft;
	int nTop;
	int nRight;
	int nBottom;

	double dPosX;
	double dPosY;
};

class CWaferPreAlignerParam
{
public:
	CWaferPreAlignerParam();
	virtual ~CWaferPreAlignerParam();
	void Reset();

	// getter
	int		GetParam_IsSetParam() const					{ return m_bSetParam; }
	int		GetParam_IsSaveResult() const				{ return m_bSaveResult; }
	int		GetParam_UseGrayScaleMax() const			{ return m_bUseGrayScaleMax; }
	int		GetParam_EdgeThreshold() const				{ return m_nEdgeThreshold; }
	double	GetParam_EllipseSizeThreshold() const		{ return m_dEllipseSizeThreshold; }

	double	GetParam_NotchFindThreshold() const			{ return m_dNotchFindThreshold; }
	int		GetParam_NotchFindROILeft() const			{ return m_nNotchFindROILeft; }
	int		GetParam_NotchFindROIRight() const			{ return m_nNotchFindROIRight; }
	int		GetParam_NotchFindROITop() const			{ return m_nNotchFindROITop; }
	int		GetParam_NotchFindROIBottom() const			{ return m_nNotchFindROIBottom; }

	double	GetParam_RefDiameter() const				{ return m_dRefDiameter; }
	double	GetParam_RefCenterX() const					{ return m_dRefCenterX; }
	double	GetParam_RefCenterY() const					{ return m_dRefCenterY; }
	double	GetParam_RefNotchDegree() const				{ return m_dRefNotchDegree; }

	int		GetParam_UseInspect() const					{ return m_bUseInspect; }
	int		GetParam_InspectMargin() const				{ return m_nInspectMargin; }
	int		GetParam_InspectFilterArea() const			{ return m_nInspectFilterArea; }
	double	GetParam_InspectFilterRatio() const			{ return m_dInspectFilterRatio; }

	// setter
	void	SetParam(int bValue)						{ m_bSetParam = bValue; }
	void	SetParam_SaveResult(int bValue)				{ m_bSaveResult = bValue; }
	void	SetParam_UseGrayScaleMax(int bValue)		{ m_bUseGrayScaleMax = bValue; }
	void	SetParam_EdgeThreshold(int nValue)			{ m_nEdgeThreshold = nValue; }
	void	SetParam_EllipseSizeThreshold(double dValue){ m_dEllipseSizeThreshold = dValue; }

	void	SetParam_NotchFindThreshold(double dValue)	{ m_dNotchFindThreshold = dValue; }
	void	SetParam_NotchFindROILeft(int nValue)		{ m_nNotchFindROILeft = nValue; }
	void	SetParam_NotchFindROIRight(int nValue)		{ m_nNotchFindROIRight = nValue; }
	void	SetParam_NotchFindROITop(int nValue)		{ m_nNotchFindROITop = nValue; }
	void	SetParam_NotchFindROIBottom(int nValue)		{ m_nNotchFindROIBottom = nValue; }

	void	SetParam_RefDiameter(double dValue)			{ m_dRefDiameter = dValue; }
	void	SetParam_RefCenterX(double dValue)			{ m_dRefCenterX = dValue; }
	void	SetParam_RefCenterY(double dValue)			{ m_dRefCenterY = dValue; }
	void	SetParam_RefNotchDegree(double dValue)		{ m_dRefNotchDegree = dValue; }

	void	SetParam_UseInspect(int bValue)				{ m_bUseInspect = bValue; }
	void	SetParam_InspectMargin(int nValue) 			{ m_nInspectMargin = nValue; }
	void	SetParam_InspectFilterArea(int nValue) 		{ m_nInspectFilterArea = nValue; }
	void	SetParam_InspectFilterRatio(double dValue)	{ m_dInspectFilterRatio = dValue; }

protected:
	int		m_bSaveResult;			// Result Image 저장 여부
	int		m_bSetParam;			// Param Set 여부
	int		m_bUseGrayScaleMax;		// 영상의 밝기 차 보정 전처리 사용 유무
	int		m_nEdgeThreshold;		// 영상 이진화 문턱값(0~255)
	double	m_dEllipseSizeThreshold;// 최적 타원과 기준 Wafer의 Size 인정 문턱값(Pixel 거리)
	double	m_dNotchFindThreshold;	// Notch 탐색을 위한 Wafer 외접 타원에서 실제 Edge 까지의 거리 문턱값
	int		m_nNotchFindROILeft;	// Notch 탐색 영역 시작 X 좌표(Pixel)
	int		m_nNotchFindROIRight;	// Notch 탐색 영역 끝 X 좌표(Pixel)
	int		m_nNotchFindROITop;		// Notch 탐색 영역 시작 Y 좌표(Pixel)
	int		m_nNotchFindROIBottom;	// Notch 탐색 영역 끝 Y 좌표(Pixel)

	double	m_dRefDiameter;			// Wafer 기준 지름 크기(Pixel)
	double	m_dRefCenterX;			// Wafer 기준 중심 X 좌표(Pixel)
	double	m_dRefCenterY;			// Wafer 기준 중심 Y 좌표(Pixel)
	double	m_dRefNotchDegree;		// Wafer 기준 Notch Angle (Degree)

	int		m_bUseInspect;			// 검사 수행 여부
	int		m_nInspectMargin;		// 검사시 Wafer 최적 타원 외곽 마진(검사 제외)
	int		m_nInspectFilterArea;	// 검사시 Blob Area Filter 조건(기본값 10Pixel)
	double	m_dInspectFilterRatio;	// 검사시 Blob Rect와 Pixel Area의 비율(0~1, 기본값 0.5)
};

class CWaferPreAlignerResult : public CWaferPreAlignerParam
{
public:
	CWaferPreAlignerResult();
	virtual ~CWaferPreAlignerResult();
	void Reset();

	// getter
	int		GetResult_Code() const				{ return m_nCode; }
	double	GetResult_Dx() const				{ return m_dDx;  }
	double	GetResult_Dy() const				{ return m_dDy;  }
	double	GetResult_DTheta() const			{ return m_dDTheta; }

	double	GetResult_CenterX() const			{ return m_dCenterX; }
	double	GetResult_CenterY() const			{ return m_dCenterY; }
	
	double	GetResult_MajorLength() const		{ return m_dMajorLength; }
	double	GetResult_MajorPosStartX() const	{ return m_dMajorStartX; }
	double	GetResult_MajorPosStartY() const	{ return m_dMajorStartY; }
	double	GetResult_MajorPosEndX() const		{ return m_dMajorEndX; }
	double	GetResult_MajorPosEndY() const		{ return m_dMajorEndY; }
	double	GetResult_MajorRadian() const		{ return m_dMajorRadian; }
	double	GetResult_MajorDegree() const		{ return m_dMajorDegree; }

	double	GetResult_MinorLength() const		{ return m_dMinorLength; }
	double	GetResult_MinorPosStartX() const	{ return m_dMinorStartX; }
	double	GetResult_MinorPosStartY() const	{ return m_dMinorStartY; }
	double	GetResult_MinorPosEndX() const		{ return m_dMinorEndX; }
	double	GetResult_MinorPosEndY() const		{ return m_dMinorEndY; }

	double	GetResult_NotchPosX() const			{ return m_dNotchX; }
	double	GetResult_NotchPosY() const			{ return m_dNotchY; }
	double	GetResult_NotchRadian() const		{ return m_dNotchRadian; }
	double	GetResult_NotchDegree() const		{ return m_dNotchDegree; }
	double	GetResult_NotchScore() const		{ return m_dNotchScore; }

	int		GetResult_DefectCount() const		{ return m_nDefectCount; }

	// setter
	void	SetResult_Code(int nCode)						{ m_nCode = nCode; }
	void	SetResult_Dx(double fValue)						{ m_dDx = fValue; }
	void	SetResult_Dy(double fValue)						{ m_dDy = fValue; }
	void	SetResult_Dt(double fValue)						{ m_dDTheta = fValue; }

	void	SetResult_CenterX(double dCenterX)				{  m_dCenterX = dCenterX; }
	void	SetResult_CenterY(double dCenterY)				{  m_dCenterY = dCenterY; }

	void	SetResult_MajorLength(double dMajorLength)		{  m_dMajorLength = dMajorLength; }
	void	SetResult_MajorPosStartX(double dMajorStartX)	{  m_dMajorStartX = dMajorStartX; }
	void	SetResult_MajorPosStartY(double dMajorStartY)	{  m_dMajorStartY = dMajorStartY; }
	void	SetResult_MajorPosEndX(double dMajorEndX)		{  m_dMajorEndX = dMajorEndX; }
	void	SetResult_MajorPosEndY(double dMajorEndY)		{  m_dMajorEndY = dMajorEndY; }
	void	SetResult_MajorRadian(double dMajorRadian)		{  m_dMajorRadian = dMajorRadian; }
	void	SetResult_MajorDegree(double dMajorDegree)		{  m_dMajorDegree = dMajorDegree; }

	void	SetResult_MinorLength(double dMinorLength)		{  m_dMinorLength = dMinorLength; }
	void	SetResult_MinorPosStartX(double dMinorStartX)	{  m_dMinorStartX = dMinorStartX; }
	void	SetResult_MinorPosStartY(double dMinorStartY)	{  m_dMinorStartY = dMinorStartY; }
	void	SetResult_MinorPosEndX(double dMinorEndX)		{  m_dMinorEndX = dMinorEndX; }
	void	SetResult_MinorPosEndY(double dMinorEndY)		{  m_dMinorEndY = dMinorEndY; }

	void	SetResult_NotchPosX(double dNotchX)				{  m_dNotchX = dNotchX; }
	void	SetResult_NotchPosY(double dNotchY)				{  m_dNotchY = dNotchY; }
	void	SetResult_NotchRadian(double dNotchRadian)		{  m_dNotchRadian = dNotchRadian; }
	void	SetResult_NotchDegree(double dNotchDegree)		{  m_dNotchDegree = dNotchDegree; }
	void	SetResult_NotchScore(double dNotchScore)		{  m_dNotchScore = dNotchScore; }
	void	SetResult_DefectCount(int nCount)				{ m_nDefectCount = nCount; }

protected:
	int		m_nCode;
	
	double	m_dDx;
	double	m_dDy;
	double	m_dDTheta;

	double	m_dCenterX;
	double	m_dCenterY;
	
	double	m_dMajorLength;
	double	m_dMajorStartX;
	double	m_dMajorStartY;
	double	m_dMajorEndX;
	double	m_dMajorEndY;
	double	m_dMajorRadian;
	double	m_dMajorDegree;

	double	m_dMinorLength;
	double	m_dMinorStartX;
	double	m_dMinorStartY;
	double	m_dMinorEndX;
	double	m_dMinorEndY;

	double	m_dNotchX;
	double	m_dNotchY;
	double	m_dNotchRadian;
	double	m_dNotchDegree;
	double	m_dNotchScore;

	int		m_nDefectCount;
};


class CWaferPreAligner
{
public:
	CWaferPreAligner();
	virtual ~CWaferPreAligner();

public:
	const CWaferPreAlignerResult* GetPreAlignerResult() const;
	int Process_PreAlign(const char* pImageBuffer, int nImageWidth, int nImageHeight, const CWaferPreAlignerResult& param);
	int Process_PreAlign(const char* pImageBuffer, int nImageWidth, int nImageHeight, const CWaferPreAlignerResult& param, PreAlignDefect* pArrDefect);

protected:
	virtual int PreAlignProcess();
	void SetPreAlignerParam(const CWaferPreAlignerResult& param, PreAlignDefect* pArrDefect=nullptr);
	int Process_PreAlign(const char* pImageBuffer, int nImageWidth, int nImageHeight);
	BOOL RotatePointF(double &dX, double &dY, double rad, double dCX, double dCY);
	double CalcEllipseDistance(double dX, double dY, double dRefX1, double dRefY1, double dRefX2, double dRefY2);
	void ConvertToPolarCoord(double dX, double dY, double dCenterX, double dCenterY, double &dRadius, double &dTheta);
	void ConvertToRectCoord(double dRadius, double dTheta, double dCenterX, double dCenterY, double &dX, double &dY);
	int MeanFitting(VectorPointDouble* pVecPolarCoord, double &peakPos, double &peakValue, int nIteration);
	
	// 2020.12.10 Add by Kim Taehyun
	int PreAlignProcessByStep();
	int PreAlignProcess_Inspect(CCHImageData& imageThreshold, CCHImageData& imageResult, CWaferPreAlignerResult& result, PreAlignDefect* pArrDefect);

protected:
	CCHImageData*			m_pImageData;
	CWaferPreAlignerResult	m_AlignResult;
	PreAlignDefect*			m_pArrDefect;
};

