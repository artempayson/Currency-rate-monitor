using Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    class Repository
    {
        const string GetAllPairsURL = "https://btc-e.nz/api/3/info";
        const string GetSpecifiedPairURL = "https://btc-e.nz/api/3/ticker"; //after "/" pair[s] should be specified
        //public async Task<List<ICurrencyPair>> GetAllPairs()
        //{
        //    using (HttpClient hc = new HttpClient())
        //    {
        //        HttpResponseMessage response = await hc.GetAsync("https://btc-e.nz/api/3/info");
        //        string responseString = await response.Content.ReadAsStringAsync();
        //       // List<ICurrencyPair> allPairs = new List<ICurrencyPair>();

        //    }
        //    return null;
        //}
        public async Task<string> R()
        {
            using (HttpClient hc = new HttpClient())
            {
                HttpResponseMessage response = await hc.GetAsync("https://btc-e.nz/api/3/info");
                string responseString = await response.Content.ReadAsStringAsync();

                return responseString;
            }
            
        }
    }
}
