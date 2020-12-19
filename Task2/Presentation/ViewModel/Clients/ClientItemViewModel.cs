using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Presentation.Command;
using Presentation.Common;
using Presentation.Model;
using Service;

namespace Presentation.ViewModel
{
    public class ClientItemViewModel : ViewModelBase, INotifyDataErrorInfo
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
            updateCommand = new RelayCommand(e => {UpdateClient();}, condition => NonEmptyInputs());
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

                errorValidator.ClearErrors(nameof(FirstName));

                if (firstName.Length > 5)
                {
                    errorValidator.AddError(nameof(FirstName), "Name's maximum length is 10!");
                }
                else if (string.IsNullOrWhiteSpace(firstName))
                {
                    errorValidator.AddError(nameof(FirstName), "Name can't be empty!");
                }

                OnPropertyChanged(nameof(FirstName));
            }
        }


        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;

                errorValidator.ClearErrors(nameof(LastName));

                if (lastName.Length > 10)
                {
                    errorValidator.AddError(nameof(LastName), "Surname's maximum length is 10!");
                } 
                else if (string.IsNullOrWhiteSpace(lastName))
                {
                    errorValidator.AddError(nameof(lastName), "Surname can't be empty!");
                }

                OnPropertyChanged(nameof(LastName));
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

        private ErrorValidator errorValidator = new ErrorValidator();

        private ICommand updateCommand;

        #endregion


        #region PrivateMethods

        private void UpdateClient()
        {
            service.UpdateClient(
                new ClientModel()
                {
                    _id = Id,
                    _firstName = FirstName,
                    _lastName = LastName
                });
        }

        private bool NonEmptyInputs()
        {
            return !(string.IsNullOrWhiteSpace(LastName) || string.IsNullOrWhiteSpace(FirstName));
        }

        #endregion


        #region Validation

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
    }

    #endregion
}
