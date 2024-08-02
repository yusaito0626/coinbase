namespace coinbase_app
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            string configFile = "coinbase_app.ini";
            InitializeComponent();
            readConfig(configFile);
            this.threadManager = new threadManager(this.decodingThCount, this.quotesThCount, this.optThCount);
            for (int i = 0; i < this.symbols.Length; ++i)
            {
                this.comboBox_symbols.Items.Add(this.symbols[i]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

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

        threadManager threadManager;

        private void display_update_Tick(object sender, EventArgs e)
        {

        }
        private void display_update_main()
        {

        }

        private void display_update_product()
        {

        }
        private void display_update_config()
        {
            this.label_url.Text = this.url;
            this.label_apiInfo.Text = this.apiFilename;
        }

        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            switch(e.TabPage.Name)
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
    }
}
