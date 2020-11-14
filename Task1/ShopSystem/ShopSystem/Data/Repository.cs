using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem.Data
{
    class Repository : IRepository
    {
        private DataContext dataContext;

        public Repository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }



        // --------------- Client -----------------  

        public void AddClient(Client client)
        {
            dataContext.clients.Add(client);
        }

        public void DeleteClient(Client client)
        {
            if (ContainsClientId(client.Id))
            {
                dataContext.clients.Remove(client);
            }
        }

        public Client GetClientById(int id)
        {
            if (ContainsClientId(id))
            {
                return dataContext.clients.Find(client => client.Id == id);
            }
            return default(Client);
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

         bool ContainsClientId(int id)
        {
            return dataContext.clients.Exists(c => c.Id == id);
        }



        // --------------- Product -----------------  


        public void AddProduct(Product product)
        {
            dataContext.products.Add(product.Id, product);
        }

        public void DeleteProduct(int id)
        {
            if (ContainsProductKey(id))
            {
                dataContext.products.Remove(id);
            }
        }


        public Product GetProductById(int id)
        {
            if (ContainsProductKey(id))
            {
                return dataContext.products[id];
            }
            return default(Product);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return dataContext.products.Values;
        }

        public IEnumerable<int> GetAllProductIds()
        {
            return dataContext.products.Keys;
        }

        public bool ContainsProductKey(int id)
        {
            return dataContext.products.ContainsKey(id);
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

        public List<int> GetAllEventIds()
        {
            return null;
        }

        public IEvent GetEventById(int id)
        {
            return dataContext.events.Find(e => e.Id == id);
        }




        // --------------- State ---------------  

        public void AddState(State state)
        {
            dataContext.states.Add(state);
        }

        public void DeleteState(State state)
        {
            dataContext.states.Remove(state);
        }

        public List<State> GetAllStates()
        {
            return dataContext.states;
        }

        public List<int> GetAllStateIds()
        {
            return null;
        }

        public State GetStateById(int id)
        {
            return dataContext.states.Find(state => state.Id == id);
        }
    }
}
