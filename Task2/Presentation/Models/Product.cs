using System;
using Service;
using Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class Product : Base, IDataErrorInfo
    {
        private String name;
        public Product(String productName) {
            Name = productName;
        }

        public String Name
        {
            get
            {
                return name;
            }
            set {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public String Error
        {
            get;
            private set;
        }

        #region IDataErrorInfo Members
        public String this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {
                    if (String.IsNullOrWhiteSpace(Name))
                    {
                        Error = "Name cannot be null or empty";
                    }
                    else
                    {
                        Error = null;
                    }
                }

                return Error;
            }
        }
        #endregion
       
        /* it should be in this newer form, that was an old tutorial
        #region IDataErrorInfo Members
        public string Error => throw new NotImplementedException();

        public string this[string columnName] => throw new NotImplementedException();

        #endregion*/
    }
}
