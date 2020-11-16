using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem.Data
{
    public class EventPurchase : IEvent 
    {
        public EventPurchase(State state, Client client) 
            : base(state, client) { }
    }
}
