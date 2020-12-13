using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Presentation.Command;
using Presentation.Model;
using Service;

namespace Presentation.ViewModel
{
    public class ClientListViewModel : ObservableObject
    {

        private ClientService clientService;
        private ClientModel currentClient;

        private ObservableCollection<ClientModel> clients;
       /*     = new ObservableCollection<ClientModel>()
        {
            new ClientModel()
            {
                _id = 1,
                _firstName = "Liza",
                _lastName = "P"
            },
            new ClientModel()
            {
                _id = 1,
                _firstName = "Liza",
                _lastName = "P"
            },
            new ClientModel()
            {
                _id = 1,
                _firstName = "Liza",
                _lastName = "P"
            }
        };*/

        public ICommand FetchClientsData { get; private set; }

        public ClientListViewModel()
        {
            clientService = new ClientService();

            FetchClientsData = new RelayCommand(e =>
            {

                clients = new ObservableCollection<ClientModel>(clientService.GetAllClients());

            });
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

        public ClientModel CurrentClient
        {
            get => currentClient;
            set
            {
                currentClient = value;
                OnPropertyChanged("CurrentClient");
            }
        }

    }
}
