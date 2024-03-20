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

            Write<ushort>(dst, (ushort)src.Length, ref offset);

            Span<byte> span = dst.Span.Slice(offset, src.Length * Marshal.SizeOf<T>());
            ushort length = (ushort)src.Length;

            for (int i = 0; i < length; i++) {
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
