using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Saitynas_API.Services.AgoraIO.Media;

public static class SignalingToken
{
    public static string GetToken(string appId, string certificate, string account, int expiredTsInSeconds)
    {
        var digestString = new StringBuilder()
            .Append(account).Append(appId)
            .Append(certificate).Append(expiredTsInSeconds);
        
        var md5 = MD5.Create();
        byte[] output = md5.ComputeHash(Encoding.UTF8.GetBytes(digestString.ToString()));
        string token = Hexlify(output);
        
        string tokenString = new StringBuilder()
            .Append('1').Append(':').Append(appId).Append(':')
            .Append(expiredTsInSeconds).Append(':').Append(token).ToString();
        
        return tokenString;
    }

    private static string Hexlify(IReadOnlyList<byte> data)
    {
        char[] digitsLower = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f'};
        int l = data.Count;
        char[] outCharArray = new char[l << 1];
        
        // two characters form the hex value.
        for (int i = 0, j = 0; i < l; i++)
        {
            outCharArray[j++] = digitsLower[(uint) (0xF0 & data[i]) >> 4];
            outCharArray[j++] = digitsLower[0x0F & data[i]];
        }

        return new string(outCharArray);
    }
}
