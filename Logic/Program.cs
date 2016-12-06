using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Logic
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository r = new Repository();
            var a = r.R().Result;
            var j = JsonConvert.DeserializeObject(a);
            var t = j as IEnumerable<IEnumerable<IEnumerable<object>>>;
            var b = t.Last().First();
           
            List<string> names = new List<string>();
            foreach (var item in b)
            {
                names.Add(item.ToString().Split('"')[1]);
            }
           
            
            
        }
    }
}
