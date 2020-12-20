using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Service;
using Data;

namespace ServicesTests
{
    [TestClass]
    public class ReturnServiceTest
    {
        private ClientService clientService = new ClientService();
        private ProductService productService = new ProductService();
        private PurchaseService purchaseService = new PurchaseService();
        private ReturnService returnService = new ReturnService();


        [TestMethod]
        public void AddReturn()
        {
            int clientId = clientService.GetLastlyAddedClient()._id;

            List<PurchaseEvent> purchases = purchaseService.GetAllClientPurchases(clientId);

            if (purchases.Count > 0)
            {
                PurchaseEvent purchaseEvent = purchases[purchases.Count - 1];

                ReturnEvent returnEvent = new ReturnEvent()
                {
                    ClientId = purchaseEvent.ClientId,
                    ProductId = purchaseEvent.ProductId,
                    EventDate = DateTime.Now.ToLongDateString()
                };

                Assert.IsTrue(returnService.AddReturnEvent(returnEvent));

                foreach (var purchase in purchaseService.GetAllClientPurchases(clientId))
                {
                    Assert.AreNotEqual(purchase.Id, purchaseEvent.Id);
                }
            }
        }

        [TestMethod]
        public void AddInvalidReturn()
        {
            ReturnEvent e = new ReturnEvent()
            {
                ClientId = 0,
                ProductId = 0,
                EventDate = DateTime.Now.ToLongDateString()
            };

            Assert.IsFalse(returnService.AddReturnEvent(e));
            Assert.IsFalse(returnService.AddReturnEvent(null));
        }

        [TestMethod]
        public void FetchClientReturns()
        {
            int id = clientService.GetLastlyAddedClient()._id;

            foreach (var returnEvent in returnService.GetAllClientReturns(id))
            {
                Assert.AreEqual(returnEvent.ClientId, id);
            }
        }

        [TestMethod]
        public void FetchNonExistingClientReturns()
        {
            Assert.AreEqual(returnService.GetAllClientReturns(0).Count, 0);
        }

        [TestMethod]
        public void FetchProductReturns()
        {
            int id = productService.GetLastlyAddedProduct()._id;

            foreach (var returnEvent in returnService.GetAllProductReturns(id))
            {
                Assert.AreEqual(returnEvent.ProductId, id);
            }
        }

        [TestMethod]
        public void FetchNonExistingProductReturns()
        {
            Assert.AreEqual(returnService.GetAllProductReturns(0).Count, 0);
        }
    }
}
