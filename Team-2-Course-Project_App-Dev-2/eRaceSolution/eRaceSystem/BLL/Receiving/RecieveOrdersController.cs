using eRace.Data.Entities;
using eRaceSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.BLL.Receiving
{
    public class RecieveOrdersController
    {
        public void AddingNewRecieveOrder(int orderid, int employeeid)
        {
            using (var context = new eRaceContext())
            {
                var addrecieveorder = context.ReceiveOrders.Add(new ReceiveOrder());

                addrecieveorder.OrderID = orderid;
                addrecieveorder.ReceiveDate = DateTime.Now;
                addrecieveorder.EmployeeID = employeeid;

                context.SaveChanges();
            }
        }
    }
}
