#region

using System;
using System.Collections.Generic;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Summoner.Spellbook
{
    public class SpellBookPageDto : RiotGamesObject
    {
        public delegate void Callback(SpellBookPageDto result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.summoner.spellbook.SpellBookPageDTO";

        public SpellBookPageDto()
        {
        }

        public SpellBookPageDto(Callback callback)
        {
            this._callback = callback;
        }

        public SpellBookPageDto(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("slotEntries")]
        public List<SlotEntry> SlotEntries { get; set; }

        [InternalName("summonerId")]
        public Int32 SummonerId { get; set; }

        [InternalName("createDate")]
        public DateTime CreateDate { get; set; }

        [InternalName("name")]
        public String Name { get; set; }

        [InternalName("pageId")]
        public Int32 PageId { get; set; }

        [InternalName("current")]
        public Boolean Current { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}