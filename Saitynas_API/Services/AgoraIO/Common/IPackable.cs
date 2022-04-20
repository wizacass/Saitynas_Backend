namespace Saitynas_API.Services.AgoraIO.Common;

public interface IPackable
{
    ByteBuf Marshal(ByteBuf outBuf);
}
