using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem.Data
{
    class ContentGenerator : IContentGenerator
    {
        public void GenerateContent(DataContext context)
        {

            // add clients 

            Client client1 = new Client(1, "L", "P");
            Client client2 = new Client(2, "R", "P");

            context.clients.Add(client1);
            context.clients.Add(client2);

            // add items 
            Product product1 = new Product(1, 20, Category.books);
            Product product2 = new Product(2, 30, Category.drugs);
            Product product3 = new Product(3, 40, Category.electronics);

            context.products.Add(1, product1);
            context.products.Add(2, product2);
            context.products.Add(3, product3);

            // add events and states 

            State state1 = new State(product1);
            State state2 = new State(product2);

            EventPurchase eventPurchase1 = new EventPurchase(state1, client1.Id, 1);
            EventPurchase eventPurchase2 = new EventPurchase(state2, client2.Id, 2);

            context.states.Add(state1);
            context.states.Add(state2);

            context.events.Add(eventPurchase1);
            context.events.Add(eventPurchase2);
        }
    }
}
