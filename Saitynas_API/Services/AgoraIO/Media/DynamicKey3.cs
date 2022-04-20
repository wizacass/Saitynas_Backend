using System.IO;
using Saitynas_API.Services.AgoraIO.Extensions;
using Saitynas_API.Services.AgoraIO.Utils;

namespace Saitynas_API.Services.AgoraIO.Media;

public static class DynamicKey3
{
    public static string Generate(
        string appID, string appCertificate,
        string channelName, int unixTs,
        int randomInt, long uid, int expiredTs
    )
    {
        const string version = "003";
        string unixTsStr = ("0000000000" + unixTs)[unixTs.ToString().Length..];
        string randomIntStr = ("00000000" + randomInt.ToString("x4"))[randomInt.ToString("x4").Length..];
        uid &= 0xFFFFFFFFL;
        string uidStr = ("0000000000" + uid)[uid.ToString().Length..];
        string expiredTsStr = ("0000000000" + expiredTs)[expiredTs.ToString().Length..];
        string signature = GenerateSignature3(appID, appCertificate, channelName, unixTsStr, randomIntStr, uidStr,
            expiredTsStr);

        return $"{version}{signature}{appID}{unixTsStr}{randomIntStr}{uidStr}{expiredTsStr}";
    }

    public static string GenerateSignature3(
        string appID, string appCertificate,
        string channelName, string unixTsStr,
        string randomIntStr, string uidStr, string expiredTsStr
    )
    {
        using var ms = new MemoryStream();
        using var writer = new BinaryWriter(ms);

        writer.Write(appID.GetByteArray());
        writer.Write(unixTsStr.GetByteArray());
        writer.Write(randomIntStr.GetByteArray());
        writer.Write(channelName.GetByteArray());
        writer.Write(uidStr.GetByteArray());
        writer.Write(expiredTsStr.GetByteArray());
        writer.Flush();

        byte[] sign = DynamicKeyUtil.EncodeHmac(appCertificate, ms.ToArray());
        return DynamicKeyUtil.BytesToHex(sign);
    }
}
