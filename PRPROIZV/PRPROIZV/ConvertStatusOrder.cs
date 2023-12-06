using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRPROIZV
{
    internal class ConvertStatusOrder
    {
        public static void ConvertStatus()
        {
            using (var context = new VinidiktovDEMnovemberEntities())
            {
                var orders = context.Order.Where(o => o.Status == 1);

                foreach (var order in orders)
                {
                    DateTime closeTime = order.DateCreation.Add(order.OrderTime).AddMinutes(order.RentalTime);

                    if (closeTime <= DateTime.Now && order.ClosingDate.Value.Date <= DateTime.Now.Date)
                    {
                        order.Status = 2;
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
