using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Data;

namespace Service
{
    class ClientService
    {
        ClientRepository repository = new ClientRepository();
        PurchaseEventRepository eventRepository = new PurchaseEventRepository();

        public void AddClient(Client client)
        {
            repository.AddClient(client);
        }

        public void DeleteClient(int id)
        {
            if (HasNoPurchases(id))
            {
                repository.DeleteClient(id);
            }
            else
            {
                // ?
                // sql doesn't allow to delete a client who is referenced
                // as a foreign key in another table (events)
            }
        }

        private bool HasNoPurchases(int id)
        {
            return eventRepository.GetPurchaseEventsByClientId(id).Count.Equals(0);
        }

        public void UpdateClient(Client client)
        {
            repository.UpdateClient(client);
        }

        public List<Client> GetAllClients()
        {
            return repository.GetAllClients();
        }

        public Client GetClientById(int id)
        {
            return repository.GetClientById(id);
        }
    }
}
