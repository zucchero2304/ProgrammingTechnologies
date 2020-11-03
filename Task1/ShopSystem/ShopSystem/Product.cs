using System;

namespace ShopSystem
{
    public class Product
    {
        private string name;
        private double price;
        private int availableQuantity;

        public Product(string name, double price, int avQt)
        {
            this.name = name;
            this.price = price;
            availableQuantity = avQt;
        }
    }
}
