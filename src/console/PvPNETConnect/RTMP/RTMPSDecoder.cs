#region

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;

#endregion

namespace LoLLauncher
{
    public class RtmpsDecoder
    {
        // Stores the data to be consumed while decoding
        private byte[] _dataBuffer;
        private int _dataPos;
        private readonly List<ClassDefinition> _classDefinitions = new List<ClassDefinition>();
        private readonly List<object> _objectReferences = new List<object>();
        // Lists of references and class definitions seen so far
        private readonly List<string> _stringReferences = new List<string>();

        private void Reset()
        {
            _stringReferences.Clear();
            _objectReferences.Clear();
            _classDefinitions.Clear();
        }

        public TypedObject DecodeConnect(byte[] data)
        {
            Reset();

            _dataBuffer = data;
            _dataPos = 0;

            var result = new TypedObject("Invoke");
            result.Add("result", DecodeAmf0());
            result.Add("invokeId", DecodeAmf0());
            result.Add("serviceCall", DecodeAmf0());
            result.Add("data", DecodeAmf0());
            if (_dataPos != _dataBuffer.Length)
            {
                for (var i = _dataPos; i < data.Length; i++)
                {
                    if (ReadByte() != '\0')
                        throw new Exception("There is other data in the buffer!");
                }
            }
            if (_dataPos != _dataBuffer.Length)
                throw new Exception("Did not consume entire buffer: " + _dataPos + " of " + _dataBuffer.Length);

            return result;
        }

        public TypedObject DecodeInvoke(byte[] data)
        {
            Reset();

            _dataBuffer = data;
            _dataPos = 0;

            var result = new TypedObject("Invoke");
            if (_dataBuffer[0] == 0x00)
            {
                _dataPos++;
                result.Add("version", 0x00);
            }
            result.Add("result", DecodeAmf0());
            result.Add("invokeId", DecodeAmf0());
            result.Add("serviceCall", DecodeAmf0());
            result.Add("data", DecodeAmf0());


            if (_dataPos != _dataBuffer.Length)
                throw new Exception("Did not consume entire buffer: " + _dataPos + " of " + _dataBuffer.Length);

            return result;
        }

        public object Decode(byte[] data)
        {
            _dataBuffer = data;
            _dataPos = 0;

            var result = Decode();

            if (_dataPos != _dataBuffer.Length)
                throw new Exception("Did not consume entire buffer: " + _dataPos + " of " + _dataBuffer.Length);

            return result;
        }

        private object Decode()
        {
            var type = ReadByte();
            switch (type)
            {
                case 0x00:
                    throw new Exception("Undefined data type");

                case 0x01:
                    return null;

                case 0x02:
                    return false;

                case 0x03:
                    return true;

                case 0x04:
                    return ReadInt();

                case 0x05:
                    return ReadDouble();

                case 0x06:
                    return ReadString();

                case 0x07:
                    return ReadXml();

                case 0x08:
                    return ReadDate();

                case 0x09:
                    return ReadArray();

                case 0x0A:
                    return ReadObject();

                case 0x0B:
                    return ReadXmlString();

                case 0x0C:
                    return ReadByteArray();
            }

            throw new Exception("Unexpected AMF3 data type: " + type);
        }

        private byte ReadByte()
        {
            var ret = _dataBuffer[_dataPos];
            _dataPos++;
            return ret;
        }

        private int ReadByteAsInt()
        {
            int ret = ReadByte();
            if (ret < 0)
                ret += 256;
            return ret;
        }

        private byte[] ReadBytes(int length)
        {
            var ret = new byte[length];
            for (var i = 0; i < length; i++)
            {
                ret[i] = _dataBuffer[_dataPos];
                _dataPos++;
            }
            return ret;
        }

        private int ReadInt()
        {
            var ret = ReadByteAsInt();
            int tmp;

            if (ret < 128)
            {
                return ret;
            }
            ret = (ret & 0x7f) << 7;
            tmp = ReadByteAsInt();
            if (tmp < 128)
            {
                ret = ret | tmp;
            }
            else
            {
                ret = (ret | tmp & 0x7f) << 7;
                tmp = ReadByteAsInt();
                if (tmp < 128)
                {
                    ret = ret | tmp;
                }
                else
                {
                    ret = (ret | tmp & 0x7f) << 8;
                    tmp = ReadByteAsInt();
                    ret = ret | tmp;
                }
            }

            // Sign extend
            var mask = 1 << 28;
            var r = -(ret & mask) | ret;
            return r;
        }

        private double ReadDouble()
        {
            long value = 0;
            for (var i = 0; i < 8; i++)
                value = (value << 8) + ReadByteAsInt();

            return BitConverter.Int64BitsToDouble(value);
        }

        private string ReadString()
        {
            var handle = ReadInt();
            var inline = ((handle & 1) != 0);
            handle = handle >> 1;

            if (inline)
            {
                if (handle == 0)
                    return "";

                var data = ReadBytes(handle);

                string str;
                try
                {
                    var enc = new UTF8Encoding();
                    str = enc.GetString(data);
                }
                catch (Exception e)
                {
                    throw new Exception("Error parsing AMF3 string from " + data + '\n' + e.Message);
                }

                _stringReferences.Add(str);

                return str;
            }
            return _stringReferences[handle];
        }

        private string ReadXml()
        {
            throw new NotImplementedException("Reading of XML is not implemented");
        }

        private DateTime ReadDate()
        {
            var handle = ReadInt();
            var inline = ((handle & 1) != 0);
            handle = handle >> 1;

            if (inline)
            {
                var ms = (long) ReadDouble();
                var d = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                d = d.AddSeconds(ms/1000);

                _objectReferences.Add(d);

                return d;
            }
            return DateTime.MinValue;
        }

        private object[] ReadArray()
        {
            var handle = ReadInt();
            var inline = ((handle & 1) != 0);
            handle = handle >> 1;

            if (inline)
            {
                var key = ReadString();
                if (key != null && !key.Equals(""))
                    throw new NotImplementedException("Associative arrays are not supported");

                var ret = new object[handle];
                _objectReferences.Add(ret);

                for (var i = 0; i < handle; i++)
                    ret[i] = Decode();

                return ret;
            }
            return (object[]) _objectReferences[handle];
        }

        private List<object> ReadList()
        {
            var handle = ReadInt();
            var inline = ((handle & 1) != 0);
            handle = handle >> 1;

            if (inline)
            {
                var key = ReadString();
                if (key != null && !key.Equals(""))
                    throw new NotImplementedException("Associative arrays are not supported");

                var ret = new List<object>();
                _objectReferences.Add(ret);

                for (var i = 0; i < handle; i++)
                    ret.Add(Decode());

                return ret;
            }
            return (List<object>) _objectReferences[handle];
        }

        private object ReadObject()
        {
            var handle = ReadInt();
            var inline = ((handle & 1) != 0);
            handle = handle >> 1;

            if (inline)
            {
                var inlineDefine = ((handle & 1) != 0);
                handle = handle >> 1;

                ClassDefinition cd;
                if (inlineDefine)
                {
                    cd = new ClassDefinition();
                    cd.Type = ReadString();

                    cd.Externalizable = ((handle & 1) != 0);
                    handle = handle >> 1;
                    cd.Dynamic = ((handle & 1) != 0);
                    handle = handle >> 1;

                    for (var i = 0; i < handle; i++)
                        cd.Members.Add(ReadString());

                    _classDefinitions.Add(cd);
                }
                else
                {
                    cd = _classDefinitions[handle];
                }

                var ret = new TypedObject(cd.Type);

                // Need to add reference here due to circular references
                _objectReferences.Add(ret);

                if (cd.Externalizable)
                {
                    if (cd.Type.Equals("DSK"))
                        ret = ReadDsk();
                    else if (cd.Type.Equals("DSA"))
                        ret = ReadDsa();
                    else if (cd.Type.Equals("flex.messaging.io.ArrayCollection"))
                    {
                        var obj = Decode();
                        ret = TypedObject.MakeArrayCollection((object[]) obj);
                    }
                    else if (cd.Type.Equals("com.riotgames.platform.systemstate.ClientSystemStatesNotification") ||
                             cd.Type.Equals("com.riotgames.platform.broadcast.BroadcastNotification"))
                    {
                        var size = 0;
                        for (var i = 0; i < 4; i++)
                            size = size*256 + ReadByteAsInt();

                        var data = ReadBytes(size);
                        var sb = new StringBuilder();
                        for (var i = 0; i < data.Length; i++)
                            sb.Append(Convert.ToChar(data[i]));

                        var serializer = new JavaScriptSerializer();
                        ret = serializer.Deserialize<TypedObject>(sb.ToString());
                        ret.Type = cd.Type;
                    }
                    else
                    {
                        //for (int i = dataPos; i < dataBuffer.length; i++)
                        //System.out.print(String.format("%02X", dataBuffer[i]));
                        //System.out.println();
                        throw new NotImplementedException("Externalizable not handled for " + cd.Type);
                    }
                }
                else
                {
                    for (var i = 0; i < cd.Members.Count; i++)
                    {
                        var key = cd.Members[i];
                        var value = Decode();
                        ret.Add(key, value);
                    }

                    if (cd.Dynamic)
                    {
                        String key;
                        while ((key = ReadString()).Length != 0)
                        {
                            var value = Decode();
                            ret.Add(key, value);
                        }
                    }
                }

                return ret;
            }
            return _objectReferences[handle];
        }

        private string ReadXmlString()
        {
            throw new NotImplementedException("Reading of XML strings is not implemented");
        }

        private byte[] ReadByteArray()
        {
            var handle = ReadInt();
            var inline = ((handle & 1) != 0);
            handle = handle >> 1;

            if (inline)
            {
                var ret = ReadBytes(handle);
                _objectReferences.Add(ret);
                return ret;
            }
            return (byte[]) _objectReferences[handle];
        }

        private TypedObject ReadDsa()
        {
            var ret = new TypedObject("DSA");

            int flag;
            var flags = ReadFlags();
            for (var i = 0; i < flags.Count; i++)
            {
                flag = flags[i];
                var bits = 0;
                if (i == 0)
                {
                    if ((flag & 0x01) != 0)
                        ret.Add("body", Decode());
                    if ((flag & 0x02) != 0)
                        ret.Add("clientId", Decode());
                    if ((flag & 0x04) != 0)
                        ret.Add("destination", Decode());
                    if ((flag & 0x08) != 0)
                        ret.Add("headers", Decode());
                    if ((flag & 0x10) != 0)
                        ret.Add("messageId", Decode());
                    if ((flag & 0x20) != 0)
                        ret.Add("timeStamp", Decode());
                    if ((flag & 0x40) != 0)
                        ret.Add("timeToLive", Decode());
                    bits = 7;
                }
                else if (i == 1)
                {
                    if ((flag & 0x01) != 0)
                    {
                        ReadByte();
                        var temp = ReadByteArray();
                        ret.Add("clientIdBytes", temp);
                        ret.Add("clientId", ByteArrayToId(temp));
                    }
                    if ((flag & 0x02) != 0)
                    {
                        ReadByte();
                        var temp = ReadByteArray();
                        ret.Add("messageIdBytes", temp);
                        ret.Add("messageId", ByteArrayToId(temp));
                    }
                    bits = 2;
                }

                ReadRemaining(flag, bits);
            }

            flags = ReadFlags();
            for (var i = 0; i < flags.Count; i++)
            {
                flag = flags[i];
                var bits = 0;

                if (i == 0)
                {
                    if ((flag & 0x01) != 0)
                        ret.Add("correlationId", Decode());
                    if ((flag & 0x02) != 0)
                    {
                        ReadByte();
                        var temp = ReadByteArray();
                        ret.Add("correlationIdBytes", temp);
                        ret.Add("correlationId", ByteArrayToId(temp));
                    }
                    bits = 2;
                }

                ReadRemaining(flag, bits);
            }

            return ret;
        }

        private TypedObject ReadDsk()
        {
            // DSK is just a DSA + extra set of flags/objects
            var ret = ReadDsa();
            ret.Type = "DSK";

            var flags = ReadFlags();
            for (var i = 0; i < flags.Count; i++)
                ReadRemaining(flags[i], 0);

            return ret;
        }

        private List<int> ReadFlags()
        {
            var flags = new List<int>();
            int flag;
            do
            {
                flag = ReadByteAsInt();
                flags.Add(flag);
            } while ((flag & 0x80) != 0);

            return flags;
        }

        private void ReadRemaining(int flag, int bits)
        {
            // For forwards compatibility, read in any other flagged objects to
            // preserve the integrity of the input stream...
            if ((flag >> bits) != 0)
            {
                for (var o = bits; o < 6; o++)
                {
                    if (((flag >> o) & 1) != 0)
                        Decode();
                }
            }
        }

        private string ByteArrayToId(byte[] data)
        {
            var ret = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
            {
                if (i == 4 || i == 6 || i == 8 || i == 10)
                    ret.Append('-');
                ret.AppendFormat("{0:X2}", data[i]);
            }

            return ret.ToString();
        }

        private object DecodeAmf0()
        {
            int type = ReadByte();
            switch (type)
            {
                case 0x00:
                    return ReadIntAmf0();

                case 0x02:
                    return ReadStringAmf0();

                case 0x03:
                    return ReadObjectAmf0();

                case 0x05:
                    return null;

                case 0x11: // AMF3
                    return Decode();
            }

            throw new NotImplementedException("AMF0 type not supported: " + type);
        }

        private string ReadStringAmf0()
        {
            var length = (ReadByteAsInt() << 8) + ReadByteAsInt();
            if (length == 0)
                return "";

            var data = ReadBytes(length);

            // UTF-8 applicable?
            string str;
            try
            {
                var enc = new UTF8Encoding();
                str = enc.GetString(data);
            }
            catch (Exception e)
            {
                throw new Exception("Error parsing AMF0 string from " + data + '\n' + e.Message);
            }

            return str;
        }

        private int ReadIntAmf0()
        {
            return (int) ReadDouble();
        }

        private TypedObject ReadObjectAmf0()
        {
            var body = new TypedObject("Body");
            string key;
            while (!(key = ReadStringAmf0()).Equals(""))
            {
                var b = ReadByte();
                if (b == 0x00)
                    body.Add(key, ReadDouble());
                else if (b == 0x02)
                    body.Add(key, ReadStringAmf0());
                else if (b == 0x05)
                    body.Add(key, null);
                else
                    throw new NotImplementedException("AMF0 type not supported: " + b);
            }
            ReadByte(); // Skip object end marker

            return body;
        }
    }
}