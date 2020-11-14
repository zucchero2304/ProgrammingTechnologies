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

        public IEnumerable<Product> GetAllProducts()
        {
            return repository.GetAllProducts(); 
        }

        public Product GetProductById(int id)
        {
            return repository.GetProductById(id);
        }

        public void AddProductToSystem(double price)
        {
            int dummyId = 0;
            repository.AddProduct(new Product(dummyId, price));
        }

        public void DeleteProductFromSystem(int id)
        {
            repository.DeleteProduct(id);
        }






        // --------------- Client -----------------  

        public List<Client> GetAllClients()
        {
            return repository.GetAllClients();
        }

        public Client GetClient(int id)
        {
            return repository.GetClientById(id);
        }


        public void AddClient(String name, String surname)
        {
            int id = GenerateId(repository.GetAllClientsIds());
            repository.AddClient(new Client(id, name, surname));
        }
        

        public void DeleteClientFromSystem(int id)
        {
            // id must exist
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

        public void PurchaiseProduct(int productId, int clientId)
        {
            // both client and product must be in the system
            
            repository.DeleteProduct(productId);

            State state = new State(GetAllProducts());
            int dummyId = 2;

            repository.AddEvent(new EventPurchaise(state, clientId, dummyId));
            repository.AddState(state);
        }


        public void ReturnProduct(Product product, int clientId)
        {       
            // client must be in the system 
            // product cannot be in the system 

             repository.AddProduct(product);
             State state = new State(GetAllProducts());

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


        public void GenerateReportForClient(int id)
        {
            // print all client events information

        }
    }
}
