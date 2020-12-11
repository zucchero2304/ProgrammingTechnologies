
using System.Linq;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (ShopDataContext db = new ShopDataContext())
            {

            }
        }
    }
}
