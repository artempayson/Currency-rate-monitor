using Logic.DTO;
using Logic.Model;
using Logic.Model.ExistingCurrencies;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    class Repository
    {
        const string GetAllPairsURL = "https://btc-e.nz/api/3/info";
        const string GetSpecifiedPairURL = "https://btc-e.nz/api/3/ticker/"; //after "/" pair[s] should be specified

        public async Task<List<ICurrencyPair>> GetAllPairs()
        {
            using (HttpClient hc = new HttpClient())
            {
                HttpResponseMessage response = await hc.GetAsync(GetAllPairsURL);
                string responseString = await response.Content.ReadAsStringAsync();
                JObject rawData = JObject.Parse(responseString);

                var allPairsRawDict = rawData.Value<JObject>("pairs").Properties().ToDictionary(key => key.Name, value => value.ToString());
                List<ICurrencyPair> allPairs = CurrencyRepository.ExistingCurrenciesList;

                foreach (var pair in allPairsRawDict)
                {
                    var rawPairItem = JsonConvert.DeserializeObject<CurrencyPairResponseGeneral>(pair.Value.Substring(10));
                    var index = allPairs.FindIndex(curp => curp.ShortName.Equals(pair.Key));
                    if (rawPairItem.Hidden == 1) allPairs[index].Disable();
                }
                return allPairs;
            }
        }
        public void UpdateSpecifiedPair(ICurrencyPair chosenPair)
        {
            using (HttpClient hc = new HttpClient())
            {
                HttpResponseMessage response =  hc.GetAsync(string.Format("{0}{1}", GetSpecifiedPairURL, chosenPair.ShortName)).Result;
                string responseString =  response.Content.ReadAsStringAsync().Result;
                var rawItem = JsonConvert.DeserializeObject<CurrencyPairResponseSpecific>(responseString);
                chosenPair.UpdateValues(rawItem.Avg, rawItem.Buy, rawItem.Sell, rawItem.High, rawItem.Low);
            }
           
        }

    }
}
