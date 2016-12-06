using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Model.ExistingCurrencies
{
    class CurrencyRepository
    {
        public List<ICurrencyPair> ExistingCurrenciesList { get; } = new List<ICurrencyPair>()
        {
            new CurrencyPair("btc_usd"),
            new CurrencyPair("btc_rur"),
            new CurrencyPair("btc_eur"),
            new CurrencyPair("ltc_btc"),
            new CurrencyPair("ltc_usd"),
            new CurrencyPair("ltc_rur"),
            new CurrencyPair("ltc_eur"),
            new CurrencyPair("nmc_btc"),
            new CurrencyPair("nmc_usd"),
            new CurrencyPair("nvc_btc"),
            new CurrencyPair("nvc_usd"),
            new CurrencyPair("usd_rur"),
            new CurrencyPair("eur_usd"),
            new CurrencyPair("eur_rur"),
            new CurrencyPair("ppc_btc"),
            new CurrencyPair("ppc_usd"),
            new CurrencyPair("dsh_btc"),
            new CurrencyPair("dsh_usd"),
            new CurrencyPair("eth_btc"),
            new CurrencyPair("eth_usd"),
            new CurrencyPair("eth_ltc"),
            new CurrencyPair("eth_rur")
        };
    }
}
