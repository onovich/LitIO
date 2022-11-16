using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour {

    private void Start() {

        byte[] dst = new byte[1024];

        int offset = 0;
        ByteWritter.Write(dst, -369, ref offset);
        ByteWritter.Write(dst, 5, ref offset);
        ByteWritter.Write(dst, 17829, ref offset);
        ByteWritter.Write(dst, 17829, ref offset);
        ByteWritter.Write(dst, true, ref offset);
        ByteWritter.Write(dst, 1.5f, ref offset);
        ByteWritter.Write(dst, 1.5, ref offset);
        ByteWritter.WriteString(dst, "Hello World!", ref offset);
        //ByteWritter.WriteArray(dst, new int[] { 1, 2, 3, 4, 5 }, ref offset);

        offset = 0;
        var a = ByteReader.Read<int>(dst, ref offset);
        var b = ByteReader.Read<int>(dst, ref offset);
        var c = ByteReader.Read<int>(dst, ref offset);
        var d = ByteReader.Read<int>(dst, ref offset);
        var e = ByteReader.Read<bool>(dst, ref offset);
        var f = ByteReader.Read<float>(dst, ref offset);
        var g = ByteReader.Read<double>(dst, ref offset);
        var h = ByteReader.ReadString(dst, ref offset);
        //var i = ByteReader.ReadArray<int>(dst, ref offset);

        Debug.Log("a=" + a + ";b=" + b + ";c=" + c + ";d=" + d + ";e=" + e + ";f=" + f + ";g=" + g + ";h=" + h);
        //Debug.Log("i.length="+i.Length);
        //Debug.Log("i="+i[0]+";"+i[1]+";"+i[2]+";"+i[3]+";"+i[4]);

    }
}
