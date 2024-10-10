using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dit.Framework.RSNMC.PLC
{
    public enum RSNMCIOType
    {
        X,
        Y,
        AI,
        AO
    }
    /// <summary>
    /// IO R/W 할 정보
    /// </summary>
    public class RSNMCIOIntefaceInfo
    {
        public ushort ID;
        public ushort ModuleOrder;
        public RSNMCIOType IoType;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="moduleOrder">X,Y,AI,AO 각각 모듈별 순서</param>
        /// <param name="ioType"></param>
        public RSNMCIOIntefaceInfo(ushort id, ushort moduleOrder, RSNMCIOType ioType)
        {
            this.ID = id;
            this.ModuleOrder= moduleOrder;            
            this.IoType = ioType;
        }
    }
}
