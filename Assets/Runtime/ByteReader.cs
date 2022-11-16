using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

public static class ByteReader {

    public static T Read<T>(Memory<byte> src, ref int offset) where T : struct {

        var span = src.Span.Slice(offset);
        var result = MemoryMarshal.Cast<byte, T>(span)[0];
        offset += Marshal.SizeOf<T>();
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
