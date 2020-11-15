using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopSystem.Data;
using System.Collections.Generic;

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

        [TestMethod]
        public void AddProduct()
        {

        }


        [TestMethod] 
        public void RemoveProduct()
        {
           
        }




        [TestMethod] 
        public void AddClients()
        {
          
        }

        [TestMethod]
        public void RemoveClient()
        {
            // normal + non existing
        }

        [TestMethod]
        public void CheckClientEvents()
        {

        }
    }
}
