using System;
using System.Runtime.InteropServices;
using UnityEngine;

public static class ByteWritter {

    public static void Write<T>(Memory<byte> dst, T src, ref int offset) where T : struct {

        var span = dst.Span.Slice(offset, Marshal.SizeOf<T>());
        MemoryMarshal.TryWrite<T>(span, ref src);
        offset += Marshal.SizeOf<T>();

    }

    public static void WriteArray<T>(Memory<byte> dst, T[] src, ref int offset) where T : struct {

        Write<byte>(dst, (byte)src.Length, ref offset);

        var span = dst.Span.Slice(offset, src.Length * Marshal.SizeOf<T>());
        var length = src.Length;
        for (int i = 0; i < length; i++) {
            Write<T>(dst, src[i], ref offset);
        }

    }

    public static void WriteString(byte[] dst, string src, ref int offset) {

        Write<byte>(dst, (byte)src.Length, ref offset);

        byte[] array = System.Text.Encoding.UTF8.GetBytes(src);
        var length = array.Length;

        for (int i = 0; i < length; i++) {
            Write<byte>(dst, array[i], ref offset);
        }

    }

}
