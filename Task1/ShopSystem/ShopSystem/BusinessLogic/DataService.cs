using System;
using System.Collections.Generic;
using System.Text;
using ShopSystem.Data;

namespace ShopSystem.Logic
{
    class DataService
    {
        private IRepository repository;

        public DataService(IRepository repository) 
        {
            this.repository = repository;
        }




        // --------------- Product -----------------  


        public void AddProductToSystem(double price, Category category)
        {
            int id = GenerateId((List<int>)repository.GetAllProductIds());
            repository.AddProduct(new Product(id, price, category));
        }

        public void DeleteProductFromSystem(int id)
        {
            repository.DeleteProduct(id);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return repository.GetAllProducts(); 
        }

        public Product GetProductById(int id)
        {
            return repository.GetProductById(id);
        }





        // --------------- Client -----------------  

        public void AddClient(String name, String surname)
        {
            int id = GenerateId(repository.GetAllClientsIds());
            repository.AddClient(new Client(id, name, surname));
        }


        public void DeleteClientFromSystem(Client client)
        {
            repository.DeleteClient(client);
        }

        public List<Client> GetAllClients()
        {
            return repository.GetAllClients();
        }

        public Client GetClient(int id)
        {
            return repository.GetClientById(id);
        }


        public List<IEvent> GetAllClientEvents(int id)
        {
            List<IEvent> events = new List<IEvent>();
            foreach( IEvent ev in repository.GetAllEvents())
            {
                if (ev.ClientId.Equals(id))
                {
                    events.Add(ev);
                }
            }
            return events;
        }




        // --------------- Actions -----------------  

        public void PurchaseProduct(int productId, int clientId)
        {
            Client client = repository.GetClientById(clientId);
            Product product = repository.GetProductById(productId);
            
            // exception will be thrown if smth is wrong 

            repository.DeleteProduct(productId);

            State state = new State(product);
            int dummyId = 2;

            repository.AddEvent(new EventPurchase(state, clientId, dummyId));
            repository.AddState(state);
        }


        public void ReturnProduct(Product product, int clientId)
        {
            Client client = repository.GetClientById(clientId);

            // how to check nicely whether a product with such id exists? 
         
            repository.AddProduct(product);

            State state = new State(product);

            int dummyId = 2;
            repository.AddEvent(new EventReturn(state, clientId, dummyId));
            repository.AddState(state);
        }


        private int GenerateId(List<int> collection)
        {
            int id = 0;

            while (collection.Contains(id))
            {
                id++;
            }

            return id;
        }
    }
}
