using System;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataTest
{
    [TestClass]
    public class EventRepositoryTests
    {
        private ClientRepository clientRepository = new ClientRepository();
        private ProductRepository productRepository = new ProductRepository();
        private PurchaseEventRepository eventRepository = new PurchaseEventRepository();

        private PurchaseEvent GetEvent()
        {
            int lastClientId = clientRepository.GetAllClients()[
                clientRepository.GetNumberOfClients() - 1].Id;

            int lastProductId = productRepository.GetAllProducts()[
                productRepository.GetNumberOfProducts() - 1].Id;

            return new PurchaseEvent()
            {
                ClientId = lastClientId,
                ProductId = lastProductId,
                EventDate = DateTime.Now.ToString()
            };
        }

        [TestMethod]
        public void AddPurchaseEvent()
        {
            PurchaseEvent e = GetEvent();

            int initialCount = eventRepository.GetAllPurchaseEvents().Count;
            eventRepository.AddPurchaseEvent(e);
            int resultCount = eventRepository.GetAllPurchaseEvents().Count;

            Assert.AreEqual(initialCount, resultCount - 1);
        }

        [TestMethod]
        public void GetPurchaseEventsByClientId()
        {

        }

        [TestMethod]
        public void GetPurchaseEventsByProductId()
        {

        }

        [TestMethod]
        public void UpdatePurchaseEvent()
        {

        }
    }
}
