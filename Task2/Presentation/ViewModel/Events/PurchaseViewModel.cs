using Presentation.Model;
using Data;

namespace Presentation.ViewModel
{
    public class PurchaseViewModel : ViewModelBase
    {

        #region InitialSetup

        public PurchaseViewModel() { }

        public PurchaseViewModel(PurchaseEvent purchaseEvent)
        {
            Id = purchaseEvent.Id;
            ProductId = purchaseEvent.ProductId;
            ClientId = purchaseEvent.ClientId;
            Date = purchaseEvent.EventDate;

        }

        #endregion


        #region API
        public int Id
        {
            get => id;

            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public int ProductId
        {
            get => productId;
            set
            {
                productId = value;
                OnPropertyChanged(nameof(ProductId));
            }
        }

        public int ClientId
        {
            get => clientId;
            set
            {
                clientId = value;
                OnPropertyChanged(nameof(ClientId));
            }
        }

        public string Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        #endregion


        #region PrivateAttributes

        private int id;
        private int productId;
        private int clientId;
        private string date;

        #endregion

    }
}
