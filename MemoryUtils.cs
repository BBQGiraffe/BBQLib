using System;
using System.Runtime.InteropServices;
namespace BBQLib
{
    public static class MemoryUtils
    {
        public static IntPtr ArrayToPointer(byte[] buffer)
        {
            IntPtr unmanagedPointer = Marshal.AllocHGlobal(buffer.Length);
            Marshal.Copy(buffer, 0, unmanagedPointer, buffer.Length);
            return unmanagedPointer;
        }

        public static IntPtr ObjectToPointer(object obj)
        {
            var handle = GCHandle.Alloc(obj);
            return (IntPtr)handle;
        }
    
    }
}