using System;
using System.Collections.Generic;
using System.Linq;
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
            int lastClientId = clientRepository.GetLastClient().Id;

            int lastProductId = productRepository.GetLastProduct().Id;

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
            int actualCount = eventRepository.GetAllPurchaseEvents().Count;

            PurchaseEvent addedEvent = eventRepository.GetPurchaseEventById(e.Id);

            Assert.AreEqual(addedEvent.Id, e.Id);
            Assert.AreEqual(addedEvent.ClientId, e.ClientId);
            Assert.AreEqual(addedEvent.ProductId, e.ProductId);

            Assert.AreEqual(initialCount, actualCount - 1);
        }

        [TestMethod]
        public void GetPurchaseEventsByClientId()
        {

            int lastClientId = clientRepository.GetLastClient().Id;

            List<PurchaseEvent> evs = eventRepository.GetPurchaseEventsByClientId(lastClientId);

            if (evs.Count > 0)
            {
                foreach (var e in evs)
                {
                    Assert.AreEqual(e.ClientId, lastClientId);
                }
            }
        }

        [TestMethod]
        public void GetPurchaseEventsNonExistingClientId()
        {
            List<PurchaseEvent> evs = eventRepository.GetPurchaseEventsByClientId(0);
            Assert.AreEqual(evs.Count, 0);
        }

        [TestMethod]
        public void GetPurchaseEventsByProductId()
        {
            PurchaseEvent ev = GetEvent();
            Assert.IsTrue(eventRepository.GetPurchaseEventsByProductId(ev.ProductId).Count >= 1);
        }

        [TestMethod]
        public void GetPurchaseEventsNonExistingProduct()
        {
            List<PurchaseEvent> evs = eventRepository.GetPurchaseEventsByProductId(0);
            Assert.AreEqual(evs.Count, 0);
        }

        [TestMethod]
        public void GetMostRecentPurchaseEvent()
        {
            List<PurchaseEvent> evs = eventRepository.GetAllPurchaseEvents();

            PurchaseEvent recent = eventRepository.GetMostRecentPurchase();
            
            Assert.AreEqual(evs.Last().Id, recent.Id);
        }
    }
}
