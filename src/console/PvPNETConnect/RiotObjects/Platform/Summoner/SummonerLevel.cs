#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Summoner
{
    public class SummonerLevel : RiotGamesObject
    {
        public delegate void Callback(SummonerLevel result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.summoner.SummonerLevel";

        public SummonerLevel()
        {
        }

        public SummonerLevel(Callback callback)
        {
            this._callback = callback;
        }

        public SummonerLevel(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("expTierMod")]
        public Double ExpTierMod { get; set; }

        [InternalName("grantRp")]
        public Double GrantRp { get; set; }

        [InternalName("expForLoss")]
        public Double ExpForLoss { get; set; }

        [InternalName("summonerTier")]
        public Double SummonerTier { get; set; }

        [InternalName("infTierMod")]
        public Double InfTierMod { get; set; }

        [InternalName("expToNextLevel")]
        public Double ExpToNextLevel { get; set; }

        [InternalName("expForWin")]
        public Double ExpForWin { get; set; }

        [InternalName("summonerLevel")]
        public Double Level { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}