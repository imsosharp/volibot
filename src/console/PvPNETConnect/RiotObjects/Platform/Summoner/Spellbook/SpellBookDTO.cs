#region

using System;
using System.Collections.Generic;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Summoner.Spellbook
{
    public class SpellBookDto : RiotGamesObject
    {
        public delegate void Callback(SpellBookDto result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.summoner.spellbook.SpellBookDTO";

        public SpellBookDto()
        {
        }

        public SpellBookDto(Callback callback)
        {
            this._callback = callback;
        }

        public SpellBookDto(TypedObject result)
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
        public List<SpellBookPageDto> BookPages { get; set; }

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