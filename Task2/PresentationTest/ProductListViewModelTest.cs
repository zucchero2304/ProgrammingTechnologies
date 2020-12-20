using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Presentation.ViewModel;
using Service;

namespace PresentationTest
{
    [TestClass]
    public class ProductListViewModelTest
    {
        private ProductListViewModel productListVM;
        private bool canExecute;

        public ProductListViewModelTest()
        {
            productListVM = new ProductListViewModel()
            {
                ProductViewModels = new ObservableCollection<ProductItemViewModel>()
                {
                    new ProductItemViewModel(
                        new ProductModel()
                        {
                            _id = 1,
                            _productName = "DummyTofu",
                            _price = 15.0f,
                            _category = "Food"
                        }),

                    new ProductItemViewModel(
                        new ProductModel()
                        {
                            _id = 1,
                            _productName = "DummyJenga",
                            _price = 15.0f,
                            _category = "Game"
                        })
                }
            };
        }

        [TestMethod]
        public void InitialSetup()
        {
            Assert.IsNull(productListVM.SelectedViewModel);

            Assert.IsNotNull(productListVM.ProductViewModels);

            Assert.AreEqual(productListVM.ProductViewModels.Count, 2);
            Assert.AreEqual(productListVM.ProductName, null);
            Assert.AreEqual(productListVM.Category, null);
            Assert.AreEqual(productListVM.Price, 0);
        }

        [TestMethod]
        public void CommandsCreated()
        {
            var addCommand = productListVM.AddCommand;
            var deleteCommand = productListVM.DeleteCommand;

            Assert.IsNotNull(addCommand);
            Assert.IsNotNull(deleteCommand);
        }

        [TestMethod]
        public void AddExecuted()
        {
            var addCommand = productListVM.AddCommand;

            productListVM.ProductName = "DummyName";
            productListVM.Category = "Food";
            productListVM.Price = 5.0f;

            canExecute = productListVM.CanAdd;

            Assert.IsTrue(addCommand.CanExecute(canExecute));
        }

        [TestMethod]
        public void AddNotExecutedEmptyName()
        {
            var addCommand = productListVM.AddCommand;

            productListVM.ProductName = string.Empty;
            productListVM.Price = 5.0f;
            productListVM.Category = "Food";

            canExecute = productListVM.CanAdd;

            Assert.IsFalse(addCommand.CanExecute(canExecute));
        }

        [TestMethod]
        public void AddNotExecutedZeroPrice()
        {
            var addCommand = productListVM.AddCommand;

            productListVM.ProductName = "DummyName";
            productListVM.Price = 0.0f;
            productListVM.Category = "Food";

            canExecute = productListVM.CanAdd;

            Assert.IsFalse(addCommand.CanExecute(canExecute));
        }


        [TestMethod]
        public void DeleteNotExecuted()
        {
            productListVM.SelectedViewModel = null;

            var deleteCommand = productListVM.DeleteCommand;

            canExecute = productListVM.ProductViewModelIsSelected();

            Assert.IsFalse(deleteCommand.CanExecute(canExecute));
        }
        
        [TestMethod]
        public void DeleteExecuted()
        {
            productListVM.SelectedViewModel = productListVM.ProductViewModels[0];

            var deleteCommand = productListVM.DeleteCommand;

            canExecute = productListVM.ProductViewModelIsSelected();

            Assert.IsTrue(deleteCommand.CanExecute(canExecute));
        }
    }
}
