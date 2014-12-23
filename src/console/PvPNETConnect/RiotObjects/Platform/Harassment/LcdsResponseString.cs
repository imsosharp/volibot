#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Harassment
{
    public class LcdsResponseString : RiotGamesObject
    {
        public delegate void Callback(LcdsResponseString result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.harassment.LcdsResponseString";

        public LcdsResponseString()
        {
        }

        public LcdsResponseString(Callback callback)
        {
            this._callback = callback;
        }

        public LcdsResponseString(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("value")]
        public String Value { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}