using System.Net.WebSockets;
using System.Text;
using System.Collections.Concurrent;
using System.Text.Json;


using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Jose;
using System.Diagnostics.SymbolStore;
using cbMsg;
using System;

namespace coinbase_connection
{
    public class coinbase_connection
    {
        private ClientWebSocket ws;
        private string url;
        private string name;
        private string privateKey;

        static Random random = new Random();

        public ConcurrentQueue<string> msgQueue;

        public Action<string> addLog = (str) => { Console.WriteLine(str); };

        private coinbase_connection()
        {
            this.ws = new ClientWebSocket();
            this.url = "";
            this.name = "";
            this.privateKey = "";

        }
        ~coinbase_connection()
        {
            this.ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Destructor Called", CancellationToken.None);
            this.ws.Dispose();
            this.url = "";
            this.name = "";
            this.privateKey = "";
        }

        public async Task connect(string _url)
        {
            this.ws = new ClientWebSocket();
            this.url = _url;
            var uri = new Uri(this.url);
            await this.ws.ConnectAsync(uri, CancellationToken.None);
        }

        public void disconnect() 
        {
            if(this.ws != null)
            {
                this.ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Disconnection called", CancellationToken.None);
                this.ws.Dispose();
            }
        }

        public bool startListen(cbChannels channel, string[] symbols = null)
        {
            string strChannel = "\"channel\":";
            switch (channel)
            {
                case cbChannels.heartbeats:
                    this.addLog("Start listening heartbeats...");
                    strChannel += "\"heartbeats\",";
                    break;
                case cbChannels.candles:
                    this.addLog("Start listening candles...");
                    strChannel += "\"candle\",";
                    break;
                case cbChannels.status:
                    this.addLog("Start listening status...");
                    strChannel += "\"status\",";
                    break;
                case cbChannels.ticker:
                    this.addLog("Start listening ticker...");
                    strChannel += "\"ticker\",";
                    break;
                case cbChannels.ticker_batch:
                    this.addLog("Start listening ticker_batch...");
                    strChannel += "\"ticker_batch\",";
                    break;
                case cbChannels.level2:
                    this.addLog("Start listening level 2...");
                    strChannel += "\"level2\",";
                    break;
                case cbChannels.user:
                    this.addLog("Start listening user...");
                    strChannel += "\"user\",";
                    break;
                case cbChannels.market_trades:
                    this.addLog("Start listening market_trades...");
                    strChannel += "\"market_trades\",";
                    break;
                case cbChannels.NONE:
                default:
                    this.addLog("[ERROR] Channel not specified");
                    strChannel = "";
                    return false;
                    break;
            }
            if (this.name != "" && this.privateKey != "")
            {
                string str_symbols = "";
                if (symbols != null)
                {
                    str_symbols = "\"product_ids\":[\"";
                    int i = 0;
                    foreach (string symbol in symbols)
                    {
                        if (i > 0)
                        {
                            str_symbols += ",\"";
                        }
                        ++i;
                        str_symbols += symbol + "\"";
                    }
                    str_symbols += "],";
                }
                string jwt = this.generateToken(this.name, this.privateKey);
                string msg = "{\"type\":\"subscribe\"," + strChannel + str_symbols + "\"jwt\":\"" + jwt + "\"}";
                var encoded = Encoding.UTF8.GetBytes(msg);
                var sending = new ArraySegment<Byte>(encoded, 0, encoded.Length);
                this.ws.SendAsync(sending, WebSocketMessageType.Text, true, CancellationToken.None);
                return true;
            }
            else
            {
                this.addLog("[ERROR] The name or the key is unknown.");
                return false;
            }
        }

        public void listen()
        {
            this.addLog("Listening thread started.");
            byte[] buffer = new byte[1073741824];
            while (true)
            {
                var segment = new ArraySegment<byte>(buffer);
                var result = this.recv(ref segment);

                if (result.IsFaulted == false)
                {
                    if (result.Result.MessageType == WebSocketMessageType.Close)
                    {
                        return;
                    }
                    if (result.Result.MessageType == WebSocketMessageType.Binary)
                    {
                        return;
                    }

                    int count = result.Result.Count;
                    while (!result.Result.EndOfMessage)
                    {
                        if (result.Result.Count == 0)
                        {
                            break;
                        }
                        if (count >= buffer.Length)
                        {
                            return;
                        }
                        segment = new ArraySegment<byte>(buffer, count, buffer.Length - count);
                        result = this.recv(ref segment);

                        count += result.Result.Count;
                    }

                    var message = Encoding.UTF8.GetString(buffer, 0, count);
                    this.msgQueue.Enqueue(message);
                }

            }
        }

        public void readApiKey(string filename)
        {
            using (var fileStream = File.OpenRead(filename))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    String line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        if (line.Substring(0, 7) == "API key")
                        {
                            this.name = line.Substring(9);
                        }
                        else if (line.Substring(0, 11) == "Private key")
                        {
                            string temp = line.Substring(13);
                            this.privateKey = parseKey(temp);
                        }
                    }
                }
            }
        }


        string parseKey(string key)
        {
            List<string> keyLines = new List<string>();
            keyLines.AddRange(key.Split("\\n", StringSplitOptions.RemoveEmptyEntries));

            keyLines.RemoveAt(0);
            keyLines.RemoveAt(keyLines.Count - 1);

            return string.Join("", keyLines);
        }

        string generateToken(string name, string secret)
        {
            var privateKeyBytes = Convert.FromBase64String(secret); // Assuming PEM is base64 encoded
            using var key = ECDsa.Create();
            key.ImportECPrivateKey(privateKeyBytes, out _);

            var payload = new Dictionary<string, object>
             {
                 { "sub", name },
                 { "iss", "cdp" },
                 { "nbf", Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds) },
                 { "exp", Convert.ToInt64((DateTime.UtcNow.AddMinutes(2) - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds) },
             };

            var extraHeaders = new Dictionary<string, object>
             {
                 { "kid", name },
                 // add nonce to prevent replay attacks with a random 10 digit number
                 { "nonce", randomHex(10) },
                 { "typ", "JWT"}
             };

            var encodedToken = Jose.JWT.Encode(payload, key, JwsAlgorithm.ES256, extraHeaders);

            return encodedToken;
        }

        string randomHex(int digits)
        {
            byte[] buffer = new byte[digits / 2];
            random.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (digits % 2 == 0)
                return result;
            return result + random.Next(16).ToString("X");
        }

        public Task<WebSocketReceiveResult> recv(ref ArraySegment<Byte> seg)
        {
            return this.ws.ReceiveAsync(seg, CancellationToken.None);
        }

        public WebSocketState getState()
        {
            return this.ws.State;
        }

        private static coinbase_connection _instance;
        private static readonly object _lockObject = new object();

        public static coinbase_connection GetInstance()
        {
            lock (_lockObject)
            {
                if (_instance == null)
                {
                    //インスタンス生成
                    _instance = new coinbase_connection();
                }
                return _instance;
            }
        }
    }

    public enum cbChannels
    {
        NONE = 0,
        heartbeats,
        candles,
        status,
        ticker,
        ticker_batch,
        level2,
        user,
        market_trades
    }
}


