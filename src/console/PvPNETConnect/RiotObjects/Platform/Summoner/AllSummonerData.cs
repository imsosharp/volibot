#region

using LoLLauncher.RiotObjects.Platform.Summoner.Masterybook;
using LoLLauncher.RiotObjects.Platform.Summoner.Spellbook;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Summoner
{
    public class AllSummonerData : RiotGamesObject
    {
        public delegate void Callback(AllSummonerData result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.summoner.AllSummonerData";

        public AllSummonerData()
        {
        }

        public AllSummonerData(Callback callback)
        {
            this._callback = callback;
        }

        public AllSummonerData(TypedObject result)
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
        public Summoner Summoner { get; set; }

        [InternalName("masteryBook")]
        public MasteryBookDto MasteryBook { get; set; }

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