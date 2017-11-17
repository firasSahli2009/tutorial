using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class ProductDataSet: IDbSet<Product>
    {
        public List<Product> Elements { get; }

        

        public ProductDataSet()
        {
            Elements = new List<Product>();
        }

        public Product FindById(int id)
        {
            var result = Elements.FirstOrDefault(p => p.Id == id);
            return result;
        }

        public Product Add(Product entity)
        {
            Elements.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            Elements.RemoveAll(p=>p.Id== id);
        }

        public Product Update(Product entity)
        {
            var index = Elements.FindIndex(p => p.Id == entity.Id);
            if (index==-1)
            {
                return null;
            }
            Elements.RemoveAt(index);
            Elements.Add(entity);
            return entity;
        }

        

        
    }
}
