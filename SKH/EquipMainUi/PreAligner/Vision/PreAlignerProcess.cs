using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;

namespace EquipMainUi.PreAligner
{
    public enum WaferPreAlignerResultCode
    {
        WaferPreAlignerResult_Exception = -8,

        // dll defined ( -7 ~ 1 )
        WaferPreAlignerResult_PtrNull = -7, 
        WaferPreAlignerResult_ImageNull,
        WaferPreAlignerResult_NoRecipe,
        WaferPreAlignerResult_ROIEmpty,
        WaferPreAlignerResult_Fail_ImageProcess,
        WaferPreAlignerResult_Fail_FindEllipse,
        WaferPreAlignerResult_Fail_FindNotch,
        WaferPreAlignerResult_Fail,
        WaferPreAlignerResult_Success
    };

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PreAlignDefect
    {
        public int nIdx;
        public int nType;
        public int nArea;
        public int nLeft;
        public int nTop;
        public int nRight;
        public int nBottom;

        public double dPosX;
        public double dPosY;
    };

    [StructLayout(LayoutKind.Sequential)]
    public class CWaferPreAlignerParam
    {
        public CWaferPreAlignerParam()
        {
            this.Reset();
        }

        public virtual void Reset()
        {
            this.m_bUseGrayScaleMax = 0;
            this.m_nEdgeThreshold = 0;
            this.m_dEllipseSizeThreshold = 0;
            this.m_dNotchFindThreshold = 0;
            this.m_nNotchFindROILeft = 0;  
            this.m_nNotchFindROIRight = 0; 
            this.m_nNotchFindROITop = 0;   
            this.m_nNotchFindROIBottom = 0;
            this.m_dRefDiameter = 0;       
            this.m_dRefCenterX = 0;        
            this.m_dRefCenterY = 0;        
            this.m_dRefNotchDegree = 0.0;
            this.m_bUseInspect = 1;
            this.m_nInspectMargin = 3;
            this.m_nInspectFilterArea = 10;
            this.m_dInspectFilterRatio = 0.5;
        }
        private double m_MemoryDummy;           // C++ 가상 소멸자 8Byte Dummy
        public int m_bSaveResult;		        // Result 영상 저장 여부
        public int m_bSetParam;			        // Param Set 여부
        public int m_bUseGrayScaleMax;          // 영상의 밝기 차 보정 전처리 사용 유무
        public int m_nEdgeThreshold;            // 영상 이진화 문턱값(0~255)
        public double m_dEllipseSizeThreshold;  // 최적 타원 탐색을 위한 최적 타원과 Wafer 크기의 거리 문턱값
        public double m_dNotchFindThreshold;  // Notch 탐색을 위한 Wafer 외접 타원에서 실제 Edge 까지의 거리 문턱값
        public int m_nNotchFindROILeft;    // Notch 탐색 영역 시작 X 좌표(Pixel)
        public int m_nNotchFindROIRight;   // Notch 탐색 영역 끝 X 좌표(Pixel)
        public int m_nNotchFindROITop;     // Notch 탐색 영역 시작 Y 좌표(Pixel)
        public int m_nNotchFindROIBottom;  // Notch 탐색 영역 끝 Y 좌표(Pixel)
        public double m_dRefDiameter;         // Wafer 기준 지름 크기(Pixel)
        public double m_dRefCenterX;          // Wafer 기준 중심 X 좌표(Pixel)
        public double m_dRefCenterY;          // Wafer 기준 중심 Y 좌표(Pixel)
        public double m_dRefNotchDegree;        // Wafer 기준 Notch Angle (Degree)

        public int m_bUseInspect;               // 검사 수행 여부
        public int m_nInspectMargin;            // 검사시 Wafer 최적 타원 외곽 마진(검사 제외, 기본값 3)
        public int m_nInspectFilterArea;        // 검사시 Blob Area Filter 조건(기본값 10Pixel)
        public double m_dInspectFilterRatio;	// 검사시 Blob Rect와 Pixel Area의 비율(0~1, 기본값 0.5)

        public virtual CWaferPreAlignerParam Clone()
        {
            return new CWaferPreAlignerParam()
            {
                m_bSaveResult         = this.m_bSaveResult,
                m_bSetParam           = this.m_bSetParam,
                m_bUseGrayScaleMax    = this.m_bUseGrayScaleMax,
                m_nEdgeThreshold      = this.m_nEdgeThreshold,
                m_dNotchFindThreshold = this.m_dNotchFindThreshold,
                m_nNotchFindROILeft   = this.m_nNotchFindROILeft,
                m_nNotchFindROITop    = this.m_nNotchFindROITop,
                m_nNotchFindROIRight  = this.m_nNotchFindROIRight,
                m_nNotchFindROIBottom = this.m_nNotchFindROIBottom,
                m_dRefCenterX         = this.m_dRefCenterX,
                m_dRefCenterY         = this.m_dRefCenterY,
                m_dRefDiameter        = this.m_dRefDiameter,
                m_dRefNotchDegree     = this.m_dRefNotchDegree,

                m_bUseInspect         = this.m_bUseInspect,
                m_nInspectMargin      = this.m_nInspectMargin,
                m_nInspectFilterArea  = this.m_nInspectFilterArea,
                m_dInspectFilterRatio = this.m_dInspectFilterRatio,
        };
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    [DefaultPropertyAttribute("Name")]
    public class CWaferPreAlignerResult : CWaferPreAlignerParam
    {
        public CWaferPreAlignerResult()
        {
            this.Reset();
        }

        public CWaferPreAlignerParam CloneParam()
        {
            return base.Clone();
        }

        public override void Reset()
        {
            m_nCode = (int)WaferPreAlignerResultCode.WaferPreAlignerResult_Fail;
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

            m_dNotchScore = 0.0;

            m_nDefectCount = 0;
        }

        //private double m_MemoryDummy; // C++ 가상 소멸자 8Byte Dummy
        public int m_nCode;
        public double m_dDx;
        public double m_dDy;
        public double m_dDTheta;
        public double m_dCenterX;
        public double m_dCenterY;
        public double m_dMajorLength;
        public double m_dMajorStartX;
        public double m_dMajorStartY;
        public double m_dMajorEndX;
        public double m_dMajorEndY;
        public double m_dMajorRadian;
        public double m_dMajorDegree;
        public double m_dMinorLength;
        public double m_dMinorStartX;
        public double m_dMinorStartY;
        public double m_dMinorEndX;
        public double m_dMinorEndY;
        public double m_dNotchX;
        public double m_dNotchY;
        public double m_dNotchRadian;
        public double m_dNotchDegree;
        public double m_dNotchScore;
        public int m_nDefectCount;

        #region recipe
        [CategoryAttribute("Recipe"), DisplayNameAttribute("12. 디버그 이미지 저장"), DescriptionAttribute("Debug 영상 저장 사용")]
        public bool UseDebugImage
        {
            get { return this.m_bSaveResult == 0 ? false : true; }
            set { m_bSaveResult = value ? 1 : 0; }
        }

        [CategoryAttribute("Recipe"), DescriptionAttribute("Parameter 설정 여부"), BrowsableAttribute(false)]
        public int IsSetParam
        {
            get { return this.m_bSetParam; }
            set { m_bSetParam = value; }
        }

        [CategoryAttribute("Recipe"), DisplayNameAttribute("01. 영상 밝기 전처리 사용"), DescriptionAttribute("영상 밝기 향상 전처리 사용")]
        public bool UseGrayScaleMax
        {
            get { return this.m_bUseGrayScaleMax == 0 ? false : true; }
            set { m_bUseGrayScaleMax = value ? 1 : 0; }
        }

        [CategoryAttribute("Recipe"), DisplayNameAttribute("02. 영상 이진화 문턱값"),  DescriptionAttribute("영상 이진화 문턱값(0~255)")]
        public int EdgeThreshold
        {
            get { return this.m_nEdgeThreshold; }
            set { m_nEdgeThreshold = value; }
        }
        [CategoryAttribute("Recipe"), DisplayNameAttribute("03. 최적 타원 크기 문턱값"), DescriptionAttribute("최적 타원과 Wafer 기준크기 오차 인정 범위(10.0 ~ )")]
        public double EllipseSizeThreshold
        {
            get { return this.m_dEllipseSizeThreshold; }
            set { m_dEllipseSizeThreshold = value; }
        }
        [CategoryAttribute("Recipe"), DisplayNameAttribute("03. Notch 탐색 문턱값"), DescriptionAttribute("Notch 탐색 경계 거리 문턱값(5.0 ~ )")]
        public double NotchFindThreshold
        {
            get { return this.m_dNotchFindThreshold; }
            set { m_dNotchFindThreshold = value; }
        }

        [CategoryAttribute("Recipe"), DisplayNameAttribute("04. Notch 탐색 ROI Top"), DescriptionAttribute("Notch 탐색 영역 상측(Pixel)")]
        public int NotchFindROITop
        {
            get { return this.m_nNotchFindROITop; }
            set { m_nNotchFindROITop = value; }
        }

        [CategoryAttribute("Recipe"), DisplayNameAttribute("05. Notch 탐색 ROI Left"), DescriptionAttribute("Notch 탐색 영역 좌측(Pixel)")]
        public int NotchFindROILeft
        {
            get { return this.m_nNotchFindROILeft; }
            set { m_nNotchFindROILeft = value; }
        }

        [CategoryAttribute("Recipe"), DisplayNameAttribute("06. Notch 탐색 ROI Bottom"), DescriptionAttribute("Notch 탐색 영역 하측(Pixel)")]
        public int NotchFindROIBottom
        {
            get { return this.m_nNotchFindROIBottom; }
            set { m_nNotchFindROIBottom = value; }
        }

        [CategoryAttribute("Recipe"), DisplayNameAttribute("07. Notch 탐색 ROI Right"), DescriptionAttribute("Notch 탐색 영역 우측(Pixel)")]
        public int NotchFindROIRight
        {
            get { return this.m_nNotchFindROIRight; }
            set { m_nNotchFindROIRight = value; }
        }

        [CategoryAttribute("Recipe"), DisplayNameAttribute("08. Wafer 기준 지름"), DescriptionAttribute("Wafer 기준 지름 크기(Pixel)")]
        public double ReferenceDiameter
        {
            get { return this.m_dRefDiameter; }
            set { m_dRefDiameter = value; }
        }

        [CategoryAttribute("Recipe"), DisplayNameAttribute("09. Wafer 기준 중심 X"), DescriptionAttribute("Wafer 기준 중심 X 좌표(Pixel)")]
        public double ReferenceCenterX
        {
            get { return this.m_dRefCenterX; }
            set { m_dRefCenterX = value; }
        }

        [CategoryAttribute("Recipe"), DisplayNameAttribute("10. Wafer 기준 중심 Y"), DescriptionAttribute("Wafer 기준 중심 Y 좌표(Pixel)")]
        public double ReferenceCenterY
        {
            get { return this.m_dRefCenterY; }
            set { m_dRefCenterY = value; }
        }

        [CategoryAttribute("Recipe"), DisplayNameAttribute("11. Wafer 기준 각도"), DescriptionAttribute("Wafer 기준 Notch Angle (Degree)")]
        public double ReferenceDegree
        {
            get { return this.m_dRefNotchDegree; }
            set { m_dRefNotchDegree = value; }
        }
        [CategoryAttribute("Recipe"), DisplayNameAttribute("13. 검사 기능 사용"), DescriptionAttribute("Wafer 외곽 검사 사용 여부")]
        public bool UseInspect
        {
            get { return this.m_bUseInspect == 0 ? false : true; }
            set { m_bUseInspect = value ? 1 : 0; }
        }

        [CategoryAttribute("Recipe"), DisplayNameAttribute("14. 검사 제외 마진"), DescriptionAttribute("Wafer 외곽 검사 제외 마진(Pixel)")]
        public int InspectMargin
        {
            get { return this.m_nInspectMargin; }
            set { m_nInspectMargin = value; }
        }

        [CategoryAttribute("Recipe"), DisplayNameAttribute("15. 검사 Area 필터"), DescriptionAttribute("Wafer 외곽 검사 결함 제외 크기(Pixel)")]
        public int InspectFilterArea
        {
            get { return this.m_nInspectFilterArea; }
            set { m_nInspectFilterArea = value; }
        }

        [CategoryAttribute("Recipe"), DisplayNameAttribute("16. 검사 넓이 비율 필터"), DescriptionAttribute("Wafer 외곽 검사 결함 제외 비율(0~1)")]
        public double InspectFilterRatio
        {
            get { return this.m_dInspectFilterRatio; }
            set { m_dInspectFilterRatio = value; }
        }
        #endregion recipe
        #region Result
        [CategoryAttribute("Result"), DisplayNameAttribute("01. 결과 코드"),  DescriptionAttribute("처리 결과")]
        public int ResultCode
        {
            get { return this.m_nCode; }
        }

        [CategoryAttribute("Result"), DisplayNameAttribute("02. X 변화량"), DescriptionAttribute("기준 위치 대비 X 변화량(Pixel)")]
        public double DX
        {
            get { return this.m_dDx; }
        }

        [CategoryAttribute("Result"), DisplayNameAttribute("03. Y 변화량"), DescriptionAttribute("기준 위치 대비 Y 변화량(Pixel)")]
        public double DY
        {
            get { return this.m_dDy; }
        }

        [CategoryAttribute("Result"), DisplayNameAttribute("04. Theta 변화량"), DescriptionAttribute("기준 각도 대비 Theta 변화량(Degree)")]
        public double DTheta
        {
            get { return this.m_dDTheta; }
        }

        [CategoryAttribute("Result"), DisplayNameAttribute("05. 중심좌표 X"), DescriptionAttribute("측정된 Wafer 중심 X 좌표(Pixel)")]
        public double CenterX
        {
            get { return this.m_dCenterX; }
        }

        [CategoryAttribute("Result"), DisplayNameAttribute("06. 중심좌표 Y"), DescriptionAttribute("측정된 Wafer 중심 Y 좌표(Pixel)")]
        public double CenterY
        {
            get { return this.m_dCenterY; }
        }

        [CategoryAttribute("Result"), DisplayNameAttribute("07. 장축 크기"), DescriptionAttribute("측정된 Wafer 장축 크기(Pixel)")]
        public double MajorLength
        {
            get { return this.m_dMajorLength; }
        }

        [CategoryAttribute("Result"), DisplayNameAttribute("08. 단축 크기"), DescriptionAttribute("측정된 Wafer 단축 크기(Pixel)")]
        public double MinorLength
        {
            get { return this.m_dMinorLength; }
        }

        [CategoryAttribute("Result"), DisplayNameAttribute("09. Notch 좌표 X"), DescriptionAttribute("측정된 Notch X 좌표(Pixel)")]
        public double NotchX
        {
            get { return this.m_dNotchX; }
        }

        [CategoryAttribute("Result"), DisplayNameAttribute("10. Notch 좌표 Y"), DescriptionAttribute("측정된 Notch Y 좌표(Pixel)")]
        public double NotchY
        {
            get { return this.m_dNotchY; }
        }

        [CategoryAttribute("Result"), DisplayNameAttribute("11. Notch 각도"), DescriptionAttribute("측정된 중심과 Notch 사이 각도(Degree)")]
        public double NotchDegree
        {
            get { return this.m_dNotchDegree; }
        }
        [CategoryAttribute("Result"), DisplayNameAttribute("12. Notch Match Score"), DescriptionAttribute("Notch Matching Score(0~100)")]
        public double NotchMatchScore
        {
            get { return this.m_dNotchScore; }
        }
        [CategoryAttribute("Result"), DisplayNameAttribute("13. 검출 결함 수"), DescriptionAttribute("Wafer 외곽 검출 결함 수")]
        public int DefectCount
        {
            get { return this.m_nDefectCount; }
        }
        #endregion result;
    }

    public class CWaferPreAlignerWrapper
    {
#if DEBUG
        [DllImport("M_WaferPreAligner_Debug.dll")]
        public static extern int PreAlignProcess(IntPtr pImageData, int nImageWidth, int nImageHeight, CWaferPreAlignerResult result, ref PreAlignDefect pArrayDefect);
        [DllImport("M_WaferPreAligner_Debug.dll")]
        public static extern int GetDllVersion();
#else
        [DllImport("M_WaferPreAligner_Release.dll")]
        public static extern int PreAlignProcess(IntPtr pImageData, int nImageWidth, int nImageHeight, CWaferPreAlignerResult result, ref PreAlignDefect pArrayDefect);
        public static extern int GetDllVersion();
#endif
    }
}
