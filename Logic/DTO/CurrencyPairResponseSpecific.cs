using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Logic.DTO
{
    class CurrencyPairResponseSpecific
    {
            public double High { get; set; }
            public double Low { get; set; }
            public double Avg { get; set; }
            public double Vol { get; set; }
            public double Vol_cur { get; set; }
            public double Last { get; set; }
            public double Buy { get; set; }
            public double Sell { get; set; }
            public long Updated { get; set; }
        }
    
}
