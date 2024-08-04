using cbMsg;
using coinbase_connection;
using System.Collections.Concurrent;
using System.Collections.Specialized;
namespace coinbase_main
{
    public class quote
    {
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
        public string side;
        public int price;
        public double quantity;
        public string updated_time;
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
            this.executedBaseAmount += trade.size;
            this.executedQuoteAmount += trade.size * trade.price;

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

        public int last;
        public int open;
        public int high;
        public int low;

        public double executedBaseAmount;
        public double executedQuoteAmount;

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

            this.last = 0;
            this.open = 0;
            this.high = 0;
            this.low = 0;
        }
    }
}
