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

         [Category(GG.boChinaLanguage ? "01. 一般" : "01. 일반"), DisplayNameAttribute(GG.boChinaLanguage ? "01. Recipe 名称" : "01. 레시피 이름"), DescriptionAttribute(GG.boChinaLanguage ? "名称" : "이름")]
        public string Name { get; set; }
        [Category(GG.boChinaLanguage ? "01. 一般" : "01. 일반"), DisplayNameAttribute(GG.boChinaLanguage ? "02. 曝光时间" : "02. 노출 시간"), DescriptionAttribute("30~30000")]
        public int ExposureTime { get; set; }
        [Category(GG.boChinaLanguage ? "01. 一般" : "01. 일반"), DisplayNameAttribute(GG.boChinaLanguage ? "03. 照明时间" : "03. 조명 밝기"), DescriptionAttribute("10~1000")]
        public int Bright { get; set; }
        [Category(GG.boChinaLanguage ? "01. 一般" : "01. 일반"), DisplayNameAttribute(GG.boChinaLanguage ? "04. 影像 二值化 Threshold" : "04. 영상 이진화 Threshold"), DescriptionAttribute(GG.boChinaLanguage ? "影像 二值化 临界点(0~255)" : "영상 이진화 문턱값(0~255)")]
        public int EdgeThreshold { get; set; }
        [Category(GG.boChinaLanguage ? "01. 一般" : "01. 일반"), DisplayNameAttribute(GG.boChinaLanguage ? "05. Notch 探索 Threshold" : "05. Notch 탐색 Threshold"), DescriptionAttribute(GG.boChinaLanguage ? "Notch 探索警界距离临界点(5.0 ~ )" : "Notch 탐색 경계 거리 문턱값(5.0 ~ )")]
        public double NotchFindThreshold { get; set; }
        [Category(GG.boChinaLanguage ? "01. 一般" : "01. 일반"), DisplayNameAttribute(GG.boChinaLanguage ? "06. 最佳椭圆大小 Threshold" : "06. 최적 타원 크기 Threshold"), DescriptionAttribute(GG.boChinaLanguage ? "最佳椭圆和Wafer 基准大小的误差认证范围(10.0 ~ )" : "최적 타원과 Wafer 기준크기 오차 인정 범위(10.0 ~ )")]
        public double EllipseSizeThreshold { get; set; }

        [Category(GG.boChinaLanguage ? "02. 高级" : "02. 고급"), DisplayNameAttribute(GG.boChinaLanguage ? "10. Debug 图像储存" : "10. 디버그 이미지 저장"), DescriptionAttribute(GG.boChinaLanguage ? "使用Debug 图像储存" : "Debug 영상 저장 사용")]
        public bool SaveResult { get; set; }
        [Category(GG.boChinaLanguage ? "02. 高级" : "02. 고급"), DescriptionAttribute(GG.boChinaLanguage ? "Parameter 设置与否" : "Parameter 설정 여부"), BrowsableAttribute(false)]
        public int SetParam { get; set; }
        [Category(GG.boChinaLanguage ? "02. 高级" : "02. 고급"), DisplayNameAttribute(GG.boChinaLanguage ? "01. 影像亮度前处理 采用" : "01. 영상 밝기 전처리 사용"), DescriptionAttribute(GG.boChinaLanguage ? "始终使用影像亮度前处理" : "영상 밝기 향상 전처리 사용")]
        public bool UseGrayScaleMax { get; set; }
        [Category(GG.boChinaLanguage ? "02. 高级" : "02. 고급"), DisplayNameAttribute(GG.boChinaLanguage ? "02. Notch 寻求 ROI Left" : "02. Notch 탐색 ROI Left"), DescriptionAttribute(GG.boChinaLanguage ? "Notch 探索领域左侧(Pixel)" : "Notch 탐색 영역 좌측(Pixel)")]
        public int NotchFindROILeft { get; set; }
        [Category(GG.boChinaLanguage ? "02. 高级" : "02. 고급"), DisplayNameAttribute(GG.boChinaLanguage ? "03. Notch 寻求 ROI Top" : "03. Notch 탐색 ROI Top"), DescriptionAttribute(GG.boChinaLanguage ? "Notch 探索领域上侧(Pixel)" : "Notch 탐색 영역 상측(Pixel)")]
        public int NotchFindROITop { get; set; }
        [Category(GG.boChinaLanguage ? "02. 高级" : "02. 고급"), DisplayNameAttribute(GG.boChinaLanguage ? "04. Notch 寻求 ROI Right" : "04. Notch 탐색 ROI Right"), DescriptionAttribute(GG.boChinaLanguage ? "Notch 探索领域右侧(Pixel)" : "Notch 탐색 영역 우측(Pixel)")]
        public int NotchFindROIRight { get; set; }
        [Category(GG.boChinaLanguage ? "02. 高级" : "02. 고급"), DisplayNameAttribute(GG.boChinaLanguage ? "05. Notch 寻求 ROI Bottom" : "05. Notch 탐색 ROI Bottom"), DescriptionAttribute(GG.boChinaLanguage ? "Notch 探索领域下侧(Pixel)" : "Notch 탐색 영역 하측(Pixel)")]
        public int NotchFindROIBottom { get; set; }
        [Category(GG.boChinaLanguage ? "02. 高级" : "02. 고급"), DisplayNameAttribute(GG.boChinaLanguage ? "06. Wafer 基准直径大小" : "06. Wafer 기준 지름"), DescriptionAttribute(GG.boChinaLanguage ? "Wafer 基准直径大小(Pixel)" : "Wafer 기준 지름 크기(Pixel)")]
        public double RefDiameter { get; set; }
        [Category(GG.boChinaLanguage ? "02. 高级" : "02. 고급"), DisplayNameAttribute(GG.boChinaLanguage ? "07. Wafer 基准中心 X" : "07. Wafer 기준 중심 X"), DescriptionAttribute(GG.boChinaLanguage ? "Wafer 基准中心X 坐标(Pixel)" : "Wafer 기준 중심 X 좌표(Pixel)")]
        public double RefCenterX { get; set; }
        [Category(GG.boChinaLanguage ? "02. 高级" : "02. 고급"), DisplayNameAttribute(GG.boChinaLanguage ? "08. Wafer 基准中心 Y" : "08. Wafer 기준 중심 Y"), DescriptionAttribute(GG.boChinaLanguage ? "Wafer 基准中心Y 坐标(Pixel)" : "Wafer 기준 중심 Y 좌표(Pixel)")]
        public double RefCenterY { get; set; }
        [Category(GG.boChinaLanguage ? "02. 高级" : "02. 고급"), DisplayNameAttribute(GG.boChinaLanguage ? "09. Wafer 基准角度" : "09. Wafer 기준 각도"), DescriptionAttribute(GG.boChinaLanguage ? "Wafer (Notch Angle)基准角度(Degree)" : "Wafer 기준 Notch Angle (Degree)")]

        public double RefNotchDegree{ get; set; }

        [Category(GG.boChinaLanguage ? "03. 检查" : "03. 검사"), DisplayNameAttribute(GG.boChinaLanguage ? "01. 是否执行检察" : "01. 검사 수행 여부"), DescriptionAttribute(GG.boChinaLanguage ? "Wafer 基准直径大小(Pixel)" : "Wafer 기준 지름 크기(Pixel)")]
        public bool UseInspect { get; set; }
        [Category(GG.boChinaLanguage ? "03. 检查" : "03. 검사"), DisplayNameAttribute(GG.boChinaLanguage ? "02. 检测时 Wafer 最佳椭圆外角Margin" : "02. 검사시 Wafer 최적 타원 외곽 마진"), DescriptionAttribute(GG.boChinaLanguage ? "检查除外, 基本值 3" : "검사 제외, 기본값 3")]
        public int InspectMargin { get; set; }
        [Category(GG.boChinaLanguage ? "03. 检查" : "03. 검사"), DisplayNameAttribute(GG.boChinaLanguage ? "03. 检查时, (Blob Area Fliter) 条件" : "03. 검사시 Blob Area Filter 조건"), DescriptionAttribute(GG.boChinaLanguage ? "基本值 10Pixel" : "기본값 10Pixel")]
        public int InspectFilterArea { get; set; }
        [Category(GG.boChinaLanguage ? "03. 检查" : "03. 검사"), DisplayNameAttribute(GG.boChinaLanguage ? "04. 检查时, (Blob Rect)和 (Pixel Area)的比列" : "04. 검사시 Blob Rect와 Pixel Area의 비율"), DescriptionAttribute(GG.boChinaLanguage ? "W0~1, 基本值 0.5" : "W0~1, 기본값 0.5")]
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
