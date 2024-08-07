// See https://aka.ms/new-console-template for more information
 
using coinbase_connection;
using System.Net.WebSockets;
using System.Text;

string configfile = "coinbase_feedreader.ini";
string outputfile = "feedFile";
string apiFilename = "coinbase_api.txt";
string url = "wss://advanced-trade-ws.coinbase.com";

string today = DateTime.Now.ToString("yyyyMMdd");

string[] symbols = ["ETH-USD"];

byte[] buffer = new byte[1073741824];

using (StreamReader sr = new StreamReader(configfile))
{
    string line;
    string[] values;
    while ((line = sr.ReadLine()) != null)
    {
        values = line.Split("=");
        if (values[0]=="outputFile")
        {
            outputfile = values[1];
        }
        else if (values[0] == "apiFile")
        {
            apiFilename = values[1];
        }
        else if (values[0] == "url")
        {
            url = values[1];
        }
        else if (values[0] == "symbolList")
        {
            symbols = values[1].Replace("[","").Replace("]","").Split(",");
        }
    }
    
    outputfile += today + ".txt";
}

using (StreamWriter sw = new StreamWriter(outputfile))
{
    coinbase_connection.coinbase_connection cc = new coinbase_connection.coinbase_connection();
    cc.readApiKey(apiFilename);
    WebSocketState st;
    await cc.connect(url);
    st = cc.getState();
    Console.WriteLine("Connection Result:" + st.ToString());
    Console.WriteLine("Start Listening...");

    cc.startListen(cbChannels.heartbeats);
    cc.startListen(cbChannels.level2, symbols);
    cc.startListen(cbChannels.market_trades, symbols);

    DateTime prevTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
    DateTime currentDatetime = prevTime;

    while (true)
    {
        var segment = new ArraySegment<byte>(buffer);
        var result = cc.recv(ref segment);


        if (result.IsFaulted == false)
        {
            if (result.Result.MessageType == WebSocketMessageType.Close)
            {
                Console.WriteLine("Endpoint Closed");
                return;
            }

            if (result.Result.MessageType == WebSocketMessageType.Binary)
            {
                Console.WriteLine("Result Binary");
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
                    Console.WriteLine("Too Large Message   " + count.ToString());
                    return;
                }
                segment = new ArraySegment<byte>(buffer, count, buffer.Length - count);
                result = cc.recv(ref segment);

                count += result.Result.Count;
            }

            var message = Encoding.UTF8.GetString(buffer, 0, count);
            if (message.Contains("heartbeats"))
            {
                currentDatetime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
                if ((int)currentDatetime.Subtract(prevTime).TotalMinutes >= 15)
                {
                    Console.WriteLine(currentDatetime.ToString() + "> " + message);
                    prevTime = currentDatetime;
                    if(currentDatetime.Hour == 17)
                    {
                        Console.WriteLine("Market Closed.");
                        cc.disconnect();
                        break;
                    }
                }
            }
            else
            {
                //cbMsg.message msg = new cbMsg.message();
                //coinbase_connection.msgParser.parseMsg(message,ref msg);
                sw.WriteLine(message);
                //Console.WriteLine("> " + message);
            }
            //Console.WriteLine("> " + message);
        }

    }
}

