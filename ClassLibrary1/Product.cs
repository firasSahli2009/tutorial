using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ClassLibrary1
{
    public class Product: Entity, IProduct
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductCategory Category { get; set; }
    }
}