#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Statistics
{
    public class FellowPlayerInfo : RiotGamesObject
    {
        public delegate void Callback(FellowPlayerInfo result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.statistics.FellowPlayerInfo";

        public FellowPlayerInfo()
        {
        }

        public FellowPlayerInfo(Callback callback)
        {
            this._callback = callback;
        }

        public FellowPlayerInfo(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("championId")]
        public Double ChampionId { get; set; }

        [InternalName("teamId")]
        public Int32 TeamId { get; set; }

        [InternalName("summonerId")]
        public Double SummonerId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}