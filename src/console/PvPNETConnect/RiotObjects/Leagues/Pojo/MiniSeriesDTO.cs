#region

using System;

#endregion

namespace LoLLauncher.RiotObjects.Leagues.Pojo
{
    public class MiniSeriesDto : RiotGamesObject
    {
        public delegate void Callback(MiniSeriesDto result);

        private readonly Callback _callback;
        private readonly string _type = "com.riotgames.leagues.pojo.MiniSeriesDTO";

        public MiniSeriesDto()
        {
        }

        public MiniSeriesDto(Callback callback)
        {
            this._callback = callback;
        }

        public MiniSeriesDto(TypedObject result)
        {
            SetFields(this, result);
        }

        public override string TypeName
        {
            get { return _type; }
        }

        [InternalName("progress")]
        public String Progress { get; set; }

        [InternalName("target")]
        public Int32 Target { get; set; }

        [InternalName("losses")]
        public Int32 Losses { get; set; }

        [InternalName("timeLeftToPlayMillis")]
        public Double TimeLeftToPlayMillis { get; set; }

        [InternalName("wins")]
        public Int32 Wins { get; set; }

        public override void DoCallback(TypedObject result)
        {
            SetFields(this, result);
            _callback(this);
        }
    }
}