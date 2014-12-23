#region

using LoLLauncher.RiotObjects.Platform.Summoner.Spellbook;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Summoner
{
    public class AllPublicSummonerDataDto : RiotGamesObject
    {
        public delegate void Callback(AllPublicSummonerDataDto result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.summoner.AllPublicSummonerDataDTO";

        public AllPublicSummonerDataDto()
        {
        }

        public AllPublicSummonerDataDto(Callback callback)
        {
            this._callback = callback;
        }

        public AllPublicSummonerDataDto(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("spellBook")]
        public SpellBookDto SpellBook { get; set; }

        [InternalName("summonerDefaultSpells")]
        public SummonerDefaultSpells SummonerDefaultSpells { get; set; }

        [InternalName("summonerTalentsAndPoints")]
        public SummonerTalentsAndPoints SummonerTalentsAndPoints { get; set; }

        [InternalName("summoner")]
        public BasePublicSummonerDto Summoner { get; set; }

        [InternalName("summonerLevelAndPoints")]
        public SummonerLevelAndPoints SummonerLevelAndPoints { get; set; }

        [InternalName("summonerLevel")]
        public SummonerLevel SummonerLevel { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}