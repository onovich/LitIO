using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

public static class ByteReader {

    public static T Read<T>(ReadOnlyMemory<byte> src, ref int offset) where T : struct {

        var length = Marshal.SizeOf<T>();
        var span = src.Span.Slice(offset);

        var result = MemoryMarshal.Cast<byte, T>(span)[0];
        offset += Marshal.SizeOf<T>();
        return result;

    }

    public static T[] ReadArray<T>(ReadOnlyMemory<byte> src, ref int offset) where T : struct {

        var length = Read<byte>(src, ref offset);
        Debug.Log("read array length=" + length);
        var span = src.Span.Slice(offset);

        var result = new T[length];
        for (int i = 0; i < span.Length; i += Marshal.SizeOf<T>()) {
            result[i] = Read<T>(src, ref offset);
        }
        return result;

    }

    public static string ReadString(byte[] src, ref int offset) {

        var length = Read<byte>(src, ref offset);
        var array = new byte[length];

        var result = Encoding.UTF8.GetString(src, offset, array.Length);
        offset += length;

        return result;

    }

}
