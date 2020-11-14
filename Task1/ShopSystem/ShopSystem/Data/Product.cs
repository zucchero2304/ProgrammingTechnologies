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

        public int Id => id;
        public double Price => price;

        public Product(int _id, double _price)
        {
            id = _id;
            price = _price;
        }
    }
}
