using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Model
{
    interface ICurrencyPair
    {
        string ShortName { get; }
        string FullName { get; }
        bool IsEnabled { get; }
        decimal High { get; }
        decimal Low { get; }
        decimal Average { get; }
        decimal Buy { get; }
        decimal Sell { get; }
        
    }
}
