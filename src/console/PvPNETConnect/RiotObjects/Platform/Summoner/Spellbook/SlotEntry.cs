#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Summoner.Spellbook
{
    public class SlotEntry : RiotGamesObject
    {
        public delegate void Callback(SlotEntry result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.summoner.spellbook.SlotEntry";

        public SlotEntry()
        {
        }

        public SlotEntry(Callback callback)
        {
            this._callback = callback;
        }

        public SlotEntry(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("runeId")]
        public Int32 RuneId { get; set; }

        [InternalName("runeSlotId")]
        public Int32 RuneSlotId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}