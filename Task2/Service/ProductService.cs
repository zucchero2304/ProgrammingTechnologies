using System;
using System.Collections.Generic;
using System.Text;
using Data;

namespace Service
{
    // how to handle nulls in thus layer? 
    public class ProductService
    {
        private ProductRepository repository = new ProductRepository();
        private PurchaseEventRepository eventRepository = new PurchaseEventRepository();
        
        public List<Product> GetAllProducts()
        {
            return repository.GetAllProducts();
        }

        public Product GetProductById(int id)
        {
            return repository.GetProductById(id);
        }

        public List<Product> GetProductsByCategory(ProductCategory category)
        {
            return repository.GetProductsByCategory(category);
        }

        public List<Product> GetProductsByPrice(float price)
        {
            return repository.GetProductsByPrice(price);
        }

        public Product GetProductByName(string name)
        {
            return repository.GetProductByName(name);
        }

        public Product GetLastlyAddedProduct()
        {
            return repository.GetLastProduct();
        }

        public void AddProduct(Product product)
        {
            if (!ContainsProductWithName(product.ProductName))
            {
                repository.AddProduct(product);
            }
        }

        public List<Product> GetProductsCheaperThan(float price)
        {
            return repository.GetProductsCheaperThan(price);
        }

        public List<Product> GetProductsMoreExpensiveThan(float price)
        {
            return repository.GetProductsMoreExpensiveThan(price);
        }
        public void DeleteProduct(int id)
        {
            if (HasNoPurchases(id))
            {
                repository.DeleteProduct(id);
            }
        }

        public List<ProductCategory> GetAllCategories()
        {
            return repository.GetAllCategories();
        }

        public ProductCategory GetProductCategoryByName(string category)
        {
            return repository.GetCategoryByName(category);
        }
        
        private bool HasNoPurchases(int id)
        {
            return eventRepository.GetPurchaseEventsByProductId(id).Count.Equals(0);
        }

        public bool ContainsProductWithName(string name)
        {
            return repository.GetProductByName(name) != null;
        }
    }
}
