# LitIO
LitIO is a serialization/deserialization tool developed in C#, designed to be lightweight. The name is derived from "Litio," the Spanish term for Lithium, chosen for its association with lightweight properties.<br/>
**LitIO 是用 C# 编写的序列化 / 反序列化工具，足够轻量级。名字取自于 Litio，锂——同样轻量级。**

# Features
The ByteWriter.Write\<T\> and ByteReader.Read\<T\> methods can be used with any value type data structures.<br/>
**ByteWriter.Write\<T\> 和 ByteReader.Read\<T\> 方法可以用于任意值类型数据结构。**

Reading and writing arrays and strings will occupy an int as an information header to store length information.<br/>
**数组和字符串的读写会自动占用一个 int 作为信息头储存长度信息。**

If manual calculation of data length is needed, the ByteCounter interface can be used.<br/>
**如果需要手动计算数据长度，可以使用 ByteCounter 接口。**

# Examples
```
// Serialization
public void WriteTo(byte[] dst, ref int offset) {
    int offset = 0;
    ByteWriter.Write<sbyte>(dst, status, ref offset);
    ByteWriter.WriteUTF8StringArray(dst, userNames, ref offset);
    ByteWriter.WriteArray<Vector2>(dst, rolePoses, ref offset);
}
```

```
// Deserialization
public void FromBytes(byte[] src, ref int offset) {
    int offset = 0;
    sbyte status = ByteReader.Read<sbyte>(src, ref offset);
    string[] userNames = ByteReader.ReadUTF8StringArray(src, ref offset);
    Vector2 rolePoses = ByteReader.ReadArray<Vector2>(src, ref offset);
}
```

```
// Evaluate
 public int GetEvaluatedSize(out bool isCertain) {
    isCertain = false;
    int count = ByteCounter.Count<sbyte>()
    + ByteCounter.CountUTF8StringArray(userNames)
    + ByteCounter.CountArray<byte>(rolePoses);
    return count;
}
```
