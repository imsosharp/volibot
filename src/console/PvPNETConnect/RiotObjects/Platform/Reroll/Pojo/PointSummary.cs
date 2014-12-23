#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Reroll.Pojo
{
    public class PointSummary : RiotGamesObject
    {
        public delegate void Callback(PointSummary result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.reroll.pojo.PointSummary";

        public PointSummary()
        {
        }

        public PointSummary(Callback callback)
        {
            this._callback = callback;
        }

        public PointSummary(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("pointsToNextRoll")]
        public Double PointsToNextRoll { get; set; }

        [InternalName("maxRolls")]
        public Int32 MaxRolls { get; set; }

        [InternalName("numberOfRolls")]
        public Int32 NumberOfRolls { get; set; }

        [InternalName("pointsCostToRoll")]
        public Double PointsCostToRoll { get; set; }

        [InternalName("currentPoints")]
        public Double CurrentPoints { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}