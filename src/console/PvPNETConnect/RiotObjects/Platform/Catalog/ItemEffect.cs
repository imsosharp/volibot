#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Catalog
{
    public class ItemEffect : RiotGamesObject
    {
        public delegate void Callback(ItemEffect result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.catalog.ItemEffect";

        public ItemEffect()
        {
        }

        public ItemEffect(Callback callback)
        {
            this._callback = callback;
        }

        public ItemEffect(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("effectId")]
        public Int32 EffectId { get; set; }

        [InternalName("itemEffectId")]
        public Int32 ItemEffectId { get; set; }

        [InternalName("effect")]
        public Effect Effect { get; set; }

        [InternalName("value")]
        public String Value { get; set; }

        [InternalName("itemId")]
        public Int32 ItemId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}