using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ShopSystem.Data
{
	public class State
	{
		private IEnumerable<Product> products;
		private int id;

		public State(IEnumerable<Product> _products)
		{
			products = _products;
		}

		public IEnumerable<Product> Products => products;
		public int Id => id;
    }
}

