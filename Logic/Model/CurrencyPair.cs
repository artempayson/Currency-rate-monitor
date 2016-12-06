using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Model
{
    class CurrencyPair : ICurrencyPair
    {
        public decimal Average { get; set; }

        public decimal Buy { get; set; }


        public decimal High { get; set; }

        public bool IsEnabled { get; }
        public decimal Low { get; set; }

        public decimal Sell { get; set; }

        public string ShortName { get; }
        public CurrencyPair(bool isEnabled, string name)
        {
            IsEnabled = isEnabled;
            ShortName = name;
        }
    }
}
