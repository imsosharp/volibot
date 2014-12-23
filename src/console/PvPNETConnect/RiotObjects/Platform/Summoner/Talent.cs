#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Summoner
{
    public class Talent : RiotGamesObject
    {
        public delegate void Callback(Talent result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.summoner.Talent";

        public Talent()
        {
        }

        public Talent(Callback callback)
        {
            this._callback = callback;
        }

        public Talent(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("index")]
        public Int32 Index { get; set; }

        [InternalName("level5Desc")]
        public String Level5Desc { get; set; }

        [InternalName("minLevel")]
        public Int32 MinLevel { get; set; }

        [InternalName("maxRank")]
        public Int32 MaxRank { get; set; }

        [InternalName("level4Desc")]
        public String Level4Desc { get; set; }

        [InternalName("tltId")]
        public Int32 TltId { get; set; }

        [InternalName("level3Desc")]
        public String Level3Desc { get; set; }

        [InternalName("talentGroupId")]
        public Int32 TalentGroupId { get; set; }

        [InternalName("gameCode")]
        public Int32 GameCode { get; set; }

        [InternalName("minTier")]
        public Int32 MinTier { get; set; }

        [InternalName("prereqTalentGameCode")]
        public object PrereqTalentGameCode { get; set; }

        [InternalName("level2Desc")]
        public String Level2Desc { get; set; }

        [InternalName("name")]
        public String Name { get; set; }

        [InternalName("talentRowId")]
        public Int32 TalentRowId { get; set; }

        [InternalName("level1Desc")]
        public String Level1Desc { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}