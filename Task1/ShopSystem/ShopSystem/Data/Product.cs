using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace ShopSystem.Data
{
    public class Product
    {
        private int id;
        private double price;
        private Category category;
        
        public int Id => id;
        public double Price => price;
        public Category Category => category;

        public Product(int _id, double _price, Category _category)
        {
            id = _id;
            price = _price;
            category = _category;
        }
    }

    public enum Category
    {
        food, 
        electronics,
        drugs,
        furniture,
        games,
        books,
        miscellaneous
    }
}
