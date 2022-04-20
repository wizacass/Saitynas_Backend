using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Saitynas_API.Services.AgoraIO.Utils;

public static class DynamicKeyUtil
{
    public static byte[] EncodeHmac(string key, byte[] message, string alg = "SHA1")
    {
        return EncodeHMAC(Encoding.UTF8.GetBytes(key), message, alg);
    }

    private static byte[] EncodeHMAC(byte[] keyBytes, byte[] textBytes, string alg = "SHA1") 
    {
        KeyedHashAlgorithm hash = alg switch
        {
            "MD5" => new HMACMD5(keyBytes),
            "SHA256" => new HMACSHA256(keyBytes),
            "SHA384" => new HMACSHA384(keyBytes),
            "SHA512" => new HMACSHA512(keyBytes),
            _ => new HMACSHA1(keyBytes)
        };

        byte[] hashBytes = hash.ComputeHash(textBytes);

        return hashBytes;
    }

    public static string BytesToHex(IEnumerable<byte> inData)
    {
        var builder = new StringBuilder();
        foreach (byte b in inData) builder.Append(b.ToString("X2"));
        return builder.ToString().ToLower();
    }
}
