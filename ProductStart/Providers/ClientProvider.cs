using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClassLibrary1;
using ProductStart.Repository;

namespace ProductStart.Providers
{
    public class ClientProvider: IClientProvider
    {
        private readonly IClientRepository _clientRepository;


        public ClientProvider(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public IEnumerable<Entity> GetAll()
        {
            return _clientRepository.GetAll();
        }

        public Client Get(int id)
        {
            return _clientRepository.Get(id);
        }

        public Client Add(Client client)
        {
            return _clientRepository.Add(client);
        }

        public Client Update(int id, Client client)
        {
            return _clientRepository.Update(id, client);
        }

        public void Delete(int id)
        {
            _clientRepository.Delete(id);
        }
    }
}