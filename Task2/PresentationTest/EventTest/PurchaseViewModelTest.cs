using Microsoft.VisualStudio.TestTools.UnitTesting;
using Presentation.ViewModel;
using System;
using System.Collections.ObjectModel;
using Service;
using Data;

namespace PresentationTest
{
    [TestClass]
    public class PurchaseViewModelTest
    {
        private readonly PurchaseViewModel purchaseViewModel;

        public PurchaseViewModelTest()
        {
            purchaseViewModel = new PurchaseViewModel(new PurchaseEvent()
            {
                Id = 3,
                ProductId = 9,
                ClientId = 8,
                EventDate = "22/12/20"
            });
        }

        [TestMethod]
        public void InitialSetup()
        {
            var id = purchaseViewModel.Id;
            var productId = purchaseViewModel.ProductId;
            var clientId = purchaseViewModel.ClientId;
            var eventDate = purchaseViewModel.Date;


            Assert.IsNotNull(id);
            Assert.IsNotNull(productId);
            Assert.IsNotNull(clientId);
            Assert.IsNotNull(eventDate);


            Assert.AreEqual(id, 3);
            Assert.AreEqual(productId, 9);
            Assert.AreEqual(clientId, 8);
            Assert.AreEqual(eventDate, "22/12/20");

        }

    }
}
