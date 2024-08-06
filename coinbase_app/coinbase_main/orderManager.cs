using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace coinbase_main
{
    internal class orderManager
    {

        public Action<string> addLog = (str) => { Console.WriteLine(str); };

        public ConcurrentStack<order> orderStack;

        public Dictionary<string, crypto> cryptos;

        private coinbase_connection.coinbase_restAPI api = coinbase_connection.coinbase_restAPI.GetInstance();
        
        public async Task<HttpResponseMessage> sendMarketOrder(string product_id, string side, double base_size, string leverage = "1.0", string margin_type = "CROSS", string retail_portfolio = "", string preview_id = "") 
        {
            crypto cp = this.checkProductId(product_id);
            if(cp == null)
            {
                this.addLog("[ERROR] Invalid product_id");
                return null;
            }
            if(!this.checkSide(ref side))
            {
                this.addLog("[ERROR] Invalid side");
                return null;
            }
            if(!this.checkSize(cp,base_size))
            {
                this.addLog("[ERROR] Invalid size");
                return null;
            }
            
            order ord;
            int i = 0;
            while(!this.orderStack.TryPop(out ord))
            {
                ++i;
                if(i > 100000)
                {
                    System.Threading.Thread.Sleep(0);
                }
            }
            var res = await this.api.createOrder(product_id, side, this.api.market_market_ioc(base_size.ToString("N8"), "base"), leverage, margin_type, retail_portfolio, preview_id);
            
            if(res.IsSuccessStatusCode)
            {
                this.decodeSuccessResponse(ref this.tempjson,res.ToString());
                ord.product_id = product_id;
                ord.client_order_id = tempjson["client_order_id"];
                ord.order_id = tempjson["order_id"];
                ord.price = 0;
                ord.side = side;
                ord.status = "LIVE";//Enum?
                ord.size = base_size;
                ord.executed_size = 0;
                ord.open_size = base_size;
                ord.new_order_time = DateTime.Now;
                ord.updated_time = ord.new_order_time;
                cp.orders.Add(ord.client_order_id, ord);
                cp.liveOrders.Add(ord.client_order_id, ord);
                //Decode the result and input to ord object
                return res;
            }
            else 
            {
                this.addLog("[ERROR] The server returned unsuccessful result.");
                this.addLog("[ERROR]" + res.ToString());
                return res;
            }
        }
        public async Task<HttpResponseMessage> sendLimitIOC(string product_id, string side, double base_size,double limit_price, string leverage = "1.0", string margin_type = "CROSS", string retail_portfolio = "", string preview_id = "")
        {
            crypto cp = this.checkProductId(product_id);
            if (cp == null)
            {
                this.addLog("[ERROR] Invalid product_id");
                return null;
            }
            if (!this.checkSide(ref side))
            {
                this.addLog("[ERROR] Invalid side");
                return null;
            }
            if(!this.checkLimitPrice(cp,limit_price))
            {
                this.addLog("[ERROR] Invalid limit price");
                return null;
            }
            if (!this.checkSize(cp, base_size))
            {
                this.addLog("[ERROR] Invalid size");
                return null;
            }

            order ord;
            int i = 0;
            while (!this.orderStack.TryPop(out ord))
            {
                ++i;
                if (i > 100000)
                {
                    System.Threading.Thread.Sleep(0);
                }
            }
            var res = await this.api.createOrder(product_id, side, this.api.sor_limit_ioc(base_size.ToString("N8"), limit_price.ToString("N2")), leverage, margin_type, retail_portfolio, preview_id);

            if (res.IsSuccessStatusCode)
            {
                this.decodeSuccessResponse(ref this.tempjson, res.ToString());
                ord.product_id = product_id;
                ord.client_order_id = tempjson["client_order_id"];
                ord.order_id = tempjson["order_id"];
                ord.price = 0;
                ord.side = side;
                ord.status = "LIVE";//Enum?
                ord.size = base_size;
                ord.executed_size = 0;
                ord.open_size = base_size;
                ord.new_order_time = DateTime.Now;
                ord.updated_time = ord.new_order_time;
                cp.orders.Add(ord.client_order_id, ord);
                cp.liveOrders.Add(ord.client_order_id, ord);
                //Decode the result and input to ord object
                return res;
            }
            else
            {
                this.addLog("[ERROR] The server returned unsuccessful result.");
                this.addLog("[ERROR]" + res.ToString());
                return res;
            }
        }
        public async Task<HttpResponseMessage> sendLimitGTC(string product_id, string side, double base_size, double limit_price,bool post_only, string leverage = "1.0", string margin_type = "CROSS", string retail_portfolio = "", string preview_id = "")
        {
            crypto cp = this.checkProductId(product_id);
            if (cp == null)
            {
                this.addLog("[ERROR] Invalid product_id");
                return null;
            }
            if (!this.checkSide(ref side))
            {
                this.addLog("[ERROR] Invalid side");
                return null;
            }
            if(!this.checkLimitPrice(cp,limit_price))
            {
                this.addLog("[ERROR] Invalid limit price");
                return null;
            }
            if (!this.checkSize(cp, base_size))
            {
                this.addLog("[ERROR] Invalid size");
                return null;
            }

            order ord;
            int i = 0;
            while (!this.orderStack.TryPop(out ord))
            {
                ++i;
                if (i > 100000)
                {
                    System.Threading.Thread.Sleep(0);
                }
            }
            var res = await this.api.createOrder(product_id, side, this.api.limit_limit_gtc(base_size.ToString("N8"), limit_price.ToString("N2"),post_only.ToString()), leverage, margin_type, retail_portfolio, preview_id);

            if (res.IsSuccessStatusCode)
            {
                this.decodeSuccessResponse(ref this.tempjson, res.ToString());
                ord.product_id = product_id;
                ord.client_order_id = tempjson["client_order_id"];
                ord.order_id = tempjson["order_id"];
                ord.price = 0;
                ord.side = side;
                ord.status = "LIVE";//Enum?
                ord.size = base_size;
                ord.executed_size = 0;
                ord.open_size = base_size;
                ord.new_order_time = DateTime.Now;
                ord.updated_time = ord.new_order_time;
                cp.orders.Add(ord.client_order_id, ord);
                cp.liveOrders.Add(ord.client_order_id, ord);
                //Decode the result and input to ord object
                return res;
            }
            else
            {
                this.addLog("[ERROR] The server returned unsuccessful result.");
                this.addLog("[ERROR]" + res.ToString());
                return res;
            }
        }
        public async Task<HttpResponseMessage> sendLimitGTD(string product_id, string side, double base_size, double limit_price, bool post_only,DateTime end_time, string leverage = "1.0", string margin_type = "CROSS", string retail_portfolio = "", string preview_id = "")
        {
            crypto cp = this.checkProductId(product_id);
            if (cp == null)
            {
                this.addLog("[ERROR] Invalid product_id");
                return null;
            }
            if (!this.checkSide(ref side))
            {
                this.addLog("[ERROR] Invalid side");
                return null;
            }
            if (!this.checkLimitPrice(cp, limit_price))
            {
                this.addLog("[ERROR] Invalid limit price");
                return null;
            }
            if (!this.checkSize(cp, base_size))
            {
                this.addLog("[ERROR] Invalid size");
                return null;
            }
            if(!this.checkEndTime(end_time))
            {
                this.addLog("[ERROR] Invalid end_time");
                return null;
            }

            order ord;
            int i = 0;
            while (!this.orderStack.TryPop(out ord))
            {
                ++i;
                if (i > 100000)
                {
                    System.Threading.Thread.Sleep(0);
                }
            }
            var res = await this.api.createOrder(product_id, side, this.api.limit_limit_gtd(base_size.ToString("N8"), limit_price.ToString("N2"), post_only.ToString(),end_time.ToString("yyyy-MM-ddTHH:mm:ssZ")), leverage, margin_type, retail_portfolio, preview_id);

            if (res.IsSuccessStatusCode)
            {
                this.decodeSuccessResponse(ref this.tempjson, res.ToString());
                ord.product_id = product_id;
                ord.client_order_id = tempjson["client_order_id"];
                ord.order_id = tempjson["order_id"];
                ord.price = 0;
                ord.side = side;
                ord.status = "LIVE";//Enum?
                ord.size = base_size;
                ord.executed_size = 0;
                ord.open_size = base_size;
                ord.new_order_time = DateTime.Now;
                ord.updated_time = ord.new_order_time;
                cp.orders.Add(ord.client_order_id, ord);
                cp.liveOrders.Add(ord.client_order_id, ord);
                //Decode the result and input to ord object
                return res;
            }
            else
            {
                this.addLog("[ERROR] The server returned unsuccessful result.");
                this.addLog("[ERROR]" + res.ToString());
                return res;
            }
        }
        public async Task<HttpResponseMessage> sendLimitFOK(string product_id, string side, double base_size, double limit_price, string leverage = "1.0", string margin_type = "CROSS", string retail_portfolio = "", string preview_id = "")
        {
            crypto cp = this.checkProductId(product_id);
            if (cp == null)
            {
                this.addLog("[ERROR] Invalid product_id");
                return null;
            }
            if (!this.checkSide(ref side))
            {
                this.addLog("[ERROR] Invalid side");
                return null;
            }
            if (!this.checkLimitPrice(cp, limit_price))
            {
                this.addLog("[ERROR] Invalid limit price");
                return null;
            }
            if (!this.checkSize(cp, base_size))
            {
                this.addLog("[ERROR] Invalid size");
                return null;
            }

            order ord;
            int i = 0;
            while (!this.orderStack.TryPop(out ord))
            {
                ++i;
                if (i > 100000)
                {
                    System.Threading.Thread.Sleep(0);
                }
            }
            var res = await this.api.createOrder(product_id, side, this.api.limit_limit_fok(base_size.ToString("N8"), limit_price.ToString("N2")), leverage, margin_type, retail_portfolio, preview_id);

            if (res.IsSuccessStatusCode)
            {
                this.decodeSuccessResponse(ref this.tempjson, res.ToString());
                ord.product_id = product_id;
                ord.client_order_id = tempjson["client_order_id"];
                ord.order_id = tempjson["order_id"];
                ord.price = 0;
                ord.side = side;
                ord.status = "LIVE";//Enum?
                ord.size = base_size;
                ord.executed_size = 0;
                ord.open_size = base_size;
                ord.new_order_time = DateTime.Now;
                ord.updated_time = ord.new_order_time;
                cp.orders.Add(ord.client_order_id, ord);
                cp.liveOrders.Add(ord.client_order_id, ord);
                //Decode the result and input to ord object
                return res;
            }
            else
            {
                this.addLog("[ERROR] The server returned unsuccessful result.");
                this.addLog("[ERROR]" + res.ToString());
                return res;
            }
        }
        public async Task<HttpResponseMessage> sendStopLimitGTC(string product_id, string side, double base_size, double limit_price,double stop_price,string stop_direction, string leverage = "1.0", string margin_type = "CROSS", string retail_portfolio = "", string preview_id = "")
        {
            crypto cp = this.checkProductId(product_id);
            if (cp == null)
            {
                this.addLog("[ERROR] Invalid product_id");
                return null;
            }
            if (!this.checkSide(ref side))
            {
                this.addLog("[ERROR] Invalid side");
                return null;
            }
            if (!this.checkLimitPrice(cp, limit_price))
            {
                this.addLog("[ERROR] Invalid limit price");
                return null;
            }
            if (!this.checkSize(cp, base_size))
            {
                this.addLog("[ERROR] Invalid size");
                return null;
            }

            order ord;
            int i = 0;
            while (!this.orderStack.TryPop(out ord))
            {
                ++i;
                if (i > 100000)
                {
                    System.Threading.Thread.Sleep(0);
                }
            }
            var res = await this.api.createOrder(product_id, side, this.api.stop_limit_stop_limit_gtc(base_size.ToString("N8"), limit_price.ToString("N2"), stop_price.ToString("N2"), stop_direction), leverage, margin_type, retail_portfolio, preview_id);

            if (res.IsSuccessStatusCode)
            {
                this.decodeSuccessResponse(ref this.tempjson, res.ToString());
                ord.product_id = product_id;
                ord.client_order_id = tempjson["client_order_id"];
                ord.order_id = tempjson["order_id"];
                ord.price = 0;
                ord.side = side;
                ord.status = "LIVE";//Enum?
                ord.size = base_size;
                ord.executed_size = 0;
                ord.open_size = base_size;
                ord.new_order_time = DateTime.Now;
                ord.updated_time = ord.new_order_time;
                cp.orders.Add(ord.client_order_id, ord);
                cp.liveOrders.Add(ord.client_order_id, ord);
                //Decode the result and input to ord object
                return res;
            }
            else
            {
                this.addLog("[ERROR] The server returned unsuccessful result.");
                this.addLog("[ERROR]" + res.ToString());
                return res;
            }
        }
        public async Task<HttpResponseMessage> sendStopLimitGTD(string product_id, string side, double base_size, double limit_price, double stop_price, string stop_direction,DateTime end_time, string leverage = "1.0", string margin_type = "CROSS", string retail_portfolio = "", string preview_id = "")
        {
            crypto cp = this.checkProductId(product_id);
            if (cp == null)
            {
                this.addLog("[ERROR] Invalid product_id");
                return null;
            }
            if (!this.checkSide(ref side))
            {
                this.addLog("[ERROR] Invalid side");
                return null;
            }
            if (!this.checkLimitPrice(cp, limit_price))
            {
                this.addLog("[ERROR] Invalid limit price");
                return null;
            }
            if (!this.checkSize(cp, base_size))
            {
                this.addLog("[ERROR] Invalid size");
                return null;
            }
            if(!this.checkEndTime(end_time))
            {
                this.addLog("[ERROR] Invalid end time");
                return null;
            }

            order ord;
            int i = 0;
            while (!this.orderStack.TryPop(out ord))
            {
                ++i;
                if (i > 100000)
                {
                    System.Threading.Thread.Sleep(0);
                }
            }
            var res = await this.api.createOrder(product_id, side, this.api.stop_limit_stop_limit_gtd(base_size.ToString("N8"), limit_price.ToString("N2"), stop_price.ToString("N2"),end_time.ToString("yyyy-MM-ddTHH:mm:ssZ"), stop_direction), leverage, margin_type, retail_portfolio, preview_id);

            if (res.IsSuccessStatusCode)
            {
                this.decodeSuccessResponse(ref this.tempjson, res.ToString());
                ord.product_id = product_id;
                ord.client_order_id = tempjson["client_order_id"];
                ord.order_id = tempjson["order_id"];
                ord.price = 0;
                ord.side = side;
                ord.status = "LIVE";//Enum?
                ord.size = base_size;
                ord.executed_size = 0;
                ord.open_size = base_size;
                ord.new_order_time = DateTime.Now;
                ord.updated_time = ord.new_order_time;
                cp.orders.Add(ord.client_order_id, ord);
                cp.liveOrders.Add(ord.client_order_id, ord);
                //Decode the result and input to ord object
                return res;
            }
            else
            {
                this.addLog("[ERROR] The server returned unsuccessful result.");
                this.addLog("[ERROR]" + res.ToString());
                return res;
            }
        }
        public async Task<HttpResponseMessage> sendTriggerBracketGTC(string product_id, string side, double base_size, double limit_price,double stop_trigger_price, string leverage = "1.0", string margin_type = "CROSS", string retail_portfolio = "", string preview_id = "")
        {
            crypto cp = this.checkProductId(product_id);
            if (cp == null)
            {
                this.addLog("[ERROR] Invalid product_id");
                return null;
            }
            if (!this.checkSide(ref side))
            {
                this.addLog("[ERROR] Invalid side");
                return null;
            }
            if (!this.checkLimitPrice(cp, limit_price))
            {
                this.addLog("[ERROR] Invalid limit price");
                return null;
            }
            if (!this.checkSize(cp, base_size))
            {
                this.addLog("[ERROR] Invalid size");
                return null;
            }

            order ord;
            int i = 0;
            while (!this.orderStack.TryPop(out ord))
            {
                ++i;
                if (i > 100000)
                {
                    System.Threading.Thread.Sleep(0);
                }
            }
            var res = await this.api.createOrder(product_id, side, this.api.trigger_bracket_gtc(base_size.ToString("N8"), limit_price.ToString("N2"), stop_trigger_price.ToString("N2")), leverage, margin_type, retail_portfolio, preview_id);

            if (res.IsSuccessStatusCode)
            {
                this.decodeSuccessResponse(ref this.tempjson, res.ToString());
                ord.product_id = product_id;
                ord.client_order_id = tempjson["client_order_id"];
                ord.order_id = tempjson["order_id"];
                ord.price = 0;
                ord.side = side;
                ord.status = "LIVE";//Enum?
                ord.size = base_size;
                ord.executed_size = 0;
                ord.open_size = base_size;
                ord.new_order_time = DateTime.Now;
                ord.updated_time = ord.new_order_time;
                cp.orders.Add(ord.client_order_id, ord);
                cp.liveOrders.Add(ord.client_order_id, ord);
                //Decode the result and input to ord object
                return res;
            }
            else
            {
                this.addLog("[ERROR] The server returned unsuccessful result.");
                this.addLog("[ERROR]" + res.ToString());
                return res;
            }
        }
        public async Task<HttpResponseMessage> sendTriggerBracketGTD(string product_id, string side, double base_size, double limit_price, double stop_trigger_price,DateTime end_time, string leverage = "1.0", string margin_type = "CROSS", string retail_portfolio = "", string preview_id = "")
        {
            crypto cp = this.checkProductId(product_id);
            if (cp == null)
            {
                this.addLog("[ERROR] Invalid product_id");
                return null;
            }
            if (!this.checkSide(ref side))
            {
                this.addLog("[ERROR] Invalid side");
                return null;
            }
            if (!this.checkLimitPrice(cp, limit_price))
            {
                this.addLog("[ERROR] Invalid limit price");
                return null;
            }
            if (!this.checkSize(cp, base_size))
            {
                this.addLog("[ERROR] Invalid size");
                return null;
            }
            if (!this.checkEndTime(end_time))
            {
                this.addLog("[ERROR] Invalid end time");
                return null;
            }

            order ord;
            int i = 0;
            while (!this.orderStack.TryPop(out ord))
            {
                ++i;
                if (i > 100000)
                {
                    System.Threading.Thread.Sleep(0);
                }
            }
            var res = await this.api.createOrder(product_id, side, this.api.trigger_bracket_gtd(base_size.ToString("N8"), limit_price.ToString("N2"), stop_trigger_price.ToString("N2"), end_time.ToString("yyyy-MM-ddTHH:mm:ssZ")), leverage, margin_type, retail_portfolio, preview_id);

            if (res.IsSuccessStatusCode)
            {
                this.decodeSuccessResponse(ref this.tempjson, res.ToString());
                ord.product_id = product_id;
                ord.client_order_id = tempjson["client_order_id"];
                ord.order_id = tempjson["order_id"];
                ord.price = 0;
                ord.side = side;
                ord.status = "LIVE";//Enum?
                ord.size = base_size;
                ord.executed_size = 0;
                ord.open_size = base_size;
                ord.new_order_time = DateTime.Now;
                ord.updated_time = ord.new_order_time;
                cp.orders.Add(ord.client_order_id, ord);
                cp.liveOrders.Add(ord.client_order_id, ord);
                //Decode the result and input to ord object
                return res;
            }
            else
            {
                this.addLog("[ERROR] The server returned unsuccessful result.");
                this.addLog("[ERROR]" + res.ToString());
                return res;
            }
        }
        public async Task<HttpResponseMessage> sendModOrder(order orgOrd, double newSize, double newPr)
        {
            crypto cp = this.checkProductId(orgOrd.product_id);
            string strPr = "";
            string strSize = "";
            if (cp == null)
            {
                this.addLog("[ERROR] Invalid product_id");
                return null;
            }
            if (newPr > 0 && newPr != orgOrd.price)
            {
                if (!this.checkLimitPrice(cp, newPr))
                {
                    this.addLog("[ERROR] Invalid limit price");
                    return null;
                }
                strPr = newPr.ToString("N2");
            }
            if (newSize >= 0 && newSize != orgOrd.size)
            {
                if (!this.checkSize(cp, newSize))
                {
                    this.addLog("[ERROR] Invalid size");
                    return null;
                }
                strSize = newSize.ToString("N8");
            }

            var res = await this.api.editOrder(orgOrd.order_id, strPr, strSize);
            if (res.IsSuccessStatusCode)
            {
                if (newPr > 0 && newPr != orgOrd.price)
                {
                    orgOrd.price = newPr;
                }
                if (newSize >= 0 && newSize != orgOrd.size)
                {
                    orgOrd.size = newSize;
                    orgOrd.open_size = newSize - orgOrd.executed_size;
                    if(orgOrd.open_size < 0)
                    {
                        orgOrd.open_size = 0;
                        orgOrd.size = orgOrd.executed_size;
                    }
                }
                //Decode the result and input to ord object
                return res;
            }
            else
            {
                this.addLog("[ERROR] The server returned unsuccessful result.");
                this.addLog("[ERROR]" + res.ToString());
                return res;
            }
        }
        public async Task<HttpResponseMessage> sendCanOrder(order orgOrd)
        {
            string[] order_ids = { orgOrd.order_id};
            var res = await this.api.cancelOrders(order_ids);
            if (res.IsSuccessStatusCode)
            {
                orgOrd.size = orgOrd.executed_size;
                orgOrd.open_size = 0;
                orgOrd.status = "CANCELLED";
                return res;
            }
            else
            {
                this.addLog("[ERROR] The server returned unsuccessful result.");
                this.addLog("[ERROR]" + res.ToString());
                return res;
            }

        }

        private crypto checkProductId(string product_id)
        {
            if(this.cryptos.ContainsKey(product_id))
            {
                return this.cryptos[product_id];
            }
            else
            {
                return null;
            }
        }
        private bool checkSide(ref string side)
        {
            if(side == "SELL" || side == "BUY")
            {
                return true;
            }
            else if(side.Equals("SELL",StringComparison.OrdinalIgnoreCase))
            {
                side = "SELL";
                return true;
            }
            else if (side.Equals("BUY", StringComparison.OrdinalIgnoreCase))
            {
                side = "BUY";
                return true;
            }
            return false;
        }
        private bool checkLimitPrice(crypto cp, double limit_price)
        {
            if(cp.maxOrdPr > 0 && (int)(limit_price / cp.quote_increment) > cp.maxOrdPr)
            {
                return false;
            }
            else if(cp.minOrdPr > 0 && (int)(limit_price / cp.quote_increment) < cp.minOrdPr)
            {
                return false;
            }
            else if(limit_price < cp.minPr || limit_price > cp.maxPr)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool checkSize(crypto cp, double base_size,double limi_price = 0,string side = "")
        {
            if(base_size > cp.maxBaseSize || base_size > this.maxBaseSize)
            {
                return false;
            }
            double price;
            if(limi_price > 0)
            {
                price = limi_price;
            }
            else if(side == "BUY" && cp.bestask > 0)
            {
                price = (double)cp.bestask / cp.quote_increment;
            }
            else if(side == "SELL" && cp.bestbid > 0)
            {
                price = (double)cp.bestbid / cp.quote_increment;
            }
            else
            {
                return false;
            }
            if(base_size * price > cp.maxQuoteSize || base_size * price > this.maxQuoteSize)
            {  
                return false;
            }
            return true;
        }
        private bool checkEndTime(DateTime end_time)
        {
            if(end_time < DateTime.Now)
            {
                return false;
            }
            return true;
        }

        private void decodeSuccessResponse(ref Dictionary<string, string> dict,string str)
        {
            string target = "\"success_response\":";
            int start = str.IndexOf(target) + target.Length + 1;
            int end = str.IndexOf("}", start);

            dict = JsonSerializer.Deserialize<Dictionary<string, string>>(str.Substring(start,end-start));
        }

        private double maxBaseSize = 1;
        private double maxQuoteSize = 10000;

        private Dictionary<string, string> tempjson;

        private static orderManager _instance;
        private static readonly object _lockObject = new object();

        public static orderManager GetInstance()
        {
            lock (_lockObject)
            {
                if (_instance == null)
                {
                    _instance = new orderManager();
                }
                return _instance;
            }
        }
    }

    public enum order_type
    {
        NONE,
        market_market_ioc,
        sor_limit_ioc,
        limit_limit_gtc,
        limit_limit_gtd,
        limit_limit_fok,
        stop_limit_stop_limit_gtc,
        stop_limit_stop_limit_gtd,
        trigger_bracket_gtc,
        trigger_bracket_gtd
    }

    public class order
    {
        public string order_id;
        public string client_order_id;
        public string status;

        public string product_id;

        public string side;

        public double price;
        public double size;
        public double executed_size;
        public double open_size;

        public DateTime new_order_time;
        public DateTime updated_time;
    }
}
