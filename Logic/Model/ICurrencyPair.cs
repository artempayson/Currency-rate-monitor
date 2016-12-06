﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Model
{
    interface ICurrencyPair
    {
        string ShortName { get; }
        void Disable();
        bool IsEnabled { get;}
        decimal High { get; }
        decimal Low { get; }
        decimal Average { get; }
        decimal Buy { get; }
        decimal Sell { get; }
        string FullName { get;}
        void UpdateValues(decimal average, decimal buy, decimal sell, decimal high, decimal low);
    }
}
