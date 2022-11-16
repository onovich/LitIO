using System;
using System.Runtime.InteropServices;
using UnityEngine;

public static class ByteWritter {

    public static void Write<T>(Memory<byte> dst, T src, ref int offset) where T : struct {

        var span = dst.Span.Slice(offset);
        MemoryMarshal.TryWrite<T>(span, ref src);
        for (byte i = 0; i < Marshal.SizeOf<T>(); i++) {
            offset += 1;
        }

    }

    public static void WriteString(byte[] dst, string src, ref int offset) {
        Write<byte>(dst, (byte)src.Length, ref offset);
        byte[] array = System.Text.Encoding.UTF8.GetBytes(src);
        var length = array.Length;
    
        for(int i = 0; i < length; i++) {
            Write<byte>(dst, array[i], ref offset);
        }
        offset += length;
    }

}
