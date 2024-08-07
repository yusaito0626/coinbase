﻿using coinbase_connection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.WebSockets;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace coinbase_main
{
    public class orderManager
    {
        private orderManager()
        {
            this.order_no = 0;
            this.orderStack = new ConcurrentStack<order>();
            for(int i = 0;i < this.STACK_SIZE; ++i)
            {
                this.orderStack.Push(new order());
            }
            this.stopListening = false;
        }

        public Action<string> addLog = (str) => { Console.WriteLine(str); };

        private int STACK_SIZE = 100000;
        public ConcurrentStack<order> orderStack;
        public ConcurrentStack<cbMsg.order> msgStack;

        public Dictionary<string, crypto> cryptos;

        private coinbase_connection.coinbase_connection ws_order;
        private coinbase_connection.coinbase_restAPI api;
        private string ws_url;

        private Thread orderListeningTh;
        private bool stopListening;

        public void initialize(string apiFile,string url, Dictionary<string, crypto> cryptos, Action<string> logFunc = null)
        {
            this.cryptos = cryptos;
            this.ws_order = new coinbase_connection.coinbase_connection();
            this.api = new coinbase_connection.coinbase_restAPI();
            this.ws_order.readApiJson(apiFile);
            this.api.readApiJson(apiFile);
            if(logFunc != null)
            {
                this.addLog = logFunc;
                this.ws_order.addLog = logFunc;
                this.api.addLog = logFunc;
            }
            this.ws_url = url;
            this.ws_order.connect(url);
            WebSocketState st;
            st = this.ws_order.getState();
            int i = 0;
            while (st.ToString() != "Open")
            {
                System.Threading.Thread.Sleep(100);
                st = this.ws_order.getState();
                ++i;
                if (i > 50)
                {
                    this.addLog("[ERROR] Failed to connect.");
                    return;
                }
            }
            if (st.ToString() == "Open")
            {
                this.addLog("[INFO] Connected Successfully");
                this.ws_order.startListen(cbChannels.heartbeats);
                this.ws_order.startListen(cbChannels.user);
                this.orderListeningTh = new Thread(new ThreadStart(this.listening));
                this.orderListeningTh.Start();
            }
            else
            {
                this.addLog("[ERROR] The websocket is not open.");
                return;
            }
        }
        public void readApiKey(string filename)
        {
            this.api.readApiJson(filename);
        }     
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
            if(!this.checkSize(cp,base_size,0,side))
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
            string order_id = this.getOrderId(product_id);
            var res = await this.api.createOrder(order_id,product_id, side, this.api.market_market_ioc(base_size.ToString("N8"), "base"), leverage, margin_type, retail_portfolio, preview_id);
            
            if(res.IsSuccessStatusCode)
            {
                ord.product_id = product_id;
                ord.client_order_id = order_id;
                //ord.order_id = tempjson["order_id"];
                ord.price = 0;
                ord.side = side;
                ord.status = "PENDING";//Enum?
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
            if (!this.checkSize(cp, base_size,limit_price,side))
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
            string order_id = this.getOrderId(product_id);
            var res = await this.api.createOrder(order_id,product_id, side, this.api.sor_limit_ioc(base_size.ToString("N8"), limit_price.ToString("N2")), leverage, margin_type, retail_portfolio, preview_id);

            if (res.IsSuccessStatusCode)
            {
                ord.product_id = product_id;
                ord.client_order_id = order_id;
                //ord.order_id = tempjson["order_id"];
                ord.price = limit_price;
                ord.side = side;
                ord.status = "PENDING";//Enum?
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
            if (!this.checkSize(cp, base_size, limit_price, side))
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
            string str_post_only = "false";
            if(post_only)
            {
                str_post_only = "true";
            }
            string order_id = this.getOrderId(product_id);
            var res = await this.api.createOrder(order_id,product_id, side, this.api.limit_limit_gtc(base_size.ToString(), limit_price.ToString(), str_post_only), leverage, margin_type, retail_portfolio, preview_id);

            if (res.IsSuccessStatusCode)
            {
                ord.product_id = product_id;
                ord.client_order_id = order_id;
                //ord.order_id = tempjson["order_id"];
                ord.price = limit_price;
                ord.side = side;
                ord.status = "PENDING";//Enum?
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
            if (!this.checkSize(cp, base_size, limit_price, side))
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
            string str_post_only = "false";
            if (post_only)
            {
                str_post_only = "true";
            }
            string order_id = this.getOrderId(product_id);
            var res = await this.api.createOrder(order_id,product_id, side, this.api.limit_limit_gtd(base_size.ToString("N8"), limit_price.ToString("N2"), str_post_only,end_time.ToString("yyyy-MM-ddTHH:mm:ssZ")), leverage, margin_type, retail_portfolio, preview_id);

            if (res.IsSuccessStatusCode)
            {
                ord.product_id = product_id;
                ord.client_order_id = order_id;
                //ord.order_id = tempjson["order_id"];
                ord.price = limit_price;
                ord.side = side;
                ord.status = "PENDING";//Enum?
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
            if (!this.checkSize(cp, base_size, limit_price, side))
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
            string order_id = this.getOrderId(product_id);
            var res = await this.api.createOrder(order_id,product_id, side, this.api.limit_limit_fok(base_size.ToString("N8"), limit_price.ToString("N2")), leverage, margin_type, retail_portfolio, preview_id);

            if (res.IsSuccessStatusCode)
            {
                ord.product_id = product_id;
                ord.client_order_id = order_id;
                //ord.order_id = tempjson["order_id"];
                ord.price = limit_price;
                ord.side = side;
                ord.status = "PENDING";//Enum?
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
            if (!this.checkSize(cp, base_size, limit_price, side))
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
            string order_id = this.getOrderId(product_id);
            var res = await this.api.createOrder(order_id,product_id, side, this.api.stop_limit_stop_limit_gtc(base_size.ToString("N8"), limit_price.ToString("N2"), stop_price.ToString("N2"), stop_direction), leverage, margin_type, retail_portfolio, preview_id);

            if (res.IsSuccessStatusCode)
            {
                ord.product_id = product_id;
                ord.client_order_id = order_id;
                //ord.order_id = tempjson["order_id"];
                ord.price = limit_price;
                ord.side = side;
                ord.status = "PENDING";//Enum?
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
            if (!this.checkSize(cp, base_size, limit_price, side))
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
            string order_id = this.getOrderId(product_id);
            var res = await this.api.createOrder(order_id, product_id, side, this.api.stop_limit_stop_limit_gtd(base_size.ToString("N8"), limit_price.ToString("N2"), stop_price.ToString("N2"),end_time.ToString("yyyy-MM-ddTHH:mm:ssZ"), stop_direction), leverage, margin_type, retail_portfolio, preview_id);

            if (res.IsSuccessStatusCode)
            {
                ord.product_id = product_id;
                ord.client_order_id = order_id;
                //ord.order_id = tempjson["order_id"];
                ord.price = limit_price;
                ord.side = side;
                ord.status = "PENDING";//Enum?
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
            if (!this.checkSize(cp, base_size, limit_price, side))
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
            string order_id = this.getOrderId(product_id);
            var res = await this.api.createOrder(order_id,product_id, side, this.api.trigger_bracket_gtc(base_size.ToString("N8"), limit_price.ToString("N2"), stop_trigger_price.ToString("N2")), leverage, margin_type, retail_portfolio, preview_id);

            if (res.IsSuccessStatusCode)
            {
                ord.product_id = product_id;
                ord.client_order_id = order_id;
                //ord.order_id = tempjson["order_id"];
                ord.price = limit_price;
                ord.side = side;
                ord.status = "PENDING";//Enum?
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
            if (!this.checkSize(cp, base_size,limit_price,side))
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
            string order_id = this.getOrderId(product_id);
            var res = await this.api.createOrder(order_id, product_id, side, this.api.trigger_bracket_gtd(base_size.ToString("N8"), limit_price.ToString("N2"), stop_trigger_price.ToString("N2"), end_time.ToString("yyyy-MM-ddTHH:mm:ssZ")), leverage, margin_type, retail_portfolio, preview_id);

            if (res.IsSuccessStatusCode)
            {
                ord.product_id = product_id;
                ord.client_order_id = order_id;
                //ord.order_id = tempjson["order_id"];
                ord.price = limit_price;
                ord.side = side;
                ord.status = "PENDING";//Enum?
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
                strPr = newPr.ToString();
            }
            else
            {
                strPr = orgOrd.price.ToString();
            }
            if (newSize >= 0 && newSize != orgOrd.size)
            {
                double pr = newPr;
                if(pr <= 0)
                {
                    pr = orgOrd.price;
                }
                if (!this.checkSize(cp, newSize, pr, orgOrd.side))
                {
                    this.addLog("[ERROR] Invalid size");
                    return null;
                }
                strSize = newSize.ToString();
            }
            else
            {
                strSize = orgOrd.size.ToString();
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
            string[] order_ids = { orgOrd.order_id };
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
            else if((int)(limit_price / cp.quote_increment) < cp.minPr || (int)(limit_price / cp.quote_increment) > cp.maxPr)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool checkSize(crypto cp, double base_size,double limit_price = 0,string side = "")
        {
            if(base_size > cp.maxBaseSize || base_size > this.maxBaseSize)
            {
                return false;
            }
            double price;
            if(limit_price > 0)
            {
                price = limit_price;
            }
            else if(side == "BUY" && cp.bestask > 0)
            {
                price = (double)cp.bestask * cp.quote_increment;
            }
            else if(side == "SELL" && cp.bestbid > 0)
            {
                price = (double)cp.bestbid * cp.quote_increment;
            }
            else
            {
                addLog(cp.bestask.ToString() + " " + cp.bestbid.ToString());
                return false;
            }
            if(base_size * price > cp.maxQuoteSize || base_size * price > this.maxQuoteSize)
            {
                addLog(base_size.ToString() + " " + price.ToString());
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
        public string getOrderId(string symbol)
        {
            int current_no = Interlocked.Increment(ref this.order_no);
            return symbol + DateTime.Now.ToString("yyyyMMddHHmmss") + current_no.ToString("D8");
        }

        private void listening()
        {
            this.addLog("Start listening order messages...");
            cbMsg.message msg = new cbMsg.message();
            cbMsg.jsOrder jso = new cbMsg.jsOrder();
            cbMsg.order ordMsg = new cbMsg.order();
            byte[] buffer = new byte[1073741824];
            while (true)
            {
                var segment = new ArraySegment<byte>(buffer);
                var result = this.ws_order.recv(ref segment);
                if (result.IsFaulted == false)
                {
                    if (result.Result.MessageType == WebSocketMessageType.Close)
                    {
                        this.addLog("Endpoint Closed");
                        break;
                    }

                    if (result.Result.MessageType == WebSocketMessageType.Binary)
                    {
                        this.addLog("Result Binary");
                        break;
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
                            this.addLog("Too Large Message   " + count.ToString());
                            break;
                        }
                        segment = new ArraySegment<byte>(buffer, count, buffer.Length - count);
                        result = this.ws_order.recv(ref segment);

                        count += result.Result.Count;
                    }
                    if (count >= buffer.Length)
                    {
                        break;
                    }

                    var message = Encoding.UTF8.GetString(buffer, 0, count);

                    parser.parseMsg(message, ref msg);
                    if (msg.channel == "user")
                    {
                        string temp;
                        string target = "\"type\":";
                        int start = msg.events.IndexOf(target) + target.Length + 1;
                        int end = msg.events.IndexOf(",", start) - 1;

                        if (msg.events.Substring(start, end - start) == "update")
                        {
                            target = "\"orders\":[";
                            start = msg.events.IndexOf(target) + target.Length;
                            int endBracket = msg.events.IndexOf("]", start);
                            while (start > 0 && start < endBracket)
                            {
                                end = msg.events.IndexOf("}", start) + 1;
                                temp = msg.events.Substring(start, end - start);
                                coinbase_connection.parser.parseOrder(temp, ref jso);
                                ordMsg.addMsg(jso);
                                if (cryptos.ContainsKey(ordMsg.product_id))
                                {
                                    crypto cp = cryptos[ordMsg.product_id];
                                    while(true)
                                    {
                                        if(Interlocked.Exchange(ref cp.orderUpdating,1) == 0)
                                        {
                                            if (cp.liveOrders.ContainsKey(ordMsg.client_order_id))
                                            {
                                                order obj = cp.liveOrders[ordMsg.client_order_id];
                                                obj.setMsg(ordMsg);
                                            }
                                            else
                                            {
                                                order ord;
                                                while(true)
                                                {
                                                    if(this.orderStack.TryPop(out ord))
                                                    {
                                                        break;
                                                    }
                                                }
                                                ord.setMsg(ordMsg);
                                                cp.liveOrders.Add(ordMsg.client_order_id, ord);
                                            }
                                            cp.orderUpdating = 0;
                                            break;
                                        }
                                    }


                                }
                                else
                                {
                                    this.addLog("[ERROR] product not found id:" + ordMsg.product_id);
                                }
                                start = msg.events.IndexOf("{", end);
                            }
                        }
                    }
                }
                if(this.stopListening)
                {
                    break;
                }
            }
        }

        private int order_no;
        private string random_str;

        private double maxBaseSize = 1;
        private double maxQuoteSize = 10000;

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

        public void setMsg(cbMsg.order msg)
        {
            this.order_id = msg.order_id;
            this.client_order_id = msg.client_order_id;
            this.status = msg.status;
            this.product_id = msg.product_id;
            this.side = msg.order_side;
            this.price = msg.limit_price;
            this.size = msg.leaves_quantity + msg.filled_value;
            this.open_size = msg.leaves_quantity;
            this.executed_size = msg.filled_value;
            this.updated_time = DateTime.Now;
        }
    }
}
