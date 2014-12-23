#region

using System;
using LoLLauncher.RiotObjects.Platform.Game;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Reroll.Pojo
{
    public class AramPlayerParticipant : Participant
    {
        public delegate void Callback(AramPlayerParticipant result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.reroll.pojo.AramPlayerParticipant";

        public AramPlayerParticipant()
        {
        }

        public AramPlayerParticipant(Callback callback)
        {
            this._callback = callback;
        }

        public AramPlayerParticipant(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("timeAddedToQueue")]
        public Double TimeAddedToQueue { get; set; }

        [InternalName("index")]
        public Int32 Index { get; set; }

        [InternalName("queueRating")]
        public Int32 QueueRating { get; set; }

        [InternalName("accountId")]
        public Double AccountId { get; set; }

        [InternalName("botDifficulty")]
        public String BotDifficulty { get; set; }

        [InternalName("originalAccountNumber")]
        public Double OriginalAccountNumber { get; set; }

        [InternalName("summonerInternalName")]
        public String SummonerInternalName { get; set; }

        [InternalName("minor")]
        public Boolean Minor { get; set; }

        [InternalName("locale")]
        public object Locale { get; set; }

        [InternalName("lastSelectedSkinIndex")]
        public Int32 LastSelectedSkinIndex { get; set; }

        [InternalName("partnerId")]
        public String PartnerId { get; set; }

        [InternalName("profileIconId")]
        public Int32 ProfileIconId { get; set; }

        [InternalName("teamOwner")]
        public Boolean TeamOwner { get; set; }

        [InternalName("pointSummary")]
        public PointSummary PointSummary { get; set; }

        [InternalName("summonerId")]
        public Double SummonerId { get; set; }

        [InternalName("badges")]
        public Int32 Badges { get; set; }

        [InternalName("pickTurn")]
        public Int32 PickTurn { get; set; }

        [InternalName("clientInSynch")]
        public Boolean ClientInSynch { get; set; }

        [InternalName("summonerName")]
        public String SummonerName { get; set; }

        [InternalName("pickMode")]
        public Int32 PickMode { get; set; }

        [InternalName("originalPlatformId")]
        public String OriginalPlatformId { get; set; }

        [InternalName("teamParticipantId")]
        public Double TeamParticipantId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}