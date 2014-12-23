#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Summoner
{
    public class SummonerTalentsAndPoints : RiotGamesObject
    {
        public delegate void Callback(SummonerTalentsAndPoints result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.summoner.SummonerTalentsAndPoints";

        public SummonerTalentsAndPoints()
        {
        }

        public SummonerTalentsAndPoints(Callback callback)
        {
            this._callback = callback;
        }

        public SummonerTalentsAndPoints(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("talentPoints")]
        public Int32 TalentPoints { get; set; }

        [InternalName("modifyDate")]
        public DateTime ModifyDate { get; set; }

        [InternalName("createDate")]
        public DateTime CreateDate { get; set; }

        [InternalName("summonerId")]
        public Double SummonerId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}