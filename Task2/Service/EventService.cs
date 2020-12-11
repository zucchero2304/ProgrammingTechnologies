using System;
using System.Collections.Generic;
using System.Text;
using Data;

namespace Service
{
    class EventService
    {
        private PurchaseEventRepository purchaseRepository = new PurchaseEventRepository();

        public void AddPurchaseEvent()
        {

        }

        public List<PurchaseEvent> GetAllPurchases()
        {
            return purchaseRepository.GetAllPurchaseEvents();
        }

        public void GetAllClientPurchases(int id)
        {
            
        }

        public void GetAllProductPurchases(int id)
        {

        }
    }
}
