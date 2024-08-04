using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinbase_main
{
    internal class orderManager
    {

        public Action<string> addLog = (str) => { Console.WriteLine(str); };

        private static orderManager _instance;
        private static readonly object _lockObject = new object();

        public static orderManager GetInstance()
        {
            lock (_lockObject)
            {
                if (_instance == null)
                {
                    //インスタンス生成
                    _instance = new orderManager();
                }
                return _instance;
            }
        }
    }
}
