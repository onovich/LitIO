using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MortiseFrame.LitIO;

namespace MortiseFrame.LitIO.Sample {
    public class Sample : MonoBehaviour {

        private void Start() {

            byte[] dst = new byte[1024];

            int offset = 0;
            ByteWriter.Write(dst, -369, ref offset);
            ByteWriter.Write(dst, 5, ref offset);
            ByteWriter.Write(dst, 17829, ref offset);
            ByteWriter.Write(dst, 17829, ref offset);
            ByteWriter.Write(dst, true, ref offset);
            ByteWriter.Write(dst, 1.5f, ref offset);
            ByteWriter.Write(dst, 1.5, ref offset);
            ByteWriter.WriteUTF8String(dst, "Hello World!", ref offset);
            ByteWriter.Write(dst, 5, ref offset);
            ByteWriter.WriteArray(dst, new int[] { 1, 2, 3, 4, 5 }, ref offset);

            offset = 0;
            var a = ByteReader.Read<int>(dst, ref offset);
            var b = ByteReader.Read<int>(dst, ref offset);
            var c = ByteReader.Read<int>(dst, ref offset);
            var d = ByteReader.Read<int>(dst, ref offset);
            var e = ByteReader.Read<bool>(dst, ref offset);
            var f = ByteReader.Read<float>(dst, ref offset);
            var g = ByteReader.Read<double>(dst, ref offset);
            var h = ByteReader.ReadUTF8String(dst, ref offset);
            var j = ByteReader.Read<int>(dst, ref offset);
            var i = ByteReader.ReadArray<int>(dst, ref offset);

            Debug.Log("a=" + a + ";b=" + b + ";c=" + c + ";d=" + d + ";e=" + e + ";f=" + f + ";g=" + g + ";h=" + h + ";j=" + j);
            Debug.Log("i.length=" + i.Length);
            Debug.Log("i=" + i[0] + ";" + i[1] + ";" + i[2] + ";" + i[3] + ";" + i[4]);

            var countInt = ByteCounter.Count<int>();
            var countByte = ByteCounter.Count<byte>();
            var countUShort = ByteCounter.Count<ushort>();
            var countString = ByteCounter.CountUTF8String("Hello World!");
            var countString2 = ByteCounter.CountUTF8String("Hello World! 你好世界！");
            var countIntArray = ByteCounter.CountArray(new int[] { 1, 2, 3, 4, 5 });
            var countStringArray = ByteCounter.CountUTF8StringArray(new string[] { "Hello", "World", "!" });

            Debug.Log("countInt=" + countInt + ";countByte=" + countByte + ";countUShort=" + countUShort + ";countString=" + countString + ";countString2=" + countString2 + ";countIntArray=" + countIntArray + ";countStringArray=" + countStringArray);

        }
    }
}
