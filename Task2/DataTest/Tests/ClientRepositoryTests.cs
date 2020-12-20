using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Data;

namespace DataTest
{
    [TestClass]
    public class ClientRepositoryTests
    {
        ClientRepository repository = new ClientRepository();
        PurchaseEventRepository eventRepository = new PurchaseEventRepository();

        [TestMethod]
        public void AddClient()
        {
            Client client = new Client()
            {
                FirstName = "John",
                LastName = "Smith"
            };
            
            repository.AddClient(client);

            Client fetchedClient = repository.GetClientByCredentials(client.FirstName, client.LastName);

            Assert.AreEqual(client.FirstName, fetchedClient.FirstName); 
            Assert.AreEqual(client.LastName, fetchedClient.LastName);
            Assert.IsNotNull(fetchedClient);
        }

        [TestMethod]
        public void FetchLastClient()
        {
            Client client = new Client()
            {
                FirstName = "John",
                LastName = "Smith"
            };

            repository.AddClient(client);

            Assert.AreEqual(repository.GetLastClient().FirstName, client.FirstName);
            Assert.AreEqual(repository.GetLastClient().LastName, client.LastName);
        }

        [TestMethod]
        public void DeleteClient()
        {
            if (repository.GetAllClients() != null)
            {
                Client clientToDelete = repository.GetAllClients()[repository.GetNumberOfClients() - 1];

                if (HasNoEvents(clientToDelete.Id))
                {
                    repository.DeleteClient(clientToDelete.Id);

                    Assert.IsNull(repository.GetClientById(clientToDelete.Id));
                }
            }
        }

        private bool HasNoEvents(int id)
        {
            return eventRepository.GetPurchaseEventsByClientId(id).Count.Equals(0);
        }

        [TestMethod]
        public void UpdateClient()
        {
            Client client = repository.GetAllClients()[0];
            client.FirstName = "Test";
            repository.UpdateClient(client);

            Client updatedClient = repository.GetClientById(client.Id);

            Assert.AreEqual(updatedClient.FirstName, "Test");
        }

        [TestMethod]
        public void GetNonExistingClient()
        {
            Client client = repository.GetClientById(0);
            Assert.IsNull(client);
        }
    }
}
