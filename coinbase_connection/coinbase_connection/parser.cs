using cbMsg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace coinbase_connection
{
    public class parser
    {
        static public void parseMsg(string strjs, ref message msg)
        {
            string target;
            int pos = -1;
            int last = -1;

            target = "\"channel\":";
            pos = strjs.IndexOf(target) + target.Length;
            if (pos >= 0)
            {
                last = strjs.IndexOf(",", pos);
                if (last < 0)
                {
                    last = strjs.Length - 1;
                }
                msg.channel = strjs.Substring(pos, last - pos).Replace("\"", "");
            }
            else
            {
                return;
            }

            target = "\"client_id\":";
            pos = strjs.IndexOf(target) + target.Length;
            if (pos >= 0)
            {
                last = strjs.IndexOf(",", pos);
                if (last < 0)
                {
                    last = strjs.Length - 1;
                }
                msg.client_id = strjs.Substring(pos, last - pos).Replace("\"", "");
            }
            else
            {
                return;
            }

            target = "\"timestamp\":";
            pos = strjs.IndexOf(target) + target.Length;
            if (pos >= 0)
            {
                last = strjs.IndexOf(",", pos);
                if (last < 0)
                {
                    last = strjs.Length - 1;
                }
                msg.timestamp = strjs.Substring(pos, last - pos).Replace("\"", "");
            }
            else
            {
                return;
            }

            target = "\"sequence_num\":";
            pos = strjs.IndexOf(target) + target.Length;
            if (pos >= 0)
            {
                last = strjs.IndexOf(",", pos);
                if (last < 0)
                {
                    last = strjs.Length - 1;
                }
                msg.sequence_num = Int64.Parse(strjs.Substring(pos, last - pos));
            }
            else
            {
                return;
            }
            target = "\"events\":";
            pos = strjs.IndexOf(target) + target.Length;
            if (pos >= 0)
            {
                last = strjs.Length - 1;
                msg.events = strjs.Substring(pos, last - pos);
            }
            else
            {
                return;
            }
        }

        static public string findSymbol(string str)
        {
            string target = "\"product_id\":";
            int start = str.IndexOf(target) + target.Length + 1;
            if(start > 0)
            {
                int end = str.IndexOf("\"", start);
                return str.Substring(start, end - start);
            }
            else
            {
                return "";
            }
        }
        static public string findEventType(string str)
        {
            string target = "\"type\":";
            int start = str.IndexOf(target) + target.Length + 1;
            if (start > 0)
            {
                int end = str.IndexOf("\"", start);
                return str.Substring(start, end - start);
            }
            else
            {
                return "";
            }
        }

        static public void parseUpdate(string str,ref cbMsg.jsUpdate obj)
        {
            obj = JsonSerializer.Deserialize<cbMsg.jsUpdate>(str);
        }
        static public void parseTrades(string str, ref cbMsg.jsTrades obj)
        {
            obj = JsonSerializer.Deserialize<cbMsg.jsTrades>(str);
        }
        static public void parseStatus(string str,ref cbMsg.product_status obj)
        {
            obj = JsonSerializer.Deserialize<cbMsg.product_status>(str);
        }
        static public void parseOrder(string str,ref cbMsg.jsOrder obj)
        {
            obj = JsonSerializer.Deserialize<cbMsg.jsOrder>(str);
        }
        static public void parsePerpetualPos(string str,ref cbMsg.jsPerpetualPosition obj)
        {
            obj = JsonSerializer.Deserialize<cbMsg.jsPerpetualPosition>(str);
        }
        static public void parseFuturePos(string str, ref cbMsg.jsFuturePosition obj)
        {
            obj = JsonSerializer.Deserialize<cbMsg.jsFuturePosition>(str);
        }

        static public void jsUpdateToTrades(string symbol,cbMsg.jsUpdate jsup, ref cbMsg.trades obj)
        {
            obj.msg_type = "l2_data";
            obj.product_id = symbol;
            obj.time = jsup.event_time;
            obj.price = double.Parse(jsup.price_level);
            obj.side = jsup.side;
            obj.size = double.Parse(jsup.new_quantity);
            obj.trade_id = "";
        }
        static public void jsTradesToTrades(cbMsg.jsTrades jstr, ref cbMsg.trades obj)
        {
            obj.msg_type = "trades";
            obj.product_id = jstr.product_id;
            obj.time = jstr.time;
            obj.price = double.Parse(jstr.price);
            obj.side = jstr.side;
            obj.size = double.Parse(jstr.size);
            obj.trade_id = jstr.trade_id;
        }
        static public void jsOrderToOrder(cbMsg.jsOrder jso,ref cbMsg.order obj)
        {
            obj.addMsg(jso);
        }

        static public void strToList(string org,ref List<string> lis)
        {
            int nest = 0;
            int i = org.IndexOf("[",0) + 1;
            int start = i;

            int comma = 0;
            int sBracket = 0;
            int eBracket = 0;
            int sBrace = 0;
            int eBrace = 0;

            int smallerS = 0;
            int smallerE = 0;

            string item;

            while (i < org.Length)
            {
                comma = org.IndexOf("[", i);
                sBracket = org.IndexOf("[", i);
                eBracket = org.IndexOf("]", i);
                sBrace = org.IndexOf("{", i);
                eBrace = org.IndexOf("}", i);

                if(nest == 0)
                {
                    if(comma > 0)
                    {
                        if (comma < sBracket && comma < sBrace)
                        {
                            item = org.Substring(start, comma - 1 - start);
                            lis.Add(item);
                            start = comma + 1;
                            i = comma + 1;
                        }
                        else
                        {
                            if(sBracket < sBrace)
                            {
                                ++nest;
                                i = sBracket + 1;
                            }
                            else
                            {
                                ++nest;
                                i = sBrace + 1;
                            }
                        }

                        if ((eBracket > 0 && comma > eBracket) || (eBrace > 0 && comma > eBrace))
                        {
                            //Error!!!
                        }
                    }
                    else
                    {
                        item = org.Substring(start,org.Length - 1 - start);
                        lis.Add(item);
                        break;
                    }
                }
                else
                {

                    if(sBrace < sBracket)
                    {
                        smallerS = sBrace;
                    }
                    else
                    {
                        smallerS = sBracket;
                    }

                    if (eBrace < eBracket)
                    {
                        smallerE = eBrace;
                    }
                    else
                    {
                        smallerE = eBracket;
                    }
                    if(smallerS < smallerE)
                    {
                        ++nest;
                        i = smallerS + 1;
                    }
                    else
                    {
                        --nest;
                        i = smallerE + 1;
                    }
                }
            }
        }
    }
}

namespace cbMsg
{
    public struct message
    {
        public string channel;
        public string client_id;
        public string timestamp;
        public long sequence_num;
        public string events;
    }

    public struct heartbeats
    {
        public string current_time { get; set; }
        public string heartbeat_counter { get; set; }
    }

    public struct jsCandle
    {
        public string start { get; set; }
        public string high { get; set; }
        public string low { get; set; }
        public string open { get; set; }
        public string close { get; set; }
        public string volume { get; set; }
        public string product_id { get; set; }
    }
    public struct candle
    {
        public candle(jsCandle jsc)
        {
            this.product_id = jsc.product_id;
            this.start = Double.Parse(jsc.start);
            this.high = Double.Parse(jsc.high);
            this.low = Double.Parse(jsc.low);
            this.open = Double.Parse(jsc.open);
            this.close = Double.Parse(jsc.close);
            this.volume = Double.Parse(jsc.volume);
        }
        public double start;
        public double high;
        public double low;
        public double open;
        public double close;
        public double volume;
        public string product_id;
    }

    public struct jsTrades
    {
        public string trade_id { get; set; }
        public string product_id { get; set; }
        public string price { get; set; }
        public string size { get; set; }
        public string side { get; set; }
        public string time { get; set; }
    }
    public struct trades
    {
        public trades(jsTrades jst)
        {
            this.msg_type = "";
            this.event_type = "";
            this.trade_id = jst.trade_id;
            this.product_id = jst.product_id;
            this.price = Double.Parse(jst.price);
            this.size = Double.Parse(jst.size);
            this.side = jst.side;
            this.time = jst.time;
        }
        public string msg_type;
        public string event_type;
        public string trade_id;
        public string product_id;
        public double price;
        public double size;
        public string side;
        public string time;
    }

    public struct product_status
    {
        public string product_type { get; set; }
        public string id { get; set; }
        public string base_currency { get; set; }
        public string quote_currency { get; set; }
        public string base_increment { get; set; }
        public string quote_increment { get; set; }
        public string display_name { get; set; }
        public string status { get; set; }
        public string status_message { get; set; }
        public string min_market_funds { get; set; }
    }

    public struct jsTicker
    {
        public string type { get; set; }
        public string product_id { get; set; }
        public string price { get; set; }
        public string volume_24_h { get; set; }
        public string low_24_h { get; set; }
        public string high_24_h { get; set; }
        public string low_52_w { get; set; }
        public string high_52_w { get; set; }
        public string price_percent_chg_24_h { get; set; }
        public string best_bid { get; set; }
        public string best_bid_quantity { get; set; }
        public string best_ask { get; set; }
        public string best_ask_quantity { get; set; }
    }

    public struct ticker
    {
        public ticker(jsTicker jst)
        {
            this.type = jst.type;
            this.product_id = jst.product_id;
            this.price = Double.Parse(jst.price);
            this.volume_24_h = Double.Parse(jst.volume_24_h);
            this.low_24_h = Double.Parse(jst.low_24_h);
            this.high_24_h = Double.Parse(jst.high_24_h);
            this.low_52_w = Double.Parse(jst.low_52_w);
            this.high_52_w = Double.Parse(jst.high_52_w);
            this.price_percent_chg_24_h = Double.Parse(jst.price_percent_chg_24_h);
            this.best_bid = Double.Parse(jst.best_bid);
            this.best_bid_quantity = Double.Parse(jst.best_bid_quantity);
            this.best_ask = Double.Parse(jst.best_ask);
            this.best_ask_quantity = Double.Parse(jst.best_ask_quantity);
        }
        public string type;
        public string product_id;
        public double price;
        public double volume_24_h;
        public double low_24_h;
        public double high_24_h;
        public double low_52_w;
        public double high_52_w;
        public double price_percent_chg_24_h;
        public double best_bid;
        public double best_bid_quantity;
        public double best_ask;
        public double best_ask_quantity;
    }

    public struct jsUpdate
    {
        public string side { get; set; }
        public string event_time { get; set; }
        public string price_level { get; set; }
        public string new_quantity { get; set; }
    }
    public struct update
    {
        public update(jsUpdate jsu)
        {
            this.side = jsu.side;
            this.event_time = jsu.event_time;
            this.price_level = Double.Parse(jsu.price_level);
            this.new_quantity = Double.Parse(jsu.new_quantity);
        }
        public string side;
        public string event_time;
        public double price_level;
        public double new_quantity;
    }
    public struct jsOrder
    {
        public string avg_price{ get; set; }
        public string cancel_reason{ get; set; }
        public string client_order_id{ get; set; }
        public string completion_percentage{ get; set; }
        public string contract_expiry_type{ get; set; }
        public string cumulative_quantity{ get; set; }
        public string filled_value{ get; set; }
        public string leaves_quantity{ get; set; }
        public string limit_price{ get; set; }
        public string number_of_fills{ get; set; }
        public string order_id{ get; set; }
        public string order_side{ get; set; }
        public string order_type{ get; set; }
        public string outstanding_hold_amount{ get; set; }
        public string post_only{ get; set; }
        public string product_id{ get; set; }
        public string product_type{ get; set; }
        public string reject_reason{ get; set; }
        public string retail_portfolio_id{ get; set; }
        public string risk_managed_by{ get; set; }
        public string status{ get; set; }
        public string stop_price{ get; set; }
        public string time_in_force{ get; set; }
        public string total_fees{ get; set; }
        public string total_value_after_fees{ get; set; }
        public string trigger_status{ get; set; }
        public string creation_time{ get; set; }
        public string end_time{ get; set; }
        public string start_time{ get; set; }
    }

    public struct order
    {
        public order(jsOrder jso)
        {
            this.avg_price = Double.Parse(jso.avg_price);
            this.cancel_reason = jso.cancel_reason;
            this.client_order_id = jso.client_order_id;
            this.completion_percentage = Double.Parse(jso.completion_percentage);
            this.contract_expiry_type = jso.contract_expiry_type;
            this.cumulative_quantity = Double.Parse(jso.cumulative_quantity);
            this.filled_value = Double.Parse(jso.filled_value);
            this.leaves_quantity = Double.Parse(jso.leaves_quantity);
            this.limit_price = Double.Parse(jso.limit_price);
            this.number_of_fills = Int32.Parse(jso.number_of_fills);
            this.order_id = jso.order_id;
            this.order_side = jso.order_side;
            this.order_type = jso.order_type;
            this.outstanding_hold_amount = Double.Parse(jso.outstanding_hold_amount);
            this.post_only = bool.Parse(jso.post_only);
            this.product_id = jso.product_id;
            this.product_type = jso.product_type;
            this.reject_reason = jso.reject_reason;
            this.retail_portfolio_id = jso.retail_portfolio_id;
            this.risk_managed_by = jso.risk_managed_by;
            this.status = jso.status;
            if (jso.stop_price != "")
            {
                this.stop_price = Double.Parse(jso.stop_price);
            }
            else
            {
                this.stop_price = -1;
            }
            this.time_in_force = jso.time_in_force;
            this.total_fees = Double.Parse(jso.total_fees);
            this.total_value_after_fees = Double.Parse(jso.total_value_after_fees);
            this.trigger_status = jso.trigger_status;
            this.creation_time = jso.creation_time;
            this.end_time = jso.end_time;
            this.start_time = jso.start_time;
        }

        public void addMsg(jsOrder jso)
        {
            if(jso.avg_price != "")
            {
                this.avg_price = Double.Parse(jso.avg_price);
            }
            this.cancel_reason = jso.cancel_reason;
            this.client_order_id = jso.client_order_id;
            if(jso.completion_percentage !="")
            {
                this.completion_percentage = Double.Parse(jso.completion_percentage);
            }
            this.contract_expiry_type = jso.contract_expiry_type;
            if(jso.cumulative_quantity !="")
            {
                this.cumulative_quantity = Double.Parse(jso.cumulative_quantity);
            }
            if(jso.filled_value != "")
            {
                this.filled_value = Double.Parse(jso.filled_value);
            }
            if(jso.leaves_quantity !="")
            {
                this.leaves_quantity = Double.Parse(jso.leaves_quantity);
            }
            if(jso.limit_price != "")
            {
                this.limit_price = Double.Parse(jso.limit_price);
            }
            if(jso.number_of_fills != "")
            {
                this.number_of_fills = Int32.Parse(jso.number_of_fills);
            }
            this.order_id = jso.order_id;
            this.order_side = jso.order_side;
            this.order_type = jso.order_type;
            if(jso.outstanding_hold_amount != "")
            {
                this.outstanding_hold_amount = Double.Parse(jso.outstanding_hold_amount);
            }
            if(jso.post_only != "")
            {
                this.post_only = bool.Parse(jso.post_only);
            }
            this.product_id = jso.product_id;
            this.product_type = jso.product_type;
            this.reject_reason = jso.reject_reason;
            this.retail_portfolio_id = jso.retail_portfolio_id;
            this.risk_managed_by = jso.risk_managed_by;
            this.status = jso.status;
            if (jso.stop_price != "")
            {
                this.stop_price = Double.Parse(jso.stop_price);
            }
            else
            {
                this.stop_price = -1;
            }
            this.time_in_force = jso.time_in_force;
            if(jso.total_fees != "")
            {
                this.total_fees = Double.Parse(jso.total_fees);
            }
            if(jso.total_value_after_fees != "")
            {
                this.total_value_after_fees = Double.Parse(jso.total_value_after_fees);
            }
            this.trigger_status = jso.trigger_status;
            this.creation_time = jso.creation_time;
            this.end_time = jso.end_time;
            this.start_time = jso.start_time;
        }

        public double avg_price;
        public string cancel_reason;
        public string client_order_id;
        public double completion_percentage;
        public string contract_expiry_type;
        public double cumulative_quantity;
        public double filled_value;
        public double leaves_quantity;
        public double limit_price;
        public int number_of_fills;
        public string order_id;
        public string order_side;
        public string order_type;
        public double outstanding_hold_amount;
        public bool post_only;
        public string product_id;
        public string product_type;
        public string reject_reason;
        public string retail_portfolio_id;
        public string risk_managed_by;
        public string status;
        public double stop_price;
        public string time_in_force;
        public double total_fees;
        public double total_value_after_fees;
        public string trigger_status;
        public string creation_time;
        public string end_time;
        public string start_time;
    }

    public struct jsPerpetualPosition
    {
        public string product_id {  get; set; }
        public string portfolio_uuid { get; set; }
        public string vwap { get; set; }
        public string entry_vwap { get; set; }
        public string position_side { get; set; }
        public string margin_type { get; set; }
        public string net_size { get; set; }
        public string buy_order_size { get; set; }
        public string sell_order_size { get; set; }
        public string leverage { get; set; }
        public string mark_price { get; set; }
        public string liquidation_price { get; set; }
        public string im_notional { get; set; }
        public string mm_notional { get; set; }
        public string position_notional { get; set; }
        public string unrealized_pnl { get; set; }
        public string aggregated_pnl { get; set; }
    }
    public struct perpetualPosition
    {
        public void addMsg(jsPerpetualPosition jspp)
        {
            this.product_id = jspp.product_id;
            this.portfolio_uuid = jspp.portfolio_uuid;
            if(jspp.vwap != "")
            {
                this.vwap = Double.Parse(jspp.vwap);
            }
            else
            {
                this.vwap = -1;
            }
            if (jspp.entry_vwap != "")
            {
                this.entry_vwap = Double.Parse(jspp.entry_vwap);
            }
            else
            {
                this.entry_vwap = -1;
            }
            this.position_side = jspp.position_side;
            this.margin_type = jspp.margin_type;
            if (jspp.net_size != "")
            {
                this.net_size = Double.Parse(jspp.net_size);
            }
            else
            {
                this.net_size = 0;
            }
            if (jspp.buy_order_size != "")
            {
                this.buy_order_size = Double.Parse(jspp.buy_order_size);
            }
            else
            {
                this.buy_order_size = -1;
            }
            if (jspp.sell_order_size != "")
            {
                this.sell_order_size = Double.Parse(jspp.sell_order_size);
            }
            else
            {
                this.sell_order_size = -1;
            }
            if (jspp.leverage != "")
            {
                this.leverage = Double.Parse(jspp.leverage);
            }
            else
            {
                this.leverage = 1;
            }
            if (jspp.mark_price != "")
            {
                this.mark_price = Double.Parse(jspp.mark_price);
            }
            else
            {
                this.mark_price = -1;
            }
            if (jspp.liquidation_price != "")
            {
                this.liquidation_price = Double.Parse(jspp.liquidation_price);
            }
            else
            {
                this.liquidation_price = -1;
            }
            if (jspp.im_notional != "")
            {
                this.im_notional = Double.Parse(jspp.im_notional);
            }
            else
            {
                this.im_notional = 0;
            }
            if (jspp.mm_notional != "")
            {
                this.mm_notional = Double.Parse(jspp.mm_notional);
            }
            else
            {
                this.mm_notional = 0;
            }
            if (jspp.position_notional != "")
            {
                this.position_notional = Double.Parse(jspp.position_notional);
            }
            else
            {
                this.position_notional = 0;
            }
            if (jspp.unrealized_pnl != "")
            {
                this.unrealized_pnl = Double.Parse(jspp.unrealized_pnl);
            }
            else
            {
                this.unrealized_pnl = 0;
            }
            if (jspp.aggregated_pnl != "")
            {
                this.aggregated_pnl = Double.Parse(jspp.aggregated_pnl);
            }
            else
            {
                this.aggregated_pnl = 0;
            }
        }
        public string product_id;
        public string portfolio_uuid;
        public double vwap;
        public double entry_vwap;
        public string position_side;
        public string margin_type;
        public double net_size;
        public double buy_order_size;
        public double sell_order_size;
        public double leverage;
        public double mark_price;
        public double liquidation_price;
        public double im_notional;
        public double mm_notional;
        public double position_notional;
        public double unrealized_pnl;
        public double aggregated_pnl;
    }
    public struct jsFuturePosition
    {
        public string product_id { get; set; }
        public string side { get; set; }
        public string number_of_contracts { get; set; }
        public string realized_pnl { get; set; }
        public string unrealized_pnl { get; set; }
        public string entry_price { get; set; }
    }
    public struct futurePosition
    {
        public void addMsg(jsFuturePosition jsfp)
        {
            this.product_id = jsfp.product_id;
            this.side = jsfp.side;
            if (jsfp.number_of_contracts != "")
            {
                this.number_of_contracts = Double.Parse(jsfp.number_of_contracts);
            }
            else
            {
                this.number_of_contracts = -1;
            }
            if (jsfp.realized_pnl != "")
            {
                this.realized_pnl = Double.Parse(jsfp.realized_pnl);
            }
            else
            {
                this.realized_pnl = 0;
            }
            if (jsfp.unrealized_pnl != "")
            {
                this.unrealized_pnl = Double.Parse(jsfp.unrealized_pnl);
            }
            else
            {
                this.unrealized_pnl = 0;
            }
            if (jsfp.entry_price != "")
            {
                this.entry_price = Double.Parse(jsfp.entry_price);
            }
            else
            {
                this.entry_price = -1;
            }
        }
        public string product_id;
        public string side;
        public double number_of_contracts;
        public double realized_pnl;
        public double unrealized_pnl;
        public double entry_price;
    }
}
