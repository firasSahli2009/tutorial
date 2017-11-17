using System.Collections.Generic;
using ClassLibrary1;

namespace ProductStart.Providers
{
    public interface IClientProvider
    {
        IEnumerable<Client> GetAll();
        Client Get(int id);

        Client Get(string nmae);
        Client Add(Client client);
        Client Update(int id, Client client);
        void Delete(int id);
    }
}