﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace Data
{
    public class ClientRepository
    {
        public List<Client> GetAllClients()
        {
            using (var db = new ShopDataContext())
            {
                return db.Clients.Select(client => client).ToList();
            }
        }

        public int GetNumberOfClients()
        {
            using (var db = new ShopDataContext())
            {
                return db.Clients.Select(client => client).ToList().Count;
            }
        }

        public Client GetClientById(int id)
        {
            using (var db = new ShopDataContext())
            {
                return db.Clients.FirstOrDefault(client => client.Id.Equals(id));
            } 
        }

        public Client GetClientByCredentials(string firstName, string lastName)
        {
            using (var db = new ShopDataContext())
            {
                return db.Clients
                    .FirstOrDefault(client => (client.FirstName.Equals(firstName) &&
                                               (client.LastName.Equals(lastName))));
            }
        }

        public void AddClient(Client client)
        {
            using (var db = new ShopDataContext())
            {
                db.Clients.InsertOnSubmit(client);
                db.SubmitChanges();
            }
        }

        public void DeleteClient(int id)
        {
            using (var db = new ShopDataContext())
            {
                Client clientToDelete = db.Clients.FirstOrDefault(client => client.Id.Equals(id));

                if (clientToDelete != null)
                {
                    db.Clients.DeleteOnSubmit(clientToDelete);
                    db.SubmitChanges();
                }
            }
        }

        public void UpdateClient(Client client)
        {
            using (var db = new ShopDataContext())
            {
                Client clientToUpdate = db.Clients.FirstOrDefault(c => c.Id.Equals(client.Id));

                if (clientToUpdate != null)
                {
                    clientToUpdate.FirstName = client.FirstName;
                    clientToUpdate.LastName = client.LastName;
                    db.SubmitChanges();
                }
            }
        }
    }
}
