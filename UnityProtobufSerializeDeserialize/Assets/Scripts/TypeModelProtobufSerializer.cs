using System;
using System.IO;
using System.Collections.Generic;
using ProtoBuf;
using ProtoBuf.Meta;

public class ProtobufSerializer
{
    private TypeModel typeModel = null;

    public ProtobufSerializer(TypeModel typeModel)
    {
        this.typeModel = typeModel;
    }

    public void Serialize(Stream dest, object value)
    {
        typeModel.Serialize(dest, value);
    }

    public object Deserialize(Stream source, Type type)
    {
        return typeModel.Deserialize(source, null, type);
    }

}

public class TypeModelProtobufSerializer : TypeModel
{
    // 这个key要从0开始
    private static readonly Dictionary<Type, int> type2KeyDic = new Dictionary<Type, int>();

    static TypeModelProtobufSerializer()
    {
        type2KeyDic.Add(typeof(ActiveCodeReqPROTO), 0);
    }

    private static void Write(ActiveCodeReqPROTO activeCodeReqPROTO, ProtoWriter protoWrite)
    {
        long accountId = activeCodeReqPROTO.AccountId;
        if (accountId != 0)
        {
            // Variant可变的
            ProtoWriter.WriteFieldHeader(1, WireType.Variant, protoWrite);
            ProtoWriter.WriteInt64(accountId, protoWrite);
        }

        string activeCode = activeCodeReqPROTO.ActiveCode;
        if (!string.IsNullOrEmpty(activeCode))
        {
            ProtoWriter.WriteFieldHeader(2, WireType.String, protoWrite);
            ProtoWriter.WriteString(activeCode, protoWrite);
        }
        ProtoWriter.AppendExtensionData(activeCodeReqPROTO, protoWrite);
    }

    private static ActiveCodeReqPROTO Read(ActiveCodeReqPROTO activeCodeReqPROTO, ProtoReader protoReader)
    {
        int fieldNum;
        while ((fieldNum = protoReader.ReadFieldHeader()) > 0)
        {
            if (fieldNum != 1)
            {
                if (fieldNum != 2)
                {
                    if (activeCodeReqPROTO == null)
                    {
                        ActiveCodeReqPROTO activeCodeReq = new ActiveCodeReqPROTO();
                        ProtoReader.NoteObject(activeCodeReq, protoReader);
                        activeCodeReqPROTO = activeCodeReq;
                    }

                    protoReader.AppendExtensionData(activeCodeReqPROTO);
                }
                else
                {
                    if (activeCodeReqPROTO == null)
                    {
                        ActiveCodeReqPROTO activeCodeReq = new ActiveCodeReqPROTO();
                        ProtoReader.NoteObject(activeCodeReq, protoReader);
                        activeCodeReqPROTO = activeCodeReq;
                    }

                    string activeCode = protoReader.ReadString();
                    if (activeCode != null)
                    {
                        activeCodeReqPROTO.ActiveCode = activeCode;
                    }
                }
            }
            else
            {
                if (activeCodeReqPROTO == null)
                {
                    ActiveCodeReqPROTO activeCodeReq = new ActiveCodeReqPROTO();
                    ProtoReader.NoteObject(activeCodeReq, protoReader);
                    activeCodeReqPROTO = activeCodeReq;
                }

                long accountId = protoReader.ReadInt64();
                activeCodeReqPROTO.AccountId = accountId;
            }
        }

        if (activeCodeReqPROTO == null)
        {
            ActiveCodeReqPROTO activeCodeReq = new ActiveCodeReqPROTO();
            ProtoReader.NoteObject(activeCodeReq, protoReader);
            activeCodeReqPROTO = activeCodeReq;
        }

        return activeCodeReqPROTO;
    }

    protected override void Serialize(int key, object value, ProtoWriter dest)
    {
        switch (key)
        {
            case 0:
                Write((ActiveCodeReqPROTO)value, dest);
                break;
            default:
                break;
        }
    }

    protected override object Deserialize(int key, object value, ProtoReader source)
    {
        switch (key)
        {
            case 0:
                return Read((ActiveCodeReqPROTO)value, source);
        }

        return null;
    }

    protected override int GetKeyImpl(Type type)
    {
        int result;
        if (type2KeyDic.TryGetValue(type, out result))
        {
            return result;
        }

        return -1;
    }

}