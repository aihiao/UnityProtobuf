using UnityEngine;
using System.IO;

public class GameMain : MonoBehaviour
{
#if UNITY_IPHONE || UNITY_EDITOR
    void Awake()
    {
        MemoryStream ms = new MemoryStream();

        ActiveCodeReqPROTO ace = new ActiveCodeReqPROTO();
        ace.AccountId = 10000000000000001;
        ace.ActiveCode = "lyw";

        ProtobufSerializer serializer = new ProtobufSerializer(new TypeModelProtobufSerializer());
        serializer.Serialize(ms, ace);

        MemoryStream memoryStream = new MemoryStream(ms.ToArray());
        ActiveCodeReqPROTO activeCodeReq = (ActiveCodeReqPROTO)serializer.Deserialize(memoryStream, typeof(ActiveCodeReqPROTO));
        Debug.LogError(string.Format("AccountId = {0}, ActiveCode = {1}", activeCodeReq.AccountId, activeCodeReq.ActiveCode));
    }
#endif

#if UNITY_ANDROID || UNITY_EDITOR
    void Start()
    {
        MemoryStream ms = new MemoryStream();

        ActiveCodeReqPROTO ace = new ActiveCodeReqPROTO();
        ace.AccountId = 10000000000000002;
        ace.ActiveCode = "lyw123456";

        MetaDataProtobufSerializer mdps = new MetaDataProtobufSerializer();
        mdps.Serialize<ActiveCodeReqPROTO>(ms, ace);
        //mdps.Serialize(ms, ace);

        // 这里MemoryStream传入一个buffer, MemoryStream的Capacity大小是buffer的大小。 
        MemoryStream memoryStream = new MemoryStream(ms.ToArray());

        // 这里反序列化传入的Stream的Capacity大小, 必须是buffer的大小。 
        //ActiveCodeReq activeCodeReq = (ActiveCodeReqPROTO)mdps.Deserialize(memoryStream, typeof(ActiveCodeReqPROTO));
        ActiveCodeReqPROTO activeCodeReq = mdps.Deserialize<ActiveCodeReqPROTO>(memoryStream);

        // 这里打印Error级别的日志, 是为了在手机上可以更好的查看日志。
        Debug.LogError(string.Format("AccountId = {0}, ActiveCode = {1}", activeCodeReq.AccountId, activeCodeReq.ActiveCode));
    }
#endif

}
