using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using System.Windows.Input;
using Presentation.Command;
using Presentation.Model;

namespace Presentation.ViewModel
{
    public class ProductItemViewModel : ViewModelBase
    {

        #region InitialSetup

        public ProductItemViewModel() { }

        public ProductItemViewModel(ProductModel product)
        {
            Id = product._id;
            ProductName = product._productName;
            Price = product._price;
            Category = product._category;

            service = new ProductService();

            configureCommands();
        }

        private void configureCommands()
        {
            updateCommand = new RelayCommand(e => { UpdateProduct(); });
        }

        #endregion


        #region API
        public int Id
        {
            get => id;

            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string ProductName
        {
            get => productName;
            set
            {
                productName = value;
                OnPropertyChanged("ProductName");
            }
        }
        public double Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }

        public string Category
        {
            get => category;
            set
            {
                category = value;
                OnPropertyChanged("Category");
            }
        }

        public ICommand UpdateCommand
        {
            get => updateCommand;
        }

        #endregion


        #region PrivateAttributes

        private int id;
        private string productName;
        private double price;
        private string category;

        private ProductService service;

        private ICommand updateCommand;

        #endregion


        #region PrivateMethods

        private void UpdateProduct()
        {
            service.UpdateSelectedProduct(this.productName);
            //doesn't work yet
        }

        #endregion
    }
}
