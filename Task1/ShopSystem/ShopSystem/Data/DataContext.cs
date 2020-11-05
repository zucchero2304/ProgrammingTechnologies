using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem
{
    // Data Context is generally a shop abstraction

    class DataContext
    {
        Dictionary<int, Product> products;
        List<Event> events; 
        List<State> states;
        List<Client> clients;
    }
}
