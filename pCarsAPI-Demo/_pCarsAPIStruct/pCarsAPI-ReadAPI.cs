using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

namespace pCarsAPI_Demo
{
    public class pCarsAPI_GetData
    {
        private static MemoryMappedFile _memoryMappedFile;
        private static GCHandle _handle;
        private static int _sharedmemorysize;
        private static byte[] _sharedMemoryReadBuffer;

        private static void InitialiseSharedMemory()
        {
            try
            {
                _memoryMappedFile = MemoryMappedFile.OpenExisting("$pcars$");
                _sharedmemorysize = Marshal.SizeOf(typeof(pCarsAPIStruct));
                _sharedMemoryReadBuffer = new byte[_sharedmemorysize];

            }
            catch (Exception ex)
            {
            }
        }

        public static Tuple<bool, pCarsAPIStruct> ReadSharedMemoryData()
        {
            var pcarsapistruct = new pCarsAPIStruct();

            try
            {
                if (_memoryMappedFile == null)
                {
                    InitialiseSharedMemory();
                }

                using (var sharedMemoryStreamView = _memoryMappedFile.CreateViewStream())
                {
                    var sharedMemoryStream = new BinaryReader(sharedMemoryStreamView);
                    _sharedMemoryReadBuffer = sharedMemoryStream.ReadBytes(_sharedmemorysize);
                    _handle = GCHandle.Alloc(_sharedMemoryReadBuffer, GCHandleType.Pinned);
                    pcarsapistruct =
                        (pCarsAPIStruct)Marshal.PtrToStructure(_handle.AddrOfPinnedObject(), typeof(pCarsAPIStruct));
                    _handle.Free();
                }

                return new Tuple<bool, pCarsAPIStruct>(true, pcarsapistruct);
            }
            catch (Exception ex)
            {
                //return false in the tuple as the read failed
                return new Tuple<bool, pCarsAPIStruct>(false, pcarsapistruct);
            }
        }
    }
}