using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class PurchaseEventRepository
    {
        public List<PurchaseEvent> GetAllPurchaseEvents()
        {
            using (var db = new ShopDataContext())
            {
                return db.PurchaseEvents.Select(ev => ev).ToList();
            }
        }

        public PurchaseEvent GetPurchaseEventById(int id)
        {
            using (var db = new ShopDataContext())
            {
                return db.PurchaseEvents.FirstOrDefault(ev => ev.Id.Equals(id));
            }
        }

        public List<PurchaseEvent> GetPurchaseEventsByClientId(int id)
        {
            using (var db = new ShopDataContext())
            {
                return db.PurchaseEvents.Where(ev => ev.ClientId.Equals(id)).ToList();
            }
        }

        public List<PurchaseEvent> GetPurchaseEventsByProductId(int id)
        {
            using (var db = new ShopDataContext())
            {
                return db.PurchaseEvents.Where(ev => ev.ProductId.Equals(id)).ToList();
            }
        }

        public void AddPurchaseEvent(PurchaseEvent e)
        {
            using (var db = new ShopDataContext())
            { 
                db.PurchaseEvents.InsertOnSubmit(e);
                db.SubmitChanges();
            }
        } 

        public void UpdatePurchaseEvent(PurchaseEvent e)
        {
            using (var db = new ShopDataContext())
            {
                PurchaseEvent eventToUpdate = db.PurchaseEvents.FirstOrDefault(ev => ev.Id.Equals(e.Id));
                eventToUpdate.ClientId = e.ClientId;
                eventToUpdate.ProductId = e.ProductId;
                eventToUpdate.EventDate = eventToUpdate.EventDate;
                db.SubmitChanges();
            }
        }
    }
}
