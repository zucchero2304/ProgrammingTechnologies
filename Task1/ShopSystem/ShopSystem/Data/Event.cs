using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem
{
    // Event has an id (to identify it within the database),
    // TimeStamp (to indicate when it happened),
    // State,
    // Client 


    // we can create two Event types: ProductPurchase, ProductDelivery

    abstract class Event
    {
        private String id;
        private DateTime dateTime;
        private State state;
        private Client client;

        public Event(String id, State state, Client client)
        {
            this.id = id;
            this.state = state;
            this.client = client;
            dateTime = DateTime.Now;
        }
    }
}
