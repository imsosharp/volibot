#region

using System;
using System.Collections.Generic;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Summoner.Masterybook
{
    public class MasteryBookDto : RiotGamesObject
    {
        public delegate void Callback(MasteryBookDto result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.summoner.masterybook.MasteryBookDTO";

        public MasteryBookDto()
        {
        }

        public MasteryBookDto(Callback callback)
        {
            this._callback = callback;
        }

        public MasteryBookDto(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("bookPagesJson")]
        public object BookPagesJson { get; set; }

        [InternalName("bookPages")]
        public List<MasteryBookPageDto> BookPages { get; set; }

        [InternalName("dateString")]
        public String DateString { get; set; }

        [InternalName("summonerId")]
        public Double SummonerId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}