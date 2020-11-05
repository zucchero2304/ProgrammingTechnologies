using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem
{
    // Services represent an application's business logic 
 
    class DataService
    {
        private IRepository repository;

        public DataService(IRepository repository) 
        {
            this.repository = repository;
        }

        public List<Product> GetAllProducts()
        {
            return repository.GetAllProducts(); 
        }

        public Product GetProduct(int id)
        {
            return repository.GetProductById(id);
        }

        public void AddProduct(Product product)
        {
            repository.AddProduct(product);
        }

        public void DeleteProduct(int id)
        {
            repository.DeleteProduct(id);
        }


        // The same logic for Client and Event
    }
}
