#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Summoner.Boost
{
    public class SummonerActiveBoostsDto : RiotGamesObject
    {
        public delegate void Callback(SummonerActiveBoostsDto result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.summoner.boost.SummonerActiveBoostsDTO";

        public SummonerActiveBoostsDto()
        {
        }

        public SummonerActiveBoostsDto(Callback callback)
        {
            this._callback = callback;
        }

        public SummonerActiveBoostsDto(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("xpBoostEndDate")]
        public Double XpBoostEndDate { get; set; }

        [InternalName("xpBoostPerWinCount")]
        public Int32 XpBoostPerWinCount { get; set; }

        [InternalName("xpLoyaltyBoost")]
        public Int32 XpLoyaltyBoost { get; set; }

        [InternalName("ipBoostPerWinCount")]
        public Int32 IpBoostPerWinCount { get; set; }

        [InternalName("ipLoyaltyBoost")]
        public Int32 IpLoyaltyBoost { get; set; }

        [InternalName("summonerId")]
        public Double SummonerId { get; set; }

        [InternalName("ipBoostEndDate")]
        public Double IpBoostEndDate { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}