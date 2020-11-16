using System;
using System.Collections.Generic;

namespace ShopSystem.Data
{
    public class DataContext
    {
        public Dictionary<int, Product> products = new Dictionary<int, Product>();
        public List<IEvent> events = new List<IEvent>();
        public List<State> states = new List<State>();
        public List<Client> clients = new List<Client>();
    }
}
