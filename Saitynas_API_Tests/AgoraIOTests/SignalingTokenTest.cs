using NUnit.Framework;
using Saitynas_API.Services.AgoraIO.Media;

namespace Saitynas_API_Tests.AgoraIOTests;

[TestFixture]
public class SignalingTokenTest
{
    [Test]
    public void testSignalingToken()
    {
        const string appId = "970ca35de60c44645bbae8a215061b33";
        const string certificate = "5cfd2fd1755d40ecb72977518be15d3b";
        const string account = "TestAccount";
        const int expiredTsInSeconds = 1446455471;
        const string expected = "1:970ca35de60c44645bbae8a215061b33:1446455471:4815d52c4fd440bac35b981c12798774";
        string result = SignalingToken.GetToken(appId, certificate, account, expiredTsInSeconds);
        
        Assert.AreEqual(expected, result);
    }
}
