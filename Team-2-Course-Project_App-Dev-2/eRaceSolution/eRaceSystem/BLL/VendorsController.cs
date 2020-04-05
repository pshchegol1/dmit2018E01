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
    public class VendorsController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        // This Method is to get a list of all the vendors
        public List<SelectionList> vendors_List()
        {
            using (var context = new eRaceContext())
            {
                var vendorList = from Vendor in context.Vendors
                                 select new SelectionList
                                 {
                                     IDValueField = Vendor.VendorID,
                                     DisplayText = Vendor.Name
                                 };
                return vendorList.ToList();
            }
        }


        public Vendor Purchasing_Vendor_Get(int vendorID)
        {
            using (var context = new eRaceContext())
            {
                var info = context.Vendors.Find(vendorID);
                return info;
            }
        }

        // Get Order Comments
        public Order Purchasing_VendorOrderComment_Get(int vendorID)
        {
            using (var context = new eRaceContext())
            {
                var info = context.Orders.Find(vendorID);
                return info;
            }
        }

        public RetrieveContact Vendor_Get(int vendorid)
        {
            using (var context = new eRaceContext())
            {
                var search = context.Orders.Find(vendorid);
                // if search == null

                return new RetrieveContact
                {
                    VendorName = search.Vendor.Name,
                    AddressandCity = search.Vendor.Address + " " + search.Vendor.City,
                    ContactName = search.Vendor.Contact,
                    Phone = search.Vendor.Phone
                };
            }
        }
    }
}
