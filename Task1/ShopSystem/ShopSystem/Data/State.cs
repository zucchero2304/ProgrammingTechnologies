using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ShopSystem.Data
{
	public class State
	{
		private IEnumerable<Product> products;
		private Product product;
		private int id;

		public State(IEnumerable<Product> _products)
		{
			products = _products;
		}

		public State(Product _product)
        {
			product = _product;
        }

		public IEnumerable<Product> Products => products;
		public int Id => id;
		public Product Product => product;
    }
}

