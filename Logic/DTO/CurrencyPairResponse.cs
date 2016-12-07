using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTO
{
    class CurrencyPairResponse
    {
        [JsonProperty(PropertyName = "hidden")]
        public byte Hidden { get; set; }
        [JsonProperty(PropertyName = "decimal_places")]
        public byte DecimalPlaces { get; set; }
        [JsonProperty(PropertyName = "min_price")]
        public decimal MinPrice { get; set; }
        [JsonProperty(PropertyName = "max_price")]
        public decimal MaxPrice { get; set; }
        [JsonProperty(PropertyName =" min_amount")]
        public decimal MinAmount { get; set; }
        [JsonProperty(PropertyName = "fee")]
        public decimal Fee { get; set; }
      
    }
}
