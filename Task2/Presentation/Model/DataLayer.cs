using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;

namespace Presentation.Model
{
    public class DataLayer
    {
        private IEnumerable<ClientModel> _clients;
        public DataLayer()
        {
            _clients = new List<ClientModel>()
            
            {
                new ClientModel() {FirstName = "Lizaveta", LastName = "Prakapovich"},
                new ClientModel() {FirstName = "Remigiusz", LastName = "Piwowarski"},
                new ClientModel() {FirstName = "Lizaveta2", LastName = "Prakapovich2"},
                new ClientModel() {FirstName = "Remigiusz2", LastName = "Piwowarski2"}
            };
        }

        public IEnumerable<ClientModel> GetClients()
        {
            return _clients;
        }

        public void Save(IEnumerable<ClientModel> clients)
        {
            _clients = clients;
        }
    }
}
