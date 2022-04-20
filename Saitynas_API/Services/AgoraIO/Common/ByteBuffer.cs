using System;

namespace Saitynas_API.Services.AgoraIO.Common;

public class ByteBuffer
{
    private const int MaxLength = 1024;

    private readonly byte[] _tempByteArray = new byte[MaxLength];

    private byte[] _returnArray;

    private int Length { get; set; }

    private int Position { get; set; }

    public ByteBuffer()
    {
        Initialize();
    }

    public ByteBuffer(byte[] bytes)
    {
        Initialize();
        PushByteArray(bytes);
    }

    public byte[] ToByteArray()
    {
        _returnArray = new byte[Length];
        Array.Copy(_tempByteArray, 0, _returnArray, 0, Length);

        return _returnArray;
    }

    public void Initialize()
    {
        _tempByteArray.Initialize();
        Length = 0;
        Position = 0;
    }

    public void PushByte(byte by)
    {
        _tempByteArray[Length++] = by;
    }

    public void PushByteArray(byte[] byteArray)
    {
        byteArray.CopyTo(_tempByteArray, Length);
        Length += byteArray.Length;
    }

    public void PushUInt16(ushort num)
    {
        _tempByteArray[Length++] = (byte) (num & 0x00ff & 0xff);
        _tempByteArray[Length++] = (byte) (((num & 0xff00) >> 8) & 0xff);
    }

    public void PushInt(uint num)
    {
        _tempByteArray[Length++] = (byte) (num & 0x000000ff & 0xff);
        _tempByteArray[Length++] = (byte) (((num & 0x0000ff00) >> 8) & 0xff);
        _tempByteArray[Length++] = (byte) (((num & 0x00ff0000) >> 16) & 0xff);
        _tempByteArray[Length++] = (byte) (((num & 0xff000000) >> 24) & 0xff);
    }

    public void PushLong(long num)
    {
        _tempByteArray[Length++] = (byte) (num & 0x000000ff & 0xff);
        _tempByteArray[Length++] = (byte) (((num & 0x0000ff00) >> 8) & 0xff);
        _tempByteArray[Length++] = (byte) (((num & 0x00ff0000) >> 16) & 0xff);
        _tempByteArray[Length++] = (byte) (((num & 0xff000000) >> 24) & 0xff);
    }

    public byte PopByte()
    {
        byte ret = _tempByteArray[Position++];
        return ret;
    }

    public ushort PopUInt16()
    {
        if (Position + 1 >= Length) return 0;

        ushort ret = (ushort) (_tempByteArray[Position] | (_tempByteArray[Position + 1] << 8));
        Position += 2;
        return ret;
    }

    public uint PopUInt()
    {
        if (Position + 3 >= Length)
            return 0;
        uint ret = (uint) (_tempByteArray[Position] | (_tempByteArray[Position + 1] << 8) |
                           (_tempByteArray[Position + 2] << 16) | (_tempByteArray[Position + 3] << 24));
        Position += 4;
        return ret;
    }

    public long PopLong()
    {
        if (Position + 3 >= Length)
            return 0;
        long ret = (_tempByteArray[Position] << 24) | (_tempByteArray[Position + 1] << 16) |
                   (_tempByteArray[Position + 2] << 8) | _tempByteArray[Position + 3];
        Position += 4;
        return ret;
    }

    public byte[] PopByteArray(int length)
    {
        if (Position + length > Length) return Array.Empty<byte>();

        byte[] ret = new byte[length];
        Array.Copy(_tempByteArray, Position, ret, 0, length);
        Position += length;

        return ret;
    }

    public byte[] PopByteArray2(int length)
    {
        if (Position <= length) return Array.Empty<byte>();

        byte[] ret = new byte[length];
        Array.Copy(_tempByteArray, Position - length, ret, 0, length);
        Position -= length;

        return ret;
    }
}
