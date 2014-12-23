#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Catalog.Runes
{
    public class RuneType : RiotGamesObject
    {
        public delegate void Callback(RuneType result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.catalog.runes.RuneType";

        public RuneType()
        {
        }

        public RuneType(Callback callback)
        {
            this._callback = callback;
        }

        public RuneType(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("runeTypeId")]
        public Int32 RuneTypeId { get; set; }

        [InternalName("name")]
        public String Name { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}