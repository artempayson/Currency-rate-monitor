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
            string responseString = r.R().Result;

            JObject rawData = JObject.Parse(responseString);

            var allPairsRawDict = rawData.Value<JObject>("pairs").Properties().ToDictionary(key => key.Name, value => value.ToString());
            List<ICurrencyPair> allPairs = CurrencyRepository.ExistingCurrenciesList;
            var mdof = new Dictionary<string, PairResponse>();

            foreach (var pair in allPairsRawDict)
            {
                var alef = pair.Value.Substring(10);
                mdof[pair.Key] = JsonConvert.DeserializeObject<PairResponse>(alef);
            }

        }
     
    }
}
