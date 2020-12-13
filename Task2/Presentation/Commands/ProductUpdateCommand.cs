using Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.Commands
{
    class ProductUpdateCommand : ICommand
    {
        private ProductViewModel viewModel;

        public ProductUpdateCommand(ProductViewModel viewModel) {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return String.IsNullOrWhiteSpace(viewModel.Product.Error);
        }

        public void Execute(object parameter)
        {
            viewModel.SaveChanges();
        }
    }
}
