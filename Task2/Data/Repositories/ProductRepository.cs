using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public class ProductRepository
    {
        public List<Product> GetAllProducts()
        {
            using (var db = new ShopDataContext())
            {
                return db.Products.Select(product => product).ToList();
            }
        }

        public Product GetProductById(int id) 
        {
            using (var db = new ShopDataContext())
            {
                return db.Products.FirstOrDefault(product => product.Id.Equals(id));
            }
        }

        public List<Product> GetProductsByCategory(ProductCategory category)
        {
            using (var db = new ShopDataContext())
            {
                return db.Products.Where(product => product.Category.Equals(category)).ToList();
            }
        }

        public List<Product> GetProductsByPrice(float price)
        {
            using (var db = new ShopDataContext())
            {
                return db.Products.Where(product => product.Price.Equals(price)).ToList();
            }
        }

        public Product GetProductByName(string name)
        {
            using (var db = new ShopDataContext())
            {
                return db.Products.FirstOrDefault(product => product.ProductName.Equals(name));
            }
        }

        public List<Product> GetProductsCheaperThan(float price)
        {
            using (var db = new ShopDataContext())
            { 
                return db.Products.Where(product => product.Price < price).ToList();
            }
        }

        public List<Product> GetProductsMoreExpensiveThan(float price)
        {
            using (var db = new ShopDataContext())
            {
                return db.Products.Where(product => product.Price > price).ToList();
            }
        }

        public Product GetLastProduct()
        {
            using (var db = new ShopDataContext())
            {
                return db.Products.Select(product => product).ToList().LastOrDefault();
            }
        }

        public void AddProduct(Product product)
        {
            using (var db = new ShopDataContext())
            {
                db.Products.InsertOnSubmit(product);
                db.SubmitChanges();
            }
        }

        public void DeleteProduct(int id)
        {
            using (var db = new ShopDataContext())
            {
                Product productToDelete = db.Products.FirstOrDefault(product => product.Id.Equals(id));

                if (productToDelete != null)
                {
                    db.Products.DeleteOnSubmit(productToDelete);
                    db.SubmitChanges();
                }
            }
        }

        public void UpdateProduct(Product p)
        {
            using (var db = new ShopDataContext())
            {
                Product productToUpdate = db.Products.FirstOrDefault(product => product.Id.Equals(p.Id));

                if (productToUpdate != null)
                {
                    productToUpdate.ProductName = p.ProductName;
                    productToUpdate.Category = p.Category;
                    productToUpdate.Price = p.Price;
                    db.SubmitChanges();
                }
            }
        }

        public List<ProductCategory> GetAllCategories()
        {
            using (var db = new ShopDataContext())
            {
                return db.ProductCategories.Select(category => category).ToList();
            }
        }

        public ProductCategory GetCategoryByName(string category)
        {
            using (var db = new ShopDataContext())
            {
                return db.ProductCategories.FirstOrDefault(c => c.Category.Equals(category));
            }
        }
    }
}
