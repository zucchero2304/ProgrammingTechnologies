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
        private readonly IDialogService dialogService;

        public ProductViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            product = new Product("Book"); //then get from db or smth
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

            var viewModel = new ProductInfoViewModel();
            viewModel.Info = "I'm coming from ProductViewModel";
            bool? result = dialogService.ShowDialog(viewModel);
        }
    }
}
 