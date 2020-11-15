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

        bool NoSuchClientId(int id)
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
            if (NoSuchEventId(IEvent.Id))
            {
                throw new KeyNotFoundException();
            }

            dataContext.events.Remove(IEvent);
        }

        public List<IEvent> GetAllEvents()
        {
            return dataContext.events;
        }

        public List<int> GetAllEventIds()
        {
            List<int> ids = new List<int>();
            foreach (IEvent IEvent in dataContext.events)
            {
                ids.Add(IEvent.Id);
            }
            return ids;
        }

        public IEvent GetEventById(int id)
        {
            if (NoSuchEventId(id))
            {
                throw new KeyNotFoundException();
            }

            return dataContext.events.Find(e => e.Id == id);
        }

        bool NoSuchEventId(int id)
        {
            return !dataContext.events.Exists(e => e.Id == id);
        }





        // --------------- State ---------------  


        public void AddState(State state)
        {
            dataContext.states.Add(state);
        }

        public void DeleteState(State state)
        {
            if (NoSuchStateId(state.Id))
            {
                throw new KeyNotFoundException();
            }

            dataContext.states.Remove(state);
        }

        public List<State> GetAllStates()
        {
            return dataContext.states;
        }

        
        public List<int> GetAllStateIds()
        {
            List<int> ids = new List<int>();
           
            foreach (State state in dataContext.states)
            {
                ids.Add(state.Id);
            }
            return ids;
        }

        public State GetStateById(int id)
        {
            if (NoSuchStateId(id))
            {
                throw new KeyNotFoundException();
            }
            return dataContext.states.Find(state => state.Id == id);
        }

        bool NoSuchStateId(int id)
        {
            return !dataContext.states.Exists(s => s.Id == id);
        } 

    }
}
