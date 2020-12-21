using Microsoft.VisualStudio.TestTools.UnitTesting;
using Presentation.ViewModel;
using System;
using System.Collections.ObjectModel;
using Service;
using Data;

namespace PresentationTest
{
    [TestClass]
    public class ReturnViewModelTest
    {
        private readonly ReturnViewModel returnViewModel;

        public ReturnViewModelTest()
        {
            returnViewModel = new ReturnViewModel(new ReturnEvent()
            {
                Id = 4,
                ProductId = 9,
                ClientId = 8,
                EventDate = "22/12/20"
            });
        }

        [TestMethod]
        public void InitialSetup()
        {
            var id = returnViewModel.Id;
            var productId = returnViewModel.ProductId;
            var clientId = returnViewModel.ClientId;
            var eventDate = returnViewModel.Date;


            Assert.IsNotNull(id);
            Assert.IsNotNull(productId);
            Assert.IsNotNull(clientId);
            Assert.IsNotNull(eventDate);


            Assert.AreEqual(id, 4);
            Assert.AreEqual(productId, 9);
            Assert.AreEqual(clientId, 8);
            Assert.AreEqual(eventDate, "22/12/20");

        }
    }
}
