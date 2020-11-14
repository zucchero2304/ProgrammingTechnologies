using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem.Data
{
     abstract class IEvent
    {
        private int id;
        private State state;
        private int client;
        private DateTime happenedOn;

        public IEvent(State _state, int _client, int _id)
        {
            id = _id;
            state = _state;
            client = _client;
            happenedOn = DateTime.Now;
        }

        public int Id => id;
        public State State => state;
        public int ClientId => client;
        public DateTime HappenedOn => happenedOn;
    }
}
