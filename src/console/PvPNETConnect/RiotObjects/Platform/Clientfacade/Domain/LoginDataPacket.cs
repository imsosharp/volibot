#region

using System;
using System.Collections.Generic;
using LoLLauncher.RiotObjects.Kudos.Dto;
using LoLLauncher.RiotObjects.Platform.Broadcast;
using LoLLauncher.RiotObjects.Platform.Game;
using LoLLauncher.RiotObjects.Platform.Statistics;
using LoLLauncher.RiotObjects.Platform.Summoner;
using LoLLauncher.RiotObjects.Platform.Systemstate;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Clientfacade.Domain
{
    public class LoginDataPacket : RiotGamesObject
    {
        public delegate void Callback(LoginDataPacket result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.clientfacade.domain.LoginDataPacket";

        public LoginDataPacket()
        {
        }

        public LoginDataPacket(Callback callback)
        {
            this._callback = callback;
        }

        public LoginDataPacket(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("playerStatSummaries")]
        public PlayerStatSummaries PlayerStatSummaries { get; set; }

        [InternalName("restrictedChatGamesRemaining")]
        public Int32 RestrictedChatGamesRemaining { get; set; }

        [InternalName("minutesUntilShutdown")]
        public Int32 MinutesUntilShutdown { get; set; }

        [InternalName("minor")]
        public Boolean Minor { get; set; }

        [InternalName("maxPracticeGameSize")]
        public Int32 MaxPracticeGameSize { get; set; }

        [InternalName("summonerCatalog")]
        public SummonerCatalog SummonerCatalog { get; set; }

        [InternalName("ipBalance")]
        public Double IpBalance { get; set; }

        [InternalName("reconnectInfo")]
        public PlatformGameLifecycleDto ReconnectInfo { get; set; }

        [InternalName("languages")]
        public List<String> Languages { get; set; }

        [InternalName("simpleMessages")]
        public List<object> SimpleMessages { get; set; }

        [InternalName("allSummonerData")]
        public AllSummonerData AllSummonerData { get; set; }

        [InternalName("customMinutesLeftToday")]
        public Int32 CustomMinutesLeftToday { get; set; }

        [InternalName("platformGameLifecycleDTO")]
        public object PlatformGameLifecycleDto { get; set; }

        [InternalName("coOpVsAiMinutesLeftToday")]
        public Int32 CoOpVsAiMinutesLeftToday { get; set; }

        [InternalName("bingeData")]
        public object BingeData { get; set; }

        [InternalName("inGhostGame")]
        public Boolean InGhostGame { get; set; }

        [InternalName("leaverPenaltyLevel")]
        public Int32 LeaverPenaltyLevel { get; set; }

        [InternalName("bingePreventionSystemEnabledForClient")]
        public Boolean BingePreventionSystemEnabledForClient { get; set; }

        [InternalName("pendingBadges")]
        public Int32 PendingBadges { get; set; }

        [InternalName("broadcastNotification")]
        public BroadcastNotification BroadcastNotification { get; set; }

        [InternalName("minutesUntilMidnight")]
        public Int32 MinutesUntilMidnight { get; set; }

        [InternalName("timeUntilFirstWinOfDay")]
        public Double TimeUntilFirstWinOfDay { get; set; }

        [InternalName("coOpVsAiMsecsUntilReset")]
        public Double CoOpVsAiMsecsUntilReset { get; set; }

        [InternalName("clientSystemStates")]
        public ClientSystemStatesNotification ClientSystemStates { get; set; }

        [InternalName("bingeMinutesRemaining")]
        public Double BingeMinutesRemaining { get; set; }

        [InternalName("pendingKudosDTO")]
        public PendingKudosDto PendingKudosDto { get; set; }

        [InternalName("leaverBusterPenaltyTime")]
        public Int32 LeaverBusterPenaltyTime { get; set; }

        [InternalName("platformId")]
        public String PlatformId { get; set; }

        [InternalName("matchMakingEnabled")]
        public Boolean MatchMakingEnabled { get; set; }

        [InternalName("minutesUntilShutdownEnabled")]
        public Boolean MinutesUntilShutdownEnabled { get; set; }

        [InternalName("rpBalance")]
        public Double RpBalance { get; set; }

        [InternalName("gameTypeConfigs")]
        public List<GameTypeConfigDto> GameTypeConfigs { get; set; }

        [InternalName("bingeIsPlayerInBingePreventionWindow")]
        public Boolean BingeIsPlayerInBingePreventionWindow { get; set; }

        [InternalName("minorShutdownEnforced")]
        public Boolean MinorShutdownEnforced { get; set; }

        [InternalName("competitiveRegion")]
        public String CompetitiveRegion { get; set; }

        [InternalName("customMsecsUntilReset")]
        public Double CustomMsecsUntilReset { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}