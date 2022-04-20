using System.Collections.Generic;

namespace Saitynas_API.Services.AgoraIO.Common;

public class ByteBuf
{
    private readonly ByteBuffer _buffer = new();
    public ByteBuf() { }

    public ByteBuf(byte[] source)
    {
        _buffer.PushByteArray(source);
    }

    public byte[] AsBytes()
    {
        return _buffer.ToByteArray();
    }

    public ByteBuf Put(ushort v)
    {
        _buffer.PushUInt16(v);
        return this;
    }

    public ByteBuf Put(uint v)
    {
        _buffer.PushLong(v);
        return this;
    }

    public ByteBuf Put(byte[] v)
    {
        Put((ushort) v.Length);
        _buffer.PushByteArray(v);
        return this;
    }

    public ByteBuf PutIntMap(Dictionary<ushort, uint> extra)
    {
        Put((ushort) extra.Count);

        foreach (var item in extra)
        {
            Put(item.Key);
            Put(item.Value);
        }

        return this;
    }

    public ushort ReadShort()
    {
        return _buffer.PopUInt16();
    }

    public uint ReadInt()
    {
        return _buffer.PopUInt();
    }

    public byte[] ReadBytes()
    {
        ushort length = ReadShort();

        return _buffer.PopByteArray(length);
    }

    public Dictionary<ushort, uint> ReadIntMap()
    {
        var map = new Dictionary<ushort, uint>();

        ushort length = ReadShort();

        for (short i = 0; i < length; ++i)
        {
            ushort k = ReadShort();
            uint v = ReadInt();
            map.Add(k, v);
        }

        return map;
    }
}
