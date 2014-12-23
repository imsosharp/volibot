#region

using System;
using System.Collections.Generic;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Matchmaking
{
    public class GameQueueConfig : RiotGamesObject
    {
        public delegate void Callback(GameQueueConfig result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.matchmaking.GameQueueConfig";

        public GameQueueConfig()
        {
        }

        public GameQueueConfig(Callback callback)
        {
            this._callback = callback;
        }

        public GameQueueConfig(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("blockedMinutesThreshold")]
        public Int32 BlockedMinutesThreshold { get; set; }

        [InternalName("minimumParticipantListSize")]
        public Int32 MinimumParticipantListSize { get; set; }

        [InternalName("ranked")]
        public Boolean Ranked { get; set; }

        [InternalName("maxLevel")]
        public Int32 MaxLevel { get; set; }

        [InternalName("minLevel")]
        public Int32 MinLevel { get; set; }

        [InternalName("gameTypeConfigId")]
        public Int32 GameTypeConfigId { get; set; }

        [InternalName("thresholdEnabled")]
        public Boolean ThresholdEnabled { get; set; }

        [InternalName("queueState")]
        public String QueueState { get; set; }

        [InternalName("type")]
        public String Type { get; set; }

        [InternalName("cacheName")]
        public String CacheName { get; set; }

        [InternalName("id")]
        public Double Id { get; set; }

        [InternalName("queueBonusKey")]
        public String QueueBonusKey { get; set; }

        [InternalName("queueStateString")]
        public String QueueStateString { get; set; }

        [InternalName("pointsConfigKey")]
        public String PointsConfigKey { get; set; }

        [InternalName("teamOnly")]
        public Boolean TeamOnly { get; set; }

        [InternalName("minimumQueueDodgeDelayTime")]
        public Int32 MinimumQueueDodgeDelayTime { get; set; }

        [InternalName("supportedMapIds")]
        public List<Int32> SupportedMapIds { get; set; }

        [InternalName("gameMode")]
        public String GameMode { get; set; }

        [InternalName("typeString")]
        public String TypeString { get; set; }

        [InternalName("numPlayersPerTeam")]
        public Int32 NumPlayersPerTeam { get; set; }

        [InternalName("maximumParticipantListSize")]
        public Int32 MaximumParticipantListSize { get; set; }

        [InternalName("disallowFreeChampions")]
        public Boolean DisallowFreeChampions { get; set; }

        [InternalName("mapSelectionAlgorithm")]
        public String MapSelectionAlgorithm { get; set; }

        [InternalName("thresholdSize")]
        public Double ThresholdSize { get; set; }

        [InternalName("matchingThrottleConfig")]
        public MatchingThrottleConfig MatchingThrottleConfig { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}