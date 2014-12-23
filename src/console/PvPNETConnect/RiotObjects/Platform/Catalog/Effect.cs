#region

using System;
using LoLLauncher.RiotObjects.Platform.Catalog.Runes;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Catalog
{
    public class Effect : RiotGamesObject
    {
        public delegate void Callback(Effect result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.catalog.Effect";

        public Effect()
        {
        }

        public Effect(Callback callback)
        {
            this._callback = callback;
        }

        public Effect(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("effectId")]
        public Int32 EffectId { get; set; }

        [InternalName("gameCode")]
        public String GameCode { get; set; }

        [InternalName("name")]
        public String Name { get; set; }

        [InternalName("categoryId")]
        public object CategoryId { get; set; }

        [InternalName("runeType")]
        public RuneType RuneType { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}