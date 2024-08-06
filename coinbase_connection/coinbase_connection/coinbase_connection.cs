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
using System.Net.Http.Headers;
using System.Globalization;

namespace coinbase_connection
{
    public class coinbase_connection
    {
        private ClientWebSocket ws;
        private string url;
        private string name;
        private string privateKey;

        public long msgCount;
        public long msgIncrement;

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
                    ++this.msgCount;
                    ++this.msgIncrement;
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
                    _instance = new coinbase_connection();
                }
                return _instance;
            }
        }
    }

    public class coinbase_restAPI
    {
        private coinbase_restAPI()
        {
            this.url_listAccount = this.url + "accounts";
            this.url_getAccount = this.url + "accounts/";//Need uuid
            this.url_createOrder = this.url + "orders";
            this.url_cancelOrder = this.url + "orders/batch_cancel";
            this.url_editOrder = this.url + "orders/edit";
            this.url_editOrderReview = this.url + "orders/edit_preview";
            this.url_listOrders = this.url + "orders/historical/batch";
            this.url_listFills = this.url + "orders/historical/fills";
            this.url_getOrder = this.url + "orders/historical/";//Need order_id
            this.url_previewOrder = this.url + "orders/preview";
            this.url_closePosition = this.url + "orders/close_position";
            this.order_no = 0;
            this.client = new HttpClient();
        }

        //Risk checks should be made in OMS
        public async Task<HttpResponseMessage> createOrder(string product_id, string side, string order_config,string leverage = "1.0", string margin_type = "CROSS", string retail_portfolio = "", string preview_id = "")
        {
            string str = "{\"client_order_id\":\"" + this.getOrderId(product_id) + "\","
                        + "\"product_id\":\"" + product_id + "\","
                        + "\"side\":\"" + side + "\","
                        + "\"order_configuration\":\"" + order_config + "\","
                        + "\"leverage\":\"" + leverage + "\","
                        + "\"margin_type\":\"" + margin_type + "\"";
            if(retail_portfolio != "")
            {
                str += ",\"retail_portfolio\":\"" + retail_portfolio + "\"";
            }
            if(preview_id != "")
            {
                str += ",\"preview_id\":\"" + preview_id + "\"";
            }
            str += "}";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://" + this.url_createOrder);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.generateToken(this.name,this.privateKey,"POST " + this.url_createOrder));
            StringContent content = new StringContent(str,null, "application/json");
            request.Content = content;
            return await client.SendAsync(request);
        }
        public async Task<HttpResponseMessage> cancelOrders(string[] order_ids)
        {
            string str = "{\"order_ids\":[";
            int i;
            for(i = 0;i < order_ids.Length;++i)
            {
                if(i == 0)
                {
                    str += "\"" + order_ids[i] + "\"";
                }
                else
                {
                    str += ",\"" + order_ids[i] + "\"";
                }
            }
            str += "]}";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://" + this.url_cancelOrder);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.generateToken(this.name, this.privateKey, "POST " + this.url_cancelOrder));
            StringContent content = new StringContent(str, null, "application/json");
            request.Content = content;
            return await client.SendAsync(request);
        }
        public async Task<HttpResponseMessage> editOrder(string order_id,string price = "", string size = "")
        {
            string str = "{\"order_id\":\"" + order_id + "\"";
            if(price != "")
            {
                str += ",\"price\":\"" + price + "\"";
            }
            if (size != "")
            {
                str += ",\"size\":\"" + size + "\"";
            }
            str += "}";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://" + this.url_editOrder);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.generateToken(this.name, this.privateKey, "POST " + this.url_editOrder));
            StringContent content = new StringContent(str, null, "application/json");
            request.Content = content;
            return await client.SendAsync(request);
        }
        public async Task<HttpResponseMessage> editPreview(string order_id, string price = "", string size = "")
        {
            string str = "{\"order_id\":\"" + order_id + "\"";
            if (price != "")
            {
                str += ",\"price\":\"" + price + "\"";
            }
            if (size != "")
            {
                str += ",\"size\":\"" + size + "\"";
            }
            str += "}";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://" + this.url_editOrderReview);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.generateToken(this.name, this.privateKey, "POST " + this.url_editOrderReview));
            StringContent content = new StringContent(str, null, "application/json");
            request.Content = content;
            return await client.SendAsync(request);
        }
        public async Task<HttpResponseMessage> listOrders(string[] order_ids, string[] product_ids, string product_type, string[] order_status, string[] time_in_forces, string[] order_types,string order_side,string start_date,string end_date,string order_placement_source,string contract_expiry_type, string[] asset_filters,string retail_portfolio_id,string limit,string cursor,string user_native_currency)
        {
            string str = "";
            int i;
            if(order_ids.Length > 0)
            {
                for(i = 0;i <order_ids.Length;++i)
                {
                    if(str == "")
                    {
                        str += "order_ids=" + order_ids[i];
                    }
                    else
                    {
                        str += "&order_ids=" + order_ids[i];
                    }
                }
            }
            if (product_ids.Length > 0)
            {
                for (i = 0; i < product_ids.Length; ++i)
                {
                    if (str == "")
                    {
                        str += "product_ids=" + product_ids[i];
                    }
                    else
                    {
                        str += "&product_ids=" + product_ids[i];
                    }
                }
            }
            if(product_type != "")
            {
                if (str == "")
                {
                    str += "product_type=" + product_type;
                }
                else
                {
                    str += "&product_type=" + product_type;
                }
            }
            if (order_status.Length > 0)
            {
                for (i = 0; i < order_status.Length; ++i)
                {
                    if (str == "")
                    {
                        str += "order_status=" + order_status[i];
                    }
                    else
                    {
                        str += "&order_status=" + order_status[i];
                    }
                }
            }
            if (time_in_forces.Length > 0)
            {
                for (i = 0; i < time_in_forces.Length; ++i)
                {
                    if (str == "")
                    {
                        str += "time_in_forces=" + time_in_forces[i];
                    }
                    else
                    {
                        str += "&time_in_forces=" + time_in_forces[i];
                    }
                }
            }
            if (order_types.Length > 0)
            {
                for (i = 0; i < order_types.Length; ++i)
                {
                    if (str == "")
                    {
                        str += "order_types=" + order_types[i];
                    }
                    else
                    {
                        str += "&order_types=" + order_types[i];
                    }
                }
            }
            if (order_side != "")
            {
                if (str == "")
                {
                    str += "order_side=" + order_side;
                }
                else
                {
                    str += "&order_side=" + order_side;
                }
            }
            if (start_date != "")
            {
                if (str == "")
                {
                    str += "start_date=" + start_date;
                }
                else
                {
                    str += "&start_date=" + start_date;
                }
            }
            if (end_date != "")
            {
                if (str == "")
                {
                    str += "end_date=" + end_date;
                }
                else
                {
                    str += "&end_date=" + end_date;
                }
            }
            if (order_placement_source != "")
            {
                if (str == "")
                {
                    str += "order_placement_source=" + order_placement_source;
                }
                else
                {
                    str += "&order_placement_source=" + order_placement_source;
                }
            }
            if (contract_expiry_type != "")
            {
                if (str == "")
                {
                    str += "contract_expiry_type=" + contract_expiry_type;
                }
                else
                {
                    str += "&contract_expiry_type=" + contract_expiry_type;
                }
            }
            if (asset_filters.Length > 0)
            {
                for (i = 0; i < asset_filters.Length; ++i)
                {
                    if (str == "")
                    {
                        str += "asset_filters=" + asset_filters[i];
                    }
                    else
                    {
                        str += "&asset_filters=" + asset_filters[i];
                    }
                }
            }
            if (retail_portfolio_id != "")
            {
                if (str == "")
                {
                    str += "retail_portfolio_id=" + retail_portfolio_id;
                }
                else
                {
                    str += "&retail_portfolio_id=" + retail_portfolio_id;
                }
            }
            if (limit != "")
            {
                if (str == "")
                {
                    str += "limit=" + limit;
                }
                else
                {
                    str += "&limit=" + limit;
                }
            }
            if (cursor != "")
            {
                if (str == "")
                {
                    str += "cursor=" + cursor;
                }
                else
                {
                    str += "&cursor=" + cursor;
                }
            }
            if (user_native_currency != "")
            {
                if (str == "")
                {
                    str += "user_native_currency=" + user_native_currency;
                }
                else
                {
                    str += "&user_native_currency=" + user_native_currency;
                }
            }
            if (str != "")
            {
                str = "?" + str;
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://" + this.url_listOrders + str);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.generateToken(this.name, this.privateKey, "GET " + this.url_listOrders));
            StringContent content = new StringContent(string.Empty);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = content;
            return await client.SendAsync(request);
        }
        public async Task<HttpResponseMessage> listFills(string[] order_ids, string[] trade_ids, string[] product_ids, string start_sequence_timestamp, string end_sequence_timestamp, string retail_portfolio_id, string limit, string cursor, string sort_by)
        {
            string str = "";
            int i;
            if (order_ids.Length > 0)
            {
                for (i = 0; i < order_ids.Length; ++i)
                {
                    if (str == "")
                    {
                        str += "order_ids=" + order_ids[i];
                    }
                    else
                    {
                        str += "&order_ids=" + order_ids[i];
                    }
                }
            }
            if (trade_ids.Length > 0)
            {
                for (i = 0; i < trade_ids.Length; ++i)
                {
                    if (str == "")
                    {
                        str += "trade_ids=" + trade_ids[i];
                    }
                    else
                    {
                        str += "&trade_ids=" + trade_ids[i];
                    }
                }
            }
            if (product_ids.Length > 0)
            {
                for (i = 0; i < product_ids.Length; ++i)
                {
                    if (str == "")
                    {
                        str += "product_ids=" + product_ids[i];
                    }
                    else
                    {
                        str += "&product_ids=" + product_ids[i];
                    }
                }
            }
            if (start_sequence_timestamp != "")
            {
                if (str == "")
                {
                    str += "start_sequence_timestamp=" + start_sequence_timestamp;
                }
                else
                {
                    str += "&start_sequence_timestamp=" + start_sequence_timestamp;
                }
            }
            if (end_sequence_timestamp != "")
            {
                if (str == "")
                {
                    str += "end_sequence_timestamp=" + end_sequence_timestamp;
                }
                else
                {
                    str += "&end_sequence_timestamp=" + end_sequence_timestamp;
                }
            }
            if (retail_portfolio_id != "")
            {
                if (str == "")
                {
                    str += "retail_portfolio_id=" + retail_portfolio_id;
                }
                else
                {
                    str += "&retail_portfolio_id=" + retail_portfolio_id;
                }
            }
            if (limit != "")
            {
                if (str == "")
                {
                    str += "limit=" + limit;
                }
                else
                {
                    str += "&limit=" + limit;
                }
            }
            if (cursor != "")
            {
                if (str == "")
                {
                    str += "cursor=" + cursor;
                }
                else
                {
                    str += "&cursor=" + cursor;
                }
            }
            if (sort_by != "")
            {
                if (str == "")
                {
                    str += "sort_by=" + sort_by;
                }
                else
                {
                    str += "&sort_by=" + sort_by;
                }
            }
            if(str != "")
            {
                str = "?" + str;
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://" + this.url_listFills + str);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.generateToken(this.name, this.privateKey, "GET " + this.url_listFills));
            StringContent content = new StringContent(string.Empty);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = content;
            return await client.SendAsync(request);
        }
        public async Task<HttpResponseMessage> getOrder(string order_id,string client_order_id,string user_native_currency)
        {
            string str = "";
            if (client_order_id != "")
            {
                if(str == "")
                {
                    str += "?client_order_id=" + client_order_id;
                }
                else
                {
                    str += "&client_order_id=" + client_order_id;
                }
                
            }
            if (user_native_currency != "")
            {
                if (str == "")
                {
                    str += "?user_native_currency=" + user_native_currency;
                }
                else
                {
                    str += "&user_native_currency=" + user_native_currency;
                }
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://" + this.url_getOrder + "/" + order_id + str);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.generateToken(this.name, this.privateKey, "GET " + this.url_getOrder));
            StringContent content = new StringContent(string.Empty);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = content;
            return await client.SendAsync(request);
        }
        public async Task<HttpResponseMessage> previewOrder(string product_id, string side, string order_config, string leverage = "1.0", string margin_type = "CROSS", string retail_portfolio = "")
        {
            string str = "{\"product_id\":\"" + product_id + "\","
                        + "\"side\":\"" + side + "\","
                        + "\"order_configuration\":\"" + order_config + "\","
                        + "\"leverage\":\"" + leverage + "\","
                        + "\"margin_type\":\"" + margin_type + "\"";
            if (retail_portfolio != "")
            {
                str += ",\"retail_portfolio\":\"" + retail_portfolio + "\"";
            }
            str += "}";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://" + this.url_previewOrder);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.generateToken(this.name, this.privateKey, "POST " + this.url_previewOrder));
            StringContent content = new StringContent(str, null, "application/json");
            request.Content = content;
            return await client.SendAsync(request);
        }
        public async Task<HttpResponseMessage> closePosition(string client_order_id, string product_id, string size = "")
        {
            string str = "{\"order_id\":\"" + client_order_id + "\",\"product_id\":\"" + product_id + "\"";
            if (size != "")
            {
                str += ",\"size\":\"" + size + "\"";
            }
            str += "}";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://" + this.url_closePosition);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.generateToken(this.name, this.privateKey, "POST " + this.url_closePosition));
            StringContent content = new StringContent(str, null, "application/json");
            request.Content = content;
            return await client.SendAsync(request);
        }
        public async Task<HttpResponseMessage> listAccount(string limit, string cursor, string retail_portfolio_id)
        {
            string str = "";
            if (limit != "")
            {
                str += "&limit=" + limit;
            }
            if (cursor != "")
            {
                str += "&cursor=" + cursor;
            }
            if (retail_portfolio_id != "")
            {
                str += "&retail_portfolio_id=" + retail_portfolio_id;
            }
            if(str != "")
            {
                str = "?" + str;
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://" + this.url_listAccount + str);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.generateToken(this.name, this.privateKey, "GET " + this.url_listAccount + str));
            StringContent content = new StringContent(string.Empty);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = content;
            return await client.SendAsync(request);
        }
        public async Task<HttpResponseMessage> getAccount(string account_uuid)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://" + this.url_getAccount + "/" + account_uuid);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.generateToken(this.name, this.privateKey, "POST " + this.url_createOrder));
            StringContent content = new StringContent(string.Empty);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = content;
            return await client.SendAsync(request);
        }

        //order Configs
        public string market_market_ioc(string size, string size_type)
        {
            string str = "";
            if (size_type == "base")
            {
                str = "\"order_configuration\":{\"market_market_ioc\":{\"base_size\":\"" + size + "\"}}";
            }
            else if (size_type == "quote")
            {
                str = "\"order_configuration\":{\"market_market_ioc\":{\"quote_size\":\"" + size + "\"}}";
            }
            return str;
        }
        public string sor_limit_ioc(string base_size, string limit_price)
        {
            return "\"order_configuration\": {\"sor_limit_ioc\":{\"base_size\": \"" + base_size + "\",\"limit_price\": \"" + limit_price + "\"}}";
        }
        public string limit_limit_gtc(string base_size, string limit_price,string post_only)
        {
            return "\"order_configuration\": {\"limit_limit_gtc\":{\"base_size\": \"" + base_size + "\",\"limit_price\": \"" + limit_price + "\",\"post_only\":" + post_only + "}}";
        }
        public string limit_limit_gtd(string base_size, string limit_price, string post_only,string end_time)
        {
            return "\"order_configuration\": {\"limit_limit_gtd\":{\"base_size\": \"" + base_size + "\",\"limit_price\": \"" + limit_price + "\",\"end_time\": \"" + end_time + "\",\"post_only\":" + post_only + "}}";
        }
        public string limit_limit_fok(string base_size,string limit_price)
        {
            return "\"order_configuration\": {\"limit_limit_fok\":{\"base_size\": \"" + base_size + "\",\"limit_price\": \"" + limit_price + "\"}}";
        }
        public string stop_limit_stop_limit_gtc(string base_size, string limit_price,string stop_price,string stop_direction)
        {
            return "\"order_configuration\": {\"stop_limit_stop_limit_gtc\":{\"base_size\": \"" + base_size + "\",\"limit_price\": \"" + limit_price + "\",\"stop_price\": \"" + stop_price + "\",\"stop_direction\": \"" + stop_direction + "\"}}";
        }
        public string stop_limit_stop_limit_gtd(string base_size, string limit_price, string stop_price, string end_time, string stop_direction)
        {
            return "\"order_configuration\": {\"stop_limit_stop_limit_gtd\":{\"base_size\": \"" + base_size + "\",\"limit_price\": \"" + limit_price + "\",\"stop_price\": \"" + stop_price + ",\"end_time\": \"" + end_time + "\",\"stop_direction\": \"" + stop_direction + "\"}}";
        }
        public string trigger_bracket_gtc(string base_size, string limit_price, string stop_trigger_price)
        {
            return "\"order_configuration\": {\"trigger_bracket_gtc\":{\"base_size\": \"" + base_size + "\",\"limit_price\": \"" + limit_price + "\",\"stop_trigger_price\": \"" + stop_trigger_price + "\"}}";
        }
        public string trigger_bracket_gtd(string base_size, string limit_price, string stop_trigger_price,string end_time)
        {
            return "\"order_configuration\": {\"trigger_bracket_gtd\":{\"base_size\": \"" + base_size + "\",\"limit_price\": \"" + limit_price + "\",\"stop_trigger_price\": \"" + stop_trigger_price + ",\"end_time\": \"" + end_time + "\"}}";
        }

        public string getOrderId(string symbol)
        {
            int current_no = Interlocked.Increment(ref this.order_no);
            return symbol + DateTime.Now.ToString("yyyyMMdd") + current_no.ToString("D8");
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

        string generateToken(string name, string secret, string uri)
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
                 { "uri", uri }
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

        private string name;
        private string privateKey;

        static Random random = new Random();
        private string url = "api.coinbase.com/api/v3/brokerage/";
        private HttpClient client;
        //Account
        private string url_listAccount;
        private string url_getAccount;
        //Order
        private string url_createOrder;
        private string url_cancelOrder;
        private string url_editOrder;
        private string url_editOrderReview;
        private string url_listOrders;
        private string url_listFills;
        private string url_getOrder;
        private string url_previewOrder;
        private string url_closePosition;

        private int order_no;

        private static coinbase_restAPI _instance;
        private static readonly object _lockObject = new object();

        public static coinbase_restAPI GetInstance()
        {
            lock (_lockObject)
            {
                if (_instance == null)
                {
                    _instance = new coinbase_restAPI();
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


