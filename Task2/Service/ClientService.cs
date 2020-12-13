using System;
using System.Collections.Generic;
using Data;

namespace Service
{
    public class ClientService
    {
        ClientRepository clientRepository = new ClientRepository();
        PurchaseEventRepository eventRepository = new PurchaseEventRepository();

        public List<ClientModel> GetAllClients()
        {
            List<ClientModel> models = new List<ClientModel>();

            foreach (var client in clientRepository.GetAllClients())
            {
                models.Add(MapClientDetails(client));
            }
            return models;
        }

        public ClientModel GetClientById(int id)
        {
            return MapClientDetails(clientRepository.GetClientById(id));
        }

        public ClientModel GetClientByCredentials(string name, string surname)
        {
            return MapClientDetails(clientRepository.GetClientByCredentials(name, surname));
        }

        public ClientModel GetLastlyAddedClient()
        {
            return MapClientDetails(clientRepository.GetLastClient());
        }

        public void AddClient(ClientModel model)
        {
            clientRepository.AddClient(MapModelDetails(model));
        }

        public void DeleteClient(int id)
        {
            if (ClientExists(id) && HasNoPurchases(id))
            {
                clientRepository.DeleteClient(id);
            }
        }

        public void UpdateClient(ClientModel model)
        {
            if (ClientExists(model._id))
            {
                clientRepository.UpdateClient(MapModelDetails(model));
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

        private Client MapModelDetails(ClientModel model)
        {
            return new Client()
            {
                Id = model._id,
                FirstName = model._firstName,
                LastName = model._lastName
            };
        }

        private ClientModel MapClientDetails(Client client)
        {
            return new ClientModel()
            {
                _id = client.Id,
                _firstName = client.FirstName,
                _lastName = client.LastName
            };
        }
    }
}
