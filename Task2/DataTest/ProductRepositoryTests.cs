using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;

namespace DataTest
{
    [TestClass]
    public class ProductRepositoryTests
    {
        private ProductRepository productRepository;
        private PurchaseEventRepository eventRepository;

        public ProductRepositoryTests()
        {
            productRepository = new ProductRepository();
            eventRepository = new PurchaseEventRepository();
        }

        private Product ProductToBeAdded()
        {
            return new Product() 
            {
                ProductName = "Tofu",
                Price = 12.5, 
                Category = new Product() { Category = "Food" }.Category,
            };
        }

        [TestMethod]
        public void AddProduct()
        {
            Product product = ProductToBeAdded();
            int initialCount = productRepository.GetAllProducts().Count;

            productRepository.AddProduct(product);

            Product fetchedProduct = productRepository.GetProductByName(product.ProductName);

            Assert.IsNotNull(fetchedProduct);
            Assert.AreEqual(product.Price, fetchedProduct.Price);
            Assert.AreEqual(product.Category, fetchedProduct.Category);
            Assert.AreEqual(product.ProductName, fetchedProduct.ProductName);
            Assert.AreEqual(initialCount, productRepository.GetAllProducts().Count - 1);
        }

        [TestMethod]
        public void DeleteProduct()
        {

            Product productToDelete = productRepository.GetLastProduct();

            if (NotPurchased(productToDelete.Id))
            {
                productRepository.DeleteProduct(productToDelete.Id);
                Assert.IsNull(productRepository.GetProductById(productToDelete.Id));
            }
        }

        [TestMethod]
        public void UpdateProduct()
        {
            Product product = productRepository.GetLastProduct();

            float randomPrice = GetRandomPrice();
            product.Price = randomPrice;

            productRepository.UpdateProduct(product);

            Product updatedProduct = productRepository.GetProductById(product.Id);

            Assert.AreEqual(updatedProduct.Price,randomPrice);
        }

        [TestMethod]
        public void GetProductsByCategory()
        {
            List<Product> products = productRepository.GetProductsByCategory(
                new ProductCategory() { Category = "Food" });

            foreach (var product in products)
            {
                Assert.AreEqual(product.Category, null);
            }
        }

        [TestMethod]
        public void GetNonExistingProductById()
        {
            Product product = productRepository.GetProductById(0);
            Assert.IsNull(product);
        }

        [TestMethod]
        public void GetNonExistingCategory()
        {
            ProductCategory nonExisting = productRepository.GetCategoryByName("NonExisting");
            Assert.IsNull(nonExisting);
        }

        [TestMethod]
        public void GetProductsNonExistingCategory()
        {
            List<Product> nonExisting = productRepository.GetProductsByCategory(
                new ProductCategory() {Category = "NonExisting"});
            Assert.AreEqual(nonExisting.Count, 0);
        }

        private float GetRandomPrice()
        {
            var random = new Random();
            return random.Next(10, 100);
        }

        private bool NotPurchased(int id)
        {
            return eventRepository.GetPurchaseEventsByProductId(id).Count.Equals(0);
        }
    }
}
