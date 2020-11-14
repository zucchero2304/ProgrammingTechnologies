using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem.Data
{
    class EventPurchaise : IEvent 
    {
        public EventPurchaise(State state, int clientId, int id) 
            : base(state, clientId, id) { }
    }
}
