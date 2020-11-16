using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopSystem.Logic;
using ShopSystem.Data;
using System;
using System. Collections.Generic;


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
            Client existingClient = service.GetClient(1);
            service.DeleteClient(existingClient);
           
            Assert.AreEqual(service.GetAllClients().Count, 1); //1, because at the beginning we had 2 clients
        }

        [TestMethod] 

        public void RemoveUnexistingClient()
        {
            Client unexistingClient = new Client(999, "DummyName", "DummySurname");
           
            Assert.ThrowsException<KeyNotFoundException>( () =>
                service.DeleteClient(unexistingClient));

            Assert.AreEqual(service.GetAllClients().Count, 2);
        }
    }
}
