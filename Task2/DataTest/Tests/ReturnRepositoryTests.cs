using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataTest.Tests
{
    [TestClass]
    public class ReturnRepositoryTests
    {
        private ClientRepository clientRepository = new ClientRepository();
        private ProductRepository productRepository = new ProductRepository();
        private ReturnEventRepository returnRepository = new ReturnEventRepository();

        [TestMethod]
        public void AllEventClientsAreValid()
        {
            foreach (var e in returnRepository.GetAllReturnEvents())
            {
                Assert.IsNotNull(clientRepository.GetClientById(e.ClientId));
            }
        }

        [TestMethod]
        public void AllEventProductsAreValid()
        {
            foreach (var e in returnRepository.GetAllReturnEvents())
            {
                Assert.IsNotNull(productRepository.GetProductById(e.ProductId));
            }
        }

        [TestMethod]
        public void GetReturnEventsByClientId()
        {
            int clientId = clientRepository.GetLastClient().Id;

            List<ReturnEvent> evs = returnRepository.GetReturnEventsByClientId(clientId);

            if (evs.Count > 0)
            {
                foreach (var e in evs)
                {
                    Assert.AreEqual(e.ClientId, clientId);
                }
            }
        }

        [TestMethod]
        public void GetPurchaseEventsNonExistingClientId()
        {
            List<ReturnEvent> evs = returnRepository.GetReturnEventsByClientId(0);
            Assert.AreEqual(evs.Count, 0);
        }

        [TestMethod]
        public void GetReturnEventsByProductId()
        {
            int productId = productRepository.GetLastProduct().Id;

            List<ReturnEvent> evs = returnRepository.GetReturnEventsByProductId(productId);

            foreach (var e in evs)
            {
                Assert.AreEqual(e.ProductId, productId);
            }
        }

        [TestMethod]
        public void GetMostRecentReturnEvent()
        {
            List<ReturnEvent> evs = returnRepository.GetAllReturnEvents();

            ReturnEvent recent = returnRepository.GetMostRecentReturn();

            Assert.AreEqual(evs.Last().Id, recent.Id);
        }
    }
}
