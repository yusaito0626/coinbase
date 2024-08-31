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
            this.connection = new coinbase_connection.coinbase_connection();
            this.connection.addLog = addLogFunc;
            this.connection.readApiJson(this.apiFilename);
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
            this.OMS = orderManager.GetInstance();
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
                while (st.ToString() != "Open")
                {
                    System.Threading.Thread.Sleep(100);
                    st = this.connection.getState();
                    ++i;
                    if (i > 50)
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
                    this.button_startListen.BackColor = System.Drawing.Color.LawnGreen;
                    this.button_startListen.FlatStyle = FlatStyle.Flat;
                    this.button_startListen.Enabled = false;
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
                    else if (values[0] == "orderLogPath")
                    {
                        orderLogPath = values[1];
                    }
                    else if (values[0] == "url")
                    {
                        url = values[1];
                    }
                    else if (values[0] == "live")
                    {
                        if (values[1] == "true")
                        {
                            live = true;
                        }
                        else
                        {
                            live = false;
                        }
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
        string orderLogPath;
        string url;
        string[] symbols;
        int decodingThCount;
        int quotesThCount;
        int optThCount;
        bool live = false;

        Dictionary<string, crypto> cryptos;
        int updating;
        crypto displayedCrypto;
        const int DEPTH = 6;
        List<KeyValuePair<Label, Label>> bidLabels;
        List<KeyValuePair<Label, Label>> askLabels;

        coinbase_connection.coinbase_connection connection;
        threadManager thManager;
        orderManager OMS;
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
            this.label_feedAll.Text = this.connection.msgCount.ToString("N0");
            this.label_feedInc.Text = this.connection.msgIncrement.ToString("N0");
            this.connection.msgIncrement = 0;
            this.label_feedQueue.Text = this.connection.msgQueue.Count().ToString("N0");
            this.writeLog();
        }
        private void display_update_product()
        {
            if (this.displayedCrypto != null)
            {
                crypto cp = this.displayedCrypto;
                int count = 0;
                while (true)
                {
                    if (Interlocked.Exchange(ref this.updating, 1) == 0)
                    {
                        this.label_symbol.Text = cp.id;
                        this.label_productType.Text = cp.product_type;
                        this.label_id.Text = cp.id;
                        this.label_baseCurr.Text = cp.base_currency;
                        this.label_quoteCurr.Text = cp.quote_currency;
                        this.label_baseInc.Text = cp.base_increment.ToString();
                        this.label_quoteInc.Text = cp.quote_increment.ToString();
                        this.label_displayName.Text = cp.display_name;
                        this.label_status.Text = cp.status;
                        this.label_statusMsg.Text = cp.status_message;
                        this.label_minMktFund.Text = cp.min_market_funds;

                        if (cp.quotesInitialized)
                        {
                            this.label_lastPr.Text = (cp.last * cp.quote_increment).ToString("N2");
                            this.label_open.Text = (cp.open * cp.quote_increment).ToString("N2");
                            this.label_high.Text = (cp.high * cp.quote_increment).ToString("N2");
                            this.label_low.Text = (cp.low * cp.quote_increment).ToString("N2");
                            this.label_volume.Text = (cp.baseTradedVolume).ToString("N2");
                            int askidx = cp.bestask;
                            int bididx = cp.bestbid;
                            int aski = 0;
                            int bidi = 0;
                            quote askquote = cp.quotes[askidx];
                            quote bidquote = cp.quotes[bididx];

                            while (true)
                            {
                                if (askquote.side == "offer" && aski < DEPTH)
                                {
                                    this.askLabels[aski].Key.Text = (askquote.price * cp.quote_increment).ToString("N2");
                                    this.askLabels[aski].Value.Text = askquote.quantity.ToString();
                                    ++aski;
                                }
                                if (bidquote.side == "bid" && bidi < DEPTH)
                                {
                                    this.bidLabels[bidi].Key.Text = (bidquote.price * cp.quote_increment).ToString("N2");
                                    this.bidLabels[bidi].Value.Text = bidquote.quantity.ToString();
                                    ++bidi;
                                }
                                if ((aski >= DEPTH && bidi >= DEPTH) || (askidx == cp.maxPr && bididx == cp.minPr))
                                {
                                    break;
                                }
                                ++askidx;
                                --bididx;
                                if (askidx <= cp.maxPr)
                                {
                                    askquote = cp.quotes[askidx];
                                }
                                else
                                {
                                    --askidx;
                                }
                                if (bididx >= cp.minPr)
                                {
                                    bidquote = cp.quotes[bididx];
                                }
                                else
                                {
                                    ++bididx;
                                }
                            }
                            while (true)
                            {
                                if (Interlocked.Exchange(ref cp.orderUpdating, 1) == 0)
                                {
                                    int i = 0;
                                    foreach (KeyValuePair<string, order> pair in cp.liveOrders)
                                    {
                                        if (this.dataGrid_orders.RowCount > i)
                                        {
                                            DataGridViewRow row = this.dataGrid_orders.Rows[i];
                                            row.Cells["status"].Value = pair.Value.status;
                                            row.Cells["side"].Value = pair.Value.side;
                                            row.Cells["price"].Value = pair.Value.price.ToString("N2");
                                            row.Cells["size"].Value = pair.Value.size.ToString();
                                            row.Cells["filled"].Value = pair.Value.executed_size.ToString();
                                            ++i;
                                        }
                                        else
                                        {
                                            this.dataGrid_orders.Rows.Add(pair.Value.status, pair.Value.side, pair.Value.price.ToString("N2"), pair.Value.size.ToString(), pair.Value.executed_size.ToString());
                                        }
                                    }
                                    for (int j = i; j < this.dataGrid_orders.RowCount; ++j)
                                    {
                                        DataGridViewRow row = this.dataGrid_orders.Rows[j];
                                        row.Cells["status"].Value = "";
                                        row.Cells["side"].Value = "";
                                        row.Cells["price"].Value = "";
                                        row.Cells["size"].Value = "";
                                        row.Cells["filled"].Value = "";
                                    }
                                    cp.orderUpdating = 0;
                                    break;
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
            while (this.logQueue.TryDequeue(out line))
            {
                this.mainLog.Text += DateTime.Now.ToString() + "   " + line + "\n";
            }
        }
        private void buttonOMS_Click(object sender, EventArgs e)
        {
            if (this.live)
            {
                string msg = "Are you willing to start live trading?";
                string caption = "Warning";
                MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
                DialogResult result = MessageBox.Show(msg, caption, buttons, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    this.addLog("Starting as a virtual mode");
                    this.live = false;
                }
                else if (result == DialogResult.Cancel)
                {
                    this.addLog("Process has been cancelled.");
                    return;
                }
                else if (result == DialogResult.Yes)
                {
                    this.addLog("Starting live trading.");
                }
                else
                {
                    this.addLog("ERROR!!!");
                    return;
                }
            }
            this.OMS.initialize(this.live, this.apiFilename, this.url, this.cryptos, this.orderLogPath, this.addLog);
            this.buttonOMS.BackColor = System.Drawing.Color.LawnGreen;
            this.buttonOMS.FlatStyle = FlatStyle.Flat;
            this.buttonOMS.Enabled = false;
        }

        private void comboBox_mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.comboBox_mode.SelectedItem == "Virtual")
            {
                this.live = false;
                this.addLog("Mode:Virtual");
            }
            else if (this.comboBox_mode.SelectedItem == "Live")
            {
                this.live = true;
                this.addLog("Mode:Live");
            }
            else
            {
                this.addLog("[ERROR] Only live/virtual mode is available for now");
            }
        }
    }
}
