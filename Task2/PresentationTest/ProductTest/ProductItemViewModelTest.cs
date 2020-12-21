using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Presentation.ViewModel;
using Service;

namespace PresentationTest
{
    [TestClass]
    public class ProductItemViewModelTest
    { 
        private ProductItemViewModel productVM;
        private bool canExecute;

        public ProductItemViewModelTest() 
        { 
            productVM = new ProductItemViewModel(

                new ProductModel()
                {
                    _id = 1,
                    _productName = "DummyTofu",
                    _price = 15.0f,
                    _category = "Food"
                });
        }

        [TestMethod]
        public void InitialSetup()
        {
            Assert.AreEqual(productVM.Id, 1);
            Assert.AreEqual(productVM.ProductName, "DummyTofu");
            Assert.AreEqual(productVM.Category, "Food");
            Assert.AreEqual(productVM.Price, 15.0f);
        }

        [TestMethod]
        public void CommandCreated()
        {
            var updateCommand = productVM.UpdateCommand;
            Assert.IsNotNull(updateCommand);
        }

        [TestMethod]
        public void UpdateExecuted()
        {
            var updateCommand = productVM.UpdateCommand;

            productVM.Price = 15.0f;

            canExecute = productVM.CanUpdate;

            Assert.IsTrue(updateCommand.CanExecute(canExecute));
        }

        [TestMethod]
        public void UpdateNotExecuted()
        {
            var updateCommand = productVM.UpdateCommand;

            productVM.Price = 0;

            canExecute = productVM.CanUpdate;

            Assert.IsFalse(updateCommand.CanExecute(canExecute));
        }
    }
}
