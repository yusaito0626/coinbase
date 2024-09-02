using cbMsg;
using coinbase_enum;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinbase_main
{
    public class quoteManager
    {
        quoteManager()
        {

        }
        public bool update_quotes(ref crypto cp)
        {
            trades td;
            if(!cp.quotesInitialized)
            {
                while (cp.qtQueue.Count > 0)
                {
                    if (cp.qtQueue.TryDequeue(out td))
                    {
                        if(!cp.quotesInitialized && td.msg_type == "trades")
                        {
                            cp.initializeQuotes(td);
                        }
                        switch(td.msg_type)
                        {
                            case "l2_data":
                                cp.updateOneQuote(td);
                                break;
                            case "trades":
                                cp.updateTrade(td);
                                break;
                        }
                        this.feedStack.Push(td);
                    }
                }
            }
            else
            {
                while (cp.qtQueue.Count > 0)
                {
                    if (cp.qtQueue.TryDequeue(out td))
                    {
                        switch (td.msg_type)
                        {
                            case "l2_data":
                                cp.updateOneQuote(td);
                                break;
                            case "trades":
                                cp.updateTrade(td);
                                break;
                        }
                        this.feedStack.Push(td);
                    }
                }
            }
            cp.updating = 0;
            return false;
        }

        //Market order/limit order taking the quotes
        public void executeOrder(ref crypto cp,ref order ord)
        {
            cp.executeOrder(ref ord);
            this.msgQueue.Enqueue(ord.ToString());
        }
        //Orders taken by other trades
        public void executeLimitOrder(ref crypto cp,cbMsg.trades trd)
        {
            if (trd.msg_type != "trades")
            {
                return;
            }
            int i = 0;
            int tradedPr = (int)(trd.price / cp.quote_increment);
            double executedBaseAmount = 0;
            while (true)
            {
                if (Interlocked.Exchange(ref cp.orderUpdating, 1) == 0)
                {
                    if (trd.side == "SELL")
                    {
                        int idx = cp.bestbid;
                        quote qt = cp.quotes[idx];
                        int currentPr = qt.price;

                        while (currentPr >= tradedPr)
                        {
                            executedBaseAmount = qt.updateOrders(trd, cp.quote_increment);
                            cp.baseExecutionBuy += executedBaseAmount;
                            cp.quoteExecutionBuy += executedBaseAmount * (double)qt.price * cp.quote_increment;
                            --idx;
                            if (idx < cp.minPr)
                            {
                                break;
                            }
                            qt = cp.quotes[idx];
                            currentPr = qt.price;
                        }
                    }
                    else if (trd.side == "BUY")
                    {
                        int idx = cp.bestask;
                        quote qt = cp.quotes[idx];
                        int currentPr = qt.price;
                        while (currentPr <= tradedPr)
                        {
                            executedBaseAmount = qt.updateOrders(trd, cp.quote_increment);
                            cp.baseExecutionSell += executedBaseAmount;
                            cp.quoteExecutionSell += executedBaseAmount * (double)qt.price * cp.quote_increment;
                            ++idx;
                            if (idx > cp.maxPr)
                            {
                                break;
                            }
                            qt = cp.quotes[idx];
                            currentPr = qt.price;
                        }
                    }
                    cp.orderUpdating = 0;
                    break;
                }
                else
                {
                    ++i;
                    if (i > 100000)
                    {
                        i = 0;
                        System.Threading.Thread.Sleep(0);
                    }
                }
            }
        }

        public ConcurrentStack<cbMsg.trades> feedStack;
        public Queue<string> msgQueue;

        public Action<string> _addLog = (str) => { Console.WriteLine(str); };
        public void addLog(string str, logType type = logType.NONE)
        {
            switch (type)
            {
                case logType.INFO:
                    this._addLog("[INFO] " + str);
                    break;
                case logType.WARNING:
                    this._addLog("[WARNING] " + str);
                    break;
                case logType.ERROR:
                    this._addLog("[ERROR] " + str);
                    break;
                case logType.CRITICAL:
                    this._addLog("[CRITICAL] " + str);
                    break;
                case logType.NONE:
                default:
                    this._addLog(str);
                    break;
            }
        }

        private static quoteManager _instance;
        private static readonly object _lockObject = new object();

        public static quoteManager GetInstance()
        {
            lock (_lockObject)
            {
                if (_instance == null)
                {
                    _instance = new quoteManager();
                }
                return _instance;
            }
        }
    }
}
