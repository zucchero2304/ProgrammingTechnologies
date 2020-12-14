using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Model;

namespace Presentation.ViewModel.Clients
{
    public class ClientEditViewModel : ViewModelBase
    {
        private int id;

        public int Id
        {
            get => id;

            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public ClientEditViewModel()
        {

        }
        public ClientEditViewModel(int clientId)
        {
            Id = clientId;
        }
    }
}
