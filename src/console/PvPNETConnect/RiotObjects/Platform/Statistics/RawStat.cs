#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Statistics
{
    public class RawStat : RiotGamesObject
    {
        public delegate void Callback(RawStat result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.statistics.RawStat";

        public RawStat()
        {
        }

        public RawStat(Callback callback)
        {
            this._callback = callback;
        }

        public RawStat(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("statType")]
        public String StatType { get; set; }

        [InternalName("value")]
        public Double Value { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}