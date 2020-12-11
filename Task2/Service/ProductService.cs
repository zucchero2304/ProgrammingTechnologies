using System;
using System.Collections.Generic;
using System.Text;
using Data;

namespace Service
{
    class ProductService
    {
        private ProductRepository repository = new ProductRepository();
        private PurchaseEventRepository eventRepository = new PurchaseEventRepository();

        public void AddProduct(Product product)
        {
            // cannot add products with the same name, price and category
            repository.AddProduct(product);
        }

        public void DeleteProduct(int id)
        {
            if (HasNoPurchases(id))
            {
                repository.DeleteProduct(id);
            }
        }

        private bool HasNoPurchases(int id)
        {
            return eventRepository.GetPurchaseEventsByProductId(id).Count.Equals(0);
        }

        public List<Product> GetAllProducts()
        {
            return repository.GetAllProducts();
        }

        public Product GetProductById(int id)
        {
            // what is null?
            return repository.GetProductById(id);
        }

        public List<Product> GetProductsByCategory(ProductCategory category)
        {
            return repository.GetProductsByCategory(category);
        } 
    }
}
