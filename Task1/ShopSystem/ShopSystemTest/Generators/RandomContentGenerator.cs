using System;
using System.Collections.Generic;
using System.Text;
using ShopSystem.Data;

using System.Linq;

namespace ShopSystemTest
{
    class RandomContentGenerator : IContentGenerator
    {
        private DataContext context;

        public RandomContentGenerator()
        {
            context = new DataContext();
        }

        public DataContext GenerateContent()
        {
            int NumberOfClients = 10;
            int NumberOfProducts = 20;
            int NumberOfEvents = 5;

            List<int> clientIds = RandomIntList(NumberOfClients);
            List<int> productIds = RandomIntList(NumberOfProducts);
           
            List<Category> categories = new List<Category> {
                Category.books,
                Category.drugs,
                Category.electronics,
                Category.food,
                Category.furniture,
                Category.games,
                Category.miscellaneous
            };

            for (int i = 0; i < NumberOfClients; i++)
            {
                context.clients.Add(new Client(clientIds[i], RandomString(5), RandomString(5)));
            }

            for (int i = 0; i < NumberOfProducts; i++)
            {
                int id = productIds[i];
                Category category = categories[RandomInt() % categories.Count];
                context.products.Add(id, new Product(id, RandomInt(), category));
            }

            for (int i = 0; i < NumberOfEvents; i++)
            {
                Product product = context.products[
                    productIds[RandomInt() % productIds.Count]];

                Client client = context.clients
                    .Find(c => c.Id == RandomInt() % clientIds.Count);

                context.events.Add(new EventPurchase(new State(product), client));
            }

            for (int i = 0; i < NumberOfEvents; i++)
            {
                Product product = context.products[
                    productIds[RandomInt() % productIds.Count]];

                Client client = context.clients
                    .Find(c => c.Id == RandomInt() % clientIds.Count);

                context.events.Add(new EventReturn(new State(product), client));
            }

            return context;
        }

        public string RandomString(int length)
        {
            const string chars = "abcdefghijklmnropqrstuvwxyz";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public List<string> RandomStringList(int howMany, int length)
        {
            List<string> strings = new List<string>();
            string str = RandomString(length);

            for (int i = 0; i < howMany - 1; i++)
            {
                strings.Add(RandomString(length));
            }
            return strings;
        }


        public int RandomInt()
        {
            var random = new Random();
            int number = random.Next();
            return number;
        }

        List<int> RandomIntList(int howMany)
        {
            List<int> numbers = new List<int>();
            int number = RandomInt();
            numbers.Add(number);

            for (int i = 0; i < howMany - 1; i++)
            {
                while (numbers.Contains(number))
                {
                    number = RandomInt();
                }
                numbers.Add(number);
            }
            return numbers;
        }
    }
}

