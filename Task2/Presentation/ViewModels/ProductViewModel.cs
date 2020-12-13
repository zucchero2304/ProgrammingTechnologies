using Presentation.Commands;
using Presentation.Models;
using Presentation.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.ViewModels
{
    internal class ProductViewModel
    {
        private Product product;
        //the ProductViewModel is responsible for showing the childView. We store the childView state here.
        private ProductInfoViewModel childViewModel;

        public ProductViewModel()
        {
            product = new Product("Book"); //then get from db or smth
            childViewModel = new ProductInfoViewModel();
            UpdateCommand = new ProductUpdateCommand(this); //passing instance of ViewModel
        }

        public Product Product { 
            get
            {
                return product;
            }
        }

        public ICommand UpdateCommand
        {
            get;
            private set;
        }
        public void SaveChanges()
        {
            ProductInfoView view = new ProductInfoView();
            view.DataContext = childViewModel;

            childViewModel.Info = Product.Name + " was updated in the database.";
            view.ShowDialog();
        }
    }
}
 