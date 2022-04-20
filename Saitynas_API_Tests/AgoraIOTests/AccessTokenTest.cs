using NUnit.Framework;
using Saitynas_API.Services.AgoraIO.Media;

namespace Saitynas_API_Tests.AgoraIOTests;

[TestFixture]
public class AccessTokenTest
{
    private const string AppId = "970CA35de60c44645bbae8a215061b33";
    private const string AppCertificate = "5CFd2fd1755d40ecb72977518be15d3b";
    private const string ChannelName = "7d72365eb983485397e3e3f9d460bdda";
    private const string Uid = "2882341273";
    private const uint Ts = 1111111;
    private const uint Salt = 1;
    private const uint ExpiredTs = 1446455471;

    [Test]
    public void TestGenerateDynamicKey()
    {
        const string expected = "006970CA35de60c44645bbae8a215061b33IACV0fZUBw+72cVoL9eyGGh3Q6Poi8bgjwVLnyKSJyOXR7dIfRBXoFHlEAABAAAAR/QQAAEAAQCvKDdW";
        var token = new AgoraAccessToken(AppId, AppCertificate, ChannelName, Uid, Ts, Salt)
        {
            Message =
            {
                Ts = Ts,
                Salt = Salt
            }
        };
        token.AddPrivilege(Privileges.KJoinChannel, ExpiredTs);

        string result = token.Build();
        
        Assert.AreEqual(expected, result);
    }
}
