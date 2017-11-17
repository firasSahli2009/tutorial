using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class ClietntDataSet: IDbSet<Client>
    {
        public List<Entity> Elements { get; }

        public ClietntDataSet()
        {
            Elements = new List<Entity>();

        }

        public Client Add(Client entity)
        {
            Elements.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            Elements.RemoveAll(p=>p.Id== id);
        }

        public Client Update(Client entity)
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

        public Client FindById(int Id)
        {
            var result = Elements.FirstOrDefault(p => p.Id == Id);
            return result as Client;
        }

       

    }
}
