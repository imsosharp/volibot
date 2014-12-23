#region

using System;
using System.Collections.Generic;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Summoner
{
    public class TalentRow : RiotGamesObject
    {
        public delegate void Callback(TalentRow result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.summoner.TalentRow";

        public TalentRow()
        {
        }

        public TalentRow(Callback callback)
        {
            this._callback = callback;
        }

        public TalentRow(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("index")]
        public Int32 Index { get; set; }

        [InternalName("talents")]
        public List<Talent> Talents { get; set; }

        [InternalName("tltGroupId")]
        public Int32 TltGroupId { get; set; }

        [InternalName("pointsToActivate")]
        public Int32 PointsToActivate { get; set; }

        [InternalName("tltRowId")]
        public Int32 TltRowId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}