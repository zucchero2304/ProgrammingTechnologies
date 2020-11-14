using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem.Data
{
    class EventPurchase : IEvent 
    {
        public EventPurchase(State state, int clientId, int id) 
            : base(state, clientId, id) { }
    }
}
