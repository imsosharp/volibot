#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Matchmaking
{
    public class MatchMakerParams : RiotGamesObject
    {
        public delegate void Callback(MatchMakerParams result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.matchmaking.MatchMakerParams";

        public MatchMakerParams()
        {
        }

        public MatchMakerParams(Callback callback)
        {
            this._callback = callback;
        }

        public MatchMakerParams(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("lastMaestroMessage")]
        public object LastMaestroMessage { get; set; }

        [InternalName("teamId")]
        public object TeamId { get; set; }

        [InternalName("languages")]
        public object Languages { get; set; }

        [InternalName("botDifficulty")]
        public String BotDifficulty { get; set; }

        [InternalName("team")]
        public object Team { get; set; }

        [InternalName("queueIds")]
        public Int32[] QueueIds { get; set; }

        [InternalName("invitationId")]
        public object InvitationId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}