using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Presentation.Command;
using Presentation.Model;
using Presentation.ViewModel;
using Service;

namespace Presentation.ViewModel
{
    public class ClientListViewModel : ViewModelBase
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

            foreach (var c in service.GetAllClients())
            {
                clientViewModels.Add(new ClientItemViewModel(c));
            }
        }

        private void configureCommands()
        {
            addCommand = new RelayCommand(e => { AddClient(); },
                c => NonEmptyInputs());

            deleteCommand = new RelayCommand(e => { DeleteClient(); },
                c => ClientViewModelIsSelected());
        }

        #endregion


        #region API

        public string FirstName
        {
            get => newClientFirstName;
            set
            {
                newClientFirstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get => newClientLastName;
            set
            {
                newClientLastName = value;
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
            return !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName);
        }

        private void ShowPopupWindow(string message)
        {
            MessageBoxShowDelegate(message);
        }


        #endregion
        



        /* not used now 

         private ClientModel selectedClient;
         private ObservableCollection<ClientModel> clients;

         private bool isClientSelected;
         private ICommand fetchAllCommand;

         public ObservableCollection<ClientModel> Clients
         {
             get => clients;
             set
             {
                 clients = value;
                 OnPropertyChanged("Clients");
             }
         }

         public ClientModel SelectedClient
         {
             get => selectedClient;
             set
             {
                 selectedClient = value;
                 IsClientSelected = true;
                 OnPropertyChanged("SelectedClient");
             }
         }

         public ICommand FetchAllCommand
         {
             get
             {
                 if (fetchAllCommand == null)
                 {
                     fetchAllCommand = new RelayCommand(e => { FetchAll(); });
                 }
                 return fetchAllCommand;
             }
         }

         public bool IsClientSelected
         {
             get => isClientSelected;
             set
             {
                 isClientSelected = value;
                 OnPropertyChanged("IsClientSelected");
             }
         }
         private void FetchAll()
         {
             clients = new ObservableCollection<ClientModel>(service.GetAllClients());
         }

         */
    }
}
