using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dit.Framework;
using Dit.Framework.PLC;

namespace Dit.Framework.Analog
{
    public partial class UcrlItem_64RD : UserControl
    {
        private ADConverter_AJ65BT_64RD3 _advn = null;
        private List<UcrlIoItem> _lstIoViewItems = new List<UcrlIoItem>();

        public UcrlItem_64RD()
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
        public void Initialize(string title, ADConverter_AJ65BT_64RD3 advn)
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
            for (int iPos = 0; iPos < 4; iPos++)
            {
                if (iPos < _advn.ChannelCount)
                {
                    SetView(iPos, string.Format("RD{0}_CH{1}_CONVERSION_COMPLETE", index, iPos + 1),            /**/_advn.XB_A2DCompleteFlag[iPos]);
                }
                else
                {
                    SetView(iPos, "",                                                                           /**/null);
                }                
            }
            SetView(04, "",                                                                                     /**/null);
            SetView(05, "",                                                                                     /**/null);
            SetView(06, "",                                                                                     /**/null);
            SetView(07, "",                                                                                     /**/null);
            SetView(08, "",                                                                                     /**/null);
            SetView(09, "",                                                                                     /**/null);           
            SetView(10, "",                                                                                     /**/null);
            SetView(11, "",                                                                                     /**/null);
            SetView(12, "",                                                                                     /**/null);
            SetView(13, "",                                                                                     /**/null);
            SetView(14, string.Format("RD{0}_INITIAL_DATA_PROCESSING_REQUEST", index),                          /**/_advn.XB_InitDataProcessReqFlag);
            SetView(15, string.Format("RD{0}_INITIAL_DATA_SETTING_COMPLETE_FLAG", index),                       /**/_advn.XB_InitDataSetCompleteFlag);
            SetView(16, string.Format("RD{0}_ERROR_STATE_FLAG", index),                                         /**/_advn.XB_ErrorStatusFlag);
            SetView(17, string.Format("RD{0}_REMOTE_READY", index),                                             /**/_advn.XB_RemoteReady);
            SetView(18, "",                                                                                     /**/null);
            SetView(19, "",                                                                                     /**/null);
            SetView(20, "",                                                                                     /**/null);
            SetView(21, "",                                                                                     /**/null);
            SetView(22, "",                                                                                     /**/null);
            SetView(23, "",                                                                                     /**/null);

            for (int iPos = 0; iPos < 4; iPos++)
            {
                if (iPos < _advn.ChannelCount)
                {
                    SetView(iPos + 24, string.Format("RD{0}_CH{1}_CONVERSION_ENABLE", index, iPos + 1),         /**/_advn.YB_EnableFlag[iPos]);
                    SetView(iPos + 28, string.Format("RD{0}_CH{1}_SAMPLING_PROCESSING", index, iPos + 1),       /**/_advn.YB_Sampling[iPos]);
                }
                else
                {
                    SetView(iPos + 24, "",                                                                      /**/null);
                    SetView(iPos + 28, "",                                                                      /**/null);
                }
            }           
            SetView(32, "",                                                                                     /**/null);
            SetView(33, "",                                                                                     /**/null);            
            SetView(34, "",                                                                                     /**/null);
            SetView(35, "",                                                                                     /**/null);
            SetView(36, "",                                                                                     /**/null);
            SetView(37, string.Format("RD{0}_OFFSET/GAIN_SELECT", index),                                       /**/_advn.YB_Offset);
            SetView(38, string.Format("RD{0}_NITIAL_DATA_PROCESS_COMPLETE_FLAG", index),                        /**/_advn.YB_InitDataProcessCompleteFlag);
            SetView(39, string.Format("RD{0}_NITIAL_DATA_SETTING_REQUEST_FLAG", index),                         /**/_advn.YB_InitDataSetReqFlag);
            SetView(40, string.Format("RD{0}_RROR_RESET_FLAG", index),                                          /**/_advn.YB_ErrorReset);
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
