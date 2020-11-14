using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem.Data
{
    class EventReturn : IEvent
    {
        public EventReturn(State state, int clientId, int id)
            : base(state, clientId, id) { }
    }
}

