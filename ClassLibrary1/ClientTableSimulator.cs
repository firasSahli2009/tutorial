
using System.Collections.Generic;
using ClassLibrary1.Factories;

namespace ClassLibrary1
{
    public class ClientTableSimulator
    {
        private static ClientTableSimulator _instance;
        private static ClietntDataSet _clientTable;
        private int _nextId = 1;

        private ProductTableSimulator productTableSimulator= ProductTableSimulator.Instance;

        private static readonly object Padlock = new object();

        public static ClientTableSimulator Instance
        {
            get
            {
                lock (Padlock)
                {
                    return _instance ?? (_instance = new ClientTableSimulator());
                }
            }
        }

        private ClientTableSimulator()
        {
            _clientTable = new ClietntDataSet();

            var product1 = productTableSimulator.FindById(1);
            var product2 = productTableSimulator.FindById(2);
            var product3 = productTableSimulator.FindById(3);

            Add(new Client
            {
                Category = ClientCategory.Company,
                Address = "Cleint 1 address",
                Name = "client",
                Products = new List<Product>
                {
                    (product1!=null)?productTableSimulator.FindById(1):ProductFactory.CreateProduct(1),
                    (product2!=null)?productTableSimulator.FindById(2):ProductFactory.CreateProduct(2)
                }
            });
            Add(new Client
            {
                Category = ClientCategory.Government,
                Address = "Toy 1 description",
                Name = "other client",
                Products = new List<Product>
                {
                    (product1!=null)?productTableSimulator.FindById(1):ProductFactory.CreateProduct(1),
                    (product2!=null)?productTableSimulator.FindById(2):ProductFactory.CreateProduct(2),
                   (product3!=null)?productTableSimulator.FindById(3):ProductFactory.CreateProduct(3)
                }
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
            return _clientTable.Add(client);
            
        }

        public Client Update(Client client)
        {
            return _clientTable.Update(client);
        }

        public void Delelete(Client client)
        {
            _clientTable.Delete( client.Id);
        }

        public Client FindById(int id)
        {
            return _clientTable.FindById(id);
        }

        public Client FindByName(string name)
        {
            return _clientTable.FindByName(name);
        }

        public IEnumerable<Client> ListAll()
        {
            return _clientTable.Elements;
        }
    }
}