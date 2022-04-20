using System;
using Saitynas_API.Services.AgoraIO.Common;

namespace Saitynas_API.Services.AgoraIO.Utils;

public static class Utils
{
    public static int GetTimestamp()
    {
        var t = DateTime.Now - new DateTime(1970, 1, 1);
        
        return (int) t.TotalSeconds;
    }

    public static int RandomInt()
    {
        return new Random().Next();
    }

    public static byte[] Pack(PrivilegeMessage packableEx)
    {
        var buffer = new ByteBuf();
        packableEx.Marshal(buffer);
        
        return buffer.AsBytes();
    }

    public static byte[] Pack(IPackable packableEx)
    {
        var buffer = new ByteBuf();
        packableEx.Marshal(buffer);
        
        return buffer.AsBytes();
    }

    public static string Base64Encode(byte[] data)
    {
        return Convert.ToBase64String(data);
    }
}
