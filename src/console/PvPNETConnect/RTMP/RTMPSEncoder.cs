#region

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace LoLLauncher
{
    public class RtmpsEncoder
    {
        public long StartTime = (long) DateTime.Now.TimeOfDay.TotalMilliseconds;

        public byte[] AddHeaders(byte[] data)
        {
            var result = new List<byte>();

            // Header byte
            result.Add(0x03);

            // Timestamp
            var timediff = ((long) DateTime.Now.TimeOfDay.TotalMilliseconds - StartTime);
            result.Add((byte) ((timediff & 0xFF0000) >> 16));
            result.Add((byte) ((timediff & 0x00FF00) >> 8));
            result.Add((byte) (timediff & 0x0000FF));

            // Body size
            result.Add((byte) ((data.Length & 0xFF0000) >> 16));
            result.Add((byte) ((data.Length & 0x00FF00) >> 8));
            result.Add((byte) (data.Length & 0x0000FF));

            // Content type
            result.Add(0x11);

            // Source ID
            result.Add(0x00);
            result.Add(0x00);
            result.Add(0x00);
            result.Add(0x00);

            // Add body
            for (var i = 0; i < data.Length; i++)
            {
                result.Add(data[i]);
                if (i%128 == 127 && i != data.Length - 1)
                    result.Add(0xC3);
            }

            var ret = new byte[result.Count];
            for (var i = 0; i < ret.Length; i++)
                ret[i] = result[i];

            return ret;
        }

        public byte[] EncodeConnect(Dictionary<string, object> paramaters)
        {
            var result = new List<Byte>();

            WriteStringAmf0(result, "connect");
            WriteIntAmf0(result, 1); // invokeId

            // Write params
            result.Add(0x11); // AMF3 object
            result.Add(0x09); // Array
            WriteAssociativeArray(result, paramaters);

            // Write service call args
            result.Add(0x01);
            result.Add(0x00); // false
            WriteStringAmf0(result, "nil"); // "nil"
            WriteStringAmf0(result, ""); // ""

            // Set up CommandMessage
            var cm = new TypedObject("flex.messaging.messages.CommandMessage");
            cm.Add("operation", 5);
            cm.Add("correlationId", "");
            cm.Add("timestamp", 0);
            cm.Add("messageId", RandomUid());
            cm.Add("body", new TypedObject(null));
            cm.Add("destination", "");
            var headers = new Dictionary<string, object>();
            headers.Add("DSMessagingVersion", 1.0);
            headers.Add("DSId", "my-rtmps");
            cm.Add("headers", headers);
            cm.Add("clientId", null);
            cm.Add("timeToLive", 0);

            // Write CommandMessage
            result.Add(0x11); // AMF3 object
            Encode(result, cm);

            var ret = new byte[result.Count];
            for (var i = 0; i < ret.Length; i++)
                ret[i] = result[i];

            ret = AddHeaders(ret);
            ret[7] = 0x14; // Change message type

            return ret;
        }

        public byte[] EncodeInvoke(int id, object data)
        {
            var result = new List<Byte>();

            result.Add(0x00); // version
            result.Add(0x05); // type?
            WriteIntAmf0(result, id); // invoke ID
            result.Add(0x05); // ???

            result.Add(0x11); // AMF3 object
            Encode(result, data);

            var ret = new byte[result.Count];
            for (var i = 0; i < ret.Length; i++)
                ret[i] = result[i];

            ret = AddHeaders(ret);

            return ret;
        }

        public byte[] Encode(object obj)
        {
            var result = new List<byte>();
            Encode(result, obj);

            var ret = new byte[result.Count];
            for (var i = 0; i < ret.Length; i++)
                ret[i] = result[i];

            return ret;
        }

        public void Encode(List<byte> ret, object obj)
        {
            if (obj == null)
            {
                ret.Add(0x01);
            }
            else if (obj is bool)
            {
                var val = (bool) obj;
                if (val)
                    ret.Add(0x03);
                else
                    ret.Add(0x02);
            }
            else if (obj is int)
            {
                ret.Add(0x04);
                WriteInt(ret, (int) obj);
            }
            else if (obj is double)
            {
                ret.Add(0x05);
                WriteDouble(ret, (double) obj);
            }
            else if (obj is string)
            {
                ret.Add(0x06);
                WriteString(ret, (string) obj);
            }
            else if (obj is DateTime)
            {
                ret.Add(0x08);
                WriteDate(ret, (DateTime) obj);
            }
            // Must precede Object[] check
            else if (obj is byte[])
            {
                ret.Add(0x0C);
                WriteByteArray(ret, (byte[]) obj);
            }
            else if (obj is object[])
            {
                ret.Add(0x09);
                WriteArray(ret, (object[]) obj);
            }
            // Must precede Map check
            else if (obj is TypedObject)
            {
                ret.Add(0x0A);
                WriteObject(ret, (TypedObject) obj);
            }
            else if (obj is Dictionary<string, object>)
            {
                ret.Add(0x09);
                WriteAssociativeArray(ret, (Dictionary<string, object>) obj);
            }
            else
            {
                throw new Exception("Unexpected object type: " + obj.GetType().FullName);
            }
        }

        private void WriteInt(List<Byte> ret, int val)
        {
            if (val < 0 || val >= 0x200000)
            {
                ret.Add((byte) (((val >> 22) & 0x7f) | 0x80));
                ret.Add((byte) (((val >> 15) & 0x7f) | 0x80));
                ret.Add((byte) (((val >> 8) & 0x7f) | 0x80));
                ret.Add((byte) (val & 0xff));
            }
            else
            {
                if (val >= 0x4000)
                {
                    ret.Add((byte) (((val >> 14) & 0x7f) | 0x80));
                }
                if (val >= 0x80)
                {
                    ret.Add((byte) (((val >> 7) & 0x7f) | 0x80));
                }
                ret.Add((byte) (val & 0x7f));
            }
        }

        private void WriteDouble(List<byte> ret, double val)
        {
            if (Double.IsNaN(val))
            {
                ret.Add(0x7F);
                ret.Add(0xFF);
                ret.Add(0xFF);
                ret.Add(0xFF);
                ret.Add(0xE0);
                ret.Add(0x00);
                ret.Add(0x00);
                ret.Add(0x00);
            }
            else
            {
                var temp = BitConverter.GetBytes(val);

                for (var i = temp.Length - 1; i >= 0; i--)
                    ret.Add(temp[i]);
            }
        }

        private void WriteString(List<byte> ret, string val)
        {
            byte[] temp = null;
            try
            {
                var encoding = new UTF8Encoding();
                temp = encoding.GetBytes(val);
            }
            catch (Exception e)
            {
                throw new Exception("Unable to encode string as UTF-8: " + val + '\n' + e.Message);
            }

            WriteInt(ret, (temp.Length << 1) | 1);

            foreach (var b in temp)
                ret.Add(b);
        }

        private void WriteDate(List<Byte> ret, DateTime val)
        {
            ret.Add(0x01);
            WriteDouble(ret, val.TimeOfDay.TotalMilliseconds);
        }

        private void WriteArray(List<byte> ret, object[] val)
        {
            WriteInt(ret, (val.Length << 1) | 1);
            ret.Add(0x01);
            foreach (var obj in val)
                Encode(ret, obj);
        }

        private void WriteAssociativeArray(List<Byte> ret, Dictionary<string, object> val)
        {
            ret.Add(0x01);
            foreach (var key in val.Keys)
            {
                WriteString(ret, key);
                Encode(ret, val[key]);
            }
            ret.Add(0x01);
        }

        private void WriteObject(List<byte> ret, TypedObject val)
        {
            if (val.Type == null || val.Type.Equals(""))
            {
                ret.Add(0x0B); // Dynamic class

                ret.Add(0x01); // No class name
                foreach (var key in val.Keys)
                {
                    WriteString(ret, key);
                    Encode(ret, val[key]);
                }
                ret.Add(0x01); // End of dynamic
            }
            else if (val.Type.Equals("flex.messaging.io.ArrayCollection"))
            {
                ret.Add(0x07); // Externalizable
                WriteString(ret, val.Type);

                Encode(ret, val["array"]);
            }
            else
            {
                WriteInt(ret, (val.Count << 4) | 3); // Inline + member count
                WriteString(ret, val.Type);

                var keyOrder = new List<String>();
                foreach (var key in val.Keys)
                {
                    WriteString(ret, key);
                    keyOrder.Add(key);
                }

                foreach (var key in keyOrder)
                    Encode(ret, val[key]);
            }
        }

        private void WriteByteArray(List<byte> ret, byte[] val)
        {
            throw new NotImplementedException("Encoding byte arrays is not implemented");
        }

        private void WriteIntAmf0(List<byte> ret, int val)
        {
            ret.Add(0x00);

            var temp = BitConverter.GetBytes((double) val);

            for (var i = temp.Length - 1; i >= 0; i--)
                ret.Add(temp[i]);
            //foreach (byte b in temp)
            //ret.Add(b);
        }

        private void WriteStringAmf0(List<byte> ret, string val)
        {
            byte[] temp = null;
            try
            {
                var encoding = new UTF8Encoding();
                temp = encoding.GetBytes(val);
            }
            catch (Exception e)
            {
                throw new Exception("Unable to encode string as UTF-8: " + val + '\n' + e.Message);
            }

            ret.Add(0x02);

            ret.Add((byte) ((temp.Length & 0xFF00) >> 8));
            ret.Add((byte) (temp.Length & 0x00FF));

            foreach (var b in temp)
                ret.Add(b);
        }

        public static string RandomUid()
        {
            var rand = new Random();

            var bytes = new byte[16];
            rand.NextBytes(bytes);

            var ret = new StringBuilder();
            for (var i = 0; i < bytes.Length; i++)
            {
                if (i == 4 || i == 6 || i == 8 || i == 10)
                    ret.Append('-');
                ret.AppendFormat("{0:X2}", bytes[i]);
            }

            return ret.ToString();
        }
    }
}