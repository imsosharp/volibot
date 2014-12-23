#region

using System.Collections.Generic;

#endregion

namespace LoLLauncher
{
    public class Packet
    {
        private byte[] _dataBuffer;
        private int _dataPos;
        private int _dataSize;
        private int _packetType;
        private readonly List<byte> _rawPacketBytes;

        public Packet()
        {
            _rawPacketBytes = new List<byte>();
        }

        public void SetSize(int size)
        {
            _dataSize = size;
            _dataBuffer = new byte[_dataSize];
        }

        public void SetType(int type)
        {
            _packetType = type;
        }

        public void Add(byte b)
        {
            _dataBuffer[_dataPos++] = b;
        }

        public bool IsComplete()
        {
            return (_dataPos == _dataSize);
        }

        public int GetSize()
        {
            return _dataSize;
        }

        public int GetPacketType()
        {
            return _packetType;
        }

        public byte[] GetData()
        {
            return _dataBuffer;
        }

        public void AddToRaw(byte b)
        {
            _rawPacketBytes.Add(b);
        }

        public void AddToRaw(byte[] b)
        {
            _rawPacketBytes.AddRange(b);
        }

        public byte[] GetRawData()
        {
            return _rawPacketBytes.ToArray();
        }
    }
}