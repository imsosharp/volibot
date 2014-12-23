#region

using System;
using LoLLauncher.RiotObjects.Platform.Catalog.Runes;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Summoner
{
    public class RuneSlot : RiotGamesObject
    {
        public delegate void Callback(RuneSlot result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.summoner.RuneSlot";

        public RuneSlot()
        {
        }

        public RuneSlot(Callback callback)
        {
            this._callback = callback;
        }

        public RuneSlot(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("id")]
        public Int32 Id { get; set; }

        [InternalName("minLevel")]
        public Int32 MinLevel { get; set; }

        [InternalName("runeType")]
        public RuneType RuneType { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}