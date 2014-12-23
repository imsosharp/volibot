#region

using System;
using System.Collections.Generic;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Summoner
{
    public class TalentGroup : RiotGamesObject
    {
        public delegate void Callback(TalentGroup result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.summoner.TalentGroup";

        public TalentGroup()
        {
        }

        public TalentGroup(Callback callback)
        {
            this._callback = callback;
        }

        public TalentGroup(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("index")]
        public Int32 Index { get; set; }

        [InternalName("talentRows")]
        public List<TalentRow> TalentRows { get; set; }

        [InternalName("name")]
        public String Name { get; set; }

        [InternalName("tltGroupId")]
        public Int32 TltGroupId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}