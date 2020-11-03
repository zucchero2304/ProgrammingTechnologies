using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ShopSystem
{
    class Shop
    {
        private string name;
        private Administrator administrator;
        private Inventory inventory;

        public Shop(string name, Administrator administrator, Inventory inventory)
        {
            this.name = name;
            this.administrator = administrator;
            this.inventory = inventory;
        }
    }
}
