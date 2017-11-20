using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.ModelFactory
{
    public class ProductModel
    {
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string SelfUrl { get; set; }

    }
}
