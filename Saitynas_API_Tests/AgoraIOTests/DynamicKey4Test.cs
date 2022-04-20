using NUnit.Framework;
using Saitynas_API.Services.AgoraIO.Media;

namespace Saitynas_API_Tests.AgoraIOTests;

[TestFixture]
public class DynamicKey4Test
{
    private const string AppID = "b9b595bcfd72479d894abe7a8cf0c37e";
    private const string AppCertificate = "3c481e60aac14b06a434a20e70de7c51";

    [Test]
    public void TestGeneratePublicSharingKey()
    {
        const string channel = "7d72365eb983485397e3e3f9d460bdda";
        const int ts = 1446455472;
        const int r = 58964981;
        string uid = 2882341273L.ToString();
        const int expiredTs = 1446455471;

        const string expected = "0047f60ef0a65df9aad718eb6c4790a3e52a468313bb9b595bcfd72479d894abe7a8cf0c37e14464554720383bbf51446455471";
        string result = DynamicKey4.GeneratePublicSharingKey(AppID, AppCertificate, channel, ts, r, uid, expiredTs);
        
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestGenerateRecordingKey()
    {
        const string channel = "7d72365eb983485397e3e3f9d460bdda";
        const int ts = 1446455472;
        const int r = 58964981;
        string uid = 2882341273L.ToString();
        const int expiredTs = 1446455471;

        const string expected = "004d77264727a82bbbeac5c65cc125cda2992ad2106b9b595bcfd72479d894abe7a8cf0c37e14464554720383bbf51446455471";
        string result = DynamicKey4.GenerateRecordingKey(AppID, AppCertificate, channel, ts, r, uid, expiredTs);
        
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestGenerateMediaChannelKey()
    {
        const string channel = "7d72365eb983485397e3e3f9d460bdda";
        const int ts = 1446455472;
        const int r = 58964981;
        string uid = 2882341273L.ToString();
        const int expiredTs = 1446455471;

        const string expected = "004d4bcd4be9daabe83581b93336313a97c696b0b0bb9b595bcfd72479d894abe7a8cf0c37e14464554720383bbf51446455471";
        string result = DynamicKey4.GenerateMediaChannelKey(AppID, AppCertificate, channel, ts, r, uid, expiredTs);
        
        Assert.AreEqual(expected, result);
    }
}
