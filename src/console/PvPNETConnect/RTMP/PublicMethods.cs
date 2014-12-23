#region

using System;
using System.Linq;
using System.Threading.Tasks;
using LoLLauncher.RiotObjects.Leagues.Pojo;
using LoLLauncher.RiotObjects.Platform.Catalog.Champion;
using LoLLauncher.RiotObjects.Platform.Clientfacade.Domain;
using LoLLauncher.RiotObjects.Platform.Game;
using LoLLauncher.RiotObjects.Platform.Game.Practice;
using LoLLauncher.RiotObjects.Platform.Harassment;
using LoLLauncher.RiotObjects.Platform.Leagues.Client.Dto;
using LoLLauncher.RiotObjects.Platform.Login;
using LoLLauncher.RiotObjects.Platform.Matchmaking;
using LoLLauncher.RiotObjects.Platform.Reroll.Pojo;
using LoLLauncher.RiotObjects.Platform.Statistics;
using LoLLauncher.RiotObjects.Platform.Statistics.Team;
using LoLLauncher.RiotObjects.Platform.Summoner;
using LoLLauncher.RiotObjects.Platform.Summoner.Boost;
using LoLLauncher.RiotObjects.Platform.Summoner.Masterybook;
using LoLLauncher.RiotObjects.Platform.Summoner.Runes;
using LoLLauncher.RiotObjects.Platform.Summoner.Spellbook;
using LoLLauncher.RiotObjects.Team;
using LoLLauncher.RiotObjects.Team.Dto;

#endregion

namespace LoLLauncher
{
    public partial class LoLConnection
    {
        /// 0.)
        private void Login(AuthenticationCredentials arg0, Session.Callback callback)
        {
            var cb = new Session(callback);
            InvokeWithCallback("loginService", "login", new object[] {arg0.GetBaseTypedObject()}, cb);
        }

        private async Task<Session> Login(AuthenticationCredentials arg0)
        {
            var id = Invoke("loginService", "login", new object[] {arg0.GetBaseTypedObject()});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new Session(messageBody);
            _results.Remove(id);
            return result;
        }

        public async Task<object> Subscribe(string service, double accountId)
        {
            var body = WrapBody(new TypedObject(), "messagingDestination", 0);
            body.Type = "flex.messaging.messages.CommandMessage";
            var headers = body.GetTO("headers");
            if (service == "bc")
                headers.Add("DSSubtopic", "bc");
            else
                headers.Add("DSSubtopic", service + "-" + _accountId);
            headers.Remove("DSRequestTimeout");
            body["clientId"] = service + "-" + _accountId;
            var id = Invoke(body);
            while (!_results.ContainsKey(id))
                await Task.Delay(10);

            var result = GetResult(id); // Read result and discard
            return null;
        }

        /// 1.)
        public void GetLoginDataPacketForUser(LoginDataPacket.Callback callback)
        {
            var cb = new LoginDataPacket(callback);
            InvokeWithCallback("clientFacadeService", "getLoginDataPacketForUser", new object[] {}, cb);
        }

        public async Task<LoginDataPacket> GetLoginDataPacketForUser()
        {
            var id = Invoke("clientFacadeService", "getLoginDataPacketForUser", new object[] {});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new LoginDataPacket(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 2.)
        public async Task<GameQueueConfig[]> GetAvailableQueues()
        {
            var id = Invoke("matchmakerService", "getAvailableQueues", new object[] {});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var result = new GameQueueConfig[_results[id].GetTO("data").GetArray("body").Length];
            for (var i = 0; i < _results[id].GetTO("data").GetArray("body").Length; i++)
            {
                result[i] = new GameQueueConfig((TypedObject) _results[id].GetTO("data").GetArray("body")[i]);
            }
            _results.Remove(id);
            return result;
        }

        /// 3.)
        public void GetSumonerActiveBoosts(SummonerActiveBoostsDto.Callback callback)
        {
            var cb = new SummonerActiveBoostsDto(callback);
            InvokeWithCallback("inventoryService", "getSumonerActiveBoosts", new object[] {}, cb);
        }

        public async Task<SummonerActiveBoostsDto> GetSumonerActiveBoosts()
        {
            var id = Invoke("inventoryService", "getSumonerActiveBoosts", new object[] {});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new SummonerActiveBoostsDto(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 4.)
        public async Task<ChampionDto[]> GetAvailableChampions()
        {
            var id = Invoke("inventoryService", "getAvailableChampions", new object[] {});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var result = new ChampionDto[_results[id].GetTO("data").GetArray("body").Length];
            for (var i = 0; i < _results[id].GetTO("data").GetArray("body").Length; i++)
            {
                result[i] = new ChampionDto((TypedObject) _results[id].GetTO("data").GetArray("body")[i]);
            }
            _results.Remove(id);
            return result;
        }

        /// 5.)
        public void GetSummonerRuneInventory(Double summonerId, SummonerRuneInventory.Callback callback)
        {
            var cb = new SummonerRuneInventory(callback);
            InvokeWithCallback("summonerRuneService", "getSummonerRuneInventory", new object[] {summonerId}, cb);
        }

        public async Task<SummonerRuneInventory> GetSummonerRuneInventory(Double summonerId)
        {
            var id = Invoke("summonerRuneService", "getSummonerRuneInventory", new object[] {summonerId});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new SummonerRuneInventory(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 6.)
        public async Task<String> PerformLcdsHeartBeat(Int32 arg0, String arg1, Int32 arg2, String arg3)
        {
            var id = Invoke("loginService", "performLCDSHeartBeat", new object[] {arg0, arg1, arg2, arg3});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var result = (String) _results[id].GetTO("data")["body"];
            _results.Remove(id);
            return result;
        }

        /// 7.)
        public void GetMyLeaguePositions(SummonerLeagueItemsDto.Callback callback)
        {
            var cb = new SummonerLeagueItemsDto(callback);
            InvokeWithCallback("leaguesServiceProxy", "getMyLeaguePositions", new object[] {}, cb);
        }

        public async Task<SummonerLeagueItemsDto> GetMyLeaguePositions()
        {
            var id = Invoke("leaguesServiceProxy", "getMyLeaguePositions", new object[] {});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new SummonerLeagueItemsDto(messageBody);
            _results.Remove(id);
            return result;
        } /*
        public async Task<SummonerLeagueItemsDTO> GetMyLeaguePositionsAndProgress()
        {
            int Id = Invoke("leaguesServiceProxy", "getMyLeaguePositionsAndProgress", new object[] { });
            while (!results.ContainsKey(Id))
                await Task.Delay(10);
            TypedObject messageBody = results[Id].GetTO("data").GetTO("body");
            SummonerLeagueItemsDTO result = new SummonerLeagueItemsDTO(messageBody);
            results.Remove(Id);
            return result;
        }*/

        /// 8.)
        public async Task<object> LoadPreferencesByKey(String arg0, Double arg1, Boolean arg2)
        {
            var id = Invoke("playerPreferencesService", "loadPreferencesByKey", new object[] {arg0, arg1, arg2});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            _results.Remove(id);
            return null;
        }

        /// 9.)
        public void GetMasteryBook(Double summonerId, MasteryBookDto.Callback callback)
        {
            var cb = new MasteryBookDto(callback);
            InvokeWithCallback("masteryBookService", "getMasteryBook", new object[] {summonerId}, cb);
        }

        public async Task<MasteryBookDto> GetMasteryBook(Double summonerId)
        {
            var id = Invoke("masteryBookService", "getMasteryBook", new object[] {summonerId});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new MasteryBookDto(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 10.)
        public void CreatePlayer(PlayerDto.Callback callback)
        {
            var cb = new PlayerDto(callback);
            InvokeWithCallback("summonerTeamService", "createPlayer", new object[] {}, cb);
        }

        public async Task<PlayerDto> CreatePlayer()
        {
            var id = Invoke("summonerTeamService", "createPlayer", new object[] {});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new PlayerDto(messageBody);
            _results.Remove(id);
            return result;
        }

        public async Task<AllSummonerData> CreateDefaultSummoner(String summonerName)
        {
            var id = Invoke("summonerService", "createDefaultSummoner", new object[] {summonerName});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new AllSummonerData(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 11.)
        public async Task<String[]> GetSummonerNames(Double[] summonerIds)
        {
            var id = Invoke("summonerService", "getSummonerNames", new object[] {summonerIds.Cast<object>().ToArray()});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var result = new String[_results[id].GetTO("data").GetArray("body").Length];
            for (var i = 0; i < _results[id].GetTO("data").GetArray("body").Length; i++)
            {
                result[i] = (String) _results[id].GetTO("data").GetArray("body")[i];
            }
            _results.Remove(id);
            return result;
        }

        /// 12.)
        public void GetChallengerLeague(String queueType, LeagueListDto.Callback callback)
        {
            var cb = new LeagueListDto(callback);
            InvokeWithCallback("leaguesServiceProxy", "getChallengerLeague", new object[] {queueType}, cb);
        }

        public async Task<LeagueListDto> GetChallengerLeague(String queueType)
        {
            var id = Invoke("leaguesServiceProxy", "getChallengerLeague", new object[] {queueType});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new LeagueListDto(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 13.)
        public void GetAllMyLeagues(SummonerLeaguesDto.Callback callback)
        {
            var cb = new SummonerLeaguesDto(callback);
            InvokeWithCallback("leaguesServiceProxy", "getAllMyLeagues", new object[] {}, cb);
        }

        public async Task<SummonerLeaguesDto> GetAllMyLeagues()
        {
            var id = Invoke("leaguesServiceProxy", "getAllMyLeagues", new object[] {});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new SummonerLeaguesDto(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 14.)
        public void GetAllSummonerDataByAccount(Double accountId, AllSummonerData.Callback callback)
        {
            var cb = new AllSummonerData(callback);
            InvokeWithCallback("summonerService", "getAllSummonerDataByAccount", new object[] {accountId}, cb);
        }

        public async Task<AllSummonerData> GetAllSummonerDataByAccount(Double accountId)
        {
            var id = Invoke("summonerService", "getAllSummonerDataByAccount", new object[] {accountId});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new AllSummonerData(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 15.)
        public void GetPointsBalance(PointSummary.Callback callback)
        {
            var cb = new PointSummary(callback);
            InvokeWithCallback("lcdsRerollService", "getPointsBalance", new object[] {}, cb);
        }

        public async Task<PointSummary> GetPointsBalance()
        {
            var id = Invoke("lcdsRerollService", "getPointsBalance", new object[] {});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new PointSummary(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 16.)
        public async Task<String> GetSummonerIcons(Double[] summonerIds)
        {
            var id = Invoke("summonerService", "getSummonerIcons", new object[] {summonerIds.Cast<object>().ToArray()});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var result = (String) _results[id].GetTO("data")["body"];
            _results.Remove(id);
            return result;
        }

        /// 17.)
        public void CallKudos(String arg0, LcdsResponseString.Callback callback)
        {
            var cb = new LcdsResponseString(callback);
            InvokeWithCallback("clientFacadeService", "callKudos", new object[] {arg0}, cb);
        }

        public async Task<LcdsResponseString> CallKudos(String arg0)
        {
            var id = Invoke("clientFacadeService", "callKudos", new object[] {arg0});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new LcdsResponseString(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 18.)
        public void RetrievePlayerStatsByAccountId(Double accountId, String season,
            PlayerLifetimeStats.Callback callback)
        {
            var cb = new PlayerLifetimeStats(callback);
            InvokeWithCallback("playerStatsService", "retrievePlayerStatsByAccountId", new object[] {accountId, season},
                cb);
        }

        public async Task<PlayerLifetimeStats> RetrievePlayerStatsByAccountId(Double accountId, String season)
        {
            var id = Invoke("playerStatsService", "retrievePlayerStatsByAccountId", new object[] {accountId, season});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new PlayerLifetimeStats(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 19.)
        public async Task<ChampionStatInfo[]> RetrieveTopPlayedChampions(Double accountId, String gameMode)
        {
            var id = Invoke("playerStatsService", "retrieveTopPlayedChampions", new object[] {accountId, gameMode});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var result = new ChampionStatInfo[_results[id].GetTO("data").GetArray("body").Length];
            for (var i = 0; i < _results[id].GetTO("data").GetArray("body").Length; i++)
            {
                result[i] = new ChampionStatInfo((TypedObject) _results[id].GetTO("data").GetArray("body")[i]);
            }
            _results.Remove(id);
            return result;
        }

        /// 20.)
        public void GetSummonerByName(String summonerName, PublicSummoner.Callback callback)
        {
            var cb = new PublicSummoner(callback);
            InvokeWithCallback("summonerService", "getSummonerByName", new object[] {summonerName}, cb);
        }

        public async Task<PublicSummoner> GetSummonerByName(String summonerName)
        {
            var id = Invoke("summonerService", "getSummonerByName", new object[] {summonerName});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new PublicSummoner(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 21.)
        public void GetAggregatedStats(Double summonerId, String gameMode, String season,
            AggregatedStats.Callback callback)
        {
            var cb = new AggregatedStats(callback);
            InvokeWithCallback("playerStatsService", "getAggregatedStats", new object[] {summonerId, gameMode, season},
                cb);
        }

        public async Task<AggregatedStats> GetAggregatedStats(Double summonerId, String gameMode, String season)
        {
            var id = Invoke("playerStatsService", "getAggregatedStats", new object[] {summonerId, gameMode, season});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new AggregatedStats(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 22.)
        public void GetRecentGames(Double accountId, RecentGames.Callback callback)
        {
            var cb = new RecentGames(callback);
            InvokeWithCallback("playerStatsService", "getRecentGames", new object[] {accountId}, cb);
        }

        public async Task<RecentGames> GetRecentGames(Double accountId)
        {
            var id = Invoke("playerStatsService", "getRecentGames", new object[] {accountId});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new RecentGames(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 23.)
        public void FindTeamById(TeamId teamId, TeamDto.Callback callback)
        {
            var cb = new TeamDto(callback);
            InvokeWithCallback("summonerTeamService", "findTeamById", new object[] {teamId.GetBaseTypedObject()}, cb);
        }

        public async Task<TeamDto> FindTeamById(TeamId teamId)
        {
            var id = Invoke("summonerTeamService", "findTeamById", new object[] {teamId.GetBaseTypedObject()});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new TeamDto(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 24.)
        public void GetLeaguesForTeam(String teamName, SummonerLeaguesDto.Callback callback)
        {
            var cb = new SummonerLeaguesDto(callback);
            InvokeWithCallback("leaguesServiceProxy", "getLeaguesForTeam", new object[] {teamName}, cb);
        }

        public async Task<SummonerLeaguesDto> GetLeaguesForTeam(String teamName)
        {
            var id = Invoke("leaguesServiceProxy", "getLeaguesForTeam", new object[] {teamName});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new SummonerLeaguesDto(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 25.)
        public async Task<TeamAggregatedStatsDto[]> GetTeamAggregatedStats(TeamId arg0)
        {
            var id = Invoke("playerStatsService", "getTeamAggregatedStats", new object[] {arg0.GetBaseTypedObject()});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var result =
                new TeamAggregatedStatsDto[_results[id].GetTO("data").GetArray("body").Length];
            for (var i = 0; i < _results[id].GetTO("data").GetArray("body").Length; i++)
            {
                result[i] = new TeamAggregatedStatsDto((TypedObject) _results[id].GetTO("data").GetArray("body")[i]);
            }
            _results.Remove(id);
            return result;
        }

        /// 26.)
        public void GetTeamEndOfGameStats(TeamId arg0, Double arg1, EndOfGameStats.Callback callback)
        {
            var cb = new EndOfGameStats(callback);
            InvokeWithCallback("playerStatsService", "getTeamEndOfGameStats",
                new object[] {arg0.GetBaseTypedObject(), arg1}, cb);
        }

        public async Task<EndOfGameStats> GetTeamEndOfGameStats(TeamId arg0, Double arg1)
        {
            var id = Invoke("playerStatsService", "getTeamEndOfGameStats",
                new object[] {arg0.GetBaseTypedObject(), arg1});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new EndOfGameStats(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 27.)
        public async Task<object> DisbandTeam(TeamId teamId)
        {
            var id = Invoke("summonerTeamService", "disbandTeam", new object[] {teamId.GetBaseTypedObject()});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            _results.Remove(id);
            return null;
        }

        /// 28.)
        public async Task<Boolean> IsNameValidAndAvailable(String teamName)
        {
            var id = Invoke("summonerTeamService", "isNameValidAndAvailable", new object[] {teamName});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var result = (Boolean) _results[id].GetTO("data")["body"];
            _results.Remove(id);
            return result;
        }

        /// 29.)
        public async Task<Boolean> IsTagValidAndAvailable(String tagName)
        {
            var id = Invoke("summonerTeamService", "isTagValidAndAvailable", new object[] {tagName});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var result = (Boolean) _results[id].GetTO("data")["body"];
            _results.Remove(id);
            return result;
        }

        /// 30.)
        public void CreateTeam(String teamName, String tagName, TeamDto.Callback callback)
        {
            var cb = new TeamDto(callback);
            InvokeWithCallback("summonerTeamService", "createTeam", new object[] {teamName, tagName}, cb);
        }

        public async Task<TeamDto> CreateTeam(String teamName, String tagName)
        {
            var id = Invoke("summonerTeamService", "createTeam", new object[] {teamName, tagName});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new TeamDto(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 31.)
        public void InvitePlayer(Double summonerId, TeamId teamId, TeamDto.Callback callback)
        {
            var cb = new TeamDto(callback);
            InvokeWithCallback("summonerTeamService", "invitePlayer",
                new object[] {summonerId, teamId.GetBaseTypedObject()}, cb);
        }

        public async Task<TeamDto> InvitePlayer(Double summonerId, TeamId teamId)
        {
            var id = Invoke("summonerTeamService", "invitePlayer",
                new object[] {summonerId, teamId.GetBaseTypedObject()});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new TeamDto(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 32.)
        public void KickPlayer(Double summonerId, TeamId teamId, TeamDto.Callback callback)
        {
            var cb = new TeamDto(callback);
            InvokeWithCallback("summonerTeamService", "kickPlayer",
                new object[] {summonerId, teamId.GetBaseTypedObject()}, cb);
        }

        public async Task<TeamDto> KickPlayer(Double summonerId, TeamId teamId)
        {
            var id = Invoke("summonerTeamService", "kickPlayer", new object[] {summonerId, teamId.GetBaseTypedObject()});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new TeamDto(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 33.)
        public void GetAllLeaguesForPlayer(Double summonerId, SummonerLeaguesDto.Callback callback)
        {
            var cb = new SummonerLeaguesDto(callback);
            InvokeWithCallback("leaguesServiceProxy", "getAllLeaguesForPlayer", new object[] {summonerId}, cb);
        }

        public async Task<SummonerLeaguesDto> GetAllLeaguesForPlayer(Double summonerId)
        {
            var id = Invoke("leaguesServiceProxy", "getAllLeaguesForPlayer", new object[] {summonerId});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new SummonerLeaguesDto(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 34.)
        public void GetAllPublicSummonerDataByAccount(Double accountId, AllPublicSummonerDataDto.Callback callback)
        {
            var cb = new AllPublicSummonerDataDto(callback);
            InvokeWithCallback("summonerService", "getAllPublicSummonerDataByAccount", new object[] {accountId}, cb);
        }

        public async Task<AllPublicSummonerDataDto> GetAllPublicSummonerDataByAccount(Double accountId)
        {
            var id = Invoke("summonerService", "getAllPublicSummonerDataByAccount", new object[] {accountId});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new AllPublicSummonerDataDto(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 35.)
        public void FindPlayer(Double summonerId, PlayerDto.Callback callback)
        {
            var cb = new PlayerDto(callback);
            InvokeWithCallback("summonerTeamService", "findPlayer", new object[] {summonerId}, cb);
        }

        public async Task<PlayerDto> FindPlayer(Double summonerId)
        {
            var id = Invoke("summonerTeamService", "findPlayer", new object[] {summonerId});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new PlayerDto(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 36.)
        public void GetSpellBook(Double summonerId, SpellBookDto.Callback callback)
        {
            var cb = new SpellBookDto(callback);
            InvokeWithCallback("spellBookService", "getSpellBook", new object[] {summonerId}, cb);
        }

        public async Task<SpellBookDto> GetSpellBook(Double summonerId)
        {
            var id = Invoke("spellBookService", "getSpellBook", new object[] {summonerId});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new SpellBookDto(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 37.)
        public void AttachToQueue(MatchMakerParams matchMakerParams, SearchingForMatchNotification.Callback callback)
        {
            var cb = new SearchingForMatchNotification(callback);
            InvokeWithCallback("matchmakerService", "attachToQueue",
                new object[] {matchMakerParams.GetBaseTypedObject()}, cb);
        }

        public async Task<SearchingForMatchNotification> AttachToQueue(MatchMakerParams matchMakerParams)
        {
            var id = Invoke("matchmakerService", "attachToQueue", new object[] {matchMakerParams.GetBaseTypedObject()});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new SearchingForMatchNotification(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 38.)
        public async Task<Boolean> CancelFromQueueIfPossible(Int32 queueId)
        {
            var id = Invoke("matchmakerService", "cancelFromQueueIfPossible", new object[] {queueId});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var result = (Boolean) _results[id].GetTO("data")["body"];
            _results.Remove(id);
            return result;
        }

        /// 39.)
        public async Task<String> GetStoreUrl()
        {
            var id = Invoke("loginService", "getStoreUrl", new object[] {});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var result = (String) _results[id].GetTO("data")["body"];
            _results.Remove(id);
            return result;
        }

        /// 40.)
        public async Task<PracticeGameSearchResult[]> ListAllPracticeGames()
        {
            var id = Invoke("gameService", "listAllPracticeGames", new object[] {});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var result =
                new PracticeGameSearchResult[_results[id].GetTO("data").GetArray("body").Length];
            for (var i = 0; i < _results[id].GetTO("data").GetArray("body").Length; i++)
            {
                result[i] = new PracticeGameSearchResult((TypedObject) _results[id].GetTO("data").GetArray("body")[i]);
            }
            _results.Remove(id);
            return result;
        }

        /// 41.)
        public async Task<object> JoinGame(Double gameId)
        {
            var id = Invoke("gameService", "joinGame", new object[] {gameId, null});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            _results.Remove(id);
            return null;
        }

        public async Task<object> JoinGame(Double gameId, string password)
        {
            var id = Invoke("gameService", "joinGame", new object[] {gameId, password});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            _results.Remove(id);
            return null;
        }

        public async Task<object> ObserveGame(Double gameId)
        {
            var id = Invoke("gameService", "observeGame", new object[] {gameId, null});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            _results.Remove(id);
            return null;
        }

        public async Task<object> ObserveGame(Double gameId, string password)
        {
            var id = Invoke("gameService", "observeGame", new object[] {gameId, password});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            _results.Remove(id);
            return null;
        }

        /// 42.)
        public async Task<String> GetSummonerInternalNameByName(String summonerName)
        {
            var id = Invoke("summonerService", "getSummonerInternalNameByName", new object[] {summonerName});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);

            var result = (String) _results[id].GetTO("data")["body"];
            _results.Remove(id);
            return result;
        }

        /// 43.)
        public async Task<Boolean> SwitchTeams(Double gameId)
        {
            var id = Invoke("gameService", "switchTeams", new object[] {gameId});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);

            var result = (Boolean) _results[id].GetTO("data")["body"];
            _results.Remove(id);
            return result;
        }

        /// 44.)
        public async Task<Boolean> SwitchPlayerToObserver(Double gameId)
        {
            var id = Invoke("gameService", "switchPlayerToObserver", new object[] {gameId});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var result = (Boolean) _results[id].GetTO("data")["body"];
            _results.Remove(id);
            return result;
        }

        /// 44.)
        public async Task<Boolean> SwitchObserverToPlayer(Double gameId, Int32 team)
        {
            var id = Invoke("gameService", "switchObserverToPlayer", new object[] {gameId, team});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var result = (Boolean) _results[id].GetTO("data")["body"];
            _results.Remove(id);
            return result;
        }

        /// 45.)
        public async Task<object> QuitGame()
        {
            var id = Invoke("gameService", "quitGame", new object[] {});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            _results.Remove(id);
            return null;
        }

        /// 46.)
        public void CreatePracticeGame(PracticeGameConfig practiceGameConfig, GameDto.Callback callback)
        {
            var cb = new GameDto(callback);
            InvokeWithCallback("gameService", "createPracticeGame",
                new object[] {practiceGameConfig.GetBaseTypedObject()}, cb);
        }

        public async Task<GameDto> CreatePracticeGame(PracticeGameConfig practiceGameConfig)
        {
            var id = Invoke("gameService", "createPracticeGame", new object[] {practiceGameConfig.GetBaseTypedObject()});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new GameDto(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 47.)
        public async Task<object> SelectBotChampion(Int32 arg0, BotParticipant arg1)
        {
            var id = Invoke("gameService", "selectBotChampion", new object[] {arg0, arg1.GetBaseTypedObject()});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            _results.Remove(id);
            return null;
        }

        /// 48.)
        public async Task<object> RemoveBotChampion(Int32 arg0, BotParticipant arg1)
        {
            var id = Invoke("gameService", "removeBotChampion", new object[] {arg0, arg1.GetBaseTypedObject()});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            _results.Remove(id);
            return null;
        }

        /// 49.)
        public void StartChampionSelection(Double gameId, Double optomisticLock, StartChampSelectDto.Callback callback)
        {
            var cb = new StartChampSelectDto(callback);
            InvokeWithCallback("gameService", "startChampionSelection", new object[] {gameId, optomisticLock}, cb);
        }

        public async Task<StartChampSelectDto> StartChampionSelection(Double gameId, Double optomisticLock)
        {
            var id = Invoke("gameService", "startChampionSelection", new object[] {gameId, optomisticLock});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new StartChampSelectDto(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 50.)
        public async Task<object> SetClientReceivedGameMessage(Double gameId, String arg1)
        {
            var id = Invoke("gameService", "setClientReceivedGameMessage", new object[] {gameId, arg1});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            _results.Remove(id);
            return null;
        }

        /// 51.)
        public void GetLatestGameTimerState(Double arg0, String arg1, Int32 arg2, GameDto.Callback callback)
        {
            var cb = new GameDto(callback);
            InvokeWithCallback("gameService", "getLatestGameTimerState", new object[] {arg0, arg1, arg2}, cb);
        }

        public async Task<GameDto> GetLatestGameTimerState(Double arg0, String arg1, Int32 arg2)
        {
            var id = Invoke("gameService", "getLatestGameTimerState", new object[] {arg0, arg1, arg2});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new GameDto(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 52.)
        public async Task<object> SelectSpells(Int32 spellOneId, Int32 spellTwoId)
        {
            var id = Invoke("gameService", "selectSpells", new object[] {spellOneId, spellTwoId});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            _results.Remove(id);
            return null;
        }

        /// 53.)
        public void SelectDefaultSpellBookPage(SpellBookPageDto spellBookPage, SpellBookPageDto.Callback callback)
        {
            var cb = new SpellBookPageDto(callback);
            InvokeWithCallback("spellBookService", "selectDefaultSpellBookPage",
                new object[] {spellBookPage.GetBaseTypedObject()}, cb);
        }

        public async Task<SpellBookPageDto> SelectDefaultSpellBookPage(SpellBookPageDto spellBookPage)
        {
            var id = Invoke("spellBookService", "selectDefaultSpellBookPage",
                new object[] {spellBookPage.GetBaseTypedObject()});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new SpellBookPageDto(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 54.)
        public async Task<object> SelectChampion(Int32 championId)
        {
            var id = Invoke("gameService", "selectChampion", new object[] {championId});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            _results.Remove(id);
            return null;
        }

        /// 55.)
        public async Task<object> SelectChampionSkin(Int32 championId, Int32 skinId)
        {
            var id = Invoke("gameService", "selectChampionSkin", new object[] {championId, skinId});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            _results.Remove(id);
            return null;
        }

        /// 56.)
        public async Task<object> ChampionSelectCompleted()
        {
            var id = Invoke("gameService", "championSelectCompleted", new object[] {});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            _results.Remove(id);
            return null;
        }

        /// 57.)
        public async Task<object> SetClientReceivedMaestroMessage(Double arg0, String arg1)
        {
            var id = Invoke("gameService", "setClientReceivedMaestroMessage", new object[] {arg0, arg1});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            _results.Remove(id);
            return null;
        }

        /// 58.)
        public void RetrieveInProgressSpectatorGameInfo(String summonerName, PlatformGameLifecycleDto.Callback callback)
        {
            var cb = new PlatformGameLifecycleDto(callback);
            InvokeWithCallback("gameService", "retrieveInProgressSpectatorGameInfo", new object[] {summonerName}, cb);
        }

        public async Task<PlatformGameLifecycleDto> RetrieveInProgressSpectatorGameInfo(String summonerName)
        {
            var id = Invoke("gameService", "retrieveInProgressSpectatorGameInfo", new object[] {summonerName});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var messageBody = _results[id].GetTO("data").GetTO("body");
            var result = new PlatformGameLifecycleDto(messageBody);
            _results.Remove(id);
            return result;
        }

        /// 59.)
        public async Task<Boolean> DeclineObserverReconnect()
        {
            var id = Invoke("gameService", "declineObserverReconnect", new object[] {});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var result = (Boolean) _results[id].GetTO("data")["body"];
            _results.Remove(id);
            return result;
        }

        public async Task<object> AcceptInviteForMatchmakingGame(double gameId)
        {
            var id = Invoke("matchmakerService", "acceptInviteForMatchmakingGame", new object[] {gameId});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            _results.Remove(id);
            return null;
        }

        public async Task<object> AcceptPoppedGame(bool accept)
        {
            var id = Invoke("gameService", "acceptPoppedGame", new object[] {accept});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            _results.Remove(id);
            return null;
        }

        public async Task<object> UpdateProfileIconId(Int32 iconId)
        {
            var id = Invoke("summonerService", "updateProfileIconId", new object[] {iconId});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            _results.Remove(id);
            return null;
        }

        public async Task<object> BanUserFromGame(double gameId, double accountId)
        {
            var id = Invoke("gameService", "banUserFromGame", new object[] {gameId, accountId});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            _results.Remove(id);
            return null;
        }

        public async Task<object> BanObserverFromGame(double gameId, double accountId)
        {
            var id = Invoke("gameService", "banObserverFromGame", new object[] {gameId, accountId});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            _results.Remove(id);
            return null;
        }

        public async Task<object> BanChampion(int championId)
        {
            var id = Invoke("gameService", "banChampion", new object[] {championId});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            _results.Remove(id);
            return null;
        }

        public async Task<ChampionBanInfoDto[]> GetChampionsForBan()
        {
            var id = Invoke("gameService", "getChampionsForBan", new object[] {});
            while (!_results.ContainsKey(id))
                await Task.Delay(10);
            var result = new ChampionBanInfoDto[_results[id].GetTO("data").GetArray("body").Length];
            for (var i = 0; i < _results[id].GetTO("data").GetArray("body").Length; i++)
            {
                result[i] = new ChampionBanInfoDto((TypedObject) _results[id].GetTO("data").GetArray("body")[i]);
            }
            _results.Remove(id);
            return result;
        }
    }
}