#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Platform.Summoner
{
    public class BasePublicSummonerDto : RiotGamesObject
    {
        public delegate void Callback(BasePublicSummonerDto result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.platform.summoner.BasePublicSummonerDTO";

        public BasePublicSummonerDto()
        {
        }

        public BasePublicSummonerDto(Callback callback)
        {
            this._callback = callback;
        }

        public BasePublicSummonerDto(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("seasonTwoTier")]
        public String SeasonTwoTier { get; set; }

        [InternalName("internalName")]
        public String InternalName { get; set; }

        [InternalName("seasonOneTier")]
        public String SeasonOneTier { get; set; }

        [InternalName("acctId")]
        public Double AcctId { get; set; }

        [InternalName("name")]
        public String Name { get; set; }

        [InternalName("sumId")]
        public Double SumId { get; set; }

        [InternalName("profileIconId")]
        public Int32 ProfileIconId { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}