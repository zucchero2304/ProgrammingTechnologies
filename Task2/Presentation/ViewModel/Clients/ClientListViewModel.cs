using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Presentation.Command;
using Presentation.Common;
using Presentation.Model;
using Presentation.ViewModel;
using Service;

namespace Presentation.ViewModel
{
    public class ClientListViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        #region InitialSetup
        public ClientListViewModel()
        {
            init();
            configureCommands();
        }

        private void init()
        {
            service = new ClientService();

            clientViewModels = new ObservableCollection<ClientItemViewModel>();

            IsClientViewModelSelected = false; 

            FetchClients();
        }

        private void configureCommands()
        {
            addCommand = new RelayCommand(e => { AddClient(); },
                condition => NonEmptyInputs());

            deleteCommand = new RelayCommand(e => { DeleteClient(); },
                condition => ClientViewModelIsSelected());
        }

        #endregion


        #region API

        public string FirstName
        {
            get => newClientFirstName;
            set
            {
                newClientFirstName = value;

                ValidateInput(newClientFirstName, nameof(FirstName));  

                OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get => newClientLastName;
            set
            {
                newClientLastName = value;

                ValidateInput(newClientLastName, nameof(LastName));

                OnPropertyChanged("LastName");
            }
        }

        public ObservableCollection<ClientItemViewModel> ClientViewModels
        {
            get => clientViewModels;

            set
            {
                clientViewModels = value;
                OnPropertyChanged("ClientViewModels");
            }
        }

        public ClientItemViewModel SelectedViewModel
        {
            get => selectedViewModel;
            set
            {
                selectedViewModel = value;
                OnPropertyChanged("SelectedViewModel");
                
                IsClientViewModelSelected = true;
            }
        }

        public bool IsClientViewModelSelected
        {
            get => isClientViewModelSelected;
            set
            {
                isClientViewModelSelected = value;
                OnPropertyChanged("IsClientViewModelSelected");
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

        private string newClientFirstName;
        private string newClientLastName;

        private ICommand addCommand;
        private ICommand deleteCommand;

        private ClientService service;
        private ClientItemViewModel selectedViewModel;
        private ObservableCollection<ClientItemViewModel> clientViewModels;

        private bool isClientViewModelSelected;

        #endregion


        #region PrivateMethods

        private void AddClient()
        {
            ClientModel newClient = new ClientModel()
            {
                _id = 0,
                _firstName = FirstName,
                _lastName = LastName
            };

            service.AddClient(newClient);
            FetchClients();

        }

        private void DeleteClient()
        {
            if (ClientHasNoEvents())
            {
                service.DeleteClient(SelectedViewModel.Id);
            }
            else
            {
                ShowPopupWindow("Cannot delete a client, since he has events registered in the system");
            }
        }

        private bool ClientHasNoEvents()
        {
            return service.HasNoEvents(SelectedViewModel.Id);
        }

        private bool ClientViewModelIsSelected()
        {
            return !(selectedViewModel is null);
        }

        private bool NonEmptyInputs()
        {
            return !(string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName));
        }

        private void ShowPopupWindow(string message)
        {
            MessageBoxShowDelegate(message);
        }


        private void FetchClients()
        {
            clientViewModels.Clear();

            Task.Run(() =>
            {
                foreach (var c in service.GetAllClients())
                {
                    clientViewModels.Add(new ClientItemViewModel(c));
                }
            });
            OnPropertyChanged("ClientViewModels");
        }


        private void ValidateInput(string field, string propertyName)
        {
            errorValidator.ClearErrors(propertyName);

            if (string.IsNullOrWhiteSpace(field))
            {
                errorValidator.AddError(propertyName, $"{propertyName} cannot be empty!");
            }
            else if (field.Length > 10)
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
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return errorValidator.GetErrors(propertyName);
        }

        public bool HasErrors => errorValidator.HasErrors;

        #endregion
    }
}
