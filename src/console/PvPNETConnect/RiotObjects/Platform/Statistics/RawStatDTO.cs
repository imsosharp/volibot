#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Statistics
{
    public class RawStatDto : RiotGamesObject
    {
        public delegate void Callback(RawStatDto result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.statistics.RawStatDTO";

        public RawStatDto()
        {
        }

        public RawStatDto(Callback callback)
        {
            this._callback = callback;
        }

        public RawStatDto(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("value")]
        public Double Value { get; set; }

        [InternalName("statTypeName")]
        public String StatTypeName { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}