#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Statistics
{
    public class LeaverPenaltyStats : RiotGamesObject
    {
        public delegate void Callback(LeaverPenaltyStats result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.statistics.LeaverPenaltyStats";

        public LeaverPenaltyStats()
        {
        }

        public LeaverPenaltyStats(Callback callback)
        {
            this._callback = callback;
        }

        public LeaverPenaltyStats(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("lastLevelIncrease")]
        public object LastLevelIncrease { get; set; }

        [InternalName("level")]
        public Int32 Level { get; set; }

        [InternalName("lastDecay")]
        public DateTime LastDecay { get; set; }

        [InternalName("userInformed")]
        public Boolean UserInformed { get; set; }

        [InternalName("points")]
        public Int32 Points { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}