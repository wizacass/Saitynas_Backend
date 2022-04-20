namespace Saitynas_API.Services.AgoraIO.Common;

public class PackContent : IPackable
{
    private uint _crcChannelName;
    private uint _crcUid;
    private byte[] _rawMessage;
    private byte[] _signature;

    public PackContent(byte[] signature, uint crcChannelName, uint crcUid, byte[] rawMessage)
    {
        _signature = signature;
        _crcChannelName = crcChannelName;
        _crcUid = crcUid;
        _rawMessage = rawMessage;
    }
    
    public ByteBuf Marshal(ByteBuf outBuf)
    {
        return outBuf.Put(_signature).Put(_crcChannelName).Put(_crcUid).Put(_rawMessage);
    }

    public void Unmarshal(ByteBuf inBuf)
    {
        _signature = inBuf.ReadBytes();
        _crcChannelName = inBuf.ReadInt();
        _crcUid = inBuf.ReadInt();
        _rawMessage = inBuf.ReadBytes();
    }
}
