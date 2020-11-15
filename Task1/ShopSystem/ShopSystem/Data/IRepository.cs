using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem.Data
{
    public interface IRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<int> GetAllProductIds();
        Product GetProductById(int id);
        void AddProduct(Product product);
        void DeleteProduct(int id);



        List<Client> GetAllClients();
        List<int> GetAllClientsIds();
        Client GetClientById(int id);
        void AddClient(Client client);
        void DeleteClient(Client client);



        List<IEvent> GetAllEvents();
        void AddEvent(IEvent IEvent);
        void DeleteEvent(IEvent IEvent);



        List<State> GetAllStates();
        void AddState(State state);
        void DeleteState(State state);
    }
}
