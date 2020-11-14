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
        private Categories category;
        

        public int Id => id;
        public double Price => price;

        public Product(int _id, double _price, Categories _category)
        {
            id = _id;
            price = _price;
            category = _category;
        }
    }
    public enum Categories
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
