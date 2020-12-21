using System.Collections.Generic;
using Data;
using Data.Repositories;

namespace Service
{
    public class ProductService
    {
        private ProductRepository repository = new ProductRepository();
        private PurchaseEventRepository purchaseRepository = new PurchaseEventRepository();
        private ReturnEventRepository returnRepository = new ReturnEventRepository();

        public List<ProductModel> GetAllProducts()
        {
            List<ProductModel> models = new List<ProductModel>();

            foreach (var product in repository.GetAllProducts())
            {
                models.Add(MapProductDetails(product));
            }
            return models;
        }

        public bool AddProduct(ProductModel product)
        {
            if (product == null || ContainsProductWithName(product._productName))
            {
                return false;
            }

            repository.AddProduct(MapModelDetails(product));
            return true;
        }

        public ProductModel GetProductById(int id)
        {
            Product product = repository.GetProductById(id);

            return (product is null) ? null : MapProductDetails(product);
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

        public bool DeleteProduct(int id)
        {
            if (CanBeDeleted(id))
            {
                repository.DeleteProduct(id);
                return true;
            }

            return false;
        }

        public bool UpdateSelectedProduct(ProductModel model)
        {
            if (model == null || !ProductExists(model._id))
            {
                return false;
            }

            repository.UpdateProduct(MapModelDetails(model));
            return true;
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
            return purchaseRepository.GetPurchaseEventsByProductId(id).Count.Equals(0);
        }

        public bool HasNoReturns(int id)
        {
            return returnRepository.GetReturnEventsByProductId(id).Count.Equals(0);
        }

        public bool CanBeDeleted(int id)
        {
            return HasNoPurchases(id) && HasNoReturns(id) && ProductExists(id);
        }

        public bool ContainsProductWithName(string name)
        {
            return repository.GetProductByName(name) != null;
        }

        public bool ProductExists(int id)
        {
            return repository.GetProductById(id) != null;
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
    }
}
