#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Reroll.Pojo
{
    internal class EogPointChangeBreakdown : RiotGamesObject
    {
        public delegate void Callback(EogPointChangeBreakdown result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.reroll.pojo.EogPointChangeBreakdown";

        public EogPointChangeBreakdown()
        {
        }

        public EogPointChangeBreakdown(Callback callback)
        {
            this._callback = callback;
        }

        public EogPointChangeBreakdown(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("pointChangeFromGamePlay")]
        public Double PointChangeFromGamePlay { get; set; }

        [InternalName("pointChangeFromChampionsOwned")]
        public Double PointChangeFromChampionsOwned { get; set; }

        [InternalName("previousPoints")]
        public Double PreviousPoints { get; set; }

        [InternalName("pointsUsed")]
        public Double PointsUsed { get; set; }

        [InternalName("endPoints")]
        public Double EndPoints { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}