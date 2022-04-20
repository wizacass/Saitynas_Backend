using NUnit.Framework;
using Saitynas_API.Services.AgoraIO.Media;

namespace Saitynas_API_Tests.AgoraIOTests;

[TestFixture]
public class DynamicKey3Test
{
    private const string AppID = "970ca35de60c44645bbae8a215061b33";
    private const string AppCertificate = "5cfd2fd1755d40ecb72977518be15d3b";
    private const string Channel = "7d72365eb983485397e3e3f9d460bdda";
    private const int Ts = 1446455472;
    private const int R = 58964981;
    private long _uid = 2882341273L;
    private const int ExpiredTs = 1446455471;

    [Test]
    public void TestGenerate()
    {
        string result = DynamicKey3.Generate(AppID, AppCertificate, Channel, Ts, R, _uid, ExpiredTs);
        const string expected = "0037666966591a93ee5a3f712e22633f31f0cbc8f13970ca35de60c44645bbae8a215061b3314464554720383bbf528823412731446455471";
        
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void Test2()
    {
        const string expected = "7666966591a93ee5a3f712e22633f31f0cbc8f13";
        string unixTsStr = ("0000000000" + Ts)[Ts.ToString().Length..];
        string randomIntStr = ("00000000" + R.ToString("x4"))[R.ToString("x4").Length..];
        _uid &= 0xFFFFFFFFL;
        string uidStr = ("0000000000" + _uid)[_uid.ToString().Length..];
        string expiredTsStr = ("0000000000" + ExpiredTs)[ExpiredTs.ToString().Length..];
        string result = DynamicKey3.GenerateSignature3(AppID, AppCertificate, Channel, unixTsStr, randomIntStr, uidStr,
            expiredTsStr);
        
        Assert.AreEqual(expected, result);
    }
}
