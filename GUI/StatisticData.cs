using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class StatisticData
    {
        public int CurrencyPairID { get; set; }
        public string CurrencyPairName;
        public List<string> axisXData { get; set; }
        public List<decimal> axisSellData { get; set; }
        public List<decimal> axisBuyData { get; set; }
        public StatisticData(int currencyPairID, string currencyPairName, List<string> axisXData, List<decimal> axisSellData, List<decimal> axisBuyData)
        {
            CurrencyPairID = currencyPairID;
            CurrencyPairName = currencyPairName;
            this.axisXData = axisXData;
            this.axisSellData = axisSellData;
            this.axisBuyData = axisBuyData;
        }


    }
}
