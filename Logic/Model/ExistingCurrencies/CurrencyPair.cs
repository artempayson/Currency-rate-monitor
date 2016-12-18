using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Model.ExistingCurrencies
{

    class CurrencyPair : ICurrencyPair
    {
        

        public string ShortName { get; }
        public string FullName { get; }
        public CurrencyPair(string shortName)
        {
            ShortName = shortName;
            FullName = CurrencyNames.existingCurrencyNames[shortName.Substring(0, 3)] + " to " + CurrencyNames.existingCurrencyNames[shortName.Substring(shortName.Length-3, 3)];
        }

        public bool IsEnabled { get; private set; } = true;
        public void Disable()
        {
            IsEnabled = false;
        }

        public decimal Average { get; private set; }
        public decimal Buy { get; private set; }
        public decimal Sell { get; private set; }
        public decimal High { get; private set; }
        public decimal Low { get; private set; }
        public decimal Last { get; private set; }

        public void UpdateValues(decimal average, decimal last, decimal buy, decimal sell, decimal high, decimal low)
        {
           
            Average = average;
            Last = last;
            Buy = buy;
            Sell = sell;
            High = high;
            Low = low;
        }
    }
}
