using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MortiseFrame.LitIO {

    public static class ByteCounter {

        public static int Count<T>() where T : struct {
            return Marshal.SizeOf<T>();
        }

        public static int CountArray<T>(T[] array) where T : struct {
            return sizeof(ushort) + (Marshal.SizeOf<T>() * array.Length);
        }

        public static int CountUTF8String(string str) {
            return sizeof(ushort) + Encoding.UTF8.GetByteCount(str);
        }

        public static int CountUTF8StringArray(string[] array) {
            int totalLength = sizeof(ushort);
            foreach (var str in array) {
                totalLength += CountUTF8String(str);
            }
            return totalLength;
        }

    }

}