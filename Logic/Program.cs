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
        static  void Main(string[] args)
        {
            Repository r = new Repository();
            var beta = r.GetAllPairs();
            var a = beta.Result[15];
             r.UpdateSpecifiedPair(a);

        }

    }
}
