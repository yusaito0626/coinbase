namespace coinbase_app
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tabControl = new TabControl();
            tabMain = new TabPage();
            button_startListen = new Button();
            mainLog = new Label();
            groupBox3 = new GroupBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            label_feedAll = new Label();
            label_feedInc = new Label();
            label_feedQueue = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            label_liveBuyIns = new Label();
            label8 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label_liveBuyOrd = new Label();
            label_liveBuyAmt = new Label();
            label_liveSellIns = new Label();
            label_liveSellOrd = new Label();
            label_liveSellAmt = new Label();
            comboBox_mode = new ComboBox();
            groupBox1 = new GroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            label_sumBuyIns = new Label();
            label10 = new Label();
            label9 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            label_sumSellIns = new Label();
            label_sumSellOrd = new Label();
            label_sumSellExeOrd = new Label();
            label_sumSellExeAmt = new Label();
            label_sumBuyExeOrd = new Label();
            label_sumBuyOrd = new Label();
            label_sumBuyExeAmt = new Label();
            tabProduct = new TabPage();
            groupBox9 = new GroupBox();
            tableLayoutPanel8 = new TableLayoutPanel();
            label_posPnl = new Label();
            label40 = new Label();
            label_totalPnl = new Label();
            label36 = new Label();
            label_tradePnl = new Label();
            label38 = new Label();
            groupBox8 = new GroupBox();
            tableLayoutPanel6 = new TableLayoutPanel();
            label_CurrPos = new Label();
            label35 = new Label();
            tableLayoutPanel5 = new TableLayoutPanel();
            label_P_buyAvgPr = new Label();
            label32 = new Label();
            label31 = new Label();
            label33 = new Label();
            label34 = new Label();
            label_P_sellAvgPr = new Label();
            label_P_sellVol = new Label();
            label_P_buyVol = new Label();
            label_symbol = new Label();
            groupBox7 = new GroupBox();
            tableLayoutPanel7 = new TableLayoutPanel();
            label_open = new Label();
            label_high = new Label();
            label_low = new Label();
            label29 = new Label();
            label30 = new Label();
            label28 = new Label();
            label_volume = new Label();
            label27 = new Label();
            label_lastPr = new Label();
            groupBox6 = new GroupBox();
            tableLayoutPanel4 = new TableLayoutPanel();
            label_qtAsk6 = new Label();
            label_prAsk1 = new Label();
            label_prAsk2 = new Label();
            label_prAsk3 = new Label();
            label_prAsk4 = new Label();
            label_prAsk5 = new Label();
            label_prAsk6 = new Label();
            label_prBid1 = new Label();
            label_prBid2 = new Label();
            label_prBid3 = new Label();
            label_prBid4 = new Label();
            label_prBid5 = new Label();
            label_prBid6 = new Label();
            label_qtBid1 = new Label();
            label_qtBid2 = new Label();
            label_qtBid3 = new Label();
            label_qtBid4 = new Label();
            label_qtBid5 = new Label();
            label_qtBid6 = new Label();
            label_qtAsk5 = new Label();
            label_qtAsk4 = new Label();
            label_qtAsk3 = new Label();
            label_qtAsk2 = new Label();
            label_qtAsk1 = new Label();
            groupBox5 = new GroupBox();
            label_minMktFund = new Label();
            label_statusMsg = new Label();
            label_status = new Label();
            label_displayName = new Label();
            label_quoteInc = new Label();
            label_baseInc = new Label();
            label_quoteCurr = new Label();
            label_baseCurr = new Label();
            label_id = new Label();
            label_productType = new Label();
            label26 = new Label();
            label25 = new Label();
            label24 = new Label();
            label23 = new Label();
            label22 = new Label();
            label21 = new Label();
            label20 = new Label();
            label19 = new Label();
            label18 = new Label();
            label17 = new Label();
            comboBox_symbols = new ComboBox();
            tabConfig = new TabPage();
            groupBox4 = new GroupBox();
            label_apiInfo = new Label();
            label_url = new Label();
            label16 = new Label();
            label15 = new Label();
            display_update = new System.Windows.Forms.Timer(components);
            buttonOMS = new Button();
            tabControl.SuspendLayout();
            tabMain.SuspendLayout();
            groupBox3.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            groupBox2.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            groupBox1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tabProduct.SuspendLayout();
            groupBox9.SuspendLayout();
            tableLayoutPanel8.SuspendLayout();
            groupBox8.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            groupBox7.SuspendLayout();
            tableLayoutPanel7.SuspendLayout();
            groupBox6.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            groupBox5.SuspendLayout();
            tabConfig.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabMain);
            tabControl.Controls.Add(tabProduct);
            tabControl.Controls.Add(tabConfig);
            tabControl.Location = new Point(2, 1);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(796, 746);
            tabControl.TabIndex = 0;
            tabControl.Selected += tabControl_Selected;
            // 
            // tabMain
            // 
            tabMain.Controls.Add(buttonOMS);
            tabMain.Controls.Add(button_startListen);
            tabMain.Controls.Add(mainLog);
            tabMain.Controls.Add(groupBox3);
            tabMain.Controls.Add(groupBox2);
            tabMain.Controls.Add(comboBox_mode);
            tabMain.Controls.Add(groupBox1);
            tabMain.Location = new Point(4, 24);
            tabMain.Name = "tabMain";
            tabMain.Padding = new Padding(3);
            tabMain.Size = new Size(788, 718);
            tabMain.TabIndex = 1;
            tabMain.Text = "Main";
            tabMain.UseVisualStyleBackColor = true;
            // 
            // button_startListen
            // 
            button_startListen.FlatStyle = FlatStyle.Popup;
            button_startListen.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_startListen.Location = new Point(604, 349);
            button_startListen.Name = "button_startListen";
            button_startListen.Size = new Size(181, 40);
            button_startListen.TabIndex = 4;
            button_startListen.Text = "Start Listening";
            button_startListen.UseVisualStyleBackColor = true;
            button_startListen.Click += button1_Click;
            // 
            // mainLog
            // 
            mainLog.BackColor = Color.White;
            mainLog.BorderStyle = BorderStyle.Fixed3D;
            mainLog.Location = new Point(7, 398);
            mainLog.Name = "mainLog";
            mainLog.Size = new Size(777, 317);
            mainLog.TabIndex = 3;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(tableLayoutPanel3);
            groupBox3.Location = new Point(6, 296);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(591, 99);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Feeds";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetDouble;
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.Controls.Add(label_feedAll, 0, 1);
            tableLayoutPanel3.Controls.Add(label_feedInc, 0, 1);
            tableLayoutPanel3.Controls.Add(label_feedQueue, 0, 1);
            tableLayoutPanel3.Controls.Add(label3, 2, 0);
            tableLayoutPanel3.Controls.Add(label2, 1, 0);
            tableLayoutPanel3.Controls.Add(label1, 0, 0);
            tableLayoutPanel3.Location = new Point(7, 22);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.Size = new Size(577, 71);
            tableLayoutPanel3.TabIndex = 2;
            // 
            // label_feedAll
            // 
            label_feedAll.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_feedAll.AutoSize = true;
            label_feedAll.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_feedAll.Location = new Point(6, 37);
            label_feedAll.Name = "label_feedAll";
            label_feedAll.Size = new Size(182, 31);
            label_feedAll.TabIndex = 5;
            label_feedAll.Text = "0";
            label_feedAll.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_feedInc
            // 
            label_feedInc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_feedInc.AutoSize = true;
            label_feedInc.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_feedInc.Location = new Point(197, 37);
            label_feedInc.Name = "label_feedInc";
            label_feedInc.Size = new Size(182, 31);
            label_feedInc.TabIndex = 4;
            label_feedInc.Text = "0";
            label_feedInc.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_feedQueue
            // 
            label_feedQueue.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_feedQueue.AutoSize = true;
            label_feedQueue.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_feedQueue.Location = new Point(388, 37);
            label_feedQueue.Name = "label_feedQueue";
            label_feedQueue.Size = new Size(183, 31);
            label_feedQueue.TabIndex = 3;
            label_feedQueue.Text = "0";
            label_feedQueue.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(388, 3);
            label3.Name = "label3";
            label3.Size = new Size(183, 31);
            label3.TabIndex = 2;
            label3.Text = "Queue";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(197, 3);
            label2.Name = "label2";
            label2.Size = new Size(182, 31);
            label2.TabIndex = 1;
            label2.Text = "Increment";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(6, 3);
            label1.Name = "label1";
            label1.Size = new Size(182, 31);
            label1.TabIndex = 0;
            label1.Text = "All";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(tableLayoutPanel2);
            groupBox2.Location = new Point(5, 166);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(591, 124);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Live Orders";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetDouble;
            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.Controls.Add(label_liveBuyIns, 0, 2);
            tableLayoutPanel2.Controls.Add(label8, 0, 2);
            tableLayoutPanel2.Controls.Add(label4, 0, 1);
            tableLayoutPanel2.Controls.Add(label5, 1, 0);
            tableLayoutPanel2.Controls.Add(label6, 2, 0);
            tableLayoutPanel2.Controls.Add(label7, 3, 0);
            tableLayoutPanel2.Controls.Add(label_liveBuyOrd, 1, 2);
            tableLayoutPanel2.Controls.Add(label_liveBuyAmt, 1, 2);
            tableLayoutPanel2.Controls.Add(label_liveSellIns, 1, 1);
            tableLayoutPanel2.Controls.Add(label_liveSellOrd, 2, 1);
            tableLayoutPanel2.Controls.Add(label_liveSellAmt, 3, 1);
            tableLayoutPanel2.Location = new Point(8, 22);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 34F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(577, 96);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // label_liveBuyIns
            // 
            label_liveBuyIns.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_liveBuyIns.AutoSize = true;
            label_liveBuyIns.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_liveBuyIns.Location = new Point(149, 64);
            label_liveBuyIns.Name = "label_liveBuyIns";
            label_liveBuyIns.Size = new Size(134, 29);
            label_liveBuyIns.TabIndex = 11;
            label_liveBuyIns.Text = "0";
            label_liveBuyIns.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(6, 64);
            label8.Name = "label8";
            label8.Size = new Size(134, 29);
            label8.TabIndex = 5;
            label8.Text = "BUY";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(6, 34);
            label4.Name = "label4";
            label4.Size = new Size(134, 27);
            label4.TabIndex = 1;
            label4.Text = "SELL";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(149, 3);
            label5.Name = "label5";
            label5.Size = new Size(134, 28);
            label5.TabIndex = 2;
            label5.Text = "Instruments";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(292, 3);
            label6.Name = "label6";
            label6.Size = new Size(134, 28);
            label6.TabIndex = 3;
            label6.Text = "Orders";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(435, 3);
            label7.Name = "label7";
            label7.Size = new Size(136, 28);
            label7.TabIndex = 4;
            label7.Text = "Amount";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_liveBuyOrd
            // 
            label_liveBuyOrd.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_liveBuyOrd.AutoSize = true;
            label_liveBuyOrd.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_liveBuyOrd.Location = new Point(292, 64);
            label_liveBuyOrd.Name = "label_liveBuyOrd";
            label_liveBuyOrd.Size = new Size(134, 29);
            label_liveBuyOrd.TabIndex = 6;
            label_liveBuyOrd.Text = "0";
            label_liveBuyOrd.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_liveBuyAmt
            // 
            label_liveBuyAmt.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_liveBuyAmt.AutoSize = true;
            label_liveBuyAmt.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_liveBuyAmt.Location = new Point(435, 64);
            label_liveBuyAmt.Name = "label_liveBuyAmt";
            label_liveBuyAmt.Size = new Size(136, 29);
            label_liveBuyAmt.TabIndex = 7;
            label_liveBuyAmt.Text = "0";
            label_liveBuyAmt.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_liveSellIns
            // 
            label_liveSellIns.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_liveSellIns.AutoSize = true;
            label_liveSellIns.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_liveSellIns.Location = new Point(149, 34);
            label_liveSellIns.Name = "label_liveSellIns";
            label_liveSellIns.Size = new Size(134, 27);
            label_liveSellIns.TabIndex = 8;
            label_liveSellIns.Text = "0";
            label_liveSellIns.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_liveSellOrd
            // 
            label_liveSellOrd.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_liveSellOrd.AutoSize = true;
            label_liveSellOrd.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_liveSellOrd.Location = new Point(292, 34);
            label_liveSellOrd.Name = "label_liveSellOrd";
            label_liveSellOrd.Size = new Size(134, 27);
            label_liveSellOrd.TabIndex = 9;
            label_liveSellOrd.Text = "0";
            label_liveSellOrd.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_liveSellAmt
            // 
            label_liveSellAmt.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_liveSellAmt.AutoSize = true;
            label_liveSellAmt.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_liveSellAmt.Location = new Point(435, 34);
            label_liveSellAmt.Name = "label_liveSellAmt";
            label_liveSellAmt.Size = new Size(136, 27);
            label_liveSellAmt.TabIndex = 10;
            label_liveSellAmt.Text = "0";
            label_liveSellAmt.TextAlign = ContentAlignment.MiddleRight;
            // 
            // comboBox_mode
            // 
            comboBox_mode.FormattingEnabled = true;
            comboBox_mode.Items.AddRange(new object[] { "Read Tick Files", "Market Feeds", "Live" });
            comboBox_mode.Location = new Point(3, 5);
            comboBox_mode.Name = "comboBox_mode";
            comboBox_mode.Size = new Size(252, 23);
            comboBox_mode.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tableLayoutPanel1);
            groupBox1.Location = new Point(3, 36);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(780, 124);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Summary";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetDouble;
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21F));
            tableLayoutPanel1.Controls.Add(label_sumBuyIns, 0, 2);
            tableLayoutPanel1.Controls.Add(label10, 0, 2);
            tableLayoutPanel1.Controls.Add(label9, 0, 1);
            tableLayoutPanel1.Controls.Add(label11, 1, 0);
            tableLayoutPanel1.Controls.Add(label12, 2, 0);
            tableLayoutPanel1.Controls.Add(label13, 3, 0);
            tableLayoutPanel1.Controls.Add(label14, 4, 0);
            tableLayoutPanel1.Controls.Add(label_sumSellIns, 1, 1);
            tableLayoutPanel1.Controls.Add(label_sumSellOrd, 2, 1);
            tableLayoutPanel1.Controls.Add(label_sumSellExeOrd, 3, 1);
            tableLayoutPanel1.Controls.Add(label_sumSellExeAmt, 4, 1);
            tableLayoutPanel1.Controls.Add(label_sumBuyExeOrd, 1, 2);
            tableLayoutPanel1.Controls.Add(label_sumBuyOrd, 1, 2);
            tableLayoutPanel1.Controls.Add(label_sumBuyExeAmt, 4, 2);
            tableLayoutPanel1.Location = new Point(3, 19);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 34F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            tableLayoutPanel1.Size = new Size(776, 108);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label_sumBuyIns
            // 
            label_sumBuyIns.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_sumBuyIns.AutoSize = true;
            label_sumBuyIns.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_sumBuyIns.Location = new Point(130, 72);
            label_sumBuyIns.Name = "label_sumBuyIns";
            label_sumBuyIns.Size = new Size(153, 33);
            label_sumBuyIns.TabIndex = 18;
            label_sumBuyIns.Text = "0";
            label_sumBuyIns.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.Location = new Point(6, 72);
            label10.Name = "label10";
            label10.Size = new Size(115, 33);
            label10.TabIndex = 6;
            label10.Text = "BUY";
            label10.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(6, 38);
            label9.Name = "label9";
            label9.Size = new Size(115, 31);
            label9.TabIndex = 2;
            label9.Text = "SELL";
            label9.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.Location = new Point(130, 3);
            label11.Name = "label11";
            label11.Size = new Size(153, 32);
            label11.TabIndex = 7;
            label11.Text = "Instruments";
            label11.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            label12.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.Location = new Point(292, 3);
            label12.Name = "label12";
            label12.Size = new Size(153, 32);
            label12.TabIndex = 8;
            label12.Text = "Orders";
            label12.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            label13.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label13.Location = new Point(454, 3);
            label13.Name = "label13";
            label13.Size = new Size(153, 32);
            label13.TabIndex = 9;
            label13.Text = "Executed Orders";
            label13.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            label14.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.Location = new Point(616, 3);
            label14.Name = "label14";
            label14.Size = new Size(154, 32);
            label14.TabIndex = 10;
            label14.Text = "Executed Amount";
            label14.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_sumSellIns
            // 
            label_sumSellIns.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_sumSellIns.AutoSize = true;
            label_sumSellIns.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_sumSellIns.Location = new Point(130, 38);
            label_sumSellIns.Name = "label_sumSellIns";
            label_sumSellIns.Size = new Size(153, 31);
            label_sumSellIns.TabIndex = 11;
            label_sumSellIns.Text = "0";
            label_sumSellIns.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_sumSellOrd
            // 
            label_sumSellOrd.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_sumSellOrd.AutoSize = true;
            label_sumSellOrd.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_sumSellOrd.Location = new Point(292, 38);
            label_sumSellOrd.Name = "label_sumSellOrd";
            label_sumSellOrd.Size = new Size(153, 31);
            label_sumSellOrd.TabIndex = 12;
            label_sumSellOrd.Text = "0";
            label_sumSellOrd.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_sumSellExeOrd
            // 
            label_sumSellExeOrd.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_sumSellExeOrd.AutoSize = true;
            label_sumSellExeOrd.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_sumSellExeOrd.Location = new Point(454, 38);
            label_sumSellExeOrd.Name = "label_sumSellExeOrd";
            label_sumSellExeOrd.Size = new Size(153, 31);
            label_sumSellExeOrd.TabIndex = 13;
            label_sumSellExeOrd.Text = "0";
            label_sumSellExeOrd.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_sumSellExeAmt
            // 
            label_sumSellExeAmt.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_sumSellExeAmt.AutoSize = true;
            label_sumSellExeAmt.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_sumSellExeAmt.Location = new Point(616, 38);
            label_sumSellExeAmt.Name = "label_sumSellExeAmt";
            label_sumSellExeAmt.Size = new Size(154, 31);
            label_sumSellExeAmt.TabIndex = 14;
            label_sumSellExeAmt.Text = "0";
            label_sumSellExeAmt.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_sumBuyExeOrd
            // 
            label_sumBuyExeOrd.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_sumBuyExeOrd.AutoSize = true;
            label_sumBuyExeOrd.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_sumBuyExeOrd.Location = new Point(454, 72);
            label_sumBuyExeOrd.Name = "label_sumBuyExeOrd";
            label_sumBuyExeOrd.Size = new Size(153, 33);
            label_sumBuyExeOrd.TabIndex = 15;
            label_sumBuyExeOrd.Text = "0";
            label_sumBuyExeOrd.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_sumBuyOrd
            // 
            label_sumBuyOrd.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_sumBuyOrd.AutoSize = true;
            label_sumBuyOrd.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_sumBuyOrd.Location = new Point(292, 72);
            label_sumBuyOrd.Name = "label_sumBuyOrd";
            label_sumBuyOrd.Size = new Size(153, 33);
            label_sumBuyOrd.TabIndex = 16;
            label_sumBuyOrd.Text = "0";
            label_sumBuyOrd.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_sumBuyExeAmt
            // 
            label_sumBuyExeAmt.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_sumBuyExeAmt.AutoSize = true;
            label_sumBuyExeAmt.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_sumBuyExeAmt.Location = new Point(616, 72);
            label_sumBuyExeAmt.Name = "label_sumBuyExeAmt";
            label_sumBuyExeAmt.Size = new Size(154, 33);
            label_sumBuyExeAmt.TabIndex = 17;
            label_sumBuyExeAmt.Text = "0";
            label_sumBuyExeAmt.TextAlign = ContentAlignment.MiddleRight;
            // 
            // tabProduct
            // 
            tabProduct.Controls.Add(groupBox9);
            tabProduct.Controls.Add(groupBox8);
            tabProduct.Controls.Add(label_symbol);
            tabProduct.Controls.Add(groupBox7);
            tabProduct.Controls.Add(groupBox6);
            tabProduct.Controls.Add(groupBox5);
            tabProduct.Controls.Add(comboBox_symbols);
            tabProduct.Location = new Point(4, 24);
            tabProduct.Name = "tabProduct";
            tabProduct.Padding = new Padding(3);
            tabProduct.Size = new Size(788, 718);
            tabProduct.TabIndex = 3;
            tabProduct.Text = "Product";
            tabProduct.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(tableLayoutPanel8);
            groupBox9.Location = new Point(15, 136);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new Size(475, 88);
            groupBox9.TabIndex = 8;
            groupBox9.TabStop = false;
            groupBox9.Text = "PnL";
            // 
            // tableLayoutPanel8
            // 
            tableLayoutPanel8.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetDouble;
            tableLayoutPanel8.ColumnCount = 3;
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            tableLayoutPanel8.Controls.Add(label_posPnl, 0, 1);
            tableLayoutPanel8.Controls.Add(label40, 0, 1);
            tableLayoutPanel8.Controls.Add(label_totalPnl, 0, 1);
            tableLayoutPanel8.Controls.Add(label36, 0, 0);
            tableLayoutPanel8.Controls.Add(label_tradePnl, 1, 0);
            tableLayoutPanel8.Controls.Add(label38, 2, 0);
            tableLayoutPanel8.Location = new Point(3, 19);
            tableLayoutPanel8.Name = "tableLayoutPanel8";
            tableLayoutPanel8.RowCount = 2;
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.Size = new Size(466, 63);
            tableLayoutPanel8.TabIndex = 0;
            // 
            // label_posPnl
            // 
            label_posPnl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_posPnl.AutoSize = true;
            label_posPnl.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_posPnl.Location = new Point(6, 33);
            label_posPnl.Name = "label_posPnl";
            label_posPnl.Size = new Size(145, 27);
            label_posPnl.TabIndex = 12;
            label_posPnl.Text = "0";
            label_posPnl.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label40
            // 
            label40.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label40.AutoSize = true;
            label40.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label40.Location = new Point(160, 33);
            label40.Name = "label40";
            label40.Size = new Size(145, 27);
            label40.TabIndex = 11;
            label40.Text = "0";
            label40.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_totalPnl
            // 
            label_totalPnl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_totalPnl.AutoSize = true;
            label_totalPnl.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_totalPnl.Location = new Point(314, 33);
            label_totalPnl.Name = "label_totalPnl";
            label_totalPnl.Size = new Size(146, 27);
            label_totalPnl.TabIndex = 10;
            label_totalPnl.Text = "0";
            label_totalPnl.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label36
            // 
            label36.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label36.AutoSize = true;
            label36.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label36.Location = new Point(6, 3);
            label36.Name = "label36";
            label36.Size = new Size(145, 27);
            label36.TabIndex = 6;
            label36.Text = "Position PnL";
            label36.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_tradePnl
            // 
            label_tradePnl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_tradePnl.AutoSize = true;
            label_tradePnl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_tradePnl.Location = new Point(160, 3);
            label_tradePnl.Name = "label_tradePnl";
            label_tradePnl.Size = new Size(145, 27);
            label_tradePnl.TabIndex = 7;
            label_tradePnl.Text = "Trade PnL";
            label_tradePnl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label38
            // 
            label38.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label38.AutoSize = true;
            label38.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label38.Location = new Point(314, 3);
            label38.Name = "label38";
            label38.Size = new Size(146, 27);
            label38.TabIndex = 8;
            label38.Text = "Total PnL";
            label38.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(tableLayoutPanel6);
            groupBox8.Controls.Add(tableLayoutPanel5);
            groupBox8.Location = new Point(15, 222);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(475, 148);
            groupBox8.TabIndex = 7;
            groupBox8.TabStop = false;
            groupBox8.Text = "Trade Summary";
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetDouble;
            tableLayoutPanel6.ColumnCount = 1;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.Controls.Add(label_CurrPos, 0, 1);
            tableLayoutPanel6.Controls.Add(label35, 0, 0);
            tableLayoutPanel6.Location = new Point(339, 76);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 2;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.Size = new Size(122, 66);
            tableLayoutPanel6.TabIndex = 1;
            // 
            // label_CurrPos
            // 
            label_CurrPos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_CurrPos.AutoSize = true;
            label_CurrPos.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_CurrPos.Location = new Point(6, 34);
            label_CurrPos.Name = "label_CurrPos";
            label_CurrPos.Size = new Size(110, 29);
            label_CurrPos.TabIndex = 11;
            label_CurrPos.Text = "0";
            label_CurrPos.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label35
            // 
            label35.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label35.AutoSize = true;
            label35.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label35.Location = new Point(6, 3);
            label35.Name = "label35";
            label35.Size = new Size(110, 28);
            label35.TabIndex = 6;
            label35.Text = "Current Position";
            label35.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetDouble;
            tableLayoutPanel5.ColumnCount = 3;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel5.Controls.Add(label_P_buyAvgPr, 0, 2);
            tableLayoutPanel5.Controls.Add(label32, 0, 2);
            tableLayoutPanel5.Controls.Add(label31, 0, 1);
            tableLayoutPanel5.Controls.Add(label33, 2, 0);
            tableLayoutPanel5.Controls.Add(label34, 1, 0);
            tableLayoutPanel5.Controls.Add(label_P_sellAvgPr, 1, 1);
            tableLayoutPanel5.Controls.Add(label_P_sellVol, 2, 1);
            tableLayoutPanel5.Controls.Add(label_P_buyVol, 1, 2);
            tableLayoutPanel5.Location = new Point(3, 43);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 3;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 33.34F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel5.Size = new Size(322, 101);
            tableLayoutPanel5.TabIndex = 0;
            // 
            // label_P_buyAvgPr
            // 
            label_P_buyAvgPr.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_P_buyAvgPr.AutoSize = true;
            label_P_buyAvgPr.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_P_buyAvgPr.Location = new Point(112, 67);
            label_P_buyAvgPr.Name = "label_P_buyAvgPr";
            label_P_buyAvgPr.Size = new Size(97, 31);
            label_P_buyAvgPr.TabIndex = 12;
            label_P_buyAvgPr.Text = "0";
            label_P_buyAvgPr.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label32
            // 
            label32.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label32.AutoSize = true;
            label32.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label32.Location = new Point(6, 67);
            label32.Name = "label32";
            label32.Size = new Size(97, 31);
            label32.TabIndex = 3;
            label32.Text = "BUY";
            label32.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label31
            // 
            label31.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label31.AutoSize = true;
            label31.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label31.Location = new Point(6, 35);
            label31.Name = "label31";
            label31.Size = new Size(97, 29);
            label31.TabIndex = 2;
            label31.Text = "SELL";
            label31.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label33
            // 
            label33.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label33.AutoSize = true;
            label33.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label33.Location = new Point(218, 3);
            label33.Name = "label33";
            label33.Size = new Size(98, 29);
            label33.TabIndex = 4;
            label33.Text = "Volume";
            label33.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label34
            // 
            label34.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label34.AutoSize = true;
            label34.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label34.Location = new Point(112, 3);
            label34.Name = "label34";
            label34.Size = new Size(97, 29);
            label34.TabIndex = 5;
            label34.Text = "Avg Price";
            label34.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_P_sellAvgPr
            // 
            label_P_sellAvgPr.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_P_sellAvgPr.AutoSize = true;
            label_P_sellAvgPr.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_P_sellAvgPr.Location = new Point(112, 35);
            label_P_sellAvgPr.Name = "label_P_sellAvgPr";
            label_P_sellAvgPr.Size = new Size(97, 29);
            label_P_sellAvgPr.TabIndex = 9;
            label_P_sellAvgPr.Text = "0";
            label_P_sellAvgPr.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_P_sellVol
            // 
            label_P_sellVol.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_P_sellVol.AutoSize = true;
            label_P_sellVol.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_P_sellVol.Location = new Point(218, 35);
            label_P_sellVol.Name = "label_P_sellVol";
            label_P_sellVol.Size = new Size(98, 29);
            label_P_sellVol.TabIndex = 10;
            label_P_sellVol.Text = "0";
            label_P_sellVol.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_P_buyVol
            // 
            label_P_buyVol.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_P_buyVol.AutoSize = true;
            label_P_buyVol.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_P_buyVol.Location = new Point(218, 67);
            label_P_buyVol.Name = "label_P_buyVol";
            label_P_buyVol.Size = new Size(98, 31);
            label_P_buyVol.TabIndex = 11;
            label_P_buyVol.Text = "0";
            label_P_buyVol.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_symbol
            // 
            label_symbol.AutoSize = true;
            label_symbol.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_symbol.Location = new Point(11, 56);
            label_symbol.Name = "label_symbol";
            label_symbol.Size = new Size(79, 25);
            label_symbol.TabIndex = 6;
            label_symbol.Text = "Symbol";
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(tableLayoutPanel7);
            groupBox7.Controls.Add(label_volume);
            groupBox7.Controls.Add(label27);
            groupBox7.Controls.Add(label_lastPr);
            groupBox7.Location = new Point(164, 41);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(326, 92);
            groupBox7.TabIndex = 5;
            groupBox7.TabStop = false;
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.ColumnCount = 2;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanel7.Controls.Add(label_open, 1, 0);
            tableLayoutPanel7.Controls.Add(label_high, 1, 1);
            tableLayoutPanel7.Controls.Add(label_low, 1, 2);
            tableLayoutPanel7.Controls.Add(label29, 0, 1);
            tableLayoutPanel7.Controls.Add(label30, 0, 2);
            tableLayoutPanel7.Controls.Add(label28, 0, 0);
            tableLayoutPanel7.Location = new Point(180, 10);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 3;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel7.Size = new Size(136, 82);
            tableLayoutPanel7.TabIndex = 8;
            // 
            // label_open
            // 
            label_open.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            label_open.Location = new Point(58, 0);
            label_open.Name = "label_open";
            label_open.Size = new Size(75, 27);
            label_open.TabIndex = 6;
            label_open.Text = "0";
            label_open.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_high
            // 
            label_high.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            label_high.Location = new Point(58, 27);
            label_high.Name = "label_high";
            label_high.Size = new Size(75, 27);
            label_high.TabIndex = 7;
            label_high.Text = "0";
            label_high.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_low
            // 
            label_low.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            label_low.Location = new Point(58, 54);
            label_low.Name = "label_low";
            label_low.Size = new Size(75, 28);
            label_low.TabIndex = 8;
            label_low.Text = "0";
            label_low.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label29
            // 
            label29.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            label29.AutoSize = true;
            label29.Location = new Point(11, 27);
            label29.Name = "label29";
            label29.Size = new Size(40, 27);
            label29.TabIndex = 4;
            label29.Text = "high : ";
            label29.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label30
            // 
            label30.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            label30.AutoSize = true;
            label30.Location = new Point(16, 54);
            label30.Name = "label30";
            label30.Size = new Size(35, 28);
            label30.TabIndex = 5;
            label30.Text = "low : ";
            label30.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label28
            // 
            label28.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            label28.AutoSize = true;
            label28.Location = new Point(8, 0);
            label28.Name = "label28";
            label28.Size = new Size(43, 27);
            label28.TabIndex = 3;
            label28.Text = "open : ";
            label28.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_volume
            // 
            label_volume.Location = new Point(61, 61);
            label_volume.Name = "label_volume";
            label_volume.Size = new Size(94, 21);
            label_volume.TabIndex = 2;
            label_volume.Text = "0";
            label_volume.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Location = new Point(8, 64);
            label27.Name = "label27";
            label27.Size = new Size(56, 15);
            label27.TabIndex = 1;
            label27.Text = "Volume : ";
            // 
            // label_lastPr
            // 
            label_lastPr.AutoSize = true;
            label_lastPr.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_lastPr.Location = new Point(19, 21);
            label_lastPr.Name = "label_lastPr";
            label_lastPr.Size = new Size(48, 21);
            label_lastPr.TabIndex = 0;
            label_lastPr.Text = "price";
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(tableLayoutPanel4);
            groupBox6.Location = new Point(503, 9);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(280, 361);
            groupBox6.TabIndex = 4;
            groupBox6.TabStop = false;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.BackColor = Color.White;
            tableLayoutPanel4.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
            tableLayoutPanel4.ColumnCount = 3;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tableLayoutPanel4.Controls.Add(label_qtAsk6, 0, 0);
            tableLayoutPanel4.Controls.Add(label_prAsk1, 1, 5);
            tableLayoutPanel4.Controls.Add(label_prAsk2, 1, 4);
            tableLayoutPanel4.Controls.Add(label_prAsk3, 1, 3);
            tableLayoutPanel4.Controls.Add(label_prAsk4, 1, 2);
            tableLayoutPanel4.Controls.Add(label_prAsk5, 1, 1);
            tableLayoutPanel4.Controls.Add(label_prAsk6, 1, 0);
            tableLayoutPanel4.Controls.Add(label_prBid1, 1, 6);
            tableLayoutPanel4.Controls.Add(label_prBid2, 1, 7);
            tableLayoutPanel4.Controls.Add(label_prBid3, 1, 8);
            tableLayoutPanel4.Controls.Add(label_prBid4, 1, 9);
            tableLayoutPanel4.Controls.Add(label_prBid5, 1, 10);
            tableLayoutPanel4.Controls.Add(label_prBid6, 1, 11);
            tableLayoutPanel4.Controls.Add(label_qtBid1, 2, 6);
            tableLayoutPanel4.Controls.Add(label_qtBid2, 2, 7);
            tableLayoutPanel4.Controls.Add(label_qtBid3, 2, 8);
            tableLayoutPanel4.Controls.Add(label_qtBid4, 2, 9);
            tableLayoutPanel4.Controls.Add(label_qtBid5, 2, 10);
            tableLayoutPanel4.Controls.Add(label_qtBid6, 2, 11);
            tableLayoutPanel4.Controls.Add(label_qtAsk5, 0, 1);
            tableLayoutPanel4.Controls.Add(label_qtAsk4, 0, 2);
            tableLayoutPanel4.Controls.Add(label_qtAsk3, 0, 3);
            tableLayoutPanel4.Controls.Add(label_qtAsk2, 0, 4);
            tableLayoutPanel4.Controls.Add(label_qtAsk1, 0, 5);
            tableLayoutPanel4.Location = new Point(1, 12);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 12;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333332F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333332F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333332F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333332F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333332F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333332F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333332F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333332F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333332F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333332F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333332F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333332F));
            tableLayoutPanel4.Size = new Size(279, 343);
            tableLayoutPanel4.TabIndex = 0;
            // 
            // label_qtAsk6
            // 
            label_qtAsk6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_qtAsk6.AutoSize = true;
            label_qtAsk6.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_qtAsk6.ForeColor = Color.Red;
            label_qtAsk6.Location = new Point(6, 3);
            label_qtAsk6.Name = "label_qtAsk6";
            label_qtAsk6.Size = new Size(82, 25);
            label_qtAsk6.TabIndex = 27;
            label_qtAsk6.Text = "0";
            label_qtAsk6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_prAsk1
            // 
            label_prAsk1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_prAsk1.AutoSize = true;
            label_prAsk1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_prAsk1.ForeColor = Color.Red;
            label_prAsk1.Location = new Point(97, 143);
            label_prAsk1.Name = "label_prAsk1";
            label_prAsk1.Size = new Size(84, 25);
            label_prAsk1.TabIndex = 9;
            label_prAsk1.Text = "0";
            label_prAsk1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_prAsk2
            // 
            label_prAsk2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_prAsk2.AutoSize = true;
            label_prAsk2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_prAsk2.ForeColor = Color.Red;
            label_prAsk2.Location = new Point(97, 115);
            label_prAsk2.Name = "label_prAsk2";
            label_prAsk2.Size = new Size(84, 25);
            label_prAsk2.TabIndex = 10;
            label_prAsk2.Text = "0";
            label_prAsk2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_prAsk3
            // 
            label_prAsk3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_prAsk3.AutoSize = true;
            label_prAsk3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_prAsk3.ForeColor = Color.Red;
            label_prAsk3.Location = new Point(97, 87);
            label_prAsk3.Name = "label_prAsk3";
            label_prAsk3.Size = new Size(84, 25);
            label_prAsk3.TabIndex = 11;
            label_prAsk3.Text = "0";
            label_prAsk3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_prAsk4
            // 
            label_prAsk4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_prAsk4.AutoSize = true;
            label_prAsk4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_prAsk4.ForeColor = Color.Red;
            label_prAsk4.Location = new Point(97, 59);
            label_prAsk4.Name = "label_prAsk4";
            label_prAsk4.Size = new Size(84, 25);
            label_prAsk4.TabIndex = 12;
            label_prAsk4.Text = "0";
            label_prAsk4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_prAsk5
            // 
            label_prAsk5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_prAsk5.AutoSize = true;
            label_prAsk5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_prAsk5.ForeColor = Color.Red;
            label_prAsk5.Location = new Point(97, 31);
            label_prAsk5.Name = "label_prAsk5";
            label_prAsk5.Size = new Size(84, 25);
            label_prAsk5.TabIndex = 13;
            label_prAsk5.Text = "0";
            label_prAsk5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_prAsk6
            // 
            label_prAsk6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_prAsk6.AutoSize = true;
            label_prAsk6.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_prAsk6.ForeColor = Color.Red;
            label_prAsk6.Location = new Point(97, 3);
            label_prAsk6.Name = "label_prAsk6";
            label_prAsk6.Size = new Size(84, 25);
            label_prAsk6.TabIndex = 14;
            label_prAsk6.Text = "0";
            label_prAsk6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_prBid1
            // 
            label_prBid1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_prBid1.AutoSize = true;
            label_prBid1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_prBid1.ForeColor = Color.LimeGreen;
            label_prBid1.Location = new Point(97, 171);
            label_prBid1.Name = "label_prBid1";
            label_prBid1.Size = new Size(84, 25);
            label_prBid1.TabIndex = 15;
            label_prBid1.Text = "0";
            label_prBid1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_prBid2
            // 
            label_prBid2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_prBid2.AutoSize = true;
            label_prBid2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_prBid2.ForeColor = Color.LimeGreen;
            label_prBid2.Location = new Point(97, 199);
            label_prBid2.Name = "label_prBid2";
            label_prBid2.Size = new Size(84, 25);
            label_prBid2.TabIndex = 16;
            label_prBid2.Text = "0";
            label_prBid2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_prBid3
            // 
            label_prBid3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_prBid3.AutoSize = true;
            label_prBid3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_prBid3.ForeColor = Color.LimeGreen;
            label_prBid3.Location = new Point(97, 227);
            label_prBid3.Name = "label_prBid3";
            label_prBid3.Size = new Size(84, 25);
            label_prBid3.TabIndex = 17;
            label_prBid3.Text = "0";
            label_prBid3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_prBid4
            // 
            label_prBid4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_prBid4.AutoSize = true;
            label_prBid4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_prBid4.ForeColor = Color.LimeGreen;
            label_prBid4.Location = new Point(97, 255);
            label_prBid4.Name = "label_prBid4";
            label_prBid4.Size = new Size(84, 25);
            label_prBid4.TabIndex = 18;
            label_prBid4.Text = "0";
            label_prBid4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_prBid5
            // 
            label_prBid5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_prBid5.AutoSize = true;
            label_prBid5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_prBid5.ForeColor = Color.LimeGreen;
            label_prBid5.Location = new Point(97, 283);
            label_prBid5.Name = "label_prBid5";
            label_prBid5.Size = new Size(84, 25);
            label_prBid5.TabIndex = 19;
            label_prBid5.Text = "0";
            label_prBid5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_prBid6
            // 
            label_prBid6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_prBid6.AutoSize = true;
            label_prBid6.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_prBid6.ForeColor = Color.LimeGreen;
            label_prBid6.Location = new Point(97, 311);
            label_prBid6.Name = "label_prBid6";
            label_prBid6.Size = new Size(84, 29);
            label_prBid6.TabIndex = 20;
            label_prBid6.Text = "0";
            label_prBid6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_qtBid1
            // 
            label_qtBid1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_qtBid1.AutoSize = true;
            label_qtBid1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_qtBid1.ForeColor = Color.LimeGreen;
            label_qtBid1.Location = new Point(190, 171);
            label_qtBid1.Name = "label_qtBid1";
            label_qtBid1.Size = new Size(83, 25);
            label_qtBid1.TabIndex = 21;
            label_qtBid1.Text = "0";
            label_qtBid1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_qtBid2
            // 
            label_qtBid2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_qtBid2.AutoSize = true;
            label_qtBid2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_qtBid2.ForeColor = Color.LimeGreen;
            label_qtBid2.Location = new Point(190, 199);
            label_qtBid2.Name = "label_qtBid2";
            label_qtBid2.Size = new Size(83, 25);
            label_qtBid2.TabIndex = 22;
            label_qtBid2.Text = "0";
            label_qtBid2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_qtBid3
            // 
            label_qtBid3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_qtBid3.AutoSize = true;
            label_qtBid3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_qtBid3.ForeColor = Color.LimeGreen;
            label_qtBid3.Location = new Point(190, 227);
            label_qtBid3.Name = "label_qtBid3";
            label_qtBid3.Size = new Size(83, 25);
            label_qtBid3.TabIndex = 23;
            label_qtBid3.Text = "0";
            label_qtBid3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_qtBid4
            // 
            label_qtBid4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_qtBid4.AutoSize = true;
            label_qtBid4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_qtBid4.ForeColor = Color.LimeGreen;
            label_qtBid4.Location = new Point(190, 255);
            label_qtBid4.Name = "label_qtBid4";
            label_qtBid4.Size = new Size(83, 25);
            label_qtBid4.TabIndex = 24;
            label_qtBid4.Text = "0";
            label_qtBid4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_qtBid5
            // 
            label_qtBid5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_qtBid5.AutoSize = true;
            label_qtBid5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_qtBid5.ForeColor = Color.LimeGreen;
            label_qtBid5.Location = new Point(190, 283);
            label_qtBid5.Name = "label_qtBid5";
            label_qtBid5.Size = new Size(83, 25);
            label_qtBid5.TabIndex = 25;
            label_qtBid5.Text = "0";
            label_qtBid5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_qtBid6
            // 
            label_qtBid6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_qtBid6.AutoSize = true;
            label_qtBid6.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_qtBid6.ForeColor = Color.LimeGreen;
            label_qtBid6.Location = new Point(190, 311);
            label_qtBid6.Name = "label_qtBid6";
            label_qtBid6.Size = new Size(83, 29);
            label_qtBid6.TabIndex = 26;
            label_qtBid6.Text = "0";
            label_qtBid6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_qtAsk5
            // 
            label_qtAsk5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_qtAsk5.AutoSize = true;
            label_qtAsk5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_qtAsk5.ForeColor = Color.Red;
            label_qtAsk5.Location = new Point(6, 31);
            label_qtAsk5.Name = "label_qtAsk5";
            label_qtAsk5.Size = new Size(82, 25);
            label_qtAsk5.TabIndex = 28;
            label_qtAsk5.Text = "0";
            label_qtAsk5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_qtAsk4
            // 
            label_qtAsk4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_qtAsk4.AutoSize = true;
            label_qtAsk4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_qtAsk4.ForeColor = Color.Red;
            label_qtAsk4.Location = new Point(6, 59);
            label_qtAsk4.Name = "label_qtAsk4";
            label_qtAsk4.Size = new Size(82, 25);
            label_qtAsk4.TabIndex = 29;
            label_qtAsk4.Text = "0";
            label_qtAsk4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_qtAsk3
            // 
            label_qtAsk3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_qtAsk3.AutoSize = true;
            label_qtAsk3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_qtAsk3.ForeColor = Color.Red;
            label_qtAsk3.Location = new Point(6, 87);
            label_qtAsk3.Name = "label_qtAsk3";
            label_qtAsk3.Size = new Size(82, 25);
            label_qtAsk3.TabIndex = 30;
            label_qtAsk3.Text = "0";
            label_qtAsk3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_qtAsk2
            // 
            label_qtAsk2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_qtAsk2.AutoSize = true;
            label_qtAsk2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_qtAsk2.ForeColor = Color.Red;
            label_qtAsk2.Location = new Point(6, 115);
            label_qtAsk2.Name = "label_qtAsk2";
            label_qtAsk2.Size = new Size(82, 25);
            label_qtAsk2.TabIndex = 31;
            label_qtAsk2.Text = "0";
            label_qtAsk2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_qtAsk1
            // 
            label_qtAsk1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label_qtAsk1.AutoSize = true;
            label_qtAsk1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_qtAsk1.ForeColor = Color.Red;
            label_qtAsk1.Location = new Point(6, 143);
            label_qtAsk1.Name = "label_qtAsk1";
            label_qtAsk1.Size = new Size(82, 25);
            label_qtAsk1.TabIndex = 32;
            label_qtAsk1.Text = "0";
            label_qtAsk1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(label_minMktFund);
            groupBox5.Controls.Add(label_statusMsg);
            groupBox5.Controls.Add(label_status);
            groupBox5.Controls.Add(label_displayName);
            groupBox5.Controls.Add(label_quoteInc);
            groupBox5.Controls.Add(label_baseInc);
            groupBox5.Controls.Add(label_quoteCurr);
            groupBox5.Controls.Add(label_baseCurr);
            groupBox5.Controls.Add(label_id);
            groupBox5.Controls.Add(label_productType);
            groupBox5.Controls.Add(label26);
            groupBox5.Controls.Add(label25);
            groupBox5.Controls.Add(label24);
            groupBox5.Controls.Add(label23);
            groupBox5.Controls.Add(label22);
            groupBox5.Controls.Add(label21);
            groupBox5.Controls.Add(label20);
            groupBox5.Controls.Add(label19);
            groupBox5.Controls.Add(label18);
            groupBox5.Controls.Add(label17);
            groupBox5.Location = new Point(562, 376);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(215, 309);
            groupBox5.TabIndex = 3;
            groupBox5.TabStop = false;
            groupBox5.Text = "Status";
            // 
            // label_minMktFund
            // 
            label_minMktFund.AutoSize = true;
            label_minMktFund.Location = new Point(120, 259);
            label_minMktFund.Name = "label_minMktFund";
            label_minMktFund.Size = new Size(0, 15);
            label_minMktFund.TabIndex = 19;
            // 
            // label_statusMsg
            // 
            label_statusMsg.AutoSize = true;
            label_statusMsg.Location = new Point(120, 229);
            label_statusMsg.Name = "label_statusMsg";
            label_statusMsg.Size = new Size(0, 15);
            label_statusMsg.TabIndex = 18;
            // 
            // label_status
            // 
            label_status.AutoSize = true;
            label_status.Location = new Point(120, 202);
            label_status.Name = "label_status";
            label_status.Size = new Size(0, 15);
            label_status.TabIndex = 17;
            // 
            // label_displayName
            // 
            label_displayName.AutoSize = true;
            label_displayName.Location = new Point(120, 176);
            label_displayName.Name = "label_displayName";
            label_displayName.Size = new Size(0, 15);
            label_displayName.TabIndex = 16;
            // 
            // label_quoteInc
            // 
            label_quoteInc.AutoSize = true;
            label_quoteInc.Location = new Point(120, 149);
            label_quoteInc.Name = "label_quoteInc";
            label_quoteInc.Size = new Size(0, 15);
            label_quoteInc.TabIndex = 15;
            // 
            // label_baseInc
            // 
            label_baseInc.AutoSize = true;
            label_baseInc.Location = new Point(120, 122);
            label_baseInc.Name = "label_baseInc";
            label_baseInc.Size = new Size(0, 15);
            label_baseInc.TabIndex = 14;
            // 
            // label_quoteCurr
            // 
            label_quoteCurr.AutoSize = true;
            label_quoteCurr.Location = new Point(120, 97);
            label_quoteCurr.Name = "label_quoteCurr";
            label_quoteCurr.Size = new Size(0, 15);
            label_quoteCurr.TabIndex = 13;
            // 
            // label_baseCurr
            // 
            label_baseCurr.AutoSize = true;
            label_baseCurr.Location = new Point(120, 73);
            label_baseCurr.Name = "label_baseCurr";
            label_baseCurr.Size = new Size(0, 15);
            label_baseCurr.TabIndex = 12;
            // 
            // label_id
            // 
            label_id.AutoSize = true;
            label_id.Location = new Point(120, 48);
            label_id.Name = "label_id";
            label_id.Size = new Size(0, 15);
            label_id.TabIndex = 11;
            // 
            // label_productType
            // 
            label_productType.AutoSize = true;
            label_productType.Location = new Point(120, 19);
            label_productType.Name = "label_productType";
            label_productType.Size = new Size(0, 15);
            label_productType.TabIndex = 10;
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Location = new Point(12, 259);
            label26.Name = "label26";
            label26.Size = new Size(102, 15);
            label26.TabIndex = 9;
            label26.Text = "min market fund :";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new Point(21, 229);
            label25.Name = "label25";
            label25.Size = new Size(93, 15);
            label25.TabIndex = 8;
            label25.Text = "status message :";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(70, 202);
            label24.Name = "label24";
            label24.Size = new Size(44, 15);
            label24.TabIndex = 7;
            label24.Text = "status :";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(31, 176);
            label23.Name = "label23";
            label23.Size = new Size(83, 15);
            label23.TabIndex = 6;
            label23.Text = "display name :";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(13, 149);
            label22.Name = "label22";
            label22.Size = new Size(101, 15);
            label22.TabIndex = 5;
            label22.Text = "quote increment :";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(20, 122);
            label21.Name = "label21";
            label21.Size = new Size(94, 15);
            label21.TabIndex = 4;
            label21.Text = "base increment :";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(21, 97);
            label20.Name = "label20";
            label20.Size = new Size(93, 15);
            label20.TabIndex = 3;
            label20.Text = "quote currency :";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(28, 73);
            label19.Name = "label19";
            label19.Size = new Size(86, 15);
            label19.TabIndex = 2;
            label19.Text = "base currency :";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(91, 48);
            label18.Name = "label18";
            label18.Size = new Size(23, 15);
            label18.TabIndex = 1;
            label18.Text = "id :";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(33, 19);
            label17.Name = "label17";
            label17.Size = new Size(81, 15);
            label17.TabIndex = 0;
            label17.Text = "product type :";
            // 
            // comboBox_symbols
            // 
            comboBox_symbols.FormattingEnabled = true;
            comboBox_symbols.Location = new Point(15, 16);
            comboBox_symbols.Name = "comboBox_symbols";
            comboBox_symbols.Size = new Size(171, 23);
            comboBox_symbols.TabIndex = 2;
            comboBox_symbols.SelectedIndexChanged += comboBox_symbols_SelectedIndexChanged;
            // 
            // tabConfig
            // 
            tabConfig.Controls.Add(groupBox4);
            tabConfig.Location = new Point(4, 24);
            tabConfig.Name = "tabConfig";
            tabConfig.Padding = new Padding(3);
            tabConfig.Size = new Size(788, 718);
            tabConfig.TabIndex = 2;
            tabConfig.Text = "Config";
            tabConfig.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(label_apiInfo);
            groupBox4.Controls.Add(label_url);
            groupBox4.Controls.Add(label16);
            groupBox4.Controls.Add(label15);
            groupBox4.Location = new Point(5, 6);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(777, 83);
            groupBox4.TabIndex = 0;
            groupBox4.TabStop = false;
            groupBox4.Text = "connection info";
            // 
            // label_apiInfo
            // 
            label_apiInfo.Location = new Point(95, 53);
            label_apiInfo.Name = "label_apiInfo";
            label_apiInfo.Size = new Size(501, 22);
            label_apiInfo.TabIndex = 3;
            label_apiInfo.Text = "File name here";
            // 
            // label_url
            // 
            label_url.Location = new Point(95, 25);
            label_url.Name = "label_url";
            label_url.Size = new Size(501, 22);
            label_url.TabIndex = 2;
            label_url.Text = "test url http://www.www.www";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label16.Location = new Point(16, 53);
            label16.Name = "label16";
            label16.Size = new Size(61, 15);
            label16.TabIndex = 1;
            label16.Text = "API Info : ";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.Location = new Point(16, 25);
            label15.Name = "label15";
            label15.Size = new Size(36, 15);
            label15.TabIndex = 0;
            label15.Text = "URL :";
            // 
            // display_update
            // 
            display_update.Enabled = true;
            display_update.Interval = 1000;
            display_update.Tick += display_update_Tick;
            // 
            // buttonOMS
            // 
            buttonOMS.FlatStyle = FlatStyle.Popup;
            buttonOMS.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonOMS.Location = new Point(602, 303);
            buttonOMS.Name = "buttonOMS";
            buttonOMS.Size = new Size(181, 40);
            buttonOMS.TabIndex = 5;
            buttonOMS.Text = "Initiate OMS";
            buttonOMS.UseVisualStyleBackColor = true;
            buttonOMS.Click += buttonOMS_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 759);
            Controls.Add(tabControl);
            Name = "Form1";
            Text = "Form1";
            tabControl.ResumeLayout(false);
            tabMain.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            groupBox2.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            groupBox1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tabProduct.ResumeLayout(false);
            tabProduct.PerformLayout();
            groupBox9.ResumeLayout(false);
            tableLayoutPanel8.ResumeLayout(false);
            tableLayoutPanel8.PerformLayout();
            groupBox8.ResumeLayout(false);
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            tableLayoutPanel7.ResumeLayout(false);
            tableLayoutPanel7.PerformLayout();
            groupBox6.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            tabConfig.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl;
        private TabPage tabMain;
        private GroupBox groupBox3;
        private GroupBox groupBox2;
        private ComboBox comboBox_mode;
        private GroupBox groupBox1;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel2;
        private Label mainLog;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label label8;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label10;
        private Label label9;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Button button_startListen;
        private Label label_feedAll;
        private Label label_feedInc;
        private Label label_feedQueue;
        private Label label_liveBuyIns;
        private Label label_liveBuyOrd;
        private Label label_liveBuyAmt;
        private Label label_liveSellIns;
        private Label label_liveSellOrd;
        private Label label_liveSellAmt;
        private Label label_sumBuyIns;
        private Label label_sumSellIns;
        private Label label_sumSellOrd;
        private Label label_sumSellExeOrd;
        private Label label_sumSellExeAmt;
        private Label label_sumBuyExeOrd;
        private Label label_sumBuyOrd;
        private Label label_sumBuyExeAmt;
        private System.Windows.Forms.Timer display_update;
        private TabPage tabConfig;
        private TabPage tabProduct;
        private ComboBox comboBox_symbols;
        private GroupBox groupBox4;
        private Label label15;
        private Label label_apiInfo;
        private Label label_url;
        private Label label16;
        private GroupBox groupBox5;
        private Label label19;
        private Label label18;
        private Label label17;
        private Label label20;
        private Label label22;
        private Label label21;
        private Label label23;
        private Label label24;
        private Label label25;
        private Label label26;
        private Label label_minMktFund;
        private Label label_statusMsg;
        private Label label_status;
        private Label label_displayName;
        private Label label_quoteInc;
        private Label label_baseInc;
        private Label label_quoteCurr;
        private Label label_baseCurr;
        private Label label_id;
        private Label label_productType;
        private GroupBox groupBox6;
        private TableLayoutPanel tableLayoutPanel4;
        private Label label_prAsk1;
        private Label label_prAsk2;
        private Label label_prAsk3;
        private Label label_prAsk4;
        private Label label_prAsk5;
        private Label label_prBid1;
        private Label label_prBid2;
        private Label label_prBid3;
        private Label label_prBid4;
        private Label label_prBid5;
        private Label label_qtBid1;
        private Label label_qtBid2;
        private Label label_qtBid3;
        private Label label_qtBid4;
        private Label label_qtBid5;
        private Label label_qtAsk5;
        private Label label_qtAsk4;
        private Label label_qtAsk3;
        private Label label_qtAsk2;
        private Label label_qtAsk1;
        private Label label_qtAsk6;
        private Label label_prAsk6;
        private Label label_prBid6;
        private Label label_qtBid6;
        private Label label_symbol;
        private GroupBox groupBox7;
        private Label label_lastPr;
        private Label label_volume;
        private Label label27;
        private Label label_low;
        private Label label_high;
        private Label label_open;
        private Label label30;
        private Label label29;
        private Label label28;
        private GroupBox groupBox8;
        private TableLayoutPanel tableLayoutPanel5;
        private Label label_P_buyAvgPr;
        private Label label32;
        private Label label31;
        private Label label33;
        private Label label34;
        private Label label_P_sellAvgPr;
        private Label label_P_sellVol;
        private Label label_P_buyVol;
        private TableLayoutPanel tableLayoutPanel6;
        private Label label_CurrPos;
        private Label label35;
        private TableLayoutPanel tableLayoutPanel7;
        private GroupBox groupBox9;
        private TableLayoutPanel tableLayoutPanel8;
        private Label label_posPnl;
        private Label label40;
        private Label label_totalPnl;
        private Label label36;
        private Label label_tradePnl;
        private Label label38;
        private Button buttonOMS;
    }
}
