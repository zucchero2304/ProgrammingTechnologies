using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem.Data
{
    public class EventReturn : IEvent
    {
        public EventReturn(State state, Client client) 
            : base(state, client) { }
    }
}

