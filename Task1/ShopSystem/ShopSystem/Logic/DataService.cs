using System;
using System.Collections.Generic;
using System.Text;
using ShopSystem.Data;

namespace ShopSystem.Logic
{
    public class DataService
    {
        private IRepository repository;

        public DataService(IRepository repository) 
        {
            this.repository = repository;
        }



        // --------------- Product -----------------  

        public void AddProduct(int id, double price, Category category)
        {
            repository.AddProduct(new Product(id, price, category));
        }

        public void DeleteProduct(int id)
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

        public List<IEvent> GetAllProductEvents(Product product)
        {
            List<IEvent> events = new List<IEvent>();

            foreach(IEvent e in repository.GetAllEvents())
            {
                if (e.State.Product.Equals(product))
                {
                    events.Add(e);
                }
            }
            return events;
        }





        // --------------- Client -----------------  

        public void AddClient(int id, String name, String surname)
        {
            //int id = GenerateId(repository.GetAllClientsIds());
            repository.AddClient(new Client(id, name, surname));
        }

        public void DeleteClient(Client client)
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
            Client client = repository.GetClientById(id);

            foreach(IEvent ev in repository.GetAllEvents())
            {
                if (ev.Client.Equals(client))
                {
                    events.Add(ev);
                }
            }
            return events;
        }





        // --------------- Actions -----------------  

        public void PurchaseProduct(int productId, int clientId)
        {
            Product product = repository.GetProductById(productId);
            Client client = repository.GetClientById(clientId);
            State state = new State(product);

            repository.DeleteProduct(productId);
            repository.AddEvent(new EventPurchase(state, client));
            repository.AddState(state);
        }


        public void ReturnProduct(Product product, int clientId)
        {
            Client client = repository.GetClientById(clientId);
            State state = new State(product);

            repository.AddProduct(product);
            repository.AddEvent(new EventReturn(state, client));
            repository.AddState(state);
        }

       
        /* private int GenerateId(List<int> collection)
        {
            int id = 0;

            while (collection.Contains(id))
            {
                id++;
            }
            return id;
        } */
    }
}
