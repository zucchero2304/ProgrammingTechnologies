using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Presentation.ViewModel;
using Service;

namespace PresentationTest
{
    [TestClass]
    public class ClientListViewModelTest
    {
        private readonly ClientListViewModel clientListViewModel;
        private bool canBeExecuted;

        public ClientListViewModelTest()
        {
            clientListViewModel = new ClientListViewModel()
            {
                ClientViewModels = new ObservableCollection<ClientItemViewModel>()
                {
                    new ClientItemViewModel(new ClientModel()
                    {
                        _id = 1,
                        _firstName = "DummyName",
                        _lastName = "DummySurname"
                    }),

                    new ClientItemViewModel(new ClientModel()
                    {
                        _id = 1,
                        _firstName = "DummyName",
                        _lastName = "DummySurname"
                    }),

                    new ClientItemViewModel(new ClientModel()
                    {
                        _id = 1,
                        _firstName = "DummyName",
                        _lastName = "DummySurname"
                    }),

                    new ClientItemViewModel(new ClientModel()
                    {
                        _id = 1,
                        _firstName = "DummyName",
                        _lastName = "DummySurname"
                    })
                }
            };
        }

        [TestMethod]
        public void InitialSetup()
        {
            Assert.IsNull(clientListViewModel.SelectedViewModel);

            Assert.IsNotNull(clientListViewModel.AddCommand);
            Assert.IsNotNull(clientListViewModel.DeleteCommand);
            Assert.IsNotNull(clientListViewModel.MessageBoxShowDelegate);
        }

        [TestMethod]
        public void ClientViewModelsCreated()
        {
            Assert.IsNotNull(clientListViewModel.ClientViewModels);
            Assert.AreEqual(clientListViewModel.ClientViewModels.Count, 4);
        }

        [TestMethod]
        public void DeleteNotExecuted()
        {
            clientListViewModel.SelectedViewModel = null;

            var deleteCommand = clientListViewModel.DeleteCommand;

            canBeExecuted = clientListViewModel.IsClientViewModelSelected;

            Assert.IsFalse(deleteCommand.CanExecute(canBeExecuted));
        }

        [TestMethod]
        public void DeleteExecuted()
        {
            clientListViewModel.SelectedViewModel = clientListViewModel.ClientViewModels[0];

            var deleteCommand = clientListViewModel.DeleteCommand;

            canBeExecuted = clientListViewModel.IsClientViewModelSelected;

            Assert.IsTrue(deleteCommand.CanExecute(canBeExecuted));
        }

        [TestMethod]
        public void AddNotExecuted()
        {
            var addCommand = clientListViewModel.AddCommand;

            clientListViewModel.FirstName = null;
            clientListViewModel.LastName = null;

            canBeExecuted = clientListViewModel.HasErrors;

            Assert.IsFalse(addCommand.CanExecute(canBeExecuted));
        }

        [TestMethod]
        public void AddExecuted()
        {
            var addCommand = clientListViewModel.AddCommand;

            clientListViewModel.FirstName = "DummyName";
            clientListViewModel.LastName = "DummySurname";

            canBeExecuted = clientListViewModel.HasErrors;

            Assert.IsTrue(addCommand.CanExecute(canBeExecuted));
        }
    }
}
