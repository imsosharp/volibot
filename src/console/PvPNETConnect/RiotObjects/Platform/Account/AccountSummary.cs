#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Account
{
    public class AccountSummary : RiotGamesObject
    {
        public delegate void Callback(AccountSummary result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.account.AccountSummary";

        public AccountSummary()
        {
        }

        public AccountSummary(Callback callback)
        {
            this._callback = callback;
        }

        public AccountSummary(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("groupCount")]
        public Int32 GroupCount { get; set; }

        [InternalName("username")]
        public String Username { get; set; }

        [InternalName("accountId")]
        public Double AccountId { get; set; }

        [InternalName("summonerInternalName")]
        public object SummonerInternalName { get; set; }

        [InternalName("admin")]
        public Boolean Admin { get; set; }

        [InternalName("hasBetaAccess")]
        public Boolean HasBetaAccess { get; set; }

        [InternalName("summonerName")]
        public object SummonerName { get; set; }

        [InternalName("partnerMode")]
        public Boolean PartnerMode { get; set; }

        [InternalName("needsPasswordReset")]
        public Boolean NeedsPasswordReset { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}