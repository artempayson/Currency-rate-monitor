using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Model.ExistingCurrencies
{
    static class CurrencyRepository
    {
        static public List<ICurrencyPair> ExistingCurrenciesList
        {
            get
            {
                var res = new List<ICurrencyPair>();
                CurrencyNames.existingCurrencyCodes.ForEach(code => res.Add(new CurrencyPair(code)));
                return res;
            }
        }
    }
}