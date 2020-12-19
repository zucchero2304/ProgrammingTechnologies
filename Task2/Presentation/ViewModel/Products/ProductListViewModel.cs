using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Presentation.Model;
using Service;
using Presentation.Command;


namespace Presentation.ViewModel
{
    public class ProductListViewModel : ViewModelBase
    {
        #region InitialSetup
        public ProductListViewModel()
        {
            init();
            configureCommands();
        }

        private void init()
        {
            service = new ProductService();

            productViewModels = new ObservableCollection<ProductItemViewModel>();

            foreach (var c in service.GetAllProducts())
            {
                productViewModels.Add(new ProductItemViewModel(c));
            }
        }

        private void configureCommands()
        {
            addCommand = new RelayCommand(e => { AddProduct(); },
                c => NonEmptyInputs());

            deleteCommand = new RelayCommand(e => { DeleteProduct(); },
                c => ProductViewModelIsSelected());
        }

        #endregion

        #region API

        public string ProductName
        {
            get => newProductName;
            set
            {
                newProductName = value;
                OnPropertyChanged("ProductName");
            }
        }

        public double Price
        {
            get => newProductPrice;
            set
            {
                newProductPrice = value;
                OnPropertyChanged("Price");
            }
        }

        public string Category
        {
            get => newProductCategory;
            set
            {
                newProductCategory = value;
                OnPropertyChanged("ProductCategory");
            }
        }

        public ObservableCollection<ProductItemViewModel> ProductViewModels
        {
            get => productViewModels;

            set
            {
                productViewModels = value;
                OnPropertyChanged("ProductViewModels");
            }
        }

        public ProductItemViewModel SelectedViewModel
        {
            get => selectedViewModel;
            set
            {
                selectedViewModel = value;
                OnPropertyChanged("SelectedViewModel");
            }
        }
        public ICommand AddCommand
        {
            get => addCommand;
        }

        public ICommand DeleteCommand
        {
            get => deleteCommand;
        }

        public Action<string> MessageBoxShowDelegate { get; set; }
            = x => throw new ArgumentOutOfRangeException(
                $"The delegate {nameof(MessageBoxShowDelegate)} must be assigned by the view layer");

        #endregion


        #region PrivateAttributes

        private string newProductName;
        private double newProductPrice;
        private string newProductCategory;

        private ICommand addCommand;
        private ICommand deleteCommand;

        private ProductService service;
        private ProductItemViewModel selectedViewModel;
        private ObservableCollection<ProductItemViewModel> productViewModels;

        #endregion


        #region PrivateMethods

        private void AddProduct()
        {
            ProductModel newProduct = new ProductModel()
            {
                _id = 0,
                _productName = ProductName,
                _price = Price,
                _category = Category
            };

            service.AddProduct(newProduct);
        }

        private void DeleteProduct()
        {
            if (ProductHasNoPurchases())
            {
                service.DeleteProduct(SelectedViewModel.Id);
            }
            else
            {
                ShowPopupWindow("Cannot delete a product, since it is included in some purchase events in the system");
            }
        }

        private bool ProductHasNoPurchases()
        {
            return service.HasNoPurchases(SelectedViewModel.Id);
        }
        private bool ProductViewModelIsSelected()
        {
            return !(selectedViewModel is null);
        }

        private bool NonEmptyInputs()
        {
            return !string.IsNullOrEmpty(ProductName) && Price > 0 && !string.IsNullOrEmpty(Category);
            //when price is provided and then deleted, it's still possible to add products - to be fixed
        }

        private void ShowPopupWindow(string message)
        {
            MessageBoxShowDelegate(message);
        }


        #endregion
    }
}
