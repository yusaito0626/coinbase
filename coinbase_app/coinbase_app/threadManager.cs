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

namespace coinbase_app
{
    public class threadManager
    {

    }
    public abstract class absThread
    {
        public int id;
        public string name;

        internal Mutex mutex;

        public int active;
        public int aborting;

        public Thread th;
        public delegate void func();

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
        private int STACK_SIZE = 1000000;

        public ConcurrentQueue<string> strQueue;
        public ConcurrentStack<cbMsg.trades> feedStack;
        public ConcurrentQueue<string> symbolQueue;

        public Dictionary<string, crypto> cryptos;

        public decodingThread()
        {
            this.aborting = 0;
            this.active = 0;
            this.mutex = new Mutex(true);
            this.feedStack = new ConcurrentStack<trades>();
            int i;
            for(i = 0;i<this.STACK_SIZE;i++)
            {
                this.feedStack.Push(new trades());
            }
        }
        public override void threadStart()
        {
            while(true)
            {
                this.mutex.WaitOne();
                if(this.active > 0)
                {
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
        public override void threadStart()
        {
            while (true)
            {
                this.mutex.WaitOne();
                if (this.active > 0)
                {
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
                            optimization = cp.updateQuote_Main();
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
        public ConcurrentQueue<string> symbolQueue;

        public override void threadStart()
        {
            base.threadStart();
        }
        public override void threadStop() { }

        public bool activate(ConcurrentQueue<string> receiver)
        {
            return true;
        }
    }
}
