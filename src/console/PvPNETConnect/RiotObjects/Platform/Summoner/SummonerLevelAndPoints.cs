#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Summoner
{
    public class SummonerLevelAndPoints : RiotGamesObject
    {
        public delegate void Callback(SummonerLevelAndPoints result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.summoner.SummonerLevelAndPoints";

        public SummonerLevelAndPoints()
        {
        }

        public SummonerLevelAndPoints(Callback callback)
        {
            this._callback = callback;
        }

        public SummonerLevelAndPoints(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("infPoints")]
        public Double InfPoints { get; set; }

        [InternalName("expPoints")]
        public Double ExpPoints { get; set; }

        [InternalName("summonerLevel")]
        public Double SummonerLevel { get; set; }

        [InternalName("summonerId")]
        public Double SummonerId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}