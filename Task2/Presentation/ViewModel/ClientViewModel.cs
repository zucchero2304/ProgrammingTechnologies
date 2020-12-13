using System.Windows.Input;
using Presentation.Command;
using Presentation.Model;
using Service;

namespace Presentation.ViewModel
{
    public class ClientViewModel: ObservableObject
    {
        private int id;
        private string firstName;
        private string lastName;

        private ICommand addCommand;
        private ICommand updateCommand;
        private ICommand deleteCommand;

        private ClientService service;

        public ClientViewModel()
        {
            service = new ClientService();

            addCommand = new RelayCommand(o => { AddClient();});
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

        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand(c => { AddClient(); });
                }
                return addCommand;
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
    }
}
