using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem
{

    // Repository's main duty is to communicate with a database.
    // Here we define basic CRUD (create, remove, update, delete) methods 
    // that represent business logic functionality


    interface IRepository
    {
        // Products 
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        void AddProduct(Product product);
        void DeleteProduct(int id);


        // Clients 
        List<Client> GetAllClients();
        Client GetClientById(string id);
        void AddClient(Client client);
        void DeleteClient(string id);


        // Events 
        List<Event> GetAllEvents();
        Event GetEventById(int id);
        //void DeleteEvent(string id);
        // void AddEvent(ShopEvent event);
    }
}
