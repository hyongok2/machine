using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.PreAligner
{
    public class CWaferPreAlignerResultToPGrid
    {
        private CWaferPreAlignerResult _obj;
        public CWaferPreAlignerResultToPGrid(CWaferPreAlignerResult obj)
        {
            if (obj == null && _obj == null)
                _obj = new CWaferPreAlignerResult();
            else
                _obj = obj;
        }

        [CategoryAttribute("Result"), DisplayNameAttribute("01. 결과 코드"), DescriptionAttribute("처리 결과")]
        public string ResultCode
        {
            get { return ((WaferPreAlignerResultCode)_obj.m_nCode).ToString(); }
        }

        [CategoryAttribute("Result"), DisplayNameAttribute("02. X 변화량"), DescriptionAttribute("기준 위치 대비 X 변화량(Pixel)")]
        public double DX
        {
            get { return _obj.m_dDx; }
        }

        [CategoryAttribute("Result"), DisplayNameAttribute("03. Y 변화량"), DescriptionAttribute("기준 위치 대비 Y 변화량(Pixel)")]
        public double DY
        {
            get { return _obj.m_dDy; }
        }

        [CategoryAttribute("Result"), DisplayNameAttribute("04. Theta 변화량"), DescriptionAttribute("기준 각도 대비 Theta 변화량(Degree)")]
        public double DTheta
        {
            get { return _obj.m_dDTheta; }
        }

        [CategoryAttribute("Result"), DisplayNameAttribute("05. 중심좌표 X"), DescriptionAttribute("측정된 Wafer 중심 X 좌표(Pixel)")]
        public double CenterX
        {
            get { return _obj.m_dCenterX; }
        }

        [CategoryAttribute("Result"), DisplayNameAttribute("06. 중심좌표 Y"), DescriptionAttribute("측정된 Wafer 중심 Y 좌표(Pixel)")]
        public double CenterY
        {
            get { return _obj.m_dCenterY; }
        }

        [CategoryAttribute("Result"), DisplayNameAttribute("07. 장축 크기"), DescriptionAttribute("측정된 Wafer 장축 크기(Pixel)")]
        public double MajorLength
        {
            get { return _obj.m_dMajorLength; }
        }

        [CategoryAttribute("Result"), DisplayNameAttribute("08. 단축 크기"), DescriptionAttribute("측정된 Wafer 단축 크기(Pixel)")]
        public double MinorLength
        {
            get { return _obj.m_dMinorLength; }
        }

        [CategoryAttribute("Result"), DisplayNameAttribute("09. Notch 좌표 X"), DescriptionAttribute("측정된 Notch X 좌표(Pixel)")]
        public double NotchX
        {
            get { return _obj.m_dNotchX; }
        }

        [CategoryAttribute("Result"), DisplayNameAttribute("10. Notch 좌표 Y"), DescriptionAttribute("측정된 Notch Y 좌표(Pixel)")]
        public double NotchY
        {
            get { return _obj.m_dNotchY; }
        }

        [CategoryAttribute("Result"), DisplayNameAttribute("11. Notch 각도"), DescriptionAttribute("측정된 중심과 Notch 사이 각도(Degree)")]
        public double NotchDegree
        {
            get { return _obj.m_dNotchDegree; }
        }

        [CategoryAttribute("Result"), DisplayNameAttribute("12. 검출 결함 수"), DescriptionAttribute("Wafer 외곽 검출 결함 수")]
        public int DefectCount
        {
            get { return _obj.m_nDefectCount; }
        }
    }
}
