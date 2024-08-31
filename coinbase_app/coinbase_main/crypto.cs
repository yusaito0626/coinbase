using cbMsg;
using coinbase_connection;
using System.Collections.Concurrent;
using System.Collections.Specialized;
using System.Diagnostics;
namespace coinbase_main
{
    public class quote
    {
        public quote()
        {
            this.ordQueue = new Queue<order>();
        }

        public void update(cbMsg.trades qt,double increment)
        {
            this.price = (int)(qt.price / increment);
            this.quantity = qt.size;
            this.updated_time = qt.time;
            if(this.quantity == 0)
            {
                this.side = "";
            }
            else
            {
                this.side = qt.side;
            }
        }
        public double updateOrders(cbMsg.trades qt,double increment)
        {
            double consumedQty = 0;
            double executedQty = 0;
            if(this.ordQueue.Count > 0)
            {
                if (this.price == (int)(qt.price / increment))
                {
                    int i = 0;
                    order ord = null;
                    order initOrd = null;
                    while (this.ordQueue.Count > 0)
                    {
                        ord = this.ordQueue.Dequeue();
                        if (i == 0)
                        {
                            if (ord.status != "FILLED" && ord.status != "CANCELLED" && (int)(ord.price / increment) == this.price)
                            {
                                ++i;
                                initOrd = ord;
                                if (ord.priorQuantity > qt.size)
                                {
                                    ord.priorQuantity -= qt.size;
                                    consumedQty += qt.size;
                                }
                                else
                                {
                                    double exceededQty = qt.size - ord.priorQuantity;
                                    consumedQty += ord.priorQuantity;
                                    ord.priorQuantity = 0;
                                    if (ord.open_size > exceededQty)
                                    {
                                        ord.open_size -= exceededQty;
                                        ord.executed_size += exceededQty;
                                        executedQty += exceededQty;
                                        ord.status = "PARTIAL";
                                    }
                                    else
                                    {
                                        executedQty += ord.open_size;
                                        ord.executed_size += ord.open_size;
                                        ord.open_size = 0;
                                        ord.status = "FILLED";
                                    }
                                }
                                this.ordQueue.Enqueue(ord);
                            }
                        }
                        else
                        {
                            if (ord == initOrd)
                            {
                                break;
                            }
                            if (ord.status != "FILLED" && ord.status != "CANCELLED" && (int)(ord.price / increment) == this.price)
                            {
                                if (ord.priorQuantity > qt.size)
                                {
                                    consumedQty += (qt.size - consumedQty);
                                    ord.priorQuantity -= qt.size;
                                }
                                else
                                {
                                    double exceededQty = qt.size - ord.priorQuantity;
                                    consumedQty += (ord.priorQuantity - consumedQty);
                                    ord.priorQuantity = 0;
                                    if (exceededQty > qt.size - (consumedQty + executedQty))
                                    {
                                        exceededQty = qt.size - (consumedQty + executedQty);
                                    }
                                    if (ord.open_size > exceededQty)
                                    {
                                        ord.open_size -= exceededQty;
                                        ord.executed_size += exceededQty;
                                        executedQty += exceededQty;
                                        ord.status = "PARTIAL";
                                    }
                                    else
                                    {
                                        executedQty += ord.open_size;
                                        ord.executed_size += ord.open_size;
                                        ord.open_size = 0;
                                        ord.status = "FILLED";
                                    }
                                }
                                this.ordQueue.Enqueue(ord);
                            }
                        }
                    }
                }
                else if((qt.side == "SELL" && this.price > (int)(qt.price / increment)) || (qt.side == "BUY" && this.price < (int)(qt.price / increment)))
                {
                    //Execute All
                    order ord = null;
                    while (this.ordQueue.Count > 0)
                    {
                        ord = this.ordQueue.Dequeue();
                        if ((int)(ord.price / increment) == this.price)
                        {
                            executedQty += ord.open_size;
                            ord.executed_size += ord.open_size;
                            ord.open_size = 0;
                            ord.status = "FILLED";
                        }
                    }
                }
            }
            return executedQty;
        }

        public string side;
        public int price;
        public double quantity;
        public string updated_time;

        //For sim
        public Queue<order> ordQueue;
    }
    public class crypto
    {
        public void updateOneQuote(cbMsg.trades qt)
        {
            int pr = (int)(qt.price / this.quote_increment);
            if (this.quotesInitialized)
            {
                if (this.quotes.ContainsKey(pr))
                {
                    this.quotes[pr].update(qt, this.quote_increment);
                    this.findToB(pr);
                }
            }
            else
            {
                if(this.quotes.ContainsKey(pr))
                {
                    this.quotes[pr].update(qt, this.quote_increment);
                }
                else
                {
                    this.quotes.Add(pr, new quote());
                    this.quotes[pr].update(qt, this.quote_increment);
                }
            }
        }

        public void updateTrade(cbMsg.trades trade)
        {
            this.baseTradedVolume += trade.size;
            this.quoteTradedVolume += trade.size * trade.price;

            this.last = (int)(trade.price / this.quote_increment);
            if(this.open == 0)
            {
                this.open = this.last;
            }
            if (this.last > this.high)
            {
                this.high = this.last;
            }
            if(this.last < this.low || this.low == 0)
            {
                this.low = this.last;
            }
        }

        public void findToB(int updated_pr)
        {
            quote current = this.quotes[updated_pr];
            if(updated_pr > this.bestbid && current.side == "bid")
            {
                this.bestbid = updated_pr;
            }
            else if (updated_pr < this.bestask && current.side == "offer")
            {
                this.bestask = updated_pr;
            }
            else if (updated_pr == this.bestbid && current.side == "")
            {
                int pr = updated_pr - 1;
                quote qt = this.quotes[pr];
                while(pr > this.minPr)
                {
                    if(qt.side == "bid")
                    {
                        this.bestbid = pr;
                        break;
                    }
                    --pr;
                }
                this.bestbid = pr;
            }
            else if (updated_pr == this.bestask && current.side == "")
            {
                int pr = updated_pr + 1;
                quote qt = this.quotes[pr];
                while (pr < this.maxPr)
                {
                    if (qt.side == "offer")
                    {
                        this.bestask = pr;
                        break;
                    }
                    ++pr;
                }
                this.bestask = pr;
            }
        }
        public void initializeQuotes(cbMsg.trades trade)
        {
            bool best = false;
            this.minPr = (int)(trade.price / this.quote_increment * 0.9);
            this.maxPr = (int)(trade.price / this.quote_increment * 1.1);

            SortedDictionary<int, quote> temp_quotes = new SortedDictionary<int, quote>();

            SortedDictionary<int, quote>.Enumerator it;
            for (it = this.quotes.GetEnumerator(); it.MoveNext();) 
            {
                temp_quotes.Add(it.Current.Key, it.Current.Value);
            }
            this.quotes.Clear();

            int prevBid = 0;

            for(int i = this.minPr; i <= this.maxPr; ++i)
            {
                if(temp_quotes.ContainsKey(i))
                {
                    this.quotes.Add(i, temp_quotes[i]);
                    if (temp_quotes[i].side == "bid")
                    {
                        prevBid = i;
                    }
                    else if(temp_quotes[i].side == "offer" && !best)
                    {
                        best = true;
                        this.bestbid = prevBid;
                        this.bestask = i;
                    }
                }
                else
                {
                    this.quotes.Add(i, new quote());
                }
            }

            this.quotesInitialized = true;
        }

        public void checkOrderQueue()
        {
            DateTime currentTime = DateTime.Now;
            order ord;
                
            if (this.orderQueue1sec.TryPeek(out ord))
            {
                while ((currentTime - ord.new_order_time).TotalSeconds > 1)
                {
                    this.orderQueue1min.Enqueue(this.orderQueue1sec.Dequeue());
                    if (!this.orderQueue1sec.TryPeek(out ord))
                    {
                        break;
                    }
                }
            }
            if (this.orderQueue1min.TryPeek(out ord))
            {
                while ((currentTime - ord.new_order_time).TotalSeconds > 60)
                {
                    this.orderQueue1min.Dequeue();
                    if (!this.orderQueue1min.TryPeek(out ord))
                    {
                        break;
                    }
                }
            }
        }

        //Market order/limit order taking the quotes
        public void executeOrder(order ord)
        {
            int i = 0;
            while(true)
            {
                if (Interlocked.Exchange(ref this.updating, 1) == 0)
                {
                    quote q;
                    if(ord.side == "SELL")
                    {
                        int idx = this.bestbid;
                        q = this.quotes[idx];
                        int iOrderPrice = (int)(ord.price / this.quote_increment);
                        while (q.price > iOrderPrice)
                        {
                            if(q.quantity < ord.open_size)
                            {
                                ord.open_size -= q.quantity;
                                ord.executed_size += q.quantity;
                                --idx;
                                if(idx < this.minPr)
                                {
                                    break;
                                }
                                q = this.quotes[idx];
                            }
                            else
                            {
                                ord.executed_size += ord.open_size;
                                ord.open_size = 0;
                                break;
                            }
                        }
                    }
                    else if(ord.side == "BUY")
                    {
                        int idx = this.bestask;
                        q = this.quotes[idx];
                        int iOrderPrice = (int)(ord.price / this.quote_increment);
                        if(iOrderPrice == 0)
                        {
                            iOrderPrice = this.maxPr;
                        }
                        while (q.price < iOrderPrice)
                        {
                            if (q.quantity < ord.open_size)
                            {
                                ord.open_size -= q.quantity;
                                ord.executed_size += q.quantity;
                                ++idx;
                                if (idx > this.maxPr)
                                {
                                    break;
                                }
                                q = this.quotes[idx];
                            }
                            else
                            {
                                ord.executed_size += ord.open_size;
                                ord.open_size = 0;
                                break;
                            }
                        }
                    }
                    this.updating = 0;
                    break;
                }
                else
                {
                    ++i;
                    if(i > 100000)
                    {
                        i = 0;
                        System.Threading.Thread.Sleep(0);
                    }
                }
            }
            
        }

        //Orders taken by other trades
        public void executeLimitOrder(cbMsg.trades trd)
        {
            if(trd.msg_type != "trades")
            {
                return;
            }
            int i = 0;
            int tradedPr = (int)(trd.price / this.quote_increment);
            double executedBaseAmount = 0;
            while (true)
            {
                if (Interlocked.Exchange(ref this.orderUpdating, 1) == 0)
                {
                    if(trd.side == "SELL")
                    {
                        int idx = this.bestbid;
                        quote qt = this.quotes[idx];
                        int currentPr = qt.price;
                        
                        while(currentPr >= tradedPr)
                        {
                            executedBaseAmount = qt.updateOrders(trd, this.quote_increment);
                            this.baseExecutionBuy += executedBaseAmount;
                            this.quoteExecutionBuy += executedBaseAmount * (double)qt.price * this.quote_increment;
                            --idx;
                            if (idx < this.minPr)
                            {
                                break;
                            }
                            qt = this.quotes[idx];
                            currentPr = qt.price;
                        }
                    }
                    else if(trd.side == "BUY")
                    {
                        int idx = this.bestask;
                        quote qt = this.quotes[idx];
                        int currentPr = qt.price;
                        while (currentPr <= tradedPr)
                        {
                            executedBaseAmount = qt.updateOrders(trd, this.quote_increment);
                            this.baseExecutionSell += executedBaseAmount;
                            this.quoteExecutionSell += executedBaseAmount * (double)qt.price * this.quote_increment;
                            ++idx;
                            if (idx > this.maxPr)
                            {
                                break;
                            }
                            qt = this.quotes[idx];
                            currentPr = qt.price;
                        }
                    }
                    this.orderUpdating = 0;
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

        public void updatePerpetual(cbMsg.perpetualPosition pp)
        {
            this.basePosition = pp.net_size;
            this.basePosPr = pp.vwap;  
        }
        public void updateFuture(cbMsg.futurePosition fp)
        {
            if(fp.side == "Short")
            {
                this.basePosition = - fp.number_of_contracts;
                this.basePosPr = fp.entry_price;
            }
            else
            {
                this.basePosition = fp.number_of_contracts;
                this.basePosPr = fp.entry_price;
            }
        }

        public void setStatus(cbMsg.product_status status)
        {
            this.product_type = status.product_type;
            this.id = status.id;
            this.base_currency = status.base_currency;
            this.quote_currency = status.quote_currency;
            this.base_increment = double.Parse(status.base_increment);
            this.quote_increment = double.Parse(status.quote_increment);
            this.display_name = status.display_name;
            this.status = status.status;
            this.status_message = status.status_message;
            this.min_market_funds = status.min_market_funds;
        }
        public string product_type { get; set; }
        public string id { get; set; }
        public string base_currency { get; set; }
        public string quote_currency { get; set; }
        public double base_increment { get; set; }
        public double quote_increment { get; set; }
        public string display_name { get; set; }
        public string status { get; set; }
        public string status_message { get; set; }
        public string min_market_funds { get; set; }

        public ConcurrentQueue<cbMsg.trades> qtQueue;
        public int updating;

        public bool quotesInitialized;
        public SortedDictionary<int, quote> quotes;
        public int bestbid;
        public int bestask;
        public int minPr;
        public int maxPr;

        public int orderUpdating;
        public Dictionary<string, order> liveOrders;
        public Dictionary<string, order> orders;

        public double baseExecutionSell;
        public double baseExecutionBuy;
        public double quoteExecutionSell;
        public double quoteExecutionBuy;

        public double basePosition;
        public double basePosPr;
        public double fee;

        public int minOrdPr;
        public int maxOrdPr;
        public double maxQuoteSize;
        public double maxBaseSize;
        public int maxNewOrderCount1sec;
        public int maxNewOrderAmount1min;
        public double maxLiveAmount;
        public Queue<order> orderQueue1sec;
        public Queue<order> orderQueue1min;

        public int last;
        public int open;
        public int high;
        public int low;

        public double baseTradedVolume;
        public double quoteTradedVolume;

        public crypto()
        {
            this.qtQueue = new ConcurrentQueue<cbMsg.trades>();
            this.updating = 0;
            this.quotesInitialized = false;
            this.quotes = new SortedDictionary<int, quote>();
            this.bestbid = -1;
            this.bestask = -1;
            this.minPr = -1;
            this.maxPr = -1;

            this.orderUpdating = 0;
            this.liveOrders = new Dictionary<string, order>();
            this.orders = new Dictionary<string, order>();

            this.baseExecutionSell = 0.0;
            this.baseExecutionBuy = 0.0;
            this.quoteExecutionSell = 0.0;
            this.quoteExecutionBuy = 0.0;

            this.minOrdPr = -1;
            this.maxOrdPr = -1;
            this.maxQuoteSize = -1;
            this.maxBaseSize = -1;
            this.maxNewOrderCount1sec = -1;
            this.maxNewOrderAmount1min = -1;
            this.maxLiveAmount = -1;
            this.orderQueue1sec = new Queue<order>();
            this.orderQueue1min = new Queue<order>();

            this.last = 0;
            this.open = 0;
            this.high = 0;
            this.low = 0;

            this.baseTradedVolume = 0;
            this.quoteTradedVolume = 0;
        }
    }
}
