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
        public ConcurrentQueue<string> strQueue;
        public ConcurrentQueue<cbMsg.trades> feedQueue;

        public decodingThread()
        {
            this.aborting = 0;
            this.active = 0;
            this.mutex = new Mutex(true);
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
            cbMsg.trades tr = new trades();
            cbMsg.jsTrades jstr = new jsTrades();
            cbMsg.jsUpdate jsup = new jsUpdate();
            cbMsg.message msg = new message();
            int trycount = 0;
            while(true)
            {
                if(this.strQueue.TryDequeue(out str))
                {
                    coinbase_connection.parser.parseMsg(str,ref msg);
                    switch (msg.channel)
                    {
                        case "l2_data":
                            break;
                        case "market_trades":
                            break;
                        default:
                            break;
                            //Do nothing
                    }
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
        public override void threadStop() { }

        public bool activate(ConcurrentQueue<string> reciever, ConcurrentQueue<cbMsg.trades> sender)
        {
            if(Interlocked.Exchange(ref this.active, 1) == 1) 
            {
                this.strQueue = reciever;
                this.feedQueue = sender;
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
        public ConcurrentQueue<cbMsg.trades> feedQueue;
        public ConcurrentQueue<string> symbolQueue;

        public override void threadStart()
        {
            base.threadStart();
        }
        public override void threadStop() { }

        public bool activate(ConcurrentQueue<string> receiver)
        {

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

        }
    }
}
