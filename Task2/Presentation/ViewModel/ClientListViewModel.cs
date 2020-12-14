using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Presentation.Command;
using Presentation.Model;
using Service;

namespace Presentation.ViewModel
{
    public class ClientListViewModel: ViewModelBase
    {
        private int id;
        private string firstName;
        private string lastName;

        private ICommand addCommand;
        private ICommand updateCommand;
        private ICommand deleteCommand;
        private ICommand fetchAllCommand;

        private ClientService service;

        private ObservableCollection<ClientModel> clients;

        private ClientModel _selectedClient;

        public ClientListViewModel()
        {
            service = new ClientService();
            clients = new ObservableCollection<ClientModel>(service.GetAllClients());
        }

        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }

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
            get => _selectedClient;
            set
            {
                _selectedClient = value;
                OnPropertyChanged("SelectedClient");
            }
        }

        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand(e => { AddClient();},
                        o => NonEmptyInputs());
                }
                return addCommand;
            }
        }

        public ICommand FetchAllCommand
        {
            get
            {
                if (fetchAllCommand == null)
                {
                    fetchAllCommand = new RelayCommand(e => { FetchAll();});
                }
                return fetchAllCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new RelayCommand(e => {DeleteClient();},
                        o => ClientHasNoEvents() && ClientIsSelected());
                }
                return deleteCommand;
            }
        }

        private void AddClient()
        {
            ClientModel newClient = new ClientModel()
            {
                _id = Id,
                _firstName = FirstName,
                _lastName = LastName
            };
            
            service.AddClient(newClient); 
        }

        private void FetchAll()
        {
            clients = new ObservableCollection<ClientModel>(service.GetAllClients());
        }

        private void DeleteClient()
        {
            service.DeleteClient(SelectedClient._id);
        }

        private bool ClientHasNoEvents()
        {
            return service.HasNoEvents(SelectedClient._id);
        }

        private bool ClientIsSelected()
        {
            return !SelectedClient.Equals(null);
        }

        private bool NonEmptyInputs()
        {
            return !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName);
        }


        /*public ICommand DisplayMessageCommand
        {
            get
            {
                if (displayMessageCommand == null)
                {
                    displayMessageCommand = new RelayCommand(i =>
                    {
                        ShowPopupWindow();
                    });
                }

                return displayMessageCommand;
            }
        }*/

        //public Lazy<IWindow> ChildWindow { get; set; }

        //public Action<string> MessageBoxShowDelegate { get; set; }
        //    = x => throw new ArgumentOutOfRangeException($"The delegate {nameof(MessageBoxShowDelegate)} must be assigned by the view layer");


        /*private void ShowPopupWindow()
        {
            MessageBoxShowDelegate("Window");
        }
        
           private void ShowTreeViewMainWindow()
        {
            IWindow _child = ChildWindow.Value;
            _child.Show();
        }
        */
    }
}
