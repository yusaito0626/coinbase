using coinbase_connection;
namespace coinbase_main
{
    public class quote
    {
        public void update(cbMsg.trades qt,double increment)
        {
            this.price = (int)(qt.price * increment);
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
        public void updateQuote(cbMsg.trades qt)
        {
            //Add how to find tob
            int pr = (int)(qt.price * this.quote_increment);
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

        public void findToB(int updated_pr)
        {
            quote current = this.quotes[updated_pr];
            if(updated_pr > this.bestbid && current.side == "bid")
            {
                this.bestbid = updated_pr;
            }
            else if (updated_pr < this.bestask && current.side == "ask")
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
                    if (qt.side == "ask")
                    {
                        this.bestask = pr;
                        break;
                    }
                    ++pr;
                }
                this.bestask = pr;
            }
        }
        public void initializeQuotes()
        {
            bool best = false;
            Dictionary<int, quote>.Enumerator minQuote = this.quotes.GetEnumerator();
            Dictionary<int, quote>.Enumerator maxQuote = minQuote;
            Dictionary<int, quote>.Enumerator prevBid = minQuote;
            Dictionary<int, quote>.Enumerator it;
            for (it = minQuote; it.MoveNext();)
            {
                if(it.Current.Value.side == "bid")
                {
                    prevBid = it;
                }
                else if(it.Current.Value.side == "ask" && !best)
                {
                    best = true;
                    this.bestbid = prevBid.Current.Key;
                    this.bestask = it.Current.Key;
                }
                maxQuote = it;
            }

            int pr = minQuote.Current.Key - (this.bestbid - minQuote.Current.Key);
            if(pr <= 0)
            {
                pr = 1;
            }
            this.minPr = pr;
            this.maxPr = maxQuote.Current.Key + (maxQuote.Current.Key - this.bestask);
            while (pr <= this.maxPr)
            {
                if (!this.quotes.ContainsKey(pr))
                {
                    this.quotes.Add(pr, new quote());
                }
                ++pr;
            }
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

        public bool quotesInitialized;
        public Dictionary<int, quote> quotes;
        public int bestbid;
        public int bestask;
        public int minPr;
        public int maxPr;

        public crypto()
        {
            
        }
    }
}
