#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Game
{
    public class PlayerCredentialsDto : RiotGamesObject
    {
        public delegate void Callback(PlayerCredentialsDto result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.game.PlayerCredentialsDto";

        public PlayerCredentialsDto()
        {
        }

        public PlayerCredentialsDto(Callback callback)
        {
            this._callback = callback;
        }

        public PlayerCredentialsDto(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("encryptionKey")]
        public String EncryptionKey { get; set; }

        [InternalName("gameId")]
        public Double GameId { get; set; }

        [InternalName("lastSelectedSkinIndex")]
        public Int32 LastSelectedSkinIndex { get; set; }

        [InternalName("serverIp")]
        public String ServerIp { get; set; }

        [InternalName("observer")]
        public Boolean Observer { get; set; }

        [InternalName("summonerId")]
        public Double SummonerId { get; set; }

        [InternalName("observerServerIp")]
        public String ObserverServerIp { get; set; }

        [InternalName("handshakeToken")]
        public String HandshakeToken { get; set; }

        [InternalName("playerId")]
        public Double PlayerId { get; set; }

        [InternalName("serverPort")]
        public Int32 ServerPort { get; set; }

        [InternalName("observerServerPort")]
        public Int32 ObserverServerPort { get; set; }

        [InternalName("summonerName")]
        public String SummonerName { get; set; }

        [InternalName("observerEncryptionKey")]
        public String ObserverEncryptionKey { get; set; }

        [InternalName("championId")]
        public Int32 ChampionId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}