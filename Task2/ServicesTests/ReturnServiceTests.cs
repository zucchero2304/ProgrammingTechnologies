using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Service;
using Data;

namespace ServicesTests
{
    [TestClass]
    public class ReturnServiceTests
    {
        ReturnService returnService = new ReturnService();
        PurchaseService purchaseService = new PurchaseService();

        ClientService clientService = new ClientService();
        ProductService productService = new ProductService();

        [TestMethod]
        public void TestMethod1()
        {
            ClientModel client = new ClientModel()
            {
                _firstName = "TestService",
                _lastName = "TestService"
            };

            ProductModel product = new ProductModel()
            {
                _productName = "TestName",
                _price = 15.0f,
                _category = new Product() { Category = "Game" }.Category,
            };

            clientService.AddClient(client);
            productService.AddProduct(product);

            ClientModel fetchedClient = clientService.GetLastlyAddedClient();
            ProductModel fetchedProduct = productService.GetLastlyAddedProduct();

            PurchaseEvent purchaseEvent = new PurchaseEvent()
            {
                ClientId = fetchedClient._id,
                ProductId = fetchedProduct._id,
                EventDate = System.DateTime.Now.ToLongDateString()
            };

            purchaseService.AddPurchaseEvent(purchaseEvent);

            PurchaseEvent fetchedPurchase =
                purchaseService.GetLastClientPurchaseOfProduct(fetchedClient._id, fetchedProduct._id);

            ReturnEvent returnEvent = new ReturnEvent()
            {
                ClientId = fetchedClient._id,
                ProductId = fetchedProduct._id,
                EventDate = DateTime.Now.ToString()
            };

            Assert.IsNotNull(purchaseService.GetPurchaseById(fetchedPurchase.Id));

            returnService.AddReturnEvent(returnEvent);

            Assert.IsNull(purchaseService.GetPurchaseById(fetchedPurchase.Id));
        }

        [TestMethod]
        public void PurchaseNotDeletedWithWrongReturnData()
        {
            ClientModel client = new ClientModel()
            {
                _firstName = "TestService2",
                _lastName = "TestService2"
            };

            ProductModel product = new ProductModel()
            {
                _productName = "TestName2",
                _price = 15.0f,
                _category = "Game"
            };

            clientService.AddClient(client);
            productService.AddProduct(product);

            ClientModel fetchedClient = clientService.GetLastlyAddedClient();
            ProductModel fetchedProduct = productService.GetLastlyAddedProduct();
            
            PurchaseEvent purchaseEvent = new PurchaseEvent()
            {
                ClientId = fetchedClient._id,
                ProductId = fetchedProduct._id,
                EventDate = System.DateTime.Now.ToLongDateString()
            };

            purchaseService.AddPurchaseEvent(purchaseEvent);

            PurchaseEvent fetchedPurchase =
                purchaseService.GetLastClientPurchaseOfProduct(fetchedClient._id, fetchedProduct._id);

            ReturnEvent returnEvent = new ReturnEvent()
            {
                ClientId = fetchedClient._id,
                ProductId = 0,
                EventDate = DateTime.Now.ToString()
            };

            Assert.IsNotNull(purchaseService.GetPurchaseById(fetchedPurchase.Id));

            returnService.AddReturnEvent(returnEvent);

            Assert.IsNotNull(purchaseService.GetPurchaseById(fetchedPurchase.Id));

        }
    }
}
