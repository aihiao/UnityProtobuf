using System.ComponentModel;
using ProtoBuf;

// 类前加上ProtoContract Attrbuit
[ProtoContract]
public class ActiveCodeReqPROTO : IExtensible
{
    private long accountId;
    private string activeCode;
    private IExtension extensionObject;

    // 成员加上ProtoMember Attribute即可, 其中ProtoMember需要一个大于0的int类型的值。原则上这个int类型没有大小限制, 但建议从1开始, 这是一个良好的习惯, 另外这个参数必需是这个类成员的唯一标识, 不可重复。
    [ProtoMember(1), DefaultValue(0L)]
    public long AccountId
    {
        get
        {
            return accountId;
        }
        set
        {
            accountId = value;
        }
    }

    [ProtoMember(2), DefaultValue("")]
    public string ActiveCode
    {
        get
        {
            return activeCode;
        }
        set
        {
            activeCode = value;
        }
    }

    IExtension IExtensible.GetExtensionObject(bool createIfMissing)
    {
        return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
    }

}

