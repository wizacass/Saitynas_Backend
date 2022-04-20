using System.IO;
using Saitynas_API.Services.AgoraIO.Extensions;
using Saitynas_API.Services.AgoraIO.Utils;

namespace Saitynas_API.Services.AgoraIO.Media;

public static class DynamicKey4
{
    private const string PublicSharingService = "APSS";
    private const string RecordingService = "ARS";
    private const string MediaChannelService = "ACS";

    /**
         * Generate Dynamic Key for Public Sharing Service
         * @param appID App is assigned by Agora
         * @param appCertificate App Certificate assigned by Agora
         * @param channelName name of channel to join, limited to 64 bytes and should be printable ASCII characters
         * @param unixTs unix timestamp in seconds when generating the Dynamic Key
         * @param randomInt salt for generating dynamic key
         * @param uid user id, range from 0 - max uint32
         * @param expiredTs should be 0
         * @return String representation of dynamic key
         * @throws Exception if any error occurs
         */
    public static string GeneratePublicSharingKey(
        string appID, string appCertificate,
        string channelName, int unixTs,
        int randomInt, string uid, int expiredTs
    )
    {
        return DoGenerate(
            appID, appCertificate,
            channelName, unixTs, randomInt, uid, expiredTs,
            PublicSharingService
        );
    }

    /**
         * Generate Dynamic Key for recording service
         * @param appID Vendor key assigned by Agora
         * @param appCertificate Sign key assigned by Agora
         * @param channelName name of channel to join, limited to 64 bytes and should be printable ASCII characters
         * @param unixTs unix timestamp in seconds when generating the Dynamic Key
         * @param randomInt salt for generating dynamic key
         * @param uid user id, range from 0 - max uint32
         * @param expiredTs should be 0
         * @return String representation of dynamic key
         * @throws Exception if any error occurs
         */
    public static string GenerateRecordingKey(
        string appID, string appCertificate,
        string channelName, int unixTs,
        int randomInt, string uid, int expiredTs
    )
    {
        return DoGenerate(appID, appCertificate, channelName, unixTs, randomInt, uid, expiredTs, RecordingService);
    }

    /**
         * Generate Dynamic Key for media channel service
         * @param appID Vendor key assigned by Agora
         * @param appCertificate Sign key assigned by Agora
         * @param channelName name of channel to join, limited to 64 bytes and should be printable ASCII characters
         * @param unixTs unix timestamp in seconds when generating the Dynamic Key
         * @param randomInt salt for generating dynamic key
         * @param uid user id, range from 0 - max uint32
         * @param expiredTs service expiring timestamp. After this timestamp, user will not be able to stay in the channel.
         * @return String representation of dynamic key
         * @throws Exception if any error occurs
         */
    public static string GenerateMediaChannelKey(
        string appID, string appCertificate,
        string channelName, int unixTs,
        int randomInt, string uid, int expiredTs
    )
    {
        return DoGenerate(appID, appCertificate, channelName, unixTs, randomInt, uid, expiredTs, MediaChannelService);
    }

    private static string DoGenerate(
        string appID, string appCertificate, string channelName,
        int unixTs, int randomInt, string uid, int expiredTs, string serviceType
    )
    {
        const string version = "004";
        string unixTsStr = ("0000000000" + unixTs)[unixTs.ToString().Length..];

        string randomIntStr = ("00000000" + randomInt.ToString("x4"))[randomInt.ToString("x4").Length..];
        string expiredTsStr = ("0000000000" + expiredTs)[expiredTs.ToString().Length..];

        string signature = GenerateSignature4(
            appID, appCertificate, channelName,
            unixTsStr, randomIntStr, uid,
            expiredTsStr, serviceType
        );

        return $"{version}{signature}{appID}{unixTsStr}{randomIntStr}{expiredTsStr}";
    }

    private static string GenerateSignature4(
        string appID, string appCertificate,
        string channelName, string unixTsStr,
        string randomIntStr, string uidStr,
        string expiredTsStr, string serviceType
    )
    {
        using var ms = new MemoryStream();
        using var writer = new BinaryWriter(ms);

        writer.Write(serviceType.GetBytes());
        writer.Write(appID.GetBytes());
        writer.Write(unixTsStr.GetBytes());
        writer.Write(randomIntStr.GetBytes());
        writer.Write(channelName.GetBytes());
        writer.Write(uidStr.GetBytes());
        writer.Write(expiredTsStr.GetBytes());

        byte[] sign = DynamicKeyUtil.EncodeHmac(appCertificate, ms.ToArray());

        return DynamicKeyUtil.BytesToHex(sign);
    }
}
