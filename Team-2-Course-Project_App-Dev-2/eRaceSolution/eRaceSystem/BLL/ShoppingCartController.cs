using DMIT2018Common.UserControls;
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
    public class ShoppingCartController
    {
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<ShoppingCart>ShoppingCart_OrderList()
        {
            using (var context = new eRaceContext())
            {
                var results = (from x in context.SalesCartItems
                               where x.Product.ProductID == x.ProductID
                               select new ShoppingCart
                               {
                                   ProductID = x.ProductID,
                                   Product = x.Product.ItemName,
                                   Quantity = x.Quantity,
                                   Price = x.Product.ItemPrice,
                                   Amount = x.Quantity * x.Product.ItemPrice
                               });
                return results.ToList();
            }
        }


        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<SalesCartItem> SalesCartItem_List()
        {
            using (var context = new eRaceContext())
            {
                return context.SalesCartItems.ToList();
            }
        }








        public void Add_ItemToCart (int employeid , int productid, int qty)
        {
            if (qty < 0)
            {
                throw new Exception("Quantity cannot be zero");
            }
            else
            {
                //var buyItem = context.SalesCartItems.Add(new SalesCartItem());
                //buyItem.ProductID = productid;
                //buyItem.EmployeeID = employeid;
                //buyItem.Quantity = qty;
                using (var context = new eRaceContext())
                {
                    Product newProduct = null;
                    int product = 0;
                    Product products = new Product();

                    Product prdct = (from x in context.Products
                                           .Where(x => x.ProductID.Equals(productid))
                                            select x).FirstOrDefault();

                    SalesCartItem exists = (from x in context.SalesCartItems
                                           .Where(x => x.EmployeeID.Equals(employeid) && x.ProductID.Equals(productid))
                                            select x).FirstOrDefault();
                    if(exists == null)
                    {
                        exists = new SalesCartItem();
                        exists.EmployeeID = employeid;
                        exists.ProductID = productid;
                        exists.UnitPrice = prdct.ItemPrice;
                        exists.Quantity = qty;
                        
                        

                        exists = context.SalesCartItems.Add(exists);
                        product = 1;
                      
                    }
                    else
                    {
                        newProduct = exists.Product.Category.Products.SingleOrDefault(x => x.ProductID == productid);
                       
                        if(newProduct == null)
                        {
                            product = exists.Product.Category.Products.Count() + 1;
                        }
                        else
                        {

                            product = exists.Quantity++;
                        }
                    }


                    context.SaveChanges();
            
                } 
            }

        }//end of Add_ItemToCart




        public void Delete_ProductItem(int employeeid, int productid)
        {
            using (var context = new eRaceContext())
            {
                SalesCartItem exists = (from x in context.SalesCartItems
                       .Where(x => x.EmployeeID.Equals(employeeid) && x.ProductID.Equals(productid))
                                        select x).FirstOrDefault();

                var findItem = context.SalesCartItems.Where(x => x.ProductID == productid).FirstOrDefault();
                context.Entry(findItem).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();


            }
        }//end of Delete_ProductItem



        public void Quantity_Refresh(int employeeid,int productid, int quantity)
        {
            using(var context = new eRaceContext())
            {
                SalesCartItem exists = (from x in context.SalesCartItems
                                           .Where(x => x.EmployeeID.Equals(employeeid) && x.ProductID.Equals(productid))
                                        select x).FirstOrDefault();


                       
                exists.EmployeeID = employeeid;
                exists.ProductID = productid;
                exists.Quantity = quantity;

                context.Entry(exists).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();





            }
        }//End of Quantity_Refresh

        public void Clear_Cart(int employeeid)
        {
            using(var context = new eRaceContext())
            {

            }
        }


        public void Payment_Order(int employeeid, decimal total, List<ShoppingCart> info)
        {
            using(var context = new eRaceContext())
            {

                Invoice products = new Invoice();

                SalesCartItem cartItem = new SalesCartItem();

                SalesCartItem exists = (from x in context.SalesCartItems
                                       .Where(x => x.EmployeeID.Equals(employeeid))
                                  select x).FirstOrDefault();






                var controller = new ShoppingCartController();

                var totalCalculate = controller.ShoppingCart_OrderList();
               

                products.InvoiceID = products.InvoiceID;
                products.EmployeeID = employeeid;
                products.InvoiceDate = DateTime.Now;
                products.SubTotal = decimal.Parse(totalCalculate.Sum(x => x.Amount).ToString());
                products.GST = decimal.Parse(totalCalculate.Sum(t => t.Quantity * t.Price * decimal.Parse(0.05.ToString())).ToString());
                products.Total = total;

                products = context.Invoices.Add(products);
                context.SaveChanges();

                //InvoiceDetail infoInvoice = new InvoiceDetail();
                var invoiceDetails = context.InvoiceDetails.Add(new InvoiceDetail());

                foreach(var item in info)
                {
                    invoiceDetails.InvoiceID = products.InvoiceID;
                    var productfind = context.SalesCartItems.Where(x => x.Product.ProductID == item.ProductID).Single();
                    productfind.Product.QuantityOnHand = productfind.Product.QuantityOnHand - item.Quantity;

                    invoiceDetails.InvoiceDetailID = invoiceDetails.InvoiceDetailID++;
                    invoiceDetails.ProductID = productfind.ProductID;
                    invoiceDetails.Price = productfind.Product.ItemPrice;
                    invoiceDetails.Quantity = item.Quantity;

                    invoiceDetails = context.InvoiceDetails.Add(invoiceDetails);
                    context.SaveChanges();
                }
                


            }


        }



        public void ClearButton_Shopping (int employeeid)
        {
            using (var context = new eRaceContext())
            {
                SalesCartItem exists = (from x in context.SalesCartItems
                       .Where(x => x.EmployeeID.Equals(employeeid))
                                        select x).FirstOrDefault();

                var clearCart = context.SalesCartItems.Where(x => x.EmployeeID == employeeid).ToList();
                foreach(var item in clearCart)
                {
                    context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                }
                context.Entry(exists).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();


            }
        }


        public GetInvoiceID InvoiceID_Display (int employeeid)
        {
            using (var context = new eRaceContext())
            {
                var result = (from i in context.Invoices
                             where i.EmployeeID == employeeid
                             orderby i.InvoiceDate descending
                             select new GetInvoiceID
                             {
                                InvoiceID = i.InvoiceID,
                                Date = i.InvoiceDate,
                                Total = i.Total
                             });
                return result.First();
            }
        }



    }
}
