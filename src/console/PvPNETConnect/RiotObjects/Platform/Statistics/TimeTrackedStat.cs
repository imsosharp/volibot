#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Statistics
{
    public class TimeTrackedStat : RiotGamesObject
    {
        public delegate void Callback(TimeTrackedStat result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.statistics.TimeTrackedStat";

        public TimeTrackedStat()
        {
        }

        public TimeTrackedStat(Callback callback)
        {
            this._callback = callback;
        }

        public TimeTrackedStat(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("timestamp")]
        public DateTime Timestamp { get; set; }

        [InternalName("type")]
        public String Type { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}