using Data;
using System;
using System.Collections.Generic;
using System.Text;
using Data.Repositories;

namespace Service
{
    public class ReturnService
    {
        private ClientRepository clientRepository = new ClientRepository();
        private ProductRepository productRepository = new ProductRepository();
        private ReturnEventRepository returnRepository = new ReturnEventRepository();
        private PurchaseEventRepository purchaseRepository = new PurchaseEventRepository();

        public void AddReturnEvent(ReturnEvent ev)
        {
            PurchaseEvent recentPurchase = GetClientRecentPurchaseOfSuchProduct(ev);

            if (recentPurchase != null)
            {
                returnRepository.AddReturnEvent(ev);
                purchaseRepository.DeletePurchaseEvent(recentPurchase.Id);
            }
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
    }
}
