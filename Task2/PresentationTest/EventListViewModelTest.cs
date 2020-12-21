using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using Presentation.ViewModel;
using Service;
using Data;

namespace PresentationTest
{
    [TestClass]
    public class EventListViewModelTest
    {
        private readonly EventListViewModel eventListViewModel;
        private bool canBeExecuted;

        public EventListViewModelTest()
        {
            eventListViewModel = new EventListViewModel()
            {
                PurchaseViewModels = new ObservableCollection<PurchaseViewModel>()
                {
                    new PurchaseViewModel(new PurchaseEvent()
                    {
                        Id = 3,
                        ProductId = 9,
                        ClientId = 8,
                        EventDate = "22/12/20"
                    }),

                    new PurchaseViewModel(new PurchaseEvent()
                    {
                        Id = 3,
                        ProductId = 9,
                        ClientId = 8,
                        EventDate = "22/12/20"
                    }),

                    new PurchaseViewModel(new PurchaseEvent()
                    {
                        Id = 3,
                        ProductId = 9,
                        ClientId = 8,
                        EventDate = "22/12/20"
                    }),
                },

            ReturnViewModels = new ObservableCollection<ReturnViewModel>()
                {
                    new ReturnViewModel(new ReturnEvent()
                    {
                        Id = 4,
                        ProductId = 9,
                        ClientId = 8,
                        EventDate = "22/12/20"
                    }),

                    new ReturnViewModel(new ReturnEvent()
                    {
                        Id = 4,
                        ProductId = 9,
                        ClientId = 8,
                        EventDate = "22/12/20"
                    }),

                    new ReturnViewModel(new ReturnEvent()
                    {
                        Id = 4,
                        ProductId = 9,
                        ClientId = 8,
                        EventDate = "22/12/20"
                    }),
                }
            };
        }

        [TestMethod]
        public void InitialSetup()
        {
            Assert.IsNull(eventListViewModel.SelectedPurchaseViewModel);
            Assert.IsNull(eventListViewModel.SelectedReturnViewModel);

            Assert.IsNotNull(eventListViewModel.PurchaseCommand);
            Assert.IsNotNull(eventListViewModel.ReturnCommand);
            Assert.IsNotNull(eventListViewModel.MessageBoxShowDelegate);

        }

        [TestMethod]
        public void PurchaseViewModelsCreated()
        {
            Assert.IsNotNull(eventListViewModel.PurchaseViewModels);
            Assert.AreEqual(eventListViewModel.PurchaseViewModels.Count, 3);
        }

        [TestMethod]
        public void ReturnViewModelsCreated()
        {
            Assert.IsNotNull(eventListViewModel.ReturnViewModels);
            Assert.AreEqual(eventListViewModel.ReturnViewModels.Count, 3);
        }

        [TestMethod]
        public void PurchaseNotExecuted()
        {
            var purchaseCommand = eventListViewModel.PurchaseCommand;

            eventListViewModel.EventId = 0;
            eventListViewModel.ProductId = 0;
            eventListViewModel.ClientId = 0;

            canBeExecuted = eventListViewModel.HasErrors;

            Assert.IsFalse(purchaseCommand.CanExecute(canBeExecuted));
        }

        [TestMethod]
        public void PurchaseExecuted()
        {
            var purchaseCommand = eventListViewModel.PurchaseCommand;

            eventListViewModel.EventId = 3;
            eventListViewModel.ProductId = 9;
            eventListViewModel.ClientId = 8;

            canBeExecuted = eventListViewModel.HasErrors;

            Assert.IsTrue(purchaseCommand.CanExecute(canBeExecuted));
        }

        public void ReturnNotExecuted()
        {
            var returnCommand = eventListViewModel.ReturnCommand;

            eventListViewModel.EventId = 0;
            eventListViewModel.ProductId = 0;
            eventListViewModel.ClientId = 0;

            canBeExecuted = eventListViewModel.HasErrors;

            Assert.IsFalse(returnCommand.CanExecute(canBeExecuted));
        }

        public void ReturnExecuted()
        {
            var returnCommand = eventListViewModel.ReturnCommand;

            eventListViewModel.EventId = 4;
            eventListViewModel.ProductId = 9;
            eventListViewModel.ClientId = 8;

            canBeExecuted = eventListViewModel.HasErrors;

            Assert.IsTrue(returnCommand.CanExecute(canBeExecuted));
        }

    }
}
