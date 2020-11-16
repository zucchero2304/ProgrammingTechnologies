using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopSystemTest
{
    [TestClass]
    public class RepositoryTest
    {
        private Repository repository;

        public RepositoryTest()
        {
            ContentGenerator generator = new ContentGenerator();
            repository = new Repository(generator.GenerateContent());
        }


        
        [TestMethod]
        public void CheckInitialState()
        {
            Assert.IsTrue(repository.GetAllClients().Count.Equals(2));
            Assert.IsTrue(repository.GetAllEvents().Count.Equals(2));
            Assert.IsTrue(repository.GetAllStates().Count.Equals(2));
        }

        //ClientTests:

        [TestMethod]
        public void AddClients()
        {
            Client client = new Client(6, "John", "Watson");
            repository.AddClient(client);
            Assert.IsTrue(repository.GetClientById(6).Equals(client));
        }

        [TestMethod]
        public void RemoveClient()
        {   // existing
            Client client1 = repository.GetClientById(1);
            repository.DeleteClient(client1);
            Assert.ThrowsException<KeyNotFoundException>(
                () => repository.GetClientById(1));
            // non existing
            Client client2 = new Client(3, "K", "M");
            Assert.ThrowsException<KeyNotFoundException>(
                () => repository.DeleteClient(client2));
        }

        [TestMethod]
        public void NoSuchClientId()
        {
            Assert.IsTrue(repository.NoSuchClientId(5));
            Assert.IsFalse(repository.NoSuchClientId(2));
        }

        [TestMethod]
        public void GetAllClientsIds()
        {
            List<int> idList = repository.GetAllClients().Select(c => c.Id).ToList();
            CollectionAssert.AreEqual(repository.GetAllClientsIds(), idList);
        }

        //ProductTests:

        [TestMethod]
        public void AddProduct()
        {
            Product sofa = new Product(10, 25, Category.furniture);
            repository.AddProduct(sofa);
            Assert.IsTrue(repository.GetProductById(10).Equals(sofa));
        }


        [TestMethod] 
        public void RemoveProduct()
        {
            repository.DeleteProduct(2);
            Assert.ThrowsException<KeyNotFoundException>(
                () => repository.GetProductById(2));
        }


        [TestMethod]
        public void NoSuchProductId()
        {
            Assert.IsTrue(repository.NoSuchProductId(11));
            Assert.IsFalse(repository.NoSuchProductId(1));
        }

        [TestMethod]
        public void GetAllProducts()
        {
            List<int> idListFromProducts = repository.GetAllProducts().Select(p => p.Id).ToList();
            List<int> idList = repository.GetAllProductIds().ToList();
            CollectionAssert.AreEqual(idListFromProducts, idList);
        }

        //EventTests:

        [TestMethod]
        public void CheckClientEvents()
        {   //is it ok to check all 3 methods in one test like that?
            Client client = new Client(9, "Sherlock", "Holmes");
            Product product = new Product(15, 90, Category.books);
            State state = new State(product);
            EventPurchase eventPurchase = new EventPurchase(state, client);
            repository.AddEvent(eventPurchase);
            Assert.IsTrue(repository.GetAllEvents().Contains(eventPurchase));
            repository.DeleteEvent(eventPurchase);
            Assert.IsFalse(repository.GetAllEvents().Contains(eventPurchase));
        }

        //StateTests

        [TestMethod]
        public void CheckStates()
        {
            Product product = new Product(11, 30, Category.electronics);
            State state = new State(product);
            //delete before it's added
            Assert.ThrowsException<Exception>(
               () => repository.DeleteState(state));
            Assert.IsTrue(repository.NoSuchState(state));
            //add
            repository.AddState(state);
            Assert.IsTrue(repository.GetAllStates().Contains(state));
            //delete after its added
            repository.DeleteState(state);
            Assert.IsFalse(repository.GetAllStates().Contains(state));
        }       

       [TestMethod] 
        public void RandomContent()
        {
            RandomContentGenerator random = new RandomContentGenerator();
            repository = new Repository(random.GenerateContent());

            Assert.IsTrue(IsUnique(repository.GetAllClientsIds()));
            Assert.IsTrue(IsUnique(repository.GetAllProductIds()));
        }

        private bool IsUnique(IEnumerable<int> list)
        {
            HashSet<int> uniqueValues = new HashSet<int>();

            foreach (int id in list)
            {
                if (!uniqueValues.Add(id))
                {
                    return false;
                }
            }
            return true;
        } 
    }
}
