using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

public static class ByteReader {

    public static T Read<T>(Memory<byte> src, ref int offset) where T : struct {

        var span = src.Span.Slice(offset);
        MemoryMarshal.TryRead<T>(span, out T result);
        for (byte i = 0; i < Marshal.SizeOf<T>(); i++) {
            offset += 1;
        }
        return result;

    }

    public static string ReadString(byte[] src, ref int offset) {
        byte length = Read<byte>(src, ref offset);
        byte[] array = new byte[length];

        string result = Encoding.UTF8.GetString(src, offset, array.Length);
        offset += length;

        return result;
    }

}
