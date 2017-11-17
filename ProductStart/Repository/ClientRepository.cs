using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClassLibrary1;

namespace ProductStart.Repository
{
    public class ClientRepository: IClientRepository
    {
        public readonly ClientTableSimulator Clients = ClientTableSimulator.Instance;

        public ClientRepository()
        {
            
        }

        public IEnumerable<Client> GetAll()
        {
            return Clients.ListAll();
        }

        public Client Get(int id)
        {
            return Clients.FindById(id);
        }

        public Client Get(string name)
        {
            return Clients.FindByName(name);
        }

        public Client Add(Client client)
        {
            if (client == null)
            {
                throw new ArgumentException("product cannot be null");
            }
            Clients.Add(client);
            return client;
        }

        public Client Update(int id, Client client)
        {
            client.Id = id;
            return Clients.Update(client);
        }

        public void Delete(int id)
        {
            Clients.Delelete(Clients.FindById(id));
        }
    }
}