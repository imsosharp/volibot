#region

using System;
using LoLLauncher.RiotObjects.Platform.Account;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Login
{
    public class Session : RiotGamesObject
    {
        public delegate void Callback(Session result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.login.Session";

        public Session()
        {
        }

        public Session(Callback callback)
        {
            this._callback = callback;
        }

        public Session(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("token")]
        public String Token { get; set; }

        [InternalName("password")]
        public String Password { get; set; }

        [InternalName("accountSummary")]
        public AccountSummary AccountSummary { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}