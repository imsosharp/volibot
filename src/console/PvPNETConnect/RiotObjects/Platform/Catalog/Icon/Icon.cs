#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Catalog.Icon
{
    public class Icon : RiotGamesObject
    {
        public delegate void Callback(Icon result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.catalog.icon.Icon";

        public Icon()
        {
        }

        public Icon(Callback callback)
        {
            this._callback = callback;
        }

        public Icon(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("purchaseDate")]
        public DateTime PurchaseDate { get; set; }

        [InternalName("iconId")]
        public Double IconId { get; set; }

        [InternalName("summonerId")]
        public Double SummonerId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}