using eRace.Data.Entities;
using eRace.Data.POCOs;
using eRaceSystem.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.BLL
{
    [DataObject]
    public class OrdersController
    {
        #region RecievingODS
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<OrderVendorList> List_VendorNamesWithOrderID()
        {
            using (var context = new eRaceContext())
            {
                var vendorlist = from x in context.Orders
                                 where x.Closed == false
                                 && x.OrderNumber != null
                                 select new OrderVendorList
                                 {
                                     DisplayText = x.OrderNumber + " - " + x.Vendor.Name,
                                     IDValueField = x.OrderID
                                 };
                return vendorlist.ToList();
            }
        }
        #endregion
        //[DataObjectMethod(DataObjectMethodType.Select, false)]
        //public List<ListVendorOrders> Get_Active_Order(int vendorID)
        //{
        //    using (var context = new eRaceContext())
        //    {
        //        var results = from x in context.OrderDetails
        //                      where x.Order.VendorID == vendorID && x.Order.OrderNumber == null
        //                      select new ListVendorOrders
        //                      {
        //                          ProductName = x.Product.ItemName,
        //                          OrderQuantity = x.Quantity,
        //                          UnitCost = x.Cost,
        //                          UnitSize = (x.OrderUnitSize).ToString() + "Per Case",
        //                          PerItemCost = x.Cost / x.OrderUnitSize,
        //                          ExtendedCost = x.Quantity * x.Cost
        //                      };
        //        return results.ToList();
        //    }
        //}
    }
}
