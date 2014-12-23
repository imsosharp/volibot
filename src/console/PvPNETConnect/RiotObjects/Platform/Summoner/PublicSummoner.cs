#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Summoner
{
    public class PublicSummoner : RiotGamesObject
    {
        public delegate void Callback(PublicSummoner result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.summoner.PublicSummoner";

        public PublicSummoner()
        {
        }

        public PublicSummoner(Callback callback)
        {
            this._callback = callback;
        }

        public PublicSummoner(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("internalName")]
        public String InternalName { get; set; }

        [InternalName("acctId")]
        public Double AcctId { get; set; }

        [InternalName("name")]
        public String Name { get; set; }

        [InternalName("profileIconId")]
        public Int32 ProfileIconId { get; set; }

        [InternalName("revisionDate")]
        public DateTime RevisionDate { get; set; }

        [InternalName("revisionId")]
        public Double RevisionId { get; set; }

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