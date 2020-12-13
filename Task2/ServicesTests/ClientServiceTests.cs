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
    public class ClientServiceTests
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
    }
}
