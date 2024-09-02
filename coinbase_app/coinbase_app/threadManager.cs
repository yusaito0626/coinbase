using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Collections.Concurrent;

using cbMsg;
using System.Runtime.Remoting;
using System.Security.AccessControl;
using coinbase_connection;
using coinbase_main;
using coinbase_enum;

namespace coinbase_app
{
    public class threadManager
    {
        private threadManager()
        {

        }
        public void initialzeThreadManager(int decoding,int quotes,int opt)
        {
            this.NofDecodingTh = decoding;
            if(this.NofDecodingTh > 1)
            {
                this.NofDecodingTh = 1;
            }
            this.NofQuotesTh = quotes;
            this.NofOptTh = opt;

            this.feedStack = new ConcurrentStack<trades>();
            int i;
            for (i = 0; i < this.STACK_SIZE; i++)
            {
                this.feedStack.Push(new trades());
            }

            this.qtManager.feedStack = this.feedStack;
            if(!this.ordManager.live)
            {
                this.qtManager.msgQueue = this.ordManager.msgQueue;
            }

            this.decodingThreads = new List<decodingThread>();
            this.updateQuotesThreads = new List<updateQuotesThread>();
            this.optimizingThreads = new List<optimizingThread>();

            this.quotesQueue = new ConcurrentQueue<string>();
            this.optimizingQueues = new List<ConcurrentQueue<string>>();

            for(i = 0;i < this.NofDecodingTh;++i)
            {
                this.decodingThreads.Add(new decodingThread(this.feedStack));
            }
            for (i = 0; i < this.NofQuotesTh; ++i)
            {
                this.updateQuotesThreads.Add(new updateQuotesThread());
                
            }
            for (i = 0; i < this.NofOptTh; ++i)
            {
                this.optimizingThreads.Add(new optimizingThread());
                this.optimizingQueues.Add(new ConcurrentQueue<string>());
            }
        }
        public void setCryptoList(Dictionary<string, crypto> dic)
        {
            this.cryptos = dic;
            foreach(var th in this.decodingThreads)
            {
                th.cryptos = this.cryptos;
            }
            foreach (var th in this.updateQuotesThreads)
            {
                th.cryptos = this.cryptos;
            }
            foreach (var th in this.optimizingThreads)
            {
                th.cryptos = this.cryptos;
            }
        }
        public void startThreads()
        {
            foreach (var th in this.decodingThreads)
            {
                th._addLog = this._addLog;
                this.addLog("Starting decoding thread",logType.INFO);
                th.th = new Thread(new ThreadStart(th.threadStart));
                th.th.Start();
            }
            foreach (var th in this.updateQuotesThreads)
            {
                th._addLog = this._addLog;
                this.addLog("Starting updating thread", logType.INFO);
                th.th = new Thread(new ThreadStart(th.threadStart));
                th.th.Start();
            }
            foreach (var th in this.optimizingThreads)
            {
                th._addLog = this._addLog;
                this.addLog("Starting optimizing thread", logType.INFO);
                th.th = new Thread(new ThreadStart(th.threadStart));
                th.th.Start();
            }
        }

        public void activateAllThreads()
        {
            foreach (var th in this.decodingThreads)
            {
                if(th.started)
                {
                    th.activate(this.feedQueue, this.quotesQueue);
                }
            }
            int i = 0;
            foreach (var th in this.updateQuotesThreads)
            {
                if(th.started)
                {
                    th.activate(quotesQueue, this.optimizingQueues[i]);
                    ++i;
                    if(i >= this.optimizingQueues.Count)
                    {
                        i = 0;
                    }
                }
            }
            i = 0;
            foreach (var th in this.optimizingThreads)
            {
                if(th.started)
                {
                    th.activate(this.optimizingQueues[i]);
                    ++i;
                    if (i >= this.optimizingQueues.Count)
                    {
                        i = 0;
                    }
                }
            }
        }

        public void setQueues(ConcurrentQueue<string> fqueue)
        {
            this.feedQueue = fqueue;
        }

        int NofDecodingTh;
        int NofQuotesTh;
        int NofOptTh;

        List<decodingThread> decodingThreads;
        List<updateQuotesThread> updateQuotesThreads;
        List<optimizingThread> optimizingThreads;

        private int STACK_SIZE = 1000000;
        ConcurrentQueue<string> feedQueue;
        ConcurrentQueue<string> quotesQueue;
        ConcurrentStack<cbMsg.trades> feedStack;

        List<ConcurrentQueue<string>> optimizingQueues;

        public Dictionary<string, crypto> cryptos;

        public quoteManager qtManager = coinbase_main.quoteManager.GetInstance();
        public orderManager ordManager = coinbase_main.orderManager.GetInstance();

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

        private static threadManager _instance;
        private static readonly object _lockObject = new object();

        public static threadManager GetInstance()
        {
            lock (_lockObject)
            {
                if (_instance == null)
                {
                    //インスタンス生成
                    _instance = new threadManager();
                }
                return _instance;
            }
        }
    }
    public abstract class absThread
    {
        public int id;
        public string name;

        internal Mutex mutex;

        public int active;
        public int aborting;
        public bool started;

        public Thread th;
        public delegate void func();
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

        public virtual void threadStart() { }
        public virtual void threadStop() 
        {
            if(active > 0)
            {
                aborting = 1;
                active = 0;
            }
            else
            {
                aborting = 1;
                active = 0;
                this.mutex.ReleaseMutex();
                active = 0;
            }
        }

    }

    public class decodingThread : absThread
    {
        //Decoding thread should be single thread

        public ConcurrentQueue<string> strQueue;
        public ConcurrentStack<cbMsg.trades> feedStack;
        public ConcurrentQueue<string> symbolQueue;

        public Dictionary<string, crypto> cryptos;

        public decodingThread(ConcurrentStack<cbMsg.trades> _feedstack)
        {
            this.aborting = 0;
            this.active = 0;
            this.mutex = new Mutex(true);
            this.feedStack = _feedstack;
        }
        public override void threadStart()
        {
            this.started = true;
            this.addLog("Decoding thread started", logType.INFO);
            while (true)
            {
                this.mutex.WaitOne();
                if(this.active > 0)
                {
                    this.addLog("Decoding thread activated", logType.INFO);
                    this.decoding();
                    this.mutex.ReleaseMutex();
                }
                if(this.aborting > 0)
                {
                    break;
                }
                this.mutex.WaitOne(0);
            }
        }

        public void decoding()
        {
            string str;
            int trycount = 0;
            
            while(true)
            {
                if(this.strQueue.TryDequeue(out str))
                {
                    this.decodeMain(str);
                }
                else
                {
                    ++trycount;
                    if(trycount > 1000000)
                    {
                        trycount = 0;
                        System.Threading.Thread.Sleep(0);
                    }
                }
            }
        }

        public void decodeMain(string str)
        {
            string symbol;
            string event_type;
            string msg_Type;
            cbMsg.trades tr;
            cbMsg.jsTrades jstr = new jsTrades();
            cbMsg.jsUpdate jsup = new jsUpdate();
            cbMsg.message msg = new message();
            int start = 0;
            int end = 0;
            string targetStr = "";
            string temp;

            crypto cp;

            coinbase_connection.parser.parseMsg(str, ref msg);
            msg_Type = msg.channel;
            symbol = coinbase_connection.parser.findSymbol(msg.events);
            event_type = coinbase_connection.parser.findEventType(msg.events);
            if(this.cryptos.ContainsKey(symbol))
            {
                cp = this.cryptos[symbol];
                switch (msg.channel)
                {
                    case "l2_data":
                        targetStr = "\"updates\":[";
                        start = msg.events.IndexOf(targetStr) + targetStr.Length;
                        while (start > 0)
                        {
                            end = msg.events.IndexOf("}", start) + 1;
                            temp = msg.events.Substring(start, end - start);
                            coinbase_connection.parser.parseUpdate(temp, ref jsup);
                            if (this.feedStack.TryPop(out tr))
                            {
                                coinbase_connection.parser.jsUpdateToTrades(symbol, jsup, ref tr);
                                tr.event_type = event_type;
                                cp.qtQueue.Enqueue(tr);
                                this.symbolQueue.Enqueue(symbol);
                            }
                            else
                            {
                                //Add more objects
                            }
                            start = msg.events.IndexOf("{", end);
                        }
                        break;
                    case "market_trades":
                        targetStr = "\"trades\":[";
                        start = msg.events.IndexOf(targetStr) + targetStr.Length;
                        while (start > 0)
                        {
                            end = msg.events.IndexOf("}", start) + 1;
                            coinbase_connection.parser.parseTrades(msg.events.Substring(start, end - start), ref jstr);
                            if (this.feedStack.TryPop(out tr))
                            {
                                coinbase_connection.parser.jsTradesToTrades(jstr, ref tr);
                                tr.event_type = event_type;
                                cp.qtQueue.Enqueue(tr);
                                this.symbolQueue.Enqueue(symbol);
                            }
                            else
                            {
                                //Add more objects
                            }
                            start = msg.events.IndexOf("{", end);
                        }
                        break;
                    default:
                        break;
                        //Do nothing
                }
            }
        }
        public override void threadStop() { }

        public bool activate(ConcurrentQueue<string> receiver, ConcurrentQueue<string> sender)
        {
            if(Interlocked.Exchange(ref this.active, 1) == 0) 
            {
                this.strQueue = receiver;
                this.symbolQueue = sender;
                this.mutex.ReleaseMutex();
                return true;
            }
            else
            {
                return false;
            }
        }
        public void deactivate()
        {
            this.active = 0;
        }
    }

    public class updateQuotesThread : absThread
    {
        public ConcurrentQueue<string> feedQueue;
        public ConcurrentQueue<string> optimizingQueue;

        public Dictionary<string, crypto> cryptos;

        public quoteManager qtManager = coinbase_main.quoteManager.GetInstance();

        public updateQuotesThread()
        {
            this.aborting = 0;
            this.active = 0;
            this.mutex = new Mutex(true);
        }
        public override void threadStart()
        {
            this.addLog("Updating thread started", logType.INFO);
            this.started = true;
            while (true)
            {
                this.mutex.WaitOne();
                if (this.active > 0)
                {
                    this.addLog("Updating thread activated", logType.INFO);
                    this.updatingQuotes();
                    this.mutex.ReleaseMutex();
                }
                if (this.aborting > 0)
                {
                    break;
                }
                this.mutex.WaitOne(0);
            }
        }
        public void updatingQuotes()
        {
            string symbol;
            int trycount = 0;
            crypto cp;
            bool optimization;

            while (true)
            {
                if (this.feedQueue.TryDequeue(out symbol))
                {
                    if(this.cryptos.ContainsKey(symbol))
                    {
                        cp = this.cryptos[symbol];
                        if(Interlocked.Exchange(ref cp.updating,1) == 0)
                        {
                            optimization = this.qtManager.update_quotes(ref cp);
                            if(cp.qtQueue.Count > 0)
                            {
                                this.feedQueue.Enqueue(symbol);
                            }
                            if(optimization)
                            {
                                this.optimizingQueue.Enqueue(symbol);
                            }
                        }
                    }
                }
                else
                {
                    ++trycount;
                    if (trycount > 1000000)
                    {
                        trycount = 0;
                        System.Threading.Thread.Sleep(0);
                    }
                }
            }
        }
        public override void threadStop() { }

        public bool activate(ConcurrentQueue<string> receiver, ConcurrentQueue<string> sender)
        {
            if (Interlocked.Exchange(ref this.active, 1) == 0)
            {
                this.feedQueue = receiver;
                this.optimizingQueue = sender;
                this.mutex.ReleaseMutex();
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class optimizingThread : absThread
    {
        public List<ConcurrentQueue<string>> symbolQueues;
        public Dictionary<string, crypto> cryptos;

        public optimizingThread()
        {
            this.aborting = 0;
            this.active = 0;
            this.mutex = new Mutex(true);
        }
        public override void threadStart()
        {
            this.addLog("Optimizing thread started", logType.INFO);
            this.started = true;
            while (true)
            {
                this.mutex.WaitOne();
                if (this.active > 0)
                {
                    this.addLog("Optimizing thread activated", logType.INFO);
                    this.optimizing();
                    this.mutex.ReleaseMutex();
                }
                if (this.aborting > 0)
                {
                    break;
                }
                this.mutex.WaitOne(0);
            }
        }

        public void optimizing()
        {
            while(true)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }
        public override void threadStop() { }

        public bool activate(ConcurrentQueue<string> receiver)
        {
            return true;
        }
    }
}
