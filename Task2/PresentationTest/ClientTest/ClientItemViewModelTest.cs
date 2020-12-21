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
    public class ClientItemViewModelTest
    {
        private readonly ClientItemViewModel clientItemViewModel;
        private bool canBeExecuted;

        public ClientItemViewModelTest()
        {
            clientItemViewModel = new ClientItemViewModel(new ClientModel()
            {
                _id = 1,
                _firstName = "DummyName",
                _lastName = "DummyLastName"
            });
        }

        [TestMethod]
        public void InitialSetup()
        {
            var id = clientItemViewModel.Id;
            var name = clientItemViewModel.FirstName;
            var lastname = clientItemViewModel.LastName;

            Assert.IsNotNull(id);
            Assert.IsNotNull(name);
            Assert.IsNotNull(lastname);

            Assert.AreEqual(id, 1);
            Assert.AreEqual(name, "DummyName");
            Assert.AreEqual(lastname, "DummyLastName");
        }

        [TestMethod]
        public void CommandCreated()
        {
            var updateCommand = clientItemViewModel.UpdateCommand;

            Assert.IsNotNull(updateCommand); 
        }

        [TestMethod]
        public void UpdateNotExecuted()
        {
            var updateCommand = clientItemViewModel.UpdateCommand;

            clientItemViewModel.FirstName = null;
            clientItemViewModel.LastName = null;

            canBeExecuted = clientItemViewModel.CanUpdate;

            Assert.IsFalse(updateCommand.CanExecute(canBeExecuted));
        }

        [TestMethod]
        public void UpdateExecuted()
        {
            var updateCommand = clientItemViewModel.UpdateCommand;

            clientItemViewModel.FirstName = "DummyNewName";
            clientItemViewModel.LastName = "DummyNewName";

            canBeExecuted = clientItemViewModel.CanUpdate;

            Assert.IsTrue(updateCommand.CanExecute(canBeExecuted));
        }
    }
}
