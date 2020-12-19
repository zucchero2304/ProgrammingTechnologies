using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Presentation.ViewModel;

namespace PresentationTest
{
    [TestClass]
    public class MainViewModelTest
    {
        [TestMethod]
        public void InitiallySetToClientListView()
        {
            MainViewModel mainViewModel = new MainViewModel();

            Assert.IsInstanceOfType(mainViewModel.SelectedViewModel, typeof(ClientListViewModel)); 

            Assert.IsTrue(mainViewModel.SwitchViewCommand.CanExecute(null));
            
            Assert.IsNotNull(mainViewModel.SwitchViewCommand);
        }

        [TestMethod]
        public void SwitchViewToClientList()
        {
            MainViewModel mainViewModel = new MainViewModel();

            mainViewModel.SwitchView("ClientListView");

            Assert.IsTrue(mainViewModel.SelectedViewModel is ClientListViewModel);
        }

        [TestMethod]
        public void SwitchViewToProductList()
        {
            MainViewModel mainViewModel = new MainViewModel();

            mainViewModel.SwitchView("ProductListView");

            Assert.IsTrue(mainViewModel.SelectedViewModel is ProductListViewModel);
        }
    }
}
