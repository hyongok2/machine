using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace EquipMainUi.PreAligner.Recipe
{
    public class PreAlignerRecipe
    {
        public PreAlignerRecipe()
        {
            Name = string.Empty;
        }

        [Browsable(false)]
        public ObjectId Id { get; set; }

        [Category("01. 일반"), DisplayNameAttribute("01. 레시피 이름"), DescriptionAttribute("이름")]
        public string Name { get; set; }
        [Category("01. 일반"), DisplayNameAttribute("02. 노출 시간"), DescriptionAttribute("30~30000")]
        public int ExposureTime { get; set; }
        [Category("01. 일반"), DisplayNameAttribute("03. 조명 밝기"), DescriptionAttribute("10~1000")]
        public int Bright { get; set; }
        [Category("01. 일반"), DisplayNameAttribute("04. 영상 이진화 Threshold"), DescriptionAttribute("영상 이진화 문턱값(0~255)")]
        public int EdgeThreshold { get; set; }        
        [Category("01. 일반"), DisplayNameAttribute("05. Notch 탐색 Threshold"), DescriptionAttribute("Notch 탐색 경계 거리 문턱값(5.0 ~ )")]
        public double NotchFindThreshold { get; set; }
        [Category("01. 일반"), DisplayNameAttribute("06. 최적 타원 크기 Threshold"), DescriptionAttribute("최적 타원과 Wafer 기준크기 오차 인정 범위(10.0 ~ )")]
        public double EllipseSizeThreshold { get; set; }

        [Category("02. 고급"), DisplayNameAttribute("10. 디버그 이미지 저장"), DescriptionAttribute("Debug 영상 저장 사용")]        
        public bool SaveResult{ get; set; }
        [Category("02. 고급"), DescriptionAttribute("Parameter 설정 여부"), BrowsableAttribute(false)]
        public int SetParam{ get; set; }
        [Category("02. 고급"), DisplayNameAttribute("01. 영상 밝기 전처리 사용"), DescriptionAttribute("영상 밝기 향상 전처리 사용")]
        public bool UseGrayScaleMax { get; set; }
        [Category("02. 고급"), DisplayNameAttribute("02. Notch 탐색 ROI Left"), DescriptionAttribute("Notch 탐색 영역 좌측(Pixel)")]
        public int NotchFindROILeft { get; set; }
        [Category("02. 고급"), DisplayNameAttribute("03. Notch 탐색 ROI Top"), DescriptionAttribute("Notch 탐색 영역 상측(Pixel)")]
        public int NotchFindROITop { get; set; }
        [Category("02. 고급"), DisplayNameAttribute("04. Notch 탐색 ROI Right"), DescriptionAttribute("Notch 탐색 영역 우측(Pixel)")]
        public int NotchFindROIRight { get; set; }
        [Category("02. 고급"), DisplayNameAttribute("05. Notch 탐색 ROI Bottom"), DescriptionAttribute("Notch 탐색 영역 하측(Pixel)")]
        public int NotchFindROIBottom { get; set; }
        [Category("02. 고급"), DisplayNameAttribute("06. Wafer 기준 지름"), DescriptionAttribute("Wafer 기준 지름 크기(Pixel)")]
        public double RefDiameter{ get; set; }
        [Category("02. 고급"), DisplayNameAttribute("07. Wafer 기준 중심 X"), DescriptionAttribute("Wafer 기준 중심 X 좌표(Pixel)")]
        public double RefCenterX{ get; set; }
        [Category("02. 고급"), DisplayNameAttribute("08. Wafer 기준 중심 Y"), DescriptionAttribute("Wafer 기준 중심 Y 좌표(Pixel)")]
        public double RefCenterY{ get; set; }
        [Category("02. 고급"), DisplayNameAttribute("09. Wafer 기준 각도"), DescriptionAttribute("Wafer 기준 Notch Angle (Degree)")]
        public double RefNotchDegree{ get; set; }

        [Category("03. 검사"), DisplayNameAttribute("01. 검사 수행 여부"), DescriptionAttribute("Wafer 기준 지름 크기(Pixel)")]
        public bool UseInspect { get; set; }
        [Category("03. 검사"), DisplayNameAttribute("02. 검사시 Wafer 최적 타원 외곽 마진"), DescriptionAttribute("검사 제외, 기본값 3")]
        public int InspectMargin { get; set; }
        [Category("03. 검사"), DisplayNameAttribute("03. 검사시 Blob Area Filter 조건"), DescriptionAttribute("기본값 10Pixel")]
        public int InspectFilterArea { get; set; }
        [Category("03. 검사"), DisplayNameAttribute("04. 검사시 Blob Rect와 Pixel Area의 비율"), DescriptionAttribute("W0~1, 기본값 0.5")]
        public double InspectFilterRatio { get; set; }

        public bool Update()
        {
            return PreAlignerRecipeDataMgr.Update(this);
        }

        public object Clone()
        {
            return new PreAlignerRecipe()
            {
                Name               = this.Name,
                ExposureTime       = this.ExposureTime,
                Bright             = this.Bright,
                SaveResult         = this.SaveResult,
                SetParam           = this.SetParam,
                UseGrayScaleMax    = this.UseGrayScaleMax,
                EdgeThreshold      = this.EdgeThreshold,
                NotchFindThreshold = this.NotchFindThreshold,
                NotchFindROILeft   = this.NotchFindROILeft,
                NotchFindROIRight  = this.NotchFindROIRight,
                NotchFindROITop    = this.NotchFindROITop,
                NotchFindROIBottom = this.NotchFindROIBottom,
                RefDiameter        = this.RefDiameter,
                RefCenterX         = this.RefCenterX,
                RefCenterY         = this.RefCenterY,
                RefNotchDegree     = this.RefNotchDegree,
                UseInspect         = this.UseInspect,
                InspectMargin      = this.InspectMargin,
                InspectFilterArea  = this.InspectFilterArea,
                InspectFilterRatio = this.InspectFilterRatio,
            };
        }

        public CWaferPreAlignerParam ToCWaferPreAlignerParam()
        {
            CWaferPreAlignerParam param = new CWaferPreAlignerParam();
            param.m_bSaveResult         = this.SaveResult ? 1 : 0;
            param.m_bSetParam           = this.SetParam;
            param.m_bUseGrayScaleMax    = this.UseGrayScaleMax ? 1 : 0;
            param.m_nEdgeThreshold      = this.EdgeThreshold;
            param.m_dNotchFindThreshold = this.NotchFindThreshold;
            param.m_nNotchFindROILeft   = this.NotchFindROILeft;
            param.m_nNotchFindROITop    = this.NotchFindROITop;
            param.m_nNotchFindROIRight  = this.NotchFindROIRight;
            param.m_nNotchFindROIBottom = this.NotchFindROIBottom;
            param.m_dRefCenterX         = this.RefCenterX;
            param.m_dRefCenterY         = this.RefCenterY;
            param.m_dRefDiameter        = this.RefDiameter;
            param.m_dRefNotchDegree     = this.RefNotchDegree;

            param.m_bUseInspect         = this.UseInspect ? 1 : 0;
            param.m_nInspectMargin      = this.InspectMargin;
            param.m_nInspectFilterArea  = this.InspectFilterArea;
            param.m_dInspectFilterRatio = this.InspectFilterRatio;             
            return param;
        }

        public static PreAlignerRecipe CreateDefault()
        {
            PreAlignerRecipe recp = new PreAlignerRecipe();
            recp.ExposureTime         = 1000;
            recp.Bright               = 1000;
            recp.SaveResult           = false;
            recp.SetParam             = 0;
            recp.UseGrayScaleMax      = false;
            recp.EdgeThreshold        = 100;
            recp.EllipseSizeThreshold = 30.0;
            recp.NotchFindThreshold   = 10.0;
            recp.NotchFindROILeft     = 3270;
            recp.NotchFindROITop      = 1435;
            recp.NotchFindROIRight    = 3370;
            recp.NotchFindROIBottom   = 1635;
            recp.RefCenterX           = 2013;
            recp.RefCenterY           = 1491;
            recp.RefDiameter          = 2600;
            recp.RefNotchDegree       = 0.0;
            recp.UseInspect           = true;
            recp.InspectMargin        = 3;
            recp.InspectFilterArea    = 10;
            recp.InspectFilterRatio   = 0.5;
            return recp;
        }
    }
}
