using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MortiseFrame.LitIO {

    public static class ByteReader {

        public static T Read<T>(ReadOnlyMemory<byte> src, ref int offset) where T : struct {

            ushort length = (ushort)Marshal.SizeOf<T>();
            ReadOnlySpan<byte> span = src.Span.Slice(offset, length);

            var result = MemoryMarshal.Cast<byte, T>(span)[0];
            offset += Marshal.SizeOf<T>();
            return result;

        }

         public static T[] ReadArray<T>(ReadOnlyMemory<byte> src, ref int offset) where T : struct {
            int length = Read<int>(src, ref offset);

            int elementSize = (int)Marshal.SizeOf<T>();
            int requiredSize = (int)(length * elementSize);
            if (offset + requiredSize > src.Length) {
                throw new ArgumentOutOfRangeException(
                    $"需要读取 {length} 个 {typeof(T).Name} (共 {requiredSize} 字节)，" +
                    $"但缓冲区只剩 {src.Length - offset} 字节");
            }

            var result = new T[length];
            for (int i = 0; i < length; i++) {
                result[i] = Read<T>(src, ref offset);
            }
            return result;
        }

        public static string ReadUTF8String(byte[] src, ref int offset) {

            ushort length = Read<ushort>(src, ref offset);
            string result = Encoding.UTF8.GetString(src, offset, length);
            offset += length;

            return result;

        }

        public static string[] ReadUTF8StringArray(byte[] src, ref int offset) {

            ushort length = Read<ushort>(src, ref offset);
            string[] result = new string[length];

            for (int i = 0; i < length; i += 1) {
                result[i] = ReadUTF8String(src, ref offset);
            }
            return result;

        }

    }

}