using System;
using Data;
using Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ServicesTests
{
    [TestClass]
    public class PurchaseEventTest 
    {
        private ClientService clientService = new ClientService();
        private ProductService productService = new ProductService();
        private PurchaseService purchaseService = new PurchaseService();

        [TestMethod]
        public void AddPurchase()
        {
            if (productService.GetAllProducts().Count > 0 && clientService.GetAllClients().Count > 0)
            {
                int clientId = clientService.GetLastlyAddedClient()._id;
                int productId = productService.GetLastlyAddedProduct()._id;

                PurchaseEvent e = new PurchaseEvent()
                {
                    ClientId = clientId,
                    ProductId = productId,
                    EventDate = DateTime.Now.ToLongDateString()
                };

                Assert.IsTrue(purchaseService.AddPurchaseEvent(e));
            }
        }

        [TestMethod]
        public void AddInvalidPurchase()
        {
            PurchaseEvent e = new PurchaseEvent()
            {
                ClientId = 0,
                ProductId = 0,
                EventDate = DateTime.Now.ToLongDateString()
            };

            Assert.IsFalse(purchaseService.AddPurchaseEvent(e));
            Assert.IsFalse(purchaseService.AddPurchaseEvent(null));
        }

        [TestMethod]
        public void FetchClientPurchases()
        {
            if (productService.GetAllProducts().Count > 0 && clientService.GetAllClients().Count > 0)
            {
                int clientId = clientService.GetLastlyAddedClient()._id;
                int productId = productService.GetLastlyAddedProduct()._id;

                PurchaseEvent e = new PurchaseEvent()
                {
                    ClientId = clientId,
                    ProductId = productId,
                    EventDate = DateTime.Now.ToLongDateString()
                };

                purchaseService.AddPurchaseEvent(e);

                foreach (var purchase in purchaseService.GetAllClientPurchases(clientId))
                {
                    Assert.AreEqual(purchase.ClientId, clientId);
                }

                PurchaseEvent last = purchaseService.GetMostRecentPurchase();

                Assert.AreEqual(last.ClientId, clientId);
                Assert.AreEqual(last.ProductId, productId);
            }
        }


        [TestMethod]
        public void FetchProductPurchases()
        {
            if (productService.GetAllProducts().Count > 0)
            {
                int productId = productService.GetLastlyAddedProduct()._id;

                foreach (var purchase in purchaseService.GetAllProductPurchases(productId))
                {
                    Assert.AreEqual(purchase.ProductId, productId);
                }
            }
        }

        [TestMethod]
        public void DeletePurchaseEvent()
        {
            int clientId = clientService.GetLastlyAddedClient()._id;
            int productId = productService.GetLastlyAddedProduct()._id;

            PurchaseEvent e = new PurchaseEvent()
            {
                ClientId = clientId,
                ProductId = productId,
                EventDate = DateTime.Now.ToLongDateString()
            };
            
            purchaseService.AddPurchaseEvent(e);

           PurchaseEvent last = purchaseService.GetLastClientPurchaseOfProduct(clientId, productId);

            if (last != null)
            {
                Assert.IsTrue(purchaseService.DeletePurchaseEvent(last.Id));

                foreach (var purchase in purchaseService.GetAllPurchases())
                {
                    Assert.AreNotEqual(purchase.Id, last.Id);
                }
            }
        }

        [TestMethod]
        public void DeleteNonExistingPurchase()
        {
            Assert.IsFalse(purchaseService.DeletePurchaseEvent(0));
        }
    }
}
