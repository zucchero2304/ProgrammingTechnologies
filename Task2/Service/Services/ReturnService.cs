using Data;
using System.Collections.Generic;
using Data.Repositories;

namespace Service
{
    public class ReturnService
    {
        private ClientRepository clientRepository = new ClientRepository();
        private ProductRepository productRepository = new ProductRepository();
        private ReturnEventRepository returnRepository = new ReturnEventRepository();
        private PurchaseEventRepository purchaseRepository = new PurchaseEventRepository();

        public bool AddReturnEvent(ReturnEvent ev)
        {
            if (ev == null || InvalidEventData(ev))
            {
                return false;
            }

            PurchaseEvent recentPurchase = GetClientRecentPurchaseOfSuchProduct(ev);

            if (recentPurchase == null)
            {
                return false;
            }

            returnRepository.AddReturnEvent(ev);
            purchaseRepository.DeletePurchaseEvent(recentPurchase.Id);
            return true;
        }

        public List<ReturnEvent> GetAllReturns()
        {
            return returnRepository.GetAllReturnEvents();
        }

        public List<ReturnEvent> GetAllClientReturns(int id)
        {
            return returnRepository.GetReturnEventsByClientId(id);
        }

        public List<ReturnEvent> GetAllProductReturns(int id)
        {
            return returnRepository.GetReturnEventsByProductId(id);
        }

        private PurchaseEvent GetClientRecentPurchaseOfSuchProduct(ReturnEvent ev)
        {
            return purchaseRepository.GetMostRecentByClientIdAndProductId(ev.ClientId, ev.ProductId);
        }

        private bool InvalidEventData(ReturnEvent ev)
        {
            return !ClientExists(ev.ClientId) || !ProductExists(ev.ProductId);
        }

        private bool ClientExists(int id)
        {
            return clientRepository.GetClientById(id) != null;
        }

        private bool ProductExists(int id)
        {
            return productRepository.GetProductById(id) != null;
        }
    }
}
