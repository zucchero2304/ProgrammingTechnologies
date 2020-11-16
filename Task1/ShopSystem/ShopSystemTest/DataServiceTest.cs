using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopSystem.Logic;
using ShopSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ShopSystemTest
{
    [TestClass]
    public class DataServiceTest
    {
        DataService service;

        public DataServiceTest()
        {
            ContentGenerator generator = new ContentGenerator();
            service = new DataService(new Repository(generator.GenerateContent()));
        }

        //ClientTests

        [TestMethod]
        public void AddClient()
        {
            service.AddClient(3, "DummyName", "DummySurname");
            Assert.AreEqual(service.GetAllClients().Count, 3);
        }

        [TestMethod]
        public void AddClientRepeatedId()
        {
            Assert.ThrowsException<Exception>(
                () => service.AddClient(2, "DummyName", "DummySurname"));
        }

        [TestMethod]
        public void RemoveClient()
        {
            Client existentClient = service.GetClient(1);
            service.DeleteClient(existentClient);

            Assert.AreEqual(service.GetAllClients().Count, 1); //1, because at the beginning we had 2 clients
        }

        [TestMethod]
        public void RemoveNonexistentClient()
        {
            Client nonexistentClient = new Client(999, "DummyName", "DummySurname");
            Assert.ThrowsException<KeyNotFoundException>(() =>
               service.DeleteClient(nonexistentClient));

            Assert.AreEqual(service.GetAllClients().Count, 2);
        }

        //ProductTests

        [TestMethod]
        public void AddProduct()
        {
            int id = 10;
            int price = 40;
            Category cat = Category.books;
            service.AddProduct(id, price, cat);
            Product p = service.GetProductById(10);
            Assert.IsTrue(p.Id == id && p.Price == price && p.Category == cat);
        }

        [TestMethod]
        public void GetAllProducts()
        {   //initially 3 products
            List<int> initialProductIdList = service.GetAllProducts().Select(p => p.Id).ToList();
            Assert.IsTrue(initialProductIdList.Count.Equals(3));
            service.AddProduct(6, 40, Category.drugs);
            List<int> currentProductIdList = service.GetAllProducts().Select(p => p.Id).ToList();
            Assert.IsTrue(currentProductIdList.Count.Equals(4));
        }

        [TestMethod]
        public void DeleteNonExistingProduct()
        {
            Assert.ThrowsException<KeyNotFoundException>(
                () => service.DeleteProduct(10));
        }

        [TestMethod]
        public void DeleteExistingProduct()
        {
            service.DeleteProduct(2);
            Assert.ThrowsException<KeyNotFoundException>(
                () => service.GetProductById(2));

        }

        [TestMethod]
        public void GetProductEvents()
        {
            Product p = service.GetProductById(1); //contained in state1 -> eventPurchase1
            List<IEvent> productEvents = service.GetAllProductEvents(p);
            Assert.IsTrue(productEvents[0].State.Product.Equals(p)); //eventPurchase1 is 1st event on the list
        }

        [TestMethod]
        public void GetClientEvents()
        {
            Client c = service.GetClient(1); //client with id = 1, contained in state1 -> eventPurchase1
            List<IEvent> productEvents = service.GetAllClientEvents(1);
            Assert.IsTrue(productEvents[0].Client.Equals(c));
        }

        //ActionTests

        [TestMethod]
        public void MakingPurchase()
        {

        }

        [TestMethod]
        public void MakingReturn()
        {

        }

    }
}
