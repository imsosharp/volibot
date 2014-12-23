#region

using System;
using System.Collections.Generic;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Summoner.Masterybook
{
    public class MasteryBookPageDto : RiotGamesObject
    {
        public delegate void Callback(MasteryBookPageDto result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.summoner.masterybook.MasteryBookPageDTO";

        public MasteryBookPageDto()
        {
        }

        public MasteryBookPageDto(Callback callback)
        {
            this._callback = callback;
        }

        public MasteryBookPageDto(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("talentEntries")]
        public List<TalentEntry> TalentEntries { get; set; }

        [InternalName("pageId")]
        public Double PageId { get; set; }

        [InternalName("name")]
        public String Name { get; set; }

        [InternalName("current")]
        public Boolean Current { get; set; }

        [InternalName("createDate")]
        public object CreateDate { get; set; }

        [InternalName("summonerId")]
        public Double SummonerId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}