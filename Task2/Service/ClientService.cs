using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Data;

namespace Service
{
    public class ClientService
    {
        ClientRepository clientRepository = new ClientRepository();
        PurchaseEventRepository eventRepository = new PurchaseEventRepository();

        public List<Client> GetAllClients()
        {
            return clientRepository.GetAllClients();
        }

        public Client GetClientById(int id)
        {
            return clientRepository.GetClientById(id);
        }

        public Client GetClientByCredentials(string name, string surname)
        {
            return clientRepository.GetClientByCredentials(name, surname);
        }

        public Client GetLastlyAddedClient()
        {
            return clientRepository.GetLastClient();
        }

        public void AddClient(Client client)
        {
            clientRepository.AddClient(client);
        }

        public void DeleteClient(int id)
        {
            if (ClientExists(id) && HasNoPurchases(id))
            {
                clientRepository.DeleteClient(id);
            }
            else
            {
                // sql doesn't allow to delete a client who is referenced
                // as a foreign key in another table (events)
            }
        }

        public void UpdateClient(Client client)
        {
            if (ClientExists(client.Id))
            {
                clientRepository.UpdateClient(client);
            }
        }

        private bool HasNoPurchases(int id)
        {
            return eventRepository.GetPurchaseEventsByClientId(id).Count.Equals(0);
        }

        private bool ClientExists(int id)
        {
            return clientRepository.GetClientById(id) != null;
        }
    }
}
