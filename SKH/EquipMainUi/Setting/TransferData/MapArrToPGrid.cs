using EquipMainUi.Struct.Detail.EFEM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EquipMainUi.Common.Convenience.PropertyGridEx;

namespace EquipMainUi.Setting.TransferData
{
    public class MappArrToPGrid
    {
        private EmEfemMappingInfo[] _map;
        [DisplayName("13"), Editor(typeof(PGridBoolEditor), typeof(UITypeEditor))] public bool _13 { get { return _map[12] != EmEfemMappingInfo.Absence; } set { _map[12] = value ? EmEfemMappingInfo.Presence : EmEfemMappingInfo.Absence; } }
        [DisplayName("12"), Editor(typeof(PGridBoolEditor), typeof(UITypeEditor))] public bool _12 { get { return _map[11] != EmEfemMappingInfo.Absence; } set { _map[11] = value ? EmEfemMappingInfo.Presence : EmEfemMappingInfo.Absence; } }
        [DisplayName("11"), Editor(typeof(PGridBoolEditor), typeof(UITypeEditor))] public bool _11 { get { return _map[10] != EmEfemMappingInfo.Absence; } set { _map[10] = value ? EmEfemMappingInfo.Presence : EmEfemMappingInfo.Absence; } }
        [DisplayName("10"), Editor(typeof(PGridBoolEditor), typeof(UITypeEditor))] public bool _10 { get { return _map[09] != EmEfemMappingInfo.Absence; } set { _map[09] = value ? EmEfemMappingInfo.Presence : EmEfemMappingInfo.Absence; } }
        [DisplayName("09"), Editor(typeof(PGridBoolEditor), typeof(UITypeEditor))] public bool _09 { get { return _map[08] != EmEfemMappingInfo.Absence; } set { _map[08] = value ? EmEfemMappingInfo.Presence : EmEfemMappingInfo.Absence; } }
        [DisplayName("08"), Editor(typeof(PGridBoolEditor), typeof(UITypeEditor))] public bool _08 { get { return _map[07] != EmEfemMappingInfo.Absence; } set { _map[07] = value ? EmEfemMappingInfo.Presence : EmEfemMappingInfo.Absence; } }
        [DisplayName("07"), Editor(typeof(PGridBoolEditor), typeof(UITypeEditor))] public bool _07 { get { return _map[06] != EmEfemMappingInfo.Absence; } set { _map[06] = value ? EmEfemMappingInfo.Presence : EmEfemMappingInfo.Absence; } }
        [DisplayName("06"), Editor(typeof(PGridBoolEditor), typeof(UITypeEditor))] public bool _06 { get { return _map[05] != EmEfemMappingInfo.Absence; } set { _map[05] = value ? EmEfemMappingInfo.Presence : EmEfemMappingInfo.Absence; } }
        [DisplayName("05"), Editor(typeof(PGridBoolEditor), typeof(UITypeEditor))] public bool _05 { get { return _map[04] != EmEfemMappingInfo.Absence; } set { _map[04] = value ? EmEfemMappingInfo.Presence : EmEfemMappingInfo.Absence; } }
        [DisplayName("04"), Editor(typeof(PGridBoolEditor), typeof(UITypeEditor))] public bool _04 { get { return _map[03] != EmEfemMappingInfo.Absence; } set { _map[03] = value ? EmEfemMappingInfo.Presence : EmEfemMappingInfo.Absence; } }
        [DisplayName("03"), Editor(typeof(PGridBoolEditor), typeof(UITypeEditor))] public bool _03 { get { return _map[02] != EmEfemMappingInfo.Absence; } set { _map[02] = value ? EmEfemMappingInfo.Presence : EmEfemMappingInfo.Absence; } }
        [DisplayName("02"), Editor(typeof(PGridBoolEditor), typeof(UITypeEditor))] public bool _02 { get { return _map[01] != EmEfemMappingInfo.Absence; } set { _map[01] = value ? EmEfemMappingInfo.Presence : EmEfemMappingInfo.Absence; } }
        [DisplayName("01"), Editor(typeof(PGridBoolEditor), typeof(UITypeEditor))] public bool _01 { get { return _map[00] != EmEfemMappingInfo.Absence; } set { _map[00] = value ? EmEfemMappingInfo.Presence : EmEfemMappingInfo.Absence; } }

        public MappArrToPGrid(EmEfemMappingInfo[] map)
        {
            _map = map;
        }

        public EmEfemMappingInfo[] Get()
        {
            return _map;
        }
    }
}
