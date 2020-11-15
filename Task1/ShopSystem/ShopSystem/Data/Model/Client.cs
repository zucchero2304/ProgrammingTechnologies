using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem.Data
{
    public class Client
    {
        private int id;
        private String name;
        private String surname;

        public Client(int _id, String _name, String _surname)
        {
            id = _id;
            name = _name;
            surname = _surname;
        }

        public int Id => id;
        public String Name => name;
        public String Surname => surname;
    }
}
