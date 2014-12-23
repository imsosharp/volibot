#region

using LoLLauncher;
using LoLLauncher.RiotObjects;

#endregion

namespace PVPNetConnect.RiotObjects
{
    /// <summary>
    ///     Unclassed Riot Object
    /// </summary>
    public class UnclassedObject : RiotGamesObject
    {
        /// <summary>
        /// </summary>
        /// <param name="result">The TypedObject result or return packet result.</param>
        public delegate void Callback(TypedObject result);

        /// <summary>
        ///     The callback method.
        /// </summary>
        private readonly Callback _callback;

        private readonly string _type = "";

        /// <summary>
        ///     Initializes a new instance of the <see cref="UnclassedObject" /> class.
        /// </summary>
        /// <param name="callback">The callback method.</param>
        public UnclassedObject(Callback callback)
        {
            this._callback = callback;
        }

        public override string TypeName
        {
            get { return _type; }
        }

        /// <summary>
        ///     Does the callback.
        /// </summary>
        /// <param name="result">The TypedObject result or return packet result.</param>
        public override void DoCallback(TypedObject result)
        {
            _callback(result);
        }
    }
}