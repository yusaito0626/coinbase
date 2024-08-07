using cbMsg;
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

        public ConcurrentStack<cbMsg.trades> feedStack;

        public Action<string> addLog = (str) => { Console.WriteLine(str); };

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
