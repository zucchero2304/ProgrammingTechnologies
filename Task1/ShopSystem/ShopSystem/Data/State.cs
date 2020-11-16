using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ShopSystem.Data
{
	public class State
	{
		private Product product;

		public State(Product _product)
        {
			product = _product;
        }

		public Product Product => product;
    }
}

