using System;
using System.IO;
using ProtoBuf;

public class MetaDataProtobufSerializer 
{
    public void Serialize<T>(Stream destination, T instance)
    {
        Serializer.Serialize<T>(destination, instance); 
    }

    // 这里反序列化传入的Stream的Capacity大小, 必须是buffer数据的大小。
    public T Deserialize<T>(Stream source)
    {
        return Serializer.Deserialize<T>(source);
    }

    public void Serialize(Stream dest, object instance)
    {
        Serializer.NonGeneric.Serialize(dest, instance);
    }

    // 这里反序列化传入的Stream的Capacity大小, 必须是buffer数据的大小。
    public object Deserialize(Stream source, Type type)
    {  
        return Serializer.NonGeneric.Deserialize(type, source);
    }

}

