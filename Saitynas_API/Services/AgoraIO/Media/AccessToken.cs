using System.IO;
using Force.Crc32;
using Saitynas_API.Services.AgoraIO.Common;
using Saitynas_API.Services.AgoraIO.Extensions;
using Saitynas_API.Services.AgoraIO.Utils;

namespace Saitynas_API.Services.AgoraIO.Media;

public class AccessToken
{
    private readonly string _appCertificate;
    private readonly string _appId;
    private readonly string _channelName;
    private uint _crcChannelName;
    private uint _crcUid;
    private byte[] _messageRawContent;
    private uint _salt;
    private byte[] _signature;
    private uint _ts;
    private readonly string _uid;
    public readonly PrivilegeMessage Message = new();

    public AccessToken(string appId, string appCertificate, string channelName, string uid)
    {
        _appId = appId;
        _appCertificate = appCertificate;
        _channelName = channelName;
        _uid = uid;
    }

    public AccessToken(string appId, string appCertificate, string channelName, string uid, uint ts, uint salt)
    {
        _appId = appId;
        _appCertificate = appCertificate;
        _channelName = channelName;
        _uid = uid;
        _ts = ts;
        _salt = salt;
    }

    public void AddPrivilege(Privileges kJoinChannel, uint expiredTs)
    {
        Message.Messages.Add((ushort) kJoinChannel, expiredTs);
    }

    public string Build()
    {
        _messageRawContent = Utils.Utils.Pack(Message);
        _signature = GenerateSignature(_appCertificate, _appId, _channelName, _uid, _messageRawContent);

        _crcChannelName = Crc32Algorithm.Compute(_channelName.GetByteArray());
        _crcUid = Crc32Algorithm.Compute(_uid.GetByteArray());

        var packContent = new PackContent(_signature, _crcChannelName, _crcUid, _messageRawContent);
        byte[] content = Utils.Utils.Pack(packContent);
        return GetVersion() + _appId + Utils.Utils.Base64Encode(content);
    }

    private static string GetVersion()
    {
        return "006";
    }

    private static byte[] GenerateSignature(
        string appCertificate,
        string appID,
        string channelName,
        string uid, byte[] message
    )
    {
        using var ms = new MemoryStream();
        using var writer = new BinaryWriter(ms);

        writer.Write(appID.GetByteArray());
        writer.Write(channelName.GetByteArray());
        writer.Write(uid.GetByteArray());
        writer.Write(message);
        writer.Flush();

        byte[] sign = DynamicKeyUtil.EncodeHmac(appCertificate, ms.ToArray(), "SHA256");
        return sign;
    }
}
