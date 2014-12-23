#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;
using LoLLauncher.RiotObjects;
using LoLLauncher.RiotObjects.Platform.Game;
using LoLLauncher.RiotObjects.Platform.Game.Message;
using LoLLauncher.RiotObjects.Platform.Login;
using LoLLauncher.RiotObjects.Platform.Matchmaking;
using LoLLauncher.RiotObjects.Platform.Messaging;
using RitoBot;

#endregion

namespace LoLLauncher
{
    public partial class LoLConnection
    {
        #region Disconnect Methods

        public void Disconnect()
        {
            var t = new Thread(() =>
            {
                if (_isConnected)
                {
                    var id = Invoke("loginService", "logout", new object[] {_authToken});
                    Join(id);
                }

                _isConnected = false;

                if (HeartbeatThread != null)
                    HeartbeatThread.Abort();

                if (DecodeThread != null)
                    DecodeThread.Abort();

                _invokeId = 2;
                _heartbeatCount = 1;
                _pendingInvokes.Clear();
                _callbacks.Clear();
                _results.Clear();

                _client = null;
                _sslStream = null;

                if (OnDisconnect != null)
                    OnDisconnect(this, EventArgs.Empty);
            });

            t.Start();
        }

        #endregion

        #region Member Declarations

        //RTMPS Connection Info
        private bool _isConnected;
        private bool _isLoggedIn;
        private TcpClient _client;
        private SslStream _sslStream;
        private string _ipAddress;
        private string _authToken;
        private int _accountId;
        private string _sessionToken;
        private string _dsId;

        //Initial Login Information
        private string _user;
        private string _password;
        private string _server;
        private string _loginQueue;
        private string _locale;
        private string _clientVersion;

        /** Garena information */
        private bool _useGarena;
        private string _garenaToken;
        private string _userId;

        //Invoke Variables
        private readonly Random _rand = new Random();
        private readonly JavaScriptSerializer _serializer = new JavaScriptSerializer();

        private int _invokeId = 2;

        private readonly List<int> _pendingInvokes = new List<int>();
        private readonly Dictionary<int, TypedObject> _results = new Dictionary<int, TypedObject>();
        private readonly Dictionary<int, RiotGamesObject> _callbacks = new Dictionary<int, RiotGamesObject>();
        public Thread DecodeThread;

        private int _heartbeatCount = 1;
        public Thread HeartbeatThread;
        private readonly Object _isInvokingLock = new Object();

        #endregion

        #region Event Handlers

        public delegate void OnConnectHandler(object sender, EventArgs e);

        public event OnConnectHandler OnConnect;

        public delegate void OnLoginQueueUpdateHandler(object sender, int positionInLine);

        public event OnLoginQueueUpdateHandler OnLoginQueueUpdate;

        public delegate void OnLoginHandler(object sender, string username, string ipAddress);

        public event OnLoginHandler OnLogin;

        public delegate void OnDisconnectHandler(object sender, EventArgs e);

        public event OnDisconnectHandler OnDisconnect;

        public delegate void OnMessageReceivedHandler(object sender, object message);

        public event OnMessageReceivedHandler OnMessageReceived;

        public delegate void OnErrorHandler(object sender, Error error);

        public event OnErrorHandler OnError;

        #endregion

        #region Connect, Login, and Heartbeat Methods

        public void Connect(string user, string password, Region region, string clientVersion)
        {
            if (!_isConnected)
            {
                var t = new Thread(() =>
                {
                    this._user = user;
                    this._password = password;
                    this._clientVersion = clientVersion;
                    //this.server = "127.0.0.1";
                    _server = RegionInfo.GetServerValue(region);
                    _loginQueue = RegionInfo.GetLoginQueueValue(region);
                    _locale = RegionInfo.GetLocaleValue(region);
                    _useGarena = RegionInfo.GetUseGarenaValue(region);

                    //Sets up our sslStream to riots servers
                    try
                    {
                        _client = new TcpClient(_server, 2099);
                    }
                    catch
                    {
                        Error("Riots servers are currently unavailable.", ErrorType.AuthKey);
                        Disconnect();
                        return;
                    }

                    //Check for riot webserver status
                    //along with gettin out Auth Key that we need for the login process.
                    if (_useGarena)
                        if (!GetGarenaToken())
                            return;

                    if (!GetAuthKey())
                        return;

                    if (!GetIpAddress())
                        return;

                    _sslStream = new SslStream(_client.GetStream(), false, AcceptAllCertificates);
                    var ar = _sslStream.BeginAuthenticateAsClient(_server, null, null);
                    using (ar.AsyncWaitHandle)
                    {
                        if (ar.AsyncWaitHandle.WaitOne(-1))
                        {
                            _sslStream.EndAuthenticateAsClient(ar);
                        }
                    }

                    if (!Handshake())
                        return;

                    BeginReceive();

                    if (!SendConnect())
                        return;

                    if (!Login())
                        return;
                    StartHeartbeat();
                });

                t.Start();
            }
        }

        private bool AcceptAllCertificates(object sender, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private bool GetGarenaToken()
        {
            /*
            try
            {
                System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

                //GET OUR USER ID
                List<byte> userIdRequestBytes = new List<byte>();

                byte[] junk = new byte[] { 0x49, 0x00, 0x00, 0x00, 0x10, 0x01, 0x00, 0x79, 0x2f };
                userIdRequestBytes.AddRange(junk);
                userIdRequestBytes.AddRange(encoding.GetBytes(user));
                for (int i = 0; i < 16; i++)
                    userIdRequestBytes.Add(0x00);

                System.Security.Cryptography.MD5 md5Cryp = System.Security.Cryptography.MD5.Create();
                byte[] inputBytes = encoding.GetBytes(password);
                byte[] md5 = md5Cryp.ComputeHash(inputBytes);

                foreach (byte b in md5)
                    userIdRequestBytes.AddRange(encoding.GetBytes(String.Format("%02x", b)));

                userIdRequestBytes.Add(0x00);
                userIdRequestBytes.Add(0x01);
                junk = new byte[] { 0xD4, 0xAE, 0x52, 0xC0, 0x2E, 0xBA, 0x72, 0x03 };
                userIdRequestBytes.AddRange(junk);

                int timestamp = (int)(DateTime.UtcNow.TimeOfDay.TotalMilliseconds / 1000);
                for (int i = 0; i < 4; i++)
                    userIdRequestBytes.Add((byte)((timestamp >> (8 * i)) & 0xFF));

                userIdRequestBytes.Add(0x00);
                userIdRequestBytes.AddRange(encoding.GetBytes("intl"));
                userIdRequestBytes.Add(0x00);

                byte[] userIdBytes = userIdRequestBytes.ToArray();

                TcpClient client = new TcpClient("203.117.158.170", 9100);
                client.GetStream().Write(userIdBytes, 0, userIdBytes.Length);
                client.GetStream().Flush();

                int id = 0;
                for (int i = 0; i < 4; i++)
                    id += client.GetStream().ReadByte() * (1 << (8 * i));

                userID = Convert.ToString(id);


                //GET TOKEN
                List<byte> tokenRequestBytes = new List<byte>();
                junk = new byte[] { 0x32, 0x00, 0x00, 0x00, 0x01, 0x03, 0x80, 0x00, 0x00 };
                tokenRequestBytes.AddRange(junk);
                tokenRequestBytes.AddRange(encoding.GetBytes(user));
                tokenRequestBytes.Add(0x00);
                foreach (byte b in md5)
                    tokenRequestBytes.AddRange(encoding.GetBytes(String.Format("%02x", b)));
                tokenRequestBytes.Add(0x00);
                tokenRequestBytes.Add(0x00);
                tokenRequestBytes.Add(0x00);

                byte[] tokenBytes = tokenRequestBytes.ToArray();

                client = new TcpClient("lol.auth.garenanow.com", 12000);
                client.GetStream().Write(tokenBytes, 0, tokenBytes.Length);
                client.GetStream().Flush();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < 5; i++)
                    client.GetStream().ReadByte();
                int c;
                while ((c = client.GetStream().ReadByte()) != 0)
                    sb.Append((char)c);

                garenaToken = sb.ToString();

                client.Close();
                return true;
            }
            catch
            {
                Error("Unable to acquire garena token", ErrorType.Login);
                Disconnect();
                return false;
            }
             */

            Error("Garena Servers are not yet supported", ErrorType.Login);
            Disconnect();
            return false;
        }

        private bool GetAuthKey()
        {
            try
            {
                var sb = new StringBuilder();
                var payload = "user=" + _user + ",password=" + _password;
                var query = "payload=" + payload;

                if (_useGarena)
                    payload = _garenaToken;

                var con = WebRequest.Create(_loginQueue + "login-queue/rest/queue/authenticate");
                con.Method = "POST";

                var outputStream = con.GetRequestStream();
                outputStream.Write(Encoding.ASCII.GetBytes(query), 0, Encoding.ASCII.GetByteCount(query));

                var webresponse = con.GetResponse();
                var inputStream = webresponse.GetResponseStream();

                int c;
                while ((c = inputStream.ReadByte()) != -1)
                    sb.Append((char) c);

                var result = _serializer.Deserialize<TypedObject>(sb.ToString());
                outputStream.Close();
                inputStream.Close();
                con.Abort();

                if (!result.ContainsKey("token"))
                {
                    var node = (int) result.GetInt("node");
                    var champ = result.GetString("champ");
                    var rate = (int) result.GetInt("rate");
                    var delay = (int) result.GetInt("delay");

                    var id = 0;
                    var cur = 0;

                    var tickers = result.GetArray("tickers");
                    foreach (var o in tickers)
                    {
                        var to = (Dictionary<string, object>) o;

                        var tnode = (int) to["node"];
                        if (tnode != node)
                            continue;

                        id = (int) to["id"];
                        cur = (int) to["current"];
                        break;
                    }

                    while (id - cur > rate)
                    {
                        sb.Clear();
                        if (OnLoginQueueUpdate != null)
                            OnLoginQueueUpdate(this, id - cur);

                        Thread.Sleep(delay);
                        con = WebRequest.Create(_loginQueue + "login-queue/rest/queue/ticker/" + champ);
                        con.Method = "GET";
                        webresponse = con.GetResponse();
                        inputStream = webresponse.GetResponseStream();

                        int d;
                        while ((d = inputStream.ReadByte()) != -1)
                            sb.Append((char) d);

                        result = _serializer.Deserialize<TypedObject>(sb.ToString());


                        inputStream.Close();
                        con.Abort();

                        if (result == null)
                            continue;

                        cur = HexToInt(result.GetString(node.ToString()));
                    }


                    while (sb.ToString() == null || !result.ContainsKey("token"))
                    {
                        try
                        {
                            sb.Clear();

                            if (id - cur < 0)
                                if (OnLoginQueueUpdate != null)
                                    OnLoginQueueUpdate(this, 0);
                                else if (OnLoginQueueUpdate != null)
                                    OnLoginQueueUpdate(this, id - cur);

                            Thread.Sleep(delay/10);
                            con = WebRequest.Create(_loginQueue + "login-queue/rest/queue/authToken/" + _user.ToLower());
                            con.Method = "GET";
                            webresponse = con.GetResponse();
                            inputStream = webresponse.GetResponseStream();

                            int f;
                            while ((f = inputStream.ReadByte()) != -1)
                                sb.Append((char) f);

                            result = _serializer.Deserialize<TypedObject>(sb.ToString());

                            inputStream.Close();
                            con.Abort();
                        }
                        catch
                        {
                        }
                    }
                }
                if (OnLoginQueueUpdate != null)
                    OnLoginQueueUpdate(this, 0);
                _authToken = result.GetString("token");

                return true;
            }
            catch (Exception e)
            {
                if (e.Message == "The remote name could not be resolved: '" + _loginQueue + "'")
                {
                    Error("Please make sure you are connected the internet!", ErrorType.AuthKey);
                    Disconnect();
                }
                else if (e.Message == "The remote server returned an error: (403) Forbidden.")
                {
                    Error("Your username or password is incorrect!", ErrorType.Password);
                    Disconnect();
                }
                else
                {
                    Error("Unable to get Auth Key \n" + e, ErrorType.AuthKey);
                    Disconnect();
                }

                return false;
            }
        }

        private int HexToInt(string hex)
        {
            var total = 0;
            for (var i = 0; i < hex.Length; i++)
            {
                var c = hex.ToCharArray()[i];
                if (c >= '0' && c <= '9')
                    total = total*16 + c - '0';
                else
                    total = total*16 + c - 'a' + 10;
            }

            return total;
        }

        private bool GetIpAddress()
        {
            try
            {
                var sb = new StringBuilder();

                var con = WebRequest.Create("http://ll.leagueoflegends.com/services/connection_info");
                var response = con.GetResponse();

                int c;
                while ((c = response.GetResponseStream().ReadByte()) != -1)
                    sb.Append((char) c);

                con.Abort();

                var result = _serializer.Deserialize<TypedObject>(sb.ToString());

                _ipAddress = result.GetString("ip_address");

                return true;
            }
            catch (Exception e)
            {
                Error("Unable to connect to Riot Games web server \n" + e.Message, ErrorType.General);
                Disconnect();
                return false;
            }
        }

        private bool Handshake()
        {
            var handshakePacket = new byte[1537];
            _rand.NextBytes(handshakePacket);
            handshakePacket[0] = 0x03;
            _sslStream.Write(handshakePacket);

            var s0 = (byte) _sslStream.ReadByte();
            if (s0 != 0x03)
            {
                Error("Server returned incorrect version in handshake: " + s0, ErrorType.Handshake);
                Disconnect();
                return false;
            }


            var responsePacket = new byte[1536];
            _sslStream.Read(responsePacket, 0, 1536);
            _sslStream.Write(responsePacket);

            // Wait for response and discard result
            var s2 = new byte[1536];
            _sslStream.Read(s2, 0, 1536);

            // Validate handshake
            var valid = true;
            for (var i = 8; i < 1536; i++)
            {
                if (handshakePacket[i + 1] != s2[i])
                {
                    valid = false;
                    break;
                }
            }

            if (!valid)
            {
                Error("Server returned invalid handshake", ErrorType.Handshake);
                Disconnect();
                return false;
            }
            return true;
        }

        private bool SendConnect()
        {
            var paramaters = new Dictionary<string, object>();
            paramaters.Add("app", "");
            paramaters.Add("flashVer", "WIN 10,6,602,161");
            paramaters.Add("swfUrl", "app:/LolClient.swf/[[DYNAMIC]]/32");
            paramaters.Add("tcUrl", "rtmps://" + _server + ":" + 2099);
            paramaters.Add("fpad", false);
            paramaters.Add("capabilities", 239);
            paramaters.Add("audioCodecs", 3575);
            paramaters.Add("videoCodecs", 252);
            paramaters.Add("videoFunction", 1);
            paramaters.Add("pageUrl", null);
            paramaters.Add("objectEncoding", 3);

            var encoder = new RtmpsEncoder();
            var connect = encoder.EncodeConnect(paramaters);

            _sslStream.Write(connect, 0, connect.Length);

            while (!_results.ContainsKey(1))
                Thread.Sleep(10);
            var result = _results[1];
            _results.Remove(1);
            if (result["result"].Equals("_error"))
            {
                Error(GetErrorMessage(result), ErrorType.Connect);
                Disconnect();
                return false;
            }

            _dsId = result.GetTO("data").GetString("id");

            _isConnected = true;
            if (OnConnect != null)
                OnConnect(this, EventArgs.Empty);

            return true;
        }

        private bool Login()
        {
            TypedObject result, body;

            // Login 1
            var cred = new AuthenticationCredentials();
            cred.Password = _password;
            cred.ClientVersion = _clientVersion;
            cred.IpAddress = _ipAddress;
            cred.SecurityAnswer = null;
            cred.Locale = _locale;
            cred.Domain = "lolclient.lol.riotgames.com";
            cred.OldPassword = null;
            cred.AuthToken = _authToken;

            if (_useGarena)
            {
                cred.PartnerCredentials = "8393 " + _garenaToken;
                cred.Username = _userId;
            }
            else
            {
                cred.PartnerCredentials = null;
                cred.Username = _user;
            }
            var id = Invoke("loginService", "login", new object[] {cred.GetBaseTypedObject()});

            result = GetResult(id);
            if (result["result"].Equals("_error"))
            {
                if (Program.AutoUpdate)
                {
                    var newVersion =
                        (string) result.GetTO("data").GetTO("rootCause").GetArray("substitutionArguments")[1];
                    if (newVersion != Program.Cversion)
                    {
                        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "config\\version.txt"))
                        {
                            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "config\\version.txt");
                        }
                        var newcversion = File.CreateText("config\\version.txt");
                        newcversion.Write(newVersion);
                    }
                    Error("Volibot updated for version " + newVersion + ". Please restart.", ErrorType.General);
                }
                else
                {
                    Error(GetErrorMessage(result), ErrorType.Login);
                }
                Disconnect();
                return false;
            }

            body = result.GetTO("data").GetTO("body");
            _sessionToken = body.GetString("token");
            _accountId = (int) body.GetTO("accountSummary").GetInt("accountId");

            // Login 2

            if (_useGarena)
                body = WrapBody(Convert.ToBase64String(Encoding.UTF8.GetBytes(_userId + ":" + _sessionToken)), "auth", 8);
            else
                body = WrapBody(Convert.ToBase64String(Encoding.UTF8.GetBytes(_user.ToLower() + ":" + _sessionToken)),
                    "auth", 8);

            body.Type = "flex.messaging.messages.CommandMessage";

            id = Invoke(body);
            result = GetResult(id); // Read result and discard

            _isLoggedIn = true;
            if (OnLogin != null)
                OnLogin(this, _user, _ipAddress);
            return true;
        }

        private string GetErrorMessage(TypedObject message)
        {
            // Works for clientVersion
            return message.GetTO("data").GetTO("rootCause").GetString("message");
        }

        private string GetErrorCode(TypedObject message)
        {
            return message.GetTO("data").GetTO("rootCause").GetString("errorCode");
        }


        private void StartHeartbeat()
        {
            HeartbeatThread = new Thread(async () =>
            {
                while (true)
                {
                    try
                    {
                        var hbTime = (long) DateTime.Now.TimeOfDay.TotalMilliseconds;
                        var result =
                            await
                                PerformLcdsHeartBeat(_accountId, _sessionToken, _heartbeatCount,
                                    DateTime.Now.ToString("ddd MMM d yyyy HH:mm:ss 'GMT-0700'"));
                        //int id = Invoke("loginService", "performLCDSHeartBeat", new object[] { accountID, sessionToken, heartbeatCount, DateTime.Now.ToString("ddd MMM d yyyy HH:mm:ss 'GMT-0700'") });
                        //Cancel(id); // Ignore result for now

                        _heartbeatCount++;

                        // Quick sleeps to shutdown the heartbeat quickly on a reconnect
                        while ((long) DateTime.Now.TimeOfDay.TotalMilliseconds - hbTime < 120000)
                            Thread.Sleep(100);
                    }
                    catch
                    {
                    }
                }
            });
            HeartbeatThread.Start();
        }

        #endregion

        #region Error Methods

        private void Error(string message, string errorCode, ErrorType type)
        {
            var error = new Error
            {
                Type = type,
                Message = message,
                ErrorCode = errorCode
            };

            if (OnError != null)
                OnError(this, error);
        }

        private void Error(string message, ErrorType type)
        {
            Error(message, "", type);
        }

        #endregion

        #region Send Methods

        private int Invoke(TypedObject packet)
        {
            lock (_isInvokingLock)
            {
                var id = NextInvokeId();
                _pendingInvokes.Add(id);

                try
                {
                    var encoder = new RtmpsEncoder();
                    var data = encoder.EncodeInvoke(id, packet);

                    _sslStream.Write(data, 0, data.Length);

                    return id;
                }
                catch (IOException e)
                {
                    // Clear the pending invoke
                    _pendingInvokes.Remove(id);

                    // Rethrow
                    throw e;
                }
            }
        }

        private int Invoke(string destination, object operation, object body)
        {
            return Invoke(WrapBody(body, destination, operation));
        }

        private int InvokeWithCallback(string destination, object operation, object body, RiotGamesObject cb)
        {
            if (_isConnected)
            {
                _callbacks.Add(_invokeId, cb); // Register the callback
                return Invoke(destination, operation, body);
            }
            Error(
                "The client is not connected. Please make sure to connect before tring to execute an Invoke command.",
                ErrorType.Invoke);
            Disconnect();
            return -1;
        }

        protected TypedObject WrapBody(object body, string destination, object operation)
        {
            var headers = new TypedObject();
            headers.Add("DSRequestTimeout", 60);
            headers.Add("DSId", _dsId);
            headers.Add("DSEndpoint", "my-rtmps");

            var ret = new TypedObject("flex.messaging.messages.RemotingMessage");
            ret.Add("operation", operation);
            ret.Add("source", null);
            ret.Add("timestamp", 0);
            ret.Add("messageId", RtmpsEncoder.RandomUid());
            ret.Add("timeToLive", 0);
            ret.Add("clientId", null);
            ret.Add("destination", destination);
            ret.Add("body", body);
            ret.Add("headers", headers);

            return ret;
        }

        protected int NextInvokeId()
        {
            return _invokeId++;
        }

        #endregion

        #region Receive Methods

        private void MessageReceived(object messageBody)
        {
            if (OnMessageReceived != null)
                OnMessageReceived(this, messageBody);
        }

        private void BeginReceive()
        {
            DecodeThread = new Thread(() =>
            {
                try
                {
                    var previousReceivedPacket = new Dictionary<int, Packet>();
                    var currentPackets = new Dictionary<int, Packet>();

                    while (true)
                    {
                        #region Basic Header

                        var basicHeader = (byte) _sslStream.ReadByte();
                        var basicHeaderStorage = new List<byte>();
                        if (basicHeader == 255)
                            Disconnect();

                        var channel = 0;
                        //1 Byte Header
                        if ((basicHeader & 0x03) != 0)
                        {
                            channel = basicHeader & 0x3F;
                            basicHeaderStorage.Add(basicHeader);
                        }
                        //2 Byte Header
                        else if ((basicHeader & 0x01) != 0)
                        {
                            var byte2 = (byte) _sslStream.ReadByte();
                            channel = 64 + byte2;
                            basicHeaderStorage.Add(basicHeader);
                            basicHeaderStorage.Add(byte2);
                        }
                        //3 Byte Header
                        else if ((basicHeader & 0x02) != 0)
                        {
                            var byte2 = (byte) _sslStream.ReadByte();
                            var byte3 = (byte) _sslStream.ReadByte();
                            basicHeaderStorage.Add(basicHeader);
                            basicHeaderStorage.Add(byte2);
                            basicHeaderStorage.Add(byte3);
                            channel = 64 + byte2 + (256*byte3);
                        }

                        #endregion

                        #region Message Header

                        var headerType = (basicHeader & 0xC0);
                        var headerSize = 0;
                        if (headerType == 0x00)
                            headerSize = 12;
                        else if (headerType == 0x40)
                            headerSize = 8;
                        else if (headerType == 0x80)
                            headerSize = 4;
                        else if (headerType == 0xC0)
                            headerSize = 0;

                        // Retrieve the packet or make a new one
                        if (!currentPackets.ContainsKey(channel))
                        {
                            currentPackets.Add(channel, new Packet());
                        }

                        var p = currentPackets[channel];
                        p.AddToRaw(basicHeaderStorage.ToArray());

                        if (headerSize == 12)
                        {
                            //Timestamp
                            var timestamp = new byte[3];
                            for (var i = 0; i < 3; i++)
                            {
                                timestamp[i] = (byte) _sslStream.ReadByte();
                                p.AddToRaw(timestamp[i]);
                            }

                            //Message Length
                            var messageLength = new byte[3];
                            for (var i = 0; i < 3; i++)
                            {
                                messageLength[i] = (byte) _sslStream.ReadByte();
                                p.AddToRaw(messageLength[i]);
                            }
                            var size = 0;
                            for (var i = 0; i < 3; i++)
                                size = size*256 + (messageLength[i] & 0xFF);
                            p.SetSize(size);

                            //Message Type
                            var messageType = _sslStream.ReadByte();
                            p.AddToRaw((byte) messageType);
                            p.SetType(messageType);

                            //Message Stream ID
                            var messageStreamId = new byte[4];
                            for (var i = 0; i < 4; i++)
                            {
                                messageStreamId[i] = (byte) _sslStream.ReadByte();
                                p.AddToRaw(messageStreamId[i]);
                            }
                        }
                        else if (headerSize == 8)
                        {
                            //Timestamp
                            var timestamp = new byte[3];
                            for (var i = 0; i < 3; i++)
                            {
                                timestamp[i] = (byte) _sslStream.ReadByte();
                                p.AddToRaw(timestamp[i]);
                            }

                            //Message Length
                            var messageLength = new byte[3];
                            for (var i = 0; i < 3; i++)
                            {
                                messageLength[i] = (byte) _sslStream.ReadByte();
                                p.AddToRaw(messageLength[i]);
                            }
                            var size = 0;
                            for (var i = 0; i < 3; i++)
                                size = size*256 + (messageLength[i] & 0xFF);
                            p.SetSize(size);

                            //Message Type
                            var messageType = _sslStream.ReadByte();
                            p.AddToRaw((byte) messageType);
                            p.SetType(messageType);
                        }
                        else if (headerSize == 4)
                        {
                            //Timestamp
                            var timestamp = new byte[3];
                            for (var i = 0; i < 3; i++)
                            {
                                timestamp[i] = (byte) _sslStream.ReadByte();
                                p.AddToRaw(timestamp[i]);
                            }

                            if (p.GetSize() == 0 && p.GetPacketType() == 0)
                            {
                                if (previousReceivedPacket.ContainsKey(channel))
                                {
                                    p.SetSize(previousReceivedPacket[channel].GetSize());
                                    p.SetType(previousReceivedPacket[channel].GetPacketType());
                                }
                            }
                        }
                        else if (headerSize == 0)
                        {
                            if (p.GetSize() == 0 && p.GetPacketType() == 0)
                            {
                                if (previousReceivedPacket.ContainsKey(channel))
                                {
                                    p.SetSize(previousReceivedPacket[channel].GetSize());
                                    p.SetType(previousReceivedPacket[channel].GetPacketType());
                                }
                            }
                        }

                        #endregion

                        #region Message Body

                        //DefaultChunkSize is 128
                        for (var i = 0; i < 128; i++)
                        {
                            var b = (byte) _sslStream.ReadByte();
                            p.Add(b);
                            p.AddToRaw(b);

                            if (p.IsComplete())
                                break;
                        }

                        if (!p.IsComplete())
                            continue;

                        if (previousReceivedPacket.ContainsKey(channel))
                            previousReceivedPacket.Remove(channel);

                        previousReceivedPacket.Add(channel, p);

                        if (currentPackets.ContainsKey(channel))
                            currentPackets.Remove(channel);

                        #endregion

                        // Decode result
                        TypedObject result;
                        var decoder = new RtmpsDecoder();
                        if (p.GetPacketType() == 0x14) // Connect
                            result = decoder.DecodeConnect(p.GetData());
                        else if (p.GetPacketType() == 0x11) // Invoke
                            result = decoder.DecodeInvoke(p.GetData());
                        else if (p.GetPacketType() == 0x06) // Set peer bandwidth
                        {
                            var data = p.GetData();
                            var windowSize = 0;
                            for (var i = 0; i < 4; i++)
                                windowSize = windowSize*256 + (data[i] & 0xFF);
                            int type = data[4];
                            continue;
                        }
                        else if (p.GetPacketType() == 0x05) // Window Acknowledgement Size
                        {
                            var data = p.GetData();
                            var windowSize = 0;
                            for (var i = 0; i < 4; i++)
                                windowSize = windowSize*256 + (data[i] & 0xFF);
                            continue;
                        }
                        else if (p.GetPacketType() == 0x03) // Ack
                        {
                            var data = p.GetData();
                            var ackSize = 0;
                            for (var i = 0; i < 4; i++)
                                ackSize = ackSize*256 + (data[i] & 0xFF);
                            continue;
                        }
                        else if (p.GetPacketType() == 0x02) //ABORT
                        {
                            var data = p.GetData();
                            continue;
                        }
                        else if (p.GetPacketType() == 0x01) //MaxChunkSize
                        {
                            var data = p.GetData();
                            continue;
                        }
                        else
                        // Skip most messages
                        {
                            continue;
                        }

                        // Store result
                        var id = result.GetInt("invokeId");

                        //Check to see if the result is valid.
                        //If it isn't, give an error and remove the callback if there is one.
                        if (result["result"].Equals("_error"))
                        {
                            Error(GetErrorMessage(result), GetErrorCode(result), ErrorType.Receive);
                        }

                        if (result["result"].Equals("receive"))
                        {
                            if (result.GetTO("data") != null)
                            {
                                var to = result.GetTO("data");
                                if (to.ContainsKey("body"))
                                {
                                    if (to["body"] is TypedObject)
                                    {
                                        new Thread(() =>
                                        {
                                            var body = (TypedObject) to["body"];
                                            if (body.Type.Equals("com.riotgames.platform.game.GameDTO"))
                                                MessageReceived(new GameDto(body));
                                            else if (body.Type.Equals("com.riotgames.platform.game.PlayerCredentialsDto"))
                                                MessageReceived(new PlayerCredentialsDto(body));
                                            else if (
                                                body.Type.Equals(
                                                    "com.riotgames.platform.game.message.GameNotification"))
                                                MessageReceived(new GameNotification(body));
                                            else if (
                                                body.Type.Equals(
                                                    "com.riotgames.platform.matchmaking.SearchingForMatchNotification"))
                                                MessageReceived(new SearchingForMatchNotification(body));
                                            else if (
                                                body.Type.Equals(
                                                    "com.riotgames.platform.messaging.StoreFulfillmentNotification"))
                                                MessageReceived(new StoreFulfillmentNotification(body));
                                            else if (
                                                body.Type.Equals(
                                                    "com.riotgames.platform.messaging.StoreFulfillmentNotification"))
                                                MessageReceived(
                                                    new StoreAccountBalanceNotification(body));
                                            else
                                                MessageReceived(body);
                                        }).Start();
                                    }
                                }
                            }
                            //MessageReceived(
                        }

                        if (id == null)
                            continue;

                        if (id == 0)
                        {
                        }
                        else if (_callbacks.ContainsKey((int) id))
                        {
                            var cb = _callbacks[(int) id];
                            _callbacks.Remove((int) id);
                            if (cb != null)
                            {
                                var messageBody = result.GetTO("data").GetTO("body");
                                new Thread(() => { cb.DoCallback(messageBody); }).Start();
                            }
                        }

                        else
                        {
                            _results.Add((int) id, result);
                        }

                        _pendingInvokes.Remove((int) id);
                    }
                }
                catch (Exception e)
                {
                    if (IsConnected())
                        Error(e.Message, ErrorType.Receive);

                    //Disconnect();
                }
            });
            DecodeThread.Start();
        }


        private TypedObject GetResult(int id)
        {
            while (IsConnected() && !_results.ContainsKey(id))
            {
                Thread.Sleep(10);
            }

            if (!IsConnected())
                return null;

            var ret = _results[id];
            _results.Remove(id);
            return ret;
        }

        private TypedObject PeekResult(int id)
        {
            if (_results.ContainsKey(id))
            {
                var ret = _results[id];
                _results.Remove(id);
                return ret;
            }
            return null;
        }

        private void Join()
        {
            while (_pendingInvokes.Count > 0)
            {
                Thread.Sleep(10);
            }
        }

        private void Join(int id)
        {
            while (IsConnected() && _pendingInvokes.Contains(id))
            {
                Thread.Sleep(10);
            }
        }

        private void Cancel(int id)
        {
            // Remove from pending invokes (only affects join())
            _pendingInvokes.Remove(id);

            // Check if we've already received the result
            if (PeekResult(id) != null)
                return;
            // Signify a cancelled invoke by giving it a null callback
            _callbacks.Add(id, null);

            // Check for race condition
            if (PeekResult(id) != null)
                _callbacks.Remove(id);
        }

        #endregion

        #region Public Client Methods

        #endregion

        #region General Returns

        public bool IsConnected()
        {
            return _isConnected;
        }

        public bool IsLoggedIn()
        {
            return _isLoggedIn;
        }

        public double AccountId()
        {
            return _accountId;
        }

        #endregion
    }
}