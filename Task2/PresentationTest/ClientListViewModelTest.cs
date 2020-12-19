using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Presentation.ViewModel;

namespace PresentationTest
{
    [TestClass]
    public class ClientListViewModelTest
    {
        [TestMethod]
        public void InitialSetup()
        {
            ClientListViewModel clientListViewModel = new ClientListViewModel();

            Assert.IsNull(clientListViewModel.SelectedViewModel);

            Assert.IsNotNull(clientListViewModel.AddCommand);
            Assert.IsNotNull(clientListViewModel.DeleteCommand);
            Assert.IsNotNull(clientListViewModel.MessageBoxShowDelegate);
            Assert.IsNotNull(clientListViewModel.ClientViewModels);

            Assert.IsFalse(clientListViewModel.IsClientViewModelSelected);
        }
    }
}
