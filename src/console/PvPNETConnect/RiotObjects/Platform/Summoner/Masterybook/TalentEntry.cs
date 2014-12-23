#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Summoner.Masterybook
{
    public class TalentEntry : RiotGamesObject
    {
        public delegate void Callback(TalentEntry result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.summoner.masterybook.TalentEntry";

        public TalentEntry()
        {
        }

        public TalentEntry(Callback callback)
        {
            this._callback = callback;
        }

        public TalentEntry(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("rank")]
        public Int32 Rank { get; set; }

        [InternalName("talentId")]
        public Int32 TalentId { get; set; }

        [InternalName("talent")]
        public Talent Talent { get; set; }

        [InternalName("summonerId")]
        public Double SummonerId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}