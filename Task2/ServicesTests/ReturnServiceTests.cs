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
            Client client = new Client()
            {
                FirstName = "TestService",
                LastName = "TestService"
            };

            Product product = new Product()
            {
                ProductName = "TestName",
                Price = 15.0f,
                Category = new Product() { Category = "Game" }.Category,
            };

            clientService.AddClient(client);
            productService.AddProduct(product);

            Client fetchedClient = clientService.GetLastlyAddedClient();
            Product fetchedProduct = productService.GetLastlyAddedProduct();

            PurchaseEvent purchaseEvent = new PurchaseEvent()
            {
                ClientId = fetchedClient.Id,
                ProductId = fetchedProduct.Id,
                EventDate = System.DateTime.Now.ToLongDateString()
            };

            purchaseService.AddPurchaseEvent(purchaseEvent);

            PurchaseEvent fetchedPurchase =
                purchaseService.GetLastClientPurchaseOfProduct(fetchedClient.Id, fetchedProduct.Id);


            ReturnEvent returnEvent = new ReturnEvent()
            {
                ClientId = fetchedClient.Id,
                ProductId = fetchedProduct.Id,
                EventDate = DateTime.Now.ToString()
            };

            Assert.IsNotNull(purchaseService.GetPurchaseById(fetchedPurchase.Id));

            returnService.AddReturnEvent(returnEvent);

            Assert.IsNull(purchaseService.GetPurchaseById(fetchedPurchase.Id));
        }
    }
}
