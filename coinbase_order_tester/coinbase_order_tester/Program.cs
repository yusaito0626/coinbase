﻿
using coinbase_connection;
using coinbase_main;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

Dictionary<string,crypto> cryptos = new Dictionary<string,crypto>();
string[] symbols = new string[1];
symbols[0] = "ETH-GBP";
Dictionary<string, string> dict;

coinbase_connection.coinbase_connection cc = new coinbase_connection.coinbase_connection();
orderManager oms = orderManager.GetInstance();
string apiFilename = "D:\\coinbase\\cdp_api_key_live.json";
cc.readApiJson(apiFilename);

string restApiMsgFile = "D:\\coinbase\\restApi_test.txt";
string order_log_path = "D:\\coinbase\\order_log\\";

string url = "wss://advanced-trade-ws.coinbase.com";

WebSocketState st;
await cc.connect(url);
st = cc.getState();
Console.WriteLine(st.ToString());

byte[] buffer = new byte[1073741824];
cc.startListen(cbChannels.heartbeats);
cc.startListen(cbChannels.status, symbols);
int heartbeatCount = 0;
cbMsg.message msg = new cbMsg.message();
cbMsg.product_status status = new cbMsg.product_status();
//Initialize crypto object
while (true)
{
    var segment = new ArraySegment<byte>(buffer);
    var result = cc.recv(ref segment);


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
                Console.WriteLine("[ERROR] The message is too large.");
                return;
            }
            segment = new ArraySegment<byte>(buffer, count, buffer.Length - count);
            result = cc.recv(ref segment);

            count += result.Result.Count;
        }

        var message = Encoding.UTF8.GetString(buffer, 0, count);
        if (message.Contains("heartbeats"))
        {
            ++heartbeatCount;
            if (heartbeatCount >= 5)
            {
                cc.disconnect();
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
                    if (cryptos.ContainsKey(status.id))
                    {
                        cryptos[status.id].setStatus(status);
                    }
                    else
                    {
                        crypto cp = new crypto();
                        cp.setStatus(status);
                        cryptos[cp.id] = cp;
                    }
                    start = msg.events.IndexOf("{", end);
                }
                if (cryptos.Count == symbols.Length)
                {
                    cc.disconnect();
                    break;
                }
            }
        }
    }
}


oms.initialize(apiFilename, url, cryptos, order_log_path);
crypto testing_cp = cryptos["ETH-GBP"];
double order_size = testing_cp.base_increment;

testing_cp.minOrdPr = (int)(1700 / testing_cp.quote_increment);
testing_cp.maxOrdPr = (int)(2500 / testing_cp.quote_increment);
testing_cp.minPr = (int)(1700 / testing_cp.quote_increment);
testing_cp.maxPr = (int)(2600 / testing_cp.quote_increment);
testing_cp.bestask = (int)(1900 / testing_cp.quote_increment);
testing_cp.bestbid = (int)(1899 / testing_cp.quote_increment);
testing_cp.maxBaseSize = 0.001;
testing_cp.maxQuoteSize = 20;

HttpResponseMessage res;

using (StreamWriter sw = new StreamWriter(restApiMsgFile))
{
    Console.WriteLine("Sending Invalid Symbol...");
    res = await oms.sendLimitGTC("test", "BUY", 0.0005, 1801.00, true);
    if (res != null)
    {
        Console.WriteLine("Failed to filter an invalid order");
        return;
    }
    Console.WriteLine("Sending Invalid Side...");
    res = await oms.sendLimitGTC(testing_cp.id, "test", 0.0005, 1801.00, true);
    if (res != null)
    {
        Console.WriteLine("Failed to filter an invalid order");
        return;
    }
    Console.WriteLine("Sending Invalid Size...");
    res = await oms.sendLimitGTC(testing_cp.id, "BUY", 0.0011, 1801.00, true);
    if (res != null)
    {
        Console.WriteLine("Failed to filter an invalid order");
        return;
    }
    Console.WriteLine("Sending Invalid Price...");
    res = await oms.sendLimitGTC(testing_cp.id, "BUY", 0.0005, 1699.00, true);
    if (res != null)
    {
        Console.WriteLine("Failed to filter an invalid order");
        return;
    }
    Console.WriteLine("Sending a proper order...");
    res = await oms.sendLimitGTC(testing_cp.id, "BUY", 0.0005, 1800.00, true);
    if (res != null)
    {
        Console.WriteLine(res.ToString());
        sw.WriteLine(res.ToString());
        sw.Flush();
        if (res.IsSuccessStatusCode)
        {
            Console.WriteLine("Order Success!.");
        }
        else
        {
            Console.WriteLine("Order Failed.");
            return;
        }
    }
    else
    {
        Console.WriteLine("The order didn't go through");
        return;
    }
    System.Threading.Thread.Sleep(5000);
    Console.WriteLine("Sending mod orders...");
    foreach (KeyValuePair<string, order> ord in testing_cp.liveOrders)
    {
        Console.WriteLine(ord.Key);
        if(ord.Value.status == "OPEN")
        {
            res = await oms.sendModOrder(ord.Value, 0.0001, 0);
            if (res != null)
            {
                Console.WriteLine(res.ToString());
                sw.WriteLine(res.ToString());
                sw.Flush();
                if (res.IsSuccessStatusCode)
                {
                    Console.WriteLine("Order Success!.");
                }
                else
                {
                    Console.WriteLine("Order Failed.");
                }
            }
            else
            {
                Console.WriteLine("The order didn't go through");
                return;
            }
        }
        else
        {
            Console.WriteLine("The order is not live yet");
            Console.WriteLine(ord.Value.status);
        }
    }
    System.Threading.Thread.Sleep(5000);
    Console.WriteLine("Sending can orders...");
    foreach (KeyValuePair<string, order> ord in testing_cp.liveOrders)
    {
        Console.WriteLine(ord.Key);
        res = await oms.sendCanOrder(ord.Value);
        if (res != null)
        {
            Console.WriteLine(res.ToString());
            sw.WriteLine(res.ToString());
            sw.Flush();
            if (res.IsSuccessStatusCode)
            {
                Console.WriteLine("Order Success!.");
            }
            else
            {
                Console.WriteLine("Order Failed.");
            }
        }
        else
        {
            Console.WriteLine("The order didn't go through");
            return;
        }
    }
    Console.WriteLine("Sending a market order...");
    res = await oms.sendMarketOrder(testing_cp.id, "BUY", 0.0005);
    if (res != null)
    {
        Console.WriteLine(res.ToString());
        sw.WriteLine(res.ToString());
        sw.Flush();
        if (res.IsSuccessStatusCode)
        {
            Console.WriteLine("Order Success!.");
            Console.WriteLine("Check the fill");
        }
        else
        {
            Console.WriteLine("Order Failed.");
            return;
        }
    }
    else
    {
        Console.WriteLine("The order didn't go through");
        return;
    }
}
while(true)
{
    System.Threading.Thread.Sleep(5000);
    Console.WriteLine("Running...");
}
