using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem.Data
{
    interface IRepository
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
        List<int> GetAllEventIds();
        IEvent GetEventById(int id);
        void AddEvent(IEvent IEvent);
        void DeleteEvent(IEvent IEvent);



        List<State> GetAllStates();
        List<int> GetAllStateIds();
        State GetStateById(int id);
        void AddState(State state);
        void DeleteState(State state);
    }
}
