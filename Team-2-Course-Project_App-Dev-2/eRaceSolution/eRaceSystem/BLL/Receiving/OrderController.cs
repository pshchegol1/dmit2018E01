using eRace.Data.Entities;
using eRace.Data.POCOs;
using eRaceSystem.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.BLL.Receiving
{
    [DataObject]
    public class OrderController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<OrderList> List_VendorOrdersListSelection(int orderid)
        {
            using (var context = new eRaceContext())
            {
                var results = from x in context.OrderDetails
                              where x.OrderID == orderid
                              select new OrderList
                              {
                                  ProductID = x.ProductID,
                                  Item = x.Product.ItemName,
                                  QuantityOrdered = x.Quantity * x.OrderUnitSize,
                                  OrderedUnits = x.Quantity + " x case of " + x.OrderUnitSize,
                                  QuantityOutstanding = (x.Quantity * x.OrderUnitSize) - ((int?)x.ReceiveOrderItems.Sum(y => (int?)y.ItemQuantity) ?? 0),  // or (x.Product.QuantityOnHand / x.OrderUnitSize) ??????
                                  ReceivedUnitsLabel = " x case of " + x.OrderUnitSize
                              };
                return results.ToList();
            }
        }

        public void ReceivingShipment(int orderid, List<OrderList> orderlist)
        {
            using (var context = new eRaceContext())
            {
                var editorderid = context.OrderDetails.Find(orderid);

                // Form to Database
                foreach (var receive in orderlist)
                {
                    #region Added QuantityOnOrder And QuantityOnHand
                    var ReceiveItem = (from x in context.OrderDetails
                                       where x.ProductID == receive.ProductID
                                       select x).FirstOrDefault();
                    var quantityonhand = (from x in context.Products
                                          where x.ProductID == receive.ProductID
                                          select x.QuantityOnHand).FirstOrDefault();
                    ReceiveItem.Product.QuantityOnHand = quantityonhand + ((receive.ReceivedUnits * ReceiveItem.OrderUnitSize) + receive.SalvagedItems);
                    var quantityonorder = (from x in context.Products
                                           where x.ProductID == receive.ProductID
                                           select x.QuantityOnOrder).FirstOrDefault();
                    ReceiveItem.Product.QuantityOnOrder = quantityonorder - ((receive.ReceivedUnits * ReceiveItem.OrderUnitSize) + receive.SalvagedItems);
                    #endregion

                    #region Added ReturnOrderItems For Rejected Units and Reason
                    if (receive.RejectedUnits > 0 && receive.Reason != null)
                    {
                        //var newreturnorderitem = context.ReturnOrderItems.Add(new ReturnOrderItem());

                        //newreturnorderitem.OrderDetailID = ReceiveItem.OrderDetailID;
                        //newreturnorderitem.ItemQuantity = receive.RejectedUnits;
                        //newreturnorderitem.Comment = receive.Reason;
                    }
                    #endregion

                    #region Adding RecieveOrderItems 
                    var newrecieveorderitems = context.ReceiveOrderItems.Add(new ReceiveOrderItem());

                    var receiveorderid = (from x in context.ReceiveOrders
                                          where x.OrderID == orderid
                                          select x.ReceiveOrderID).FirstOrDefault();

                    newrecieveorderitems.ReceiveOrderID = receiveorderid;
                    newrecieveorderitems.OrderDetailID = ReceiveItem.OrderDetailID;
                    newrecieveorderitems.ItemQuantity = (receive.ReceivedUnits * ReceiveItem.OrderUnitSize) + receive.SalvagedItems;
                    #endregion

                    #region Checking If All QuantityOutstanding = 0 To Close Form
                    // intialize a variable to count the quantity outstanding to determine the if it would be zero or collect the quantity on order count
                    //if (quantityoutstanding.Count = 0)
                    //{
                    //    var closeorder = context.Orders.Find(editorderid);
                    //    closeorder.Closed = true;
                    //}
                    #endregion

                    context.Entry(ReceiveItem).State = EntityState.Modified;
                }

                // ListView To Database
                //for (int p = 0; p < UnorderedItemsListView.Items.Count; p++)
                //{
                    #region Add New UnOrderedItems to Database
                    //var newunordereditems = context.ReturnOrderItems.Add(new ReturnOrderItem());

                    //newunordereditems.OrderDetailID =
                    //newunordereditems.ItemQuantity = 
                    //newunordereditems.VendorProductID = 
                    #endregion
                //}

                context.SaveChanges();
            }
        }
    }
}
