using System;
using System.Runtime.InteropServices;

namespace MortiseFrame.LitIO {
    public static class ByteWriter {

        public static void Write<T>(Memory<byte> dst, T src, ref int offset) where T : struct {

            Span<byte> span = dst.Span.Slice(offset, Marshal.SizeOf<T>());
            try {
                MemoryMarshal.TryWrite<T>(span, ref src);
            } catch (Exception e) {
                throw new Exception($"Failed to write {typeof(T).Name} to memory at offset {offset}", e);
            }
            offset += Marshal.SizeOf<T>();

        }

         public static void WriteArray<T>(Memory<byte> dst, T[] src, ref int offset) where T : struct {
            int elementSize = (int)Marshal.SizeOf<T>();
            int requiredSize = (int)(sizeof(int) + (src.Length * elementSize));
            if (offset + requiredSize > dst.Length) {
                throw new ArgumentOutOfRangeException(
                    $"需要写入 {src.Length} 个 {typeof(T).Name} (共 {requiredSize} 字节)，" +
                    $"但缓冲区只剩 {dst.Length - offset} 字节");
            }

            Write<int>(dst, (int)src.Length, ref offset);

            for (int i = 0; i < src.Length; i++) {
                Write<T>(dst, src[i], ref offset);
            }
        }

        public static void WriteUTF8String(byte[] dst, string src, ref int offset) {

            byte[] array = System.Text.Encoding.UTF8.GetBytes(src);
            ushort length = (ushort)array.Length;

            Write<ushort>(dst, length, ref offset);

            Buffer.BlockCopy(array, 0, dst, offset, length);
            offset += length;
        }

        public static void WriteUTF8StringArray(byte[] dst, string[] src, ref int offset) {

            Write<ushort>(dst, (ushort)src.Length, ref offset);

            var length = src.Length;
            for (int i = 0; i < length; i++) {
                WriteUTF8String(dst, src[i], ref offset);
            }

        }

    }

}
