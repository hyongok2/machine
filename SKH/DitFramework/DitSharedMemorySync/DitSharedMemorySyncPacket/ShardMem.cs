using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.MemoryMappedFiles;

namespace DitSharedMemoryPacket
{
    public class ShardMem
    {
        private MemoryMappedFile _file = null;
        private MemoryMappedViewAccessor _accessor = null;

        public string Name { get; set; }
        public int Size { get; set; }

        public int Open()
        {
            //MemoryMappedFile.OpenExisting()
            _file = MemoryMappedFile.CreateOrOpen(Name, Size);
            _accessor = _file.CreateViewAccessor();

            return 0;
        }
        public int Close()
        {
            _file.Dispose();
            _accessor.Dispose();

            return 0;
        }

        public int ReadBytes(int position, int length, out byte[] read)
        {
            read = new byte[length];

            try
            {
                _accessor.ReadArray<byte>(position, read, 0, length);
            }
            catch (System.Exception ex)
            {
                return -1;
            }
            return 0;
        }
        public int WriteBytes(int position, int length, byte[] read)
        {   
            try
            {
                _accessor.WriteArray<byte>(position, read, 0, length);
            }
            catch (System.Exception ex)
            {
                return -1;
            }
            return 0;
        }
        public void Write(long position, int value)
        {
            _accessor.Write(position, value);
        }
        public int ReadInt32(long position)
        {
            return _accessor.ReadInt32(position);
        }        
    }

}
