
using System.Collections.Generic;


namespace ClassLibrary1
{
    public class ClientTableSimulator
    {
        private static ClientTableSimulator _instance;
        private static ClietntDataSet clientTable;
        private int _nextId = 1;

        private static readonly object Padlock = new object();

        public static ClientTableSimulator Instance
        {
            get
            {
                lock (Padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new ClientTableSimulator();
                    }
                    return _instance;
                }
            }
        }

        private ClientTableSimulator()
        {
            clientTable = new ClietntDataSet();

            Add(new Client
            {
                Category = ClientCategory.Company,
                Address = "Cleint 1 address",
                Name = "client"
            });
            Add(new Client
            {
                Category = ClientCategory.Government,
                Address = "Toy 1 description",
                Name = "Toys1"
            });

            Add(new Client
            {
                Category = ClientCategory.Individual,
                Address = " address client 3 ",
                Name = "client3"
            });

        }

        public Client Add(Client client)
        {
            client.Id=_nextId++;
            return clientTable.Add(client);
            
        }

        public Client Update(Client client)
        {
            return clientTable.Update(client);
        }

        public void Delelete(Client client)
        {
            clientTable.Delete( client.Id);
        }

        public Client FindById(int id)
        {
            return clientTable.FindById(id);
        }

        public Client FindByName(string name)
        {
            return clientTable.FindByName(name);
        }

        public IEnumerable<Entity> ListAll()
        {
            return clientTable.Elements;
        }
    }
}