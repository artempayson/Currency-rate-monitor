using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Logic.Model;
using Logic.Model.ExistingCurrencies;
using Logic.DTO;

namespace Logic
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository r = new Repository();
            double ssa = 0.12;
            var beta = r.GetAllPairs();
            var a = beta.Result[15];
            r.UpdateSpecifiedPair(a);
            List<CurrencyPair> aaaaaa = new List<CurrencyPair> { new CurrencyPair("btc_usd"), new CurrencyPair("btc_rur") };
           var b = aaaaaa.ToDictionary(key => key.FullName, value => value);
            var slss = 42;
        }

    }
}
