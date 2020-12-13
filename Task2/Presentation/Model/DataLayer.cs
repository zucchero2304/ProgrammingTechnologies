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
