﻿using System;
using System.Collections.Generic;
using System.Text;
using Data;

namespace Service
{
    public class PurchaseService
    {
        private PurchaseEventRepository purchaseRepository = new PurchaseEventRepository();
        private ClientRepository clientRepository = new ClientRepository();
        private ProductRepository productRepository = new ProductRepository();

        public void AddPurchaseEvent(PurchaseEvent ev)
        {
            if (ClientExists(ev.ClientId) && ProductExists(ev.ProductId) && !PurchaseExists(ev.Id))
            {
                purchaseRepository.AddPurchaseEvent(ev);
            }
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

        public PurchaseEvent GetLastClientPurchaseOfProduct(int clientId, int productId)
        {
            return purchaseRepository.GetMostRecentByClientIdAndProductId(clientId, productId);
        }

        private bool ClientExists(int id)
        {
            return clientRepository.GetClientById(id) != null;
        }

        private bool ProductExists(int id)
        {
            return productRepository.GetProductById(id) != null;
        }

        private bool PurchaseExists(int id)
        {
            return purchaseRepository.GetPurchaseEventById(id) != null;
        }
    }
}
