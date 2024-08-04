using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using coinbase_connection;
using coinbase_main;

namespace coinbase_app
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.updating = 0;
            string configFile = "coinbase_app.ini";
            InitializeComponent();
            this.bidLabels = new List<KeyValuePair<Label, Label>>();
            this.bidLabels.Add(new KeyValuePair<Label, Label>(this.label_prBid1, this.label_qtBid1));
            this.bidLabels.Add(new KeyValuePair<Label, Label>(this.label_prBid2, this.label_qtBid2));
            this.bidLabels.Add(new KeyValuePair<Label, Label>(this.label_prBid3, this.label_qtBid3));
            this.bidLabels.Add(new KeyValuePair<Label, Label>(this.label_prBid4, this.label_qtBid4));
            this.bidLabels.Add(new KeyValuePair<Label, Label>(this.label_prBid5, this.label_qtBid5));
            this.bidLabels.Add(new KeyValuePair<Label, Label>(this.label_prBid6, this.label_qtBid6));
            this.askLabels = new List<KeyValuePair<Label, Label>>();
            this.askLabels.Add(new KeyValuePair<Label, Label>(this.label_prAsk1, this.label_qtAsk1));
            this.askLabels.Add(new KeyValuePair<Label, Label>(this.label_prAsk2, this.label_qtAsk2));
            this.askLabels.Add(new KeyValuePair<Label, Label>(this.label_prAsk3, this.label_qtAsk3));
            this.askLabels.Add(new KeyValuePair<Label, Label>(this.label_prAsk4, this.label_qtAsk4));
            this.askLabels.Add(new KeyValuePair<Label, Label>(this.label_prAsk5, this.label_qtAsk5));
            this.askLabels.Add(new KeyValuePair<Label, Label>(this.label_prAsk6, this.label_qtAsk6));
            this.logQueue = new ConcurrentQueue<string>();
            readConfig(configFile);
            Action<string> addLogFunc = this.addLog;
            this.connection = coinbase_connection.coinbase_connection.GetInstance();
            this.connection.addLog = addLogFunc;
            this.connection.readApiKey(this.apiFilename);
            this.connection.msgQueue = new System.Collections.Concurrent.ConcurrentQueue<string>();
            this.thManager = coinbase_app.threadManager.GetInstance();
            this.thManager.addLog = addLogFunc;
            this.thManager.initialzeThreadManager(this.decodingThCount, this.quotesThCount, this.optThCount);
            this.thManager.setQueues(this.connection.msgQueue);
            this.thManager.startThreads();
            this.cryptos = new Dictionary<string, crypto>();
            this.displayedCrypto = null;
            for (int i = 0; i < this.symbols.Length; ++i)
            {
                this.comboBox_symbols.Items.Add(this.symbols[i]);
            }
            this.getCryptoStatus();
        }

        async Task getCryptoStatus()
        {
            if (this.connection != null)
            {
                WebSocketState st;
                await this.connection.connect(this.url);
                st = this.connection.getState();
                if (st.ToString() == "Open")
                {
                    byte[] buffer = new byte[1073741824];
                    this.connection.startListen(cbChannels.heartbeats);
                    this.connection.startListen(cbChannels.status, this.symbols);
                    int heartbeatCount = 0;
                    cbMsg.message msg = new cbMsg.message();
                    cbMsg.product_status status = new cbMsg.product_status();
                    while (true)
                    {
                        var segment = new ArraySegment<byte>(buffer);
                        var result = this.connection.recv(ref segment);


                        if (result.IsFaulted == false)
                        {
                            if (result.Result.MessageType == WebSocketMessageType.Close)
                            {
                                return;
                            }
                            if (result.Result.MessageType == WebSocketMessageType.Binary)
                            {
                                return;
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
                                    this.addLog("[ERROR] The message is too large.");
                                    return;
                                }
                                segment = new ArraySegment<byte>(buffer, count, buffer.Length - count);
                                result = this.connection.recv(ref segment);

                                count += result.Result.Count;
                            }

                            var message = Encoding.UTF8.GetString(buffer, 0, count);
                            if (message.Contains("heartbeats"))
                            {
                                ++heartbeatCount;
                                if (heartbeatCount >= 5)
                                {
                                    this.connection.disconnect();
                                    break;
                                }
                            }
                            else
                            {
                                coinbase_connection.parser.parseMsg(message, ref msg);
                                if (msg.channel == "status")
                                {
                                    string targetStr = "\"products\":[";
                                    int start = msg.events.IndexOf(targetStr) + targetStr.Length;
                                    int end;
                                    while (start > 0)
                                    {
                                        end = msg.events.IndexOf("}", start) + 1;
                                        coinbase_connection.parser.parseStatus(msg.events.Substring(start, end - start), ref status);
                                        if (this.cryptos.ContainsKey(status.id))
                                        {
                                            this.cryptos[status.id].setStatus(status);
                                        }
                                        else
                                        {
                                            crypto cp = new crypto();
                                            cp.setStatus(status);
                                            this.cryptos[cp.id] = cp;
                                        }
                                        start = msg.events.IndexOf("{", end);
                                    }
                                    if (this.cryptos.Count == this.symbols.Length)
                                    {
                                        this.connection.disconnect();
                                        break;
                                    }
                                }
                            }
                        }

                    }
                }
                this.thManager.setCryptoList(this.cryptos);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.connection != null)
            {
                WebSocketState st;
                this.connection.connect(this.url);
                st = this.connection.getState();
                int i = 0;
                while(st.ToString() != "Open")
                {
                    System.Threading.Thread.Sleep(100);
                    st = this.connection.getState();
                    ++i;
                    if(i > 50)
                    {
                        this.addLog("[ERROR] Failed to connect.");
                        return;
                    }
                }
                if (st.ToString() == "Open")
                {
                    this.addLog("[INFO] Connected Successfully");
                    this.connection.startListen(cbChannels.heartbeats);
                    this.connection.startListen(cbChannels.level2, this.symbols);
                    this.connection.startListen(cbChannels.market_trades, this.symbols);
                    this.listeningThread = new Thread(new ThreadStart(this.connection.listen));
                    this.listeningThread.Start();
                    this.thManager.activateAllThreads();
                }
            }
        }

        private void readConfig(string filename)
        {
            using (StreamReader sr = new StreamReader(filename))
            {
                string line;
                string[] values;
                while ((line = sr.ReadLine()) != null)
                {
                    values = line.Split("=");
                    if (values[0] == "apiFile")
                    {
                        apiFilename = values[1];
                    }
                    else if (values[0] == "url")
                    {
                        url = values[1];
                    }
                    else if (values[0] == "symbolList")
                    {
                        symbols = values[1].Replace("[", "").Replace("]", "").Split(",");
                    }
                    else if (values[0] == "decodingThCount")
                    {
                        decodingThCount = int.Parse(values[1]);
                    }
                    else if (values[0] == "quotesThCount")
                    {
                        quotesThCount = int.Parse(values[1]);
                    }
                    else if (values[0] == "optThCount")
                    {
                        optThCount = int.Parse(values[1]);
                    }
                }
            }
        }

        //Config
        string apiFilename;
        string url;
        string[] symbols;
        int decodingThCount;
        int quotesThCount;
        int optThCount;

        Dictionary<string, crypto> cryptos;
        int updating;
        crypto displayedCrypto;
        const int DEPTH = 6;
        List<KeyValuePair<Label, Label>> bidLabels;
        List<KeyValuePair<Label, Label>> askLabels;

        coinbase_connection.coinbase_connection connection;
        threadManager thManager;
        System.Threading.Thread listeningThread;

        ConcurrentQueue<string> logQueue;

        private void display_update_Tick(object sender, EventArgs e)
        {
            switch (this.tabControl.SelectedTab.Name)
            {
                case "tabMain":
                    this.display_update_main();
                    break;
                case "tabProduct":
                    this.display_update_product();
                    break;
                case "tabConfig":
                    this.display_update_config();
                    break;
            }
        }
        private void display_update_main()
        {
            this.writeLog();
        }

        private void display_update_product()
        {
            if (this.displayedCrypto != null)
            {
                int count = 0;
                while (true)
                {
                    if (Interlocked.Exchange(ref this.updating, 1) == 0)
                    {
                        this.label_productType.Text = this.displayedCrypto.product_type;
                        this.label_id.Text = this.displayedCrypto.id;
                        this.label_baseCurr.Text = this.displayedCrypto.base_currency;
                        this.label_quoteCurr.Text = this.displayedCrypto.quote_currency;
                        this.label_baseInc.Text = this.displayedCrypto.base_increment.ToString();
                        this.label_quoteInc.Text = this.displayedCrypto.quote_increment.ToString();
                        this.label_displayName.Text = this.displayedCrypto.display_name;
                        this.label_status.Text = this.displayedCrypto.status;
                        this.label_statusMsg.Text = this.displayedCrypto.status_message;
                        this.label_minMktFund.Text = this.displayedCrypto.min_market_funds;

                        if(this.displayedCrypto.quotesInitialized)
                        {
                            int askidx = this.displayedCrypto.bestask;
                            int bididx = this.displayedCrypto.bestbid;
                            int aski = 0;
                            int bidi = 0;
                            quote askquote = this.displayedCrypto.quotes[askidx];
                            quote bidquote = this.displayedCrypto.quotes[bididx];

                            while (true)
                            {
                                if (askquote.side == "offer" && aski < DEPTH)
                                {
                                    this.askLabels[aski].Key.Text = (askquote.price * this.displayedCrypto.quote_increment).ToString("N2");
                                    this.askLabels[aski].Value.Text = askquote.quantity.ToString();
                                    ++aski;
                                }
                                if (bidquote.side == "bid" && bidi < DEPTH)
                                {
                                    this.bidLabels[bidi].Key.Text = (bidquote.price * this.displayedCrypto.quote_increment).ToString("N2");
                                    this.bidLabels[bidi].Value.Text = bidquote.quantity.ToString();
                                    ++bidi;
                                }
                                ++askidx;
                                --bididx;
                                if ((aski >= DEPTH && bidi >= DEPTH) || (askidx == this.displayedCrypto.quotes.Count() && bididx < 0))
                                {
                                    break;
                                }
                                if (askidx <= this.displayedCrypto.maxPr)
                                {
                                    askquote = this.displayedCrypto.quotes[askidx];
                                }
                                if (bididx >= this.displayedCrypto.minPr)
                                {
                                    bidquote = this.displayedCrypto.quotes[bididx];
                                }
                            }
                        }
                        this.updating = 0;
                        break;
                    }
                    else
                    {
                        ++count;
                        if (count > 100000)
                        {
                            count = 0;
                            System.Threading.Thread.Sleep(0);
                        }
                    }
                }

            }
        }
        private void display_update_config()
        {
            this.label_url.Text = this.url;
            this.label_apiInfo.Text = this.apiFilename;
        }

        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            switch (e.TabPage.Name)
            {
                case "tabMain":
                    this.display_update_main();
                    break;
                case "tabProduct":
                    this.display_update_product();
                    break;
                case "tabConfig":
                    this.display_update_config();
                    break;
            }
        }

        private void comboBox_symbols_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count = 0;
            while (true)
            {
                if (Interlocked.Exchange(ref this.updating, 1) == 0)
                {
                    string symbol = this.comboBox_symbols.SelectedItem.ToString();
                    if (this.cryptos.ContainsKey(symbol))
                    {
                        this.displayedCrypto = this.cryptos[symbol];
                    }
                    this.updating = 0;
                    break;  
                }
                else
                {
                    ++count;
                    if (count > 100000)
                    {
                        count = 0;
                        System.Threading.Thread.Sleep(0);
                    }
                }
            }
            this.display_update_product();
        }

        public void addLog(string str)
        {
            this.logQueue.Enqueue(str);
        }
        public void writeLog()
        {
            string line;
            while(this.logQueue.TryDequeue(out line))
            {
                this.mainLog.Text += line + "\n";
            }
        }
    }
}
