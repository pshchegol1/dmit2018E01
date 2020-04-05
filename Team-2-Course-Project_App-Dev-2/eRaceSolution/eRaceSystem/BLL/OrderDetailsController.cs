using eRace.Data.POCOs;
using eRaceSystem.DAL;
using eRace.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.BLL
{
    public class OrderDetailsController
    {
        #region Receiving Force Closure
        public void Force_Closure(int orderid, string comment)
        {
            using (var context = new eRaceContext())
            {
                var ForceClosure = from x in context.OrderDetails
                                   where x.OrderID == orderid
                                   //select x;
                                   select x.OrderDetailID;

                //var OrderDetail = context.OrderDetails.Find(orderid);
                //var OrderDetailQuantity = OrderDetail.Quantity;

                //var results = (from x in context.ReceiveOrderItems
                //               where x.OrderDetail.OrderID == orderid
                //               select x.OrderDetailID).ToList();

                //foreach (var order in orderlist)
                //{
                //    var quantityzero = from x in context.OrderDetails
                //                       where 
                //}

                var commitClosure = context.Orders.Find(orderid);
                commitClosure.Comment = comment;
                commitClosure.Closed = true;

                context.Entry(commitClosure).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        #endregion


        #region Purchasing System Order details

        #region List Vendors
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<OrderVendorList> List_VendorNamesWithOrderID()
        {
            using (var context = new eRaceContext())
            {
                var vendorlist = from x in context.Orders
                                 where x.Closed == false
                                 select new OrderVendorList
                                 {
                                     DisplayText = x.OrderNumber + " - " + x.Vendor.Name,
                                     IDValueField = x.OrderID
                                 };
                return vendorlist.ToList();
            }
        }
        #endregion

        #region  Get Current Active Order
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ListVendorOrders> Get_Active_Order(int vendorID)
        {
            using (var context = new eRaceContext())
            {
                var results = from x in context.OrderDetails
                              where x.Order.VendorID == vendorID && x.Order.OrderNumber == null && x.Order.OrderDate == null
                              select new ListVendorOrders
                              {
                                  OrderDetailID = x.OrderDetailID,
                                  ProductID = x.ProductID,
                                  OrderID = x.OrderID,
                                  ProductName = x.Product.ItemName,
                                  OrderQuantity = x.Quantity,
                                  UnitCost = x.Cost,
                                  UnitSize = x.OrderUnitSize,
                                  PerItemCost = x.Cost / x.OrderUnitSize,
                                  ExtendedCost = x.Quantity * x.Cost
                              };
                return results.ToList();
            }
        }
        #endregion

        #region Qty Refresh
        public void Quantity_Refresh(int orderDetailID, int quantity, decimal unitCost)
        {
            using (var context = new eRaceContext())
            {
                OrderDetail exists = (from x in context.OrderDetails
                                           .Where(x => x.OrderDetailID.Equals(orderDetailID))
                                      select x).FirstOrDefault();
                exists.OrderDetailID = orderDetailID;
                exists.Quantity = quantity;
                exists.Cost = unitCost;
                context.Entry(exists).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }//End of Quantity_Refresh
        #endregion

        #region Add product to Order
        public void Add_ProductToOrder(int productid, int orderid, int qty, decimal itemPrice)
        {
            using (var context = new eRaceContext())
            {
                OrderDetail newOrder = null;
                int order = 0;
                OrderDetail orderDetails = new OrderDetail();

                OrderDetail exists = (from x in context.OrderDetails
                                            .Where(x => x.ProductID.Equals(productid))
                                      select x).FirstOrDefault();
                if (exists == null)
                {
                    exists = new OrderDetail();
                    exists.ProductID = productid;
                    exists.OrderID = orderid;
                    exists.Quantity = qty;
                    exists.Cost = itemPrice;

                    exists = context.OrderDetails.Add(exists);
                    order = 1;
                }
            }
        }
        #endregion
        #region Delete item from order
        public void Delete_ProductItem(int OrderDetailID)
        {
            using (var context = new eRaceContext())
            {
                OrderDetail exists = (from x in context.OrderDetails
                       .Where(x => x.OrderDetailID.Equals(OrderDetailID))
                                      select x).FirstOrDefault();

                var findItem = context.OrderDetails.Where(x => x.OrderDetailID == OrderDetailID).FirstOrDefault();
                context.Entry(findItem).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();


            }
        }//end of Delete_ProductItem

        public void Delete_Order(int OrderID)
        {
            using (var context = new eRaceContext())
            {
                OrderDetail exists = (from x in context.OrderDetails
                       .Where(x => x.OrderID.Equals(OrderID))
                                      select x).FirstOrDefault();

                var findItem = context.OrderDetails.Where(x => x.OrderDetailID == OrderID).FirstOrDefault();
                context.Entry(findItem).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();


            }
        }//end of Delete_ProductItem
        #endregion
        #endregion
    }
}
