using System;
using System.Collections.Generic;
using ClassLibrary1;

namespace ProductStart.Repository
{
    public interface IClientRepository
    {

        IEnumerable<Client> GetAll();
        Client Get(int id);

        Client Get(string name);
        Client Add(Client client);
        Client Update(int id, Client client);
        void Delete(int id);
    }
}