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

        public List<ProductModel> GetAllProducts()
        {
            List<ProductModel> models = new List<ProductModel>();

            foreach (var product in repository.GetAllProducts())
            {
                models.Add(MapProductDetails(product));
            }
            return models;
        }

        private Product MapModelDetails(ProductModel model)
        {
            return new Product()
            {
                Id = model._id,
                ProductName = model._productName,
                Price = model._price,
                Category = model._category
            };
        }

        private ProductModel MapProductDetails(Product product)
        {
            return new ProductModel()
            {
                _id = product.Id,
                _productName = product.ProductName,
                _price = product.Price,
                _category = product.Category
            };
        }

        public ProductModel GetProductById(int id)
        {
            return MapProductDetails(repository.GetProductById(id));
        }

        public List<ProductModel> GetProductsByCategory(ProductCategory category)
        {
            List<ProductModel> models = new List<ProductModel>();

            foreach (var product in repository.GetProductsByCategory(category))
            {
                models.Add(MapProductDetails(product));
            }
            return models;
        }

        public List<ProductModel> GetProductsByPrice(float price)
        {
            List<ProductModel> models = new List<ProductModel>();

            foreach (var product in repository.GetProductsByPrice(price))
            {
                models.Add(MapProductDetails(product));
            }
            return models;
        }

        public ProductModel GetProductByName(string name)
        {
            return MapProductDetails(repository.GetProductByName(name));
        }

        public ProductModel GetLastlyAddedProduct()
        {
            return MapProductDetails(repository.GetLastProduct());
        }

        public void AddProduct(ProductModel product)
        {
            if (!ContainsProductWithName(product._productName))
            {
                repository.AddProduct(MapModelDetails(product));
            }
        }
        public List<ProductModel> GetProductsCheaperThan(float price)
        {
            List<ProductModel> models = new List<ProductModel>();

            foreach (var product in repository.GetProductsCheaperThan(price))
            {
                models.Add(MapProductDetails(product));
            }
            return models;

        }

        public List<ProductModel> GetProductsMoreExpensiveThan(float price)
        {
            List<ProductModel> models = new List<ProductModel>();

            foreach (var product in repository.GetProductsMoreExpensiveThan(price))
            {
                models.Add(MapProductDetails(product));
            }
            return models;

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

        public bool HasNoPurchases(int id)
        {
            return eventRepository.GetPurchaseEventsByProductId(id).Count.Equals(0);
        }

        public bool ContainsProductWithName(string name)
        {
            return repository.GetProductByName(name) != null;
        }

        public void UpdateSelectedProduct(string name)
        {
            repository.UpdateProduct(MapModelDetails(GetProductByName(name)));
        }

    }
}
