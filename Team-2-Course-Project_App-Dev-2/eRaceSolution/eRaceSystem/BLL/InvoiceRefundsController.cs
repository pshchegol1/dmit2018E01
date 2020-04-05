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
    public class InvoiceRefundsController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<InvoiceRefunds> InvoiceInformation_Table(int invoiceid)
        {
           
            using (var context = new eRaceContext())
            {
                var results = from x in context.InvoiceDetails
                              where x.Invoice.InvoiceID == invoiceid
                              select new InvoiceRefunds
                              {
                                  InvoiceDetailID = x.InvoiceDetailID,
                                  Product = x.Product.ItemName,
                                  Quantity = x.Quantity,
                                  Price = x.Product.ItemPrice,
                                  Amount = x.Quantity * x.Product.ItemPrice,
                                  ReStockChg = x.Product.ReStockCharge
                                  //Reason = x.Product.StoreRefunds.Contains(x => x.Reason)

                              };
                return results.ToList();
                //premium = b.ScreenType.Premium == false ? "No":  "Yes"
            }
        }
    }
}
