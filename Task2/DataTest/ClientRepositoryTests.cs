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
        public void RowNumberIsIncremented()
        {
            int initialCount = repository.GetAllClients().Count;
            Console.WriteLine(initialCount);

            AddClient();

            Assert.AreEqual(initialCount, repository.GetAllClients().Count - 1);
        }

        [TestMethod]
        public void DeleteClient()
        {
            if (repository.GetAllClients() != null)
            {
                Client clientToDelete = repository.GetAllClients()[repository.GetNumberOfClients() - 1];

                repository.DeleteClient(clientToDelete.Id);
                
                Assert.IsNull(repository.GetClientById(clientToDelete.Id));
            }
        }

        [TestMethod]
        public void UpdateClient()
        {
            Client client = repository.GetAllClients()[0];
            client.FirstName = "Lizaveta";
            repository.UpdateClient(client);

            Client updatedClient = repository.GetClientById(client.Id);

            Assert.AreEqual(updatedClient.FirstName, "Lizaveta");
        }
    }
}
