using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTO
{
    class CurrencyPairResponseSpecific
    {
            public decimal High { get; set; }
            public decimal Low { get; set; }
            public decimal Avg { get; set; }
            public decimal Vol { get; set; }
            public decimal Vol_cur { get; set; }
            public decimal Last { get; set; }
            public decimal Buy { get; set; }
            public decimal Sell { get; set; }
            public long Updated { get; set; }
        }
    
}
