using System;
using System.Collections.Generic;

namespace ShopSystem.Data
{
    public class Repository : IRepository
    {
        private DataContext dataContext;

        public Repository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }




        // --------------- Client -----------------  

        public void AddClient(Client client)
        {
            if (!NoSuchClientId(client.Id))
            {
                throw new Exception();
            }

            dataContext.clients.Add(client);
        }

        public void DeleteClient(Client client)
        {
            if (NoSuchClientId(client.Id))
            {
                throw new KeyNotFoundException();
            }

            dataContext.clients.Remove(client);
        }

        public Client GetClientById(int id)
        {
            if (NoSuchClientId(id))
            {
                throw new KeyNotFoundException();
            }
            return dataContext.clients.Find(client => client.Id == id);
        }

        public List<Client> GetAllClients()
        {
            return dataContext.clients;
        }

        public List<int> GetAllClientsIds() 
        {
            List<int> ids = new List<int>();

            foreach(Client client in dataContext.clients)
            {
                ids.Add(client.Id);
            }
            return ids;
        }

        public bool NoSuchClientId(int id)
        {
            return !dataContext.clients.Exists(c => c.Id == id);
        }




        // --------------- Product -----------------  

        public void AddProduct(Product product)
        {
            dataContext.products.Add(product.Id, product);
        }

        public void DeleteProduct(int id)
        {
            if (NoSuchProductId(id))
            {
                throw new KeyNotFoundException();
            }

            dataContext.products.Remove(id);
        }

        public Product GetProductById(int id)
        {
            if (NoSuchProductId(id))
            {
                throw new KeyNotFoundException();
            }

            return dataContext.products[id];
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return dataContext.products.Values;
        }

        public IEnumerable<int> GetAllProductIds()
        {
            return dataContext.products.Keys;
        }

        public bool NoSuchProductId(int id)
        {
            return !dataContext.products.ContainsKey(id);
        }

   



        // --------------- Event ---------------- 

        public void AddEvent(IEvent IEvent)
        {
            dataContext.events.Add(IEvent);
        }

        public void DeleteEvent(IEvent IEvent)
        {
            dataContext.events.Remove(IEvent);
        }

        public List<IEvent> GetAllEvents()
        {
            return dataContext.events;
        }




        // --------------- State ---------------  

        public void AddState(State state)
        {
            dataContext.states.Add(state);
        }

        public void DeleteState(State state)
        {
            if (NoSuchState(state))
            {
                throw new Exception();
            }

            dataContext.states.Remove(state);
        }

        public List<State> GetAllStates()
        {
            return dataContext.states;
        }


        public bool NoSuchState(State state)
        {
            return !dataContext.states.Exists(s => s.Equals(state));
        } 

    }
}
