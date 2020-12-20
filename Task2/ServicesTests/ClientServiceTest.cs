using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;

namespace ServicesTests
{
    [TestClass]
    public class ClientServiceTest
    {
        ClientService service = new ClientService();

        [TestMethod]
        public void FetchAllClients()
        {
            foreach (var model in service.GetAllClients())
            {
                Assert.IsTrue(model is ClientModel);
                Console.WriteLine(model._id);
            }

            Assert.IsTrue(service.GetAllClients().Count > 0);
        }

        [TestMethod]
        public void AddClient()
        {
            ClientModel model = new ClientModel()
            {
                _firstName = "TestName",
                _lastName = "TestLastName"
            };

            Assert.IsTrue(service.AddClient(model));
        }

        [TestMethod]
        public void AddNullClient()
        {
            Assert.IsFalse(service.AddClient(null));
        }

        [TestMethod]
        public void DeleteExistingClient()
        {
            service.AddClient(new ClientModel()
            {
                _firstName = "TestName",
                _lastName = "TestLastName"
            });

            Assert.IsTrue(service.DeleteClient(service.GetLastlyAddedClient()._id));
        }

        [TestMethod]
        public void DeleteNonExistingClient()
        {
            Assert.IsFalse(service.DeleteClient(0));
        }

        [TestMethod]
        public void UpdateClient()
        {
            ClientModel model = service.GetLastlyAddedClient();

            if (model != null)
            {
                model._firstName = "ChangedName";
                Assert.IsTrue(service.UpdateClient(model));
                Assert.AreEqual(service.GetClientById(model._id)._firstName, "ChangedName");
            }
        }

        [TestMethod]
        public void UpdateNonExistingClient()
        {
            ClientModel model = new ClientModel()
            {
                _id = 0,
                _firstName = "TestName",
                _lastName = "TestLastName"
            };

            Assert.IsTrue(service.HasNoEvents(model._id));
            Assert.IsFalse(service.UpdateClient(model));
        }

        [TestMethod]
        public void GetClientZeroId()
        {
            Assert.IsNull(service.GetClientById(0));
        }
    }
}
