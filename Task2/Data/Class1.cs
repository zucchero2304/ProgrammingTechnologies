using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Class1
    {
        private ProductDataContext _db = new ProductDataContext();

        Class1()
        {
            // exemplary usage 
            var c = from product in _db.Products
                where product.Price > 0
                select product;
        }
    }
}
