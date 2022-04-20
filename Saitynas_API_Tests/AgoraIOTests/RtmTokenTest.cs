using NUnit.Framework;
using Saitynas_API.Services.AgoraIO.Media;

namespace Saitynas_API_Tests.AgoraIOTests;

[TestFixture]
public class RtmTokenTest
{
    private const string AppId = "970CA35de60c44645bbae8a215061b33";
    private const string AppCertificate = "5CFd2fd1755d40ecb72977518be15d3b";
    private const string Uid = "2882341273";
    private const uint ExpiredTs = 0;
    private const uint Ts = 1111111;
    private const uint Salt = 1;

    [Test]
    public void TestRtmToken()
    {
        const string expected = "006970CA35de60c44645bbae8a215061b33IAB/luJx7c3zCxag46cPAwofHXnoslvPjP1rvRJIgxemHFegUeUAAAAAEAABAAAAR/QQAAEA6AMAAAAA";
        var token = new AgoraAccessToken(AppId, AppCertificate, Uid, "", Ts, Salt)
        {
            Message =
            {
                Ts = Ts,
                Salt = Salt
            }
        };
        token.AddPrivilege(Privileges.KRtmLogin, ExpiredTs);
        
        string result = token.Build();
        
        Assert.AreEqual(expected, result);
    }
}
