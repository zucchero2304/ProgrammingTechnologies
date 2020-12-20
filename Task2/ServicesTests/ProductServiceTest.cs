using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;

namespace ServicesTests
{
    [TestClass]
    public class ProductServiceTest
    {
        ProductService service = new ProductService();

        [TestMethod]
        public void AddNullProduct()
        {
            Assert.IsFalse(service.AddProduct(null));
        }

        [TestMethod]
        public void DeleteProduct()
        {
            int id = service.GetLastlyAddedProduct()._id;

            if (service.CanBeDeleted(id))
            {
                Assert.IsTrue(service.DeleteProduct(id));
            }
        }

        [TestMethod]
        public void DeleteNonExistingProduct()
        {
            Assert.IsFalse(service.DeleteProduct(0));
        }

        [TestMethod]
        public void GetProductByNonExistingId()
        {
            Assert.IsNull(service.GetProductById(0));
        }

        [TestMethod]
        public void CheckFetchingByPrice()
        {
            int price = 10;

            foreach (var product in service.GetProductsByPrice(price))
            {
                Assert.AreEqual(product._price, price);
            }
        }

        [TestMethod]
        public void CheckFetchingProductsCheaperThan()
        {
            int price = 10;

            foreach (var product in service.GetProductsCheaperThan(price))
            {
                Assert.IsTrue(product._price < price);
            }
        }

        [TestMethod]
        public void CheckFetchingProductsMoreExpensiveThan()
        {
            int price = 10;

            foreach (var product in service.GetProductsMoreExpensiveThan(price))
            {
                Assert.IsTrue(product._price > price);
            }
        }

        [TestMethod]
        public void GetProductsByCategory()
        {
            ProductCategory category = new ProductCategory() {Category = "Food"};

            foreach (var product in service.GetProductsByCategory(category))
            {
                Assert.AreEqual(product._category, category);
            }
        }

        [TestMethod]
        public void GetProductsByNonExistingCategory()
        {
            ProductCategory category = new ProductCategory() { Category = "NonExisting" };

            Assert.IsTrue(service.GetProductsByCategory(category).Count.Equals(0));
        }
    }
}
