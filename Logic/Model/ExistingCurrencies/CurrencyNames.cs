using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Model.ExistingCurrencies
{
    class CurrencyNames
    {
        public static Dictionary<string, string> existingCurrencyNames = new Dictionary<string, string>
        {
            {"btc","Bitcoin"},
            {"ltc","Litecoin" },
            {"rur","Russian Ruble" },
            {"usd","United States Dollar" },
            {"eur","European Union Euro" },
            {"nmc","Namecoin" },
            {"nvc","Novacoin" },
            {"ppc","Peercoin" },
            {"dsh","Dashcoin" },
            {"eth","Etherium" }

        };
        public static List<string> existingCurrencyCodes { get; } = new List<string>
{
"btc_usd",
"btc_rur",
"btc_eur",
"ltc_btc",
"ltc_usd",
"ltc_rur",
"ltc_eur",
"nmc_btc",
"nmc_usd",
"nvc_btc",
"nvc_usd",
"usd_rur",
"eur_usd",
"eur_rur",
"ppc_btc",
"ppc_usd",
"dsh_btc",
"dsh_usd",
"eth_btc",
"eth_usd",
"eth_eur",
"eth_ltc",
"eth_rur"
        };
    }
}
