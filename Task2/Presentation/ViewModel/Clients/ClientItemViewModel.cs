using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Presentation.Command;
using Presentation.Model;
using Service;

namespace Presentation.ViewModel
{
    public class ClientItemViewModel : ViewModelBase
    {
        #region InitialSetup

        public ClientItemViewModel() { }

        public ClientItemViewModel(ClientModel client)
        {
            Id = client._id;
            FirstName = client._firstName;
            LastName = client._lastName;

            service = new ClientService();

            configureCommands();
        }

        private void configureCommands()
        {
            updateCommand = new RelayCommand(e => {UpdateClient();});
        }

        #endregion


        #region API
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

        public ICommand UpdateCommand
        {
            get => updateCommand;
        }

        #endregion


        #region PrivateAttributes

        private string firstName;
        private string lastName;
        private int id;

        private ClientService service;

        private ICommand updateCommand;

        #endregion


        #region PrivateMethods

        private void UpdateClient()
        {
            // not implemented yet
        }

        #endregion
    }
}
