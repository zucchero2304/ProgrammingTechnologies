using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem.Data
{
    class DataContext
    {
        public Dictionary<int, Product> products;
        public List<IEvent> events; 
        public List<State> states;
        public List<Client> clients;
    }
}
