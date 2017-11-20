using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.ModelFactory
{
    public class ClientModel
    {
        public string SelfUrl { get; set; }

        public List<string> ProductsLinks { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

    }
}
