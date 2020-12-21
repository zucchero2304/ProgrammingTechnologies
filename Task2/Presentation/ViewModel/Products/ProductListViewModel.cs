using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Presentation.Model;
using Service;
using Presentation.Command;
using Presentation.Common;
using System.ComponentModel;
using System.Collections;

namespace Presentation.ViewModel
{
    public class ProductListViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        #region InitialSetup
        public ProductListViewModel()
        {
            Init();
            ConfigureCommands();
        }

        private void Init()
        {
            service = new ProductService();

            productViewModels = new ObservableCollection<ProductItemViewModel>();

            FetchProducts();
        }

        private void ConfigureCommands()
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
                ValidateStringInput(newProductName, nameof(ProductName));
                OnPropertyChanged(nameof(ProductName));
            }
        }

        public double Price
        {
            get => newProductPrice;
            set
            {
                newProductPrice = value;
                ValidatePriceInput(newProductPrice, nameof(Price));
                OnPropertyChanged(nameof(Price));
            }
        }

        public string Category
        {
            get => newProductCategory;
            set
            {
                newProductCategory = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        public ObservableCollection<ProductItemViewModel> ProductViewModels
        {
            get => productViewModels;

            set
            {
                productViewModels = value;
                OnPropertyChanged(nameof(ProductViewModels));
            }
        }

        public ProductItemViewModel SelectedViewModel
        {
            get => selectedViewModel;
            set
            {
                selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
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

        public bool CanAdd => !HasErrors;


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

        private void FetchProducts()
        {
            productViewModels.Clear();

            foreach (var c in service.GetAllProducts())
            {
                productViewModels.Add(new ProductItemViewModel(c));
            }

            OnPropertyChanged(nameof(ProductViewModels));
        }

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
            FetchProducts();
        }

        private void DeleteProduct()
        {
            if (service.DeleteProduct(SelectedViewModel.Id))
            {
                ShowPopupWindow("Successfully deleted a product");
                FetchProducts();
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

        public bool ProductViewModelIsSelected()
        {
            return !(selectedViewModel is null);
        }

        private bool NonEmptyInputs()
        {
            return !string.IsNullOrEmpty(ProductName) && Price > 0 && !string.IsNullOrEmpty(Category);
        }

        private void ShowPopupWindow(string message)
        {
            MessageBoxShowDelegate(message);
        }


        private void ValidateStringInput(string field, string propertyName)
        {
            errorValidator.ClearErrors(propertyName);

            if (string.IsNullOrWhiteSpace(field))
            {
                errorValidator.AddError(propertyName, $"{propertyName} cannot be empty!");
            }
            else if (field.Length > 20)
            {
                errorValidator.AddError(propertyName, $"Maximum length of {propertyName} is 20!");
            }
        }

        private void ValidatePriceInput(double field, string propertyName)
        {
            errorValidator.ClearErrors(propertyName);

            if (field <= 0)
            {
                errorValidator.AddError(propertyName, $"{propertyName} has to be more than zero. It's a shop, not a charity!");
            }
            else if (field.ToString().Length > 10)
            {
                errorValidator.AddError(propertyName, $"Maximum length of {propertyName} is 10!");
            }
        }

        #endregion


        #region Validation

        private ErrorValidator errorValidator = new ErrorValidator();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private void ErrorsViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
            OnPropertyChanged(nameof(CanAdd));
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return errorValidator.GetErrors(propertyName);
        }

        public bool HasErrors => errorValidator.HasErrors;

        #endregion
    }
}
