using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dit.Framework.PLC;

namespace Dit.Framework.Analog
{
    public partial class UcrlItem_68DAVN : UserControl
    {
        private ADConverter_AJ65VBTCU_68DAVN _advn = null;
        private List<UcrlIoItem> _lstIoViewItems = new List<UcrlIoItem>();

        public UcrlItem_68DAVN()
        {
            InitializeComponent();

            #region _IO Item add to list
            _lstIoViewItems.Add(ucrlIoItem01);
            _lstIoViewItems.Add(ucrlIoItem02);
            _lstIoViewItems.Add(ucrlIoItem03);
            _lstIoViewItems.Add(ucrlIoItem04);
            _lstIoViewItems.Add(ucrlIoItem05);
            _lstIoViewItems.Add(ucrlIoItem06);
            _lstIoViewItems.Add(ucrlIoItem07);
            _lstIoViewItems.Add(ucrlIoItem08);
            _lstIoViewItems.Add(ucrlIoItem09);
            _lstIoViewItems.Add(ucrlIoItem10);
            _lstIoViewItems.Add(ucrlIoItem11);
            _lstIoViewItems.Add(ucrlIoItem12);
            _lstIoViewItems.Add(ucrlIoItem13);
            _lstIoViewItems.Add(ucrlIoItem14);
            _lstIoViewItems.Add(ucrlIoItem15);
            _lstIoViewItems.Add(ucrlIoItem16);
            _lstIoViewItems.Add(ucrlIoItem17);
            _lstIoViewItems.Add(ucrlIoItem18);
            _lstIoViewItems.Add(ucrlIoItem19);
            _lstIoViewItems.Add(ucrlIoItem20);
            _lstIoViewItems.Add(ucrlIoItem21);
            _lstIoViewItems.Add(ucrlIoItem22);
            _lstIoViewItems.Add(ucrlIoItem23);
            _lstIoViewItems.Add(ucrlIoItem24);
            _lstIoViewItems.Add(ucrlIoItem25);
            _lstIoViewItems.Add(ucrlIoItem26);
            _lstIoViewItems.Add(ucrlIoItem27);
            _lstIoViewItems.Add(ucrlIoItem28);
            _lstIoViewItems.Add(ucrlIoItem29);
            _lstIoViewItems.Add(ucrlIoItem30);
            _lstIoViewItems.Add(ucrlIoItem31);
            _lstIoViewItems.Add(ucrlIoItem32);
            _lstIoViewItems.Add(ucrlIoItem33);
            _lstIoViewItems.Add(ucrlIoItem34);
            _lstIoViewItems.Add(ucrlIoItem35);
            _lstIoViewItems.Add(ucrlIoItem36);
            _lstIoViewItems.Add(ucrlIoItem37);
            _lstIoViewItems.Add(ucrlIoItem38);
            _lstIoViewItems.Add(ucrlIoItem39);
            _lstIoViewItems.Add(ucrlIoItem40);
            _lstIoViewItems.Add(ucrlIoItem41);
            _lstIoViewItems.Add(ucrlIoItem42);
            _lstIoViewItems.Add(ucrlIoItem43);
            _lstIoViewItems.Add(ucrlIoItem44);
            _lstIoViewItems.Add(ucrlIoItem45);
            _lstIoViewItems.Add(ucrlIoItem46);
            _lstIoViewItems.Add(ucrlIoItem47);
            _lstIoViewItems.Add(ucrlIoItem48);
            #endregion
        }
        public void Initialize(string title, ADConverter_AJ65VBTCU_68DAVN advn)
        {
            _advn = advn;
            lblTitle.Text = title;
        }
        public void UpdateUi()
        {
            _lstIoViewItems.ForEach(f =>
            {
                f.UpdateBitColor();
            });
        }
        public void FillMonitor(int index = 1)
        {
            SetView(00, "",                                                                                     /**/null);
            SetView(01, "",                                                                                     /**/null);
            SetView(02, "",                                                                                     /**/null);
            SetView(03, "",                                                                                     /**/null);
            SetView(04, "",                                                                                     /**/null);
            SetView(05, "",                                                                                     /**/null);
            SetView(06, "",                                                                                     /**/null);
            SetView(07, "",                                                                                     /**/null);
            SetView(08, "",                                                                                     /**/null);
            SetView(09, "",                                                                                     /**/null);
            SetView(10, string.Format("DA{0}_INITIAL_DATA_PROCESSING_REQUEST", index),                          /**/_advn.XB_DataInitReqFlag);
            SetView(11, string.Format("DA{0}_INITIAL_DATA_SETTING_COMPLETE_FLAG", index),                       /**/_advn.XB_DataSetCompleteFlag);
            SetView(12, string.Format("DA{0}_ERROR_STATE_FLAG", index),                                         /**/_advn.XB_ErrorStatusFlag);
            SetView(13, string.Format("DA{0}_REMOTE_READY", index),                                             /**/_advn.XB_RemoteReady);
            SetView(14, "",                                                                                     /**/null);
            SetView(15, "",                                                                                     /**/null);
            SetView(16, "",                                                                                     /**/null);
            SetView(17, "",                                                                                     /**/null);
            SetView(18, "",                                                                                     /**/null);
            SetView(19, "",                                                                                     /**/null);
            SetView(20, "",                                                                                     /**/null);
            SetView(21, "",                                                                                     /**/null);
            SetView(22, "",                                                                                     /**/null);
            SetView(23, "",                                                                                     /**/null);

            for (int iPos = 0; iPos < 8; iPos++)
            {
                if (iPos < _advn.ChannelCount)
                {
                    SetView(iPos + 24, string.Format("DA{0}_CH{1}_OUTPUT_PERMIT", index, iPos + 1),            /**/_advn.YB_OutputPermitBit[iPos]);
                }
                else
                {
                    SetView(iPos + 24, "",                                                                           /**/null);
                }
            }
            SetView(32, "",                                                                                     /**/null);
            SetView(33, "",                                                                                     /**/null);
            SetView(34, string.Format("DA{0}_NITIAL_DATA_PROCESS_COMPLETE_FLAG", index),                        /**/_advn.YB_DataInitCompleteFlag);
            SetView(35, string.Format("DA{0}_NITIAL_DATA_SETTING_REQUEST_FLAG", index),                         /**/_advn.YB_DataSetReqFlag);
            SetView(36, string.Format("DA{0}_RROR_RESET_FLAG", index),                                          /**/_advn.YB_ErrorResetFlag);
            SetView(37, "",                                                                                     /**/null);
            SetView(38, "",                                                                                     /**/null);
            SetView(39, "",                                                                                     /**/null);
            SetView(40, "",                                                                                     /**/null);
            SetView(41, "",                                                                                     /**/null);
            SetView(42, "",                                                                                     /**/null);
            SetView(43, "",                                                                                     /**/null);
            SetView(44, "",                                                                                     /**/null);
            SetView(45, "",                                                                                     /**/null);
            SetView(46, "",                                                                                     /**/null);
            SetView(47, "",                                                                                     /**/null);
        }
        //Review
        public void SetView(int iPos, string name, PlcAddr addr)
        {
            _lstIoViewItems[iPos].Name = name;
            _lstIoViewItems[iPos].MonAddress = addr;
        }
    }
}
