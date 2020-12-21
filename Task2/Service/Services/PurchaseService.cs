using System.Collections.Generic;
using Data;

namespace Service
{
    public class PurchaseService
    {
        private PurchaseEventRepository purchaseRepository = new PurchaseEventRepository();
        private ClientRepository clientRepository = new ClientRepository();
        private ProductRepository productRepository = new ProductRepository();

        public bool AddPurchaseEvent(PurchaseEvent ev)
        {
            if (ev == null || InvalidEventData(ev))
            {
                return false;
            }

            purchaseRepository.AddPurchaseEvent(ev);
            return true;
        }

        public bool DeletePurchaseEvent(int id)
        {
            if (PurchaseExists(id))
            {
                purchaseRepository.DeletePurchaseEvent(id);
                return true;
            }

            return false;
        }

        public List<PurchaseEvent> GetAllPurchases()
        {
            return purchaseRepository.GetAllPurchaseEvents();
        }

        public PurchaseEvent GetPurchaseById(int id)
        {
            return purchaseRepository.GetPurchaseEventById(id);
        }

        public List<PurchaseEvent> GetAllClientPurchases(int id)
        {
            return purchaseRepository.GetPurchaseEventsByClientId(id);
        }

        public List<PurchaseEvent> GetAllProductPurchases(int id)
        {
            return purchaseRepository.GetPurchaseEventsByProductId(id);
        }

        public PurchaseEvent GetMostRecentPurchase()
        {
            return purchaseRepository.GetMostRecentPurchase();
        }

        public PurchaseEvent GetLastClientPurchaseOfProduct(int clientId, int productId)
        {
            return purchaseRepository.GetMostRecentByClientIdAndProductId(clientId, productId);
        }

        private bool InvalidEventData(PurchaseEvent ev)
        {
            return PurchaseExists(ev.Id) || !ClientExists(ev.ClientId) || !ProductExists(ev.ProductId);
        }

        public bool ClientExists(int id)
        {
            return clientRepository.GetClientById(id) != null;
        }

        public bool ProductExists(int id)
        {
            return productRepository.GetProductById(id) != null;
        }

        public bool PurchaseExists(int id)
        {
            return purchaseRepository.GetPurchaseEventById(id) != null;
        }
    }
}
