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
            button1 = new Button();
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
            tabControl.SuspendLayout();
            tabMain.SuspendLayout();
            groupBox3.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            groupBox2.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            groupBox1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tabProduct.SuspendLayout();
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
            tabMain.Controls.Add(button1);
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
            // button1
            // 
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(604, 349);
            button1.Name = "button1";
            button1.Size = new Size(181, 40);
            button1.TabIndex = 4;
            button1.Text = "Start";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
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
            groupBox5.Location = new Point(14, 46);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(228, 309);
            groupBox5.TabIndex = 3;
            groupBox5.TabStop = false;
            groupBox5.Text = "Status";
            // 
            // label_minMktFund
            // 
            label_minMktFund.AutoSize = true;
            label_minMktFund.Location = new Point(120, 259);
            label_minMktFund.Name = "label_minMktFund";
            label_minMktFund.Size = new Size(30, 15);
            label_minMktFund.TabIndex = 19;
            label_minMktFund.Text = "type";
            // 
            // label_statusMsg
            // 
            label_statusMsg.AutoSize = true;
            label_statusMsg.Location = new Point(120, 229);
            label_statusMsg.Name = "label_statusMsg";
            label_statusMsg.Size = new Size(30, 15);
            label_statusMsg.TabIndex = 18;
            label_statusMsg.Text = "type";
            // 
            // label_status
            // 
            label_status.AutoSize = true;
            label_status.Location = new Point(120, 202);
            label_status.Name = "label_status";
            label_status.Size = new Size(30, 15);
            label_status.TabIndex = 17;
            label_status.Text = "type";
            // 
            // label_displayName
            // 
            label_displayName.AutoSize = true;
            label_displayName.Location = new Point(120, 176);
            label_displayName.Name = "label_displayName";
            label_displayName.Size = new Size(30, 15);
            label_displayName.TabIndex = 16;
            label_displayName.Text = "type";
            // 
            // label_quoteInc
            // 
            label_quoteInc.AutoSize = true;
            label_quoteInc.Location = new Point(120, 149);
            label_quoteInc.Name = "label_quoteInc";
            label_quoteInc.Size = new Size(30, 15);
            label_quoteInc.TabIndex = 15;
            label_quoteInc.Text = "type";
            // 
            // label_baseInc
            // 
            label_baseInc.AutoSize = true;
            label_baseInc.Location = new Point(120, 122);
            label_baseInc.Name = "label_baseInc";
            label_baseInc.Size = new Size(30, 15);
            label_baseInc.TabIndex = 14;
            label_baseInc.Text = "type";
            // 
            // label_quoteCurr
            // 
            label_quoteCurr.AutoSize = true;
            label_quoteCurr.Location = new Point(120, 97);
            label_quoteCurr.Name = "label_quoteCurr";
            label_quoteCurr.Size = new Size(30, 15);
            label_quoteCurr.TabIndex = 13;
            label_quoteCurr.Text = "type";
            // 
            // label_baseCurr
            // 
            label_baseCurr.AutoSize = true;
            label_baseCurr.Location = new Point(120, 73);
            label_baseCurr.Name = "label_baseCurr";
            label_baseCurr.Size = new Size(30, 15);
            label_baseCurr.TabIndex = 12;
            label_baseCurr.Text = "type";
            // 
            // label_id
            // 
            label_id.AutoSize = true;
            label_id.Location = new Point(120, 48);
            label_id.Name = "label_id";
            label_id.Size = new Size(30, 15);
            label_id.TabIndex = 11;
            label_id.Text = "type";
            // 
            // label_productType
            // 
            label_productType.AutoSize = true;
            label_productType.Location = new Point(120, 19);
            label_productType.Name = "label_productType";
            label_productType.Size = new Size(30, 15);
            label_productType.TabIndex = 10;
            label_productType.Text = "type";
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
            display_update.Interval = 1000;
            display_update.Tick += display_update_Tick;
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
        private Button button1;
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
    }
}
