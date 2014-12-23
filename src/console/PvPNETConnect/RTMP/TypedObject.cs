#region

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace LoLLauncher
{
    public class TypedObject : Dictionary<string, object>
    {
        private static long _serialVersionUid = 1244827787088018807L;
        public string Type;

        public TypedObject()
        {
            Type = null;
        }

        public TypedObject(string type)
        {
            this.Type = type;
        }

        public static TypedObject MakeArrayCollection(object[] data)
        {
            var ret = new TypedObject("flex.messaging.io.ArrayCollection");
            ret.Add("array", data);
            return ret;
        }

        public TypedObject GetTO(string key)
        {
            if (ContainsKey(key) && this[key] is TypedObject)
                return (TypedObject) this[key];

            return null;
        }

        public string GetString(string key)
        {
            return (string) this[key];
        }

        public int? GetInt(string key)
        {
            var val = this[key];
            if (val == null)
                return null;
            if (val is int)
                return (int) val;
            return Convert.ToInt32((double) val);
        }

        public double? GetDouble(string key)
        {
            var val = this[key];
            if (val == null)
                return null;
            if (val is double)
                return (double) val;
            return Convert.ToDouble((int) val);
        }

        public bool GetBool(string key)
        {
            return (bool) this[key];
        }

        public object[] GetArray(string key)
        {
            if (this[key] is TypedObject && GetTO(key).Type.Equals("flex.messaging.io.ArrayCollection"))
                return (object[]) GetTO(key)["array"];
            return (object[]) this[key];
        }

        public override string ToString()
        {
            if (Type == null)
                return base.ToString();
            if (Type.Equals("flex.messaging.io.ArrayCollection"))
            {
                var sb = new StringBuilder();
                var data = (object[]) this["array"];
                sb.Append("ArrayCollection[");
                for (var i = 0; i < data.Length; i++)
                {
                    sb.Append(data[i]);
                    if (i < data.Length - 1)
                        sb.Append(", ");
                }
                sb.Append(']');
                return sb.ToString();
            }
            var val = "";
            foreach (var entry in this)
            {
                val += entry.Key + " : " + entry.Value + "\n";
                // do something with entry.Value or entry.Key
            }
            return val + Type + ":" + base.ToString();
        }
    }
}