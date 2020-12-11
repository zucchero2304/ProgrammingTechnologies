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
        private ProductRepository repository;
        private PurchaseEventRepository eventRepository;
        private List<ProductCategory> categories;

        public ProductRepositoryTests()
        {
            repository = new ProductRepository();
            eventRepository = new PurchaseEventRepository();
            categories = repository.GetAllCategories();
        }

        private Product AddedProduct()
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

            Product product = AddedProduct();

            repository.AddProduct(product);

            Product fetchedProduct = repository.GetProductByName(product.ProductName);

            Assert.IsNotNull(fetchedProduct);
            Assert.AreEqual(product.Price, fetchedProduct.Price);
            Assert.AreEqual(product.Category, fetchedProduct.Category);
            Assert.AreEqual(product.ProductName, fetchedProduct.ProductName);
        }

        [TestMethod]
        public void RowNumberIsIncremented()
        {
            int initialCount = repository.GetAllProducts().Count;
            Console.WriteLine(initialCount);
            
            AddProduct();
            
            Assert.AreEqual(initialCount, repository.GetAllProducts().Count - 1);
        }

        [TestMethod]
        public void DeleteProduct()
        {
            Product productToDelete = repository.GetAllProducts()[repository.GetNumberOfProducts() - 1];

            if (NotPurchased(productToDelete.Id))
            {
                repository.DeleteProduct(productToDelete.Id);
                Assert.IsNull(repository.GetProductById(productToDelete.Id));
            }
        }

         private bool NotPurchased(int id)
         {
             return eventRepository.GetPurchaseEventsByProductId(id).Count.Equals(0);
         }

        [TestMethod]
        public void UpdateProduct()
        {
            Product product = repository.GetAllProducts()[0];

            float randomPrice = GetRandomPrice();
            product.Price = randomPrice;

            repository.UpdateProduct(product);

            Product updatedProduct = repository.GetProductById(product.Id);

            Assert.AreEqual(updatedProduct.Price,randomPrice);
        }

        private float GetRandomPrice()
        {
            var random = new Random();
            return random.Next(10, 100);
        }

        [TestMethod]
        public void GetProductsByCategory()
        {
            List<Product> products = repository.GetProductsByCategory(
                new ProductCategory() { Category = "Food" });

            foreach (var product in products)
            {
                Assert.AreEqual(product.Category, "Food");
            }
        }
    }
}
