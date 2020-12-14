using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Presentation.Command;
using Presentation.Model;

namespace Presentation.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase selectedViewModel;

        private ICommand switchViewCommand;

        public MainViewModel()
        {
            selectedViewModel = new ClientListViewModel();
        }

        public ViewModelBase SelectedViewModel
        {
            get => selectedViewModel;
            set
            {
                selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public ICommand SwitchViewCommand
        {
            get
            {
                if (switchViewCommand == null)
                {
                    switchViewCommand = new RelayCommand(view => { SwitchView(view.ToString());});
                }

                return switchViewCommand;
            }
        }

        public void SwitchView(string viewName)
        {
            switch (viewName)
            {
                case "ClientListView":
                    ShowClientListView();
                    break;

                case "ProductListView":
                    ShowProductsView();
                    break;

                case "EventsListView":
                    ShowEventsView();
                    break;
            }
        }

        private void ShowClientListView()
        {
            SelectedViewModel = new ClientListViewModel();
        }

        private void ShowProductsView()
        {
            SelectedViewModel = new ProductListViewModel();
        }

        private void ShowEventsView()
        {
            // not implemented yet
        }
    }
}
