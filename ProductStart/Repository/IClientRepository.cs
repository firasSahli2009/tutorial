using System.Collections.Generic;
using ClassLibrary1;

namespace ProductStart.Repository
{
    public interface IClientRepository
    {

        IEnumerable<Entity> GetAll();
        Client Get(int id);
        Client Add(Client client);
        Client Update(int id, Client client);
        void Delete(int id);
    }
}