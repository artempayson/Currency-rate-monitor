﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Model.ExistingCurrencies
{





    class CurrencyPair : ICurrencyPair

    {
        private Dictionary<string, string> existingCurrencyNames = new Dictionary<string, string>
        {
            {"btc","Bitcoin"},
            {"ltc","Litecoin" },
            {"rur","Russian Ruble" },
            {"usd","United States Dollar" },
            {"eur","European Union Euro" },
            {"nmc","Namecoin" },
            {"nvc","Novacoin" },
            {"ppc","Peercoin" },
            {"dsh","Dashcoin" },
            {"eth","Etherium" }
            
        };

        public string ShortName { get; }
        public string FullName { get; }
        public CurrencyPair(string shortName)
        {
            ShortName = shortName;
            FullName = existingCurrencyNames[shortName.Substring(0, 3)] + " to " + existingCurrencyNames[shortName.Substring(shortName.Length-4, 3)];
        }
        public bool IsEnabled { get; }

        public decimal Average { get; private set; }
        public decimal Buy { get; private set; }
        public decimal Sell { get; private set; }
        public decimal High { get; private set; }
        public decimal Low { get; private set; }

        public void UpdateValues(decimal average, decimal buy, decimal sell, decimal high, decimal low)
        {
            Average = average;
            Buy = buy;
            Sell = sell;
            High = high;
            Low = low;
        }
    }
}
