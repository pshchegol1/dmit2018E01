using eRace.Data.DTOs;
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
    public class CategoryController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<SelectionList> List_Category()
        {
            using (var context = new eRaceContext())
            {
                var results = from x in context.Categories
                              orderby x.Description
                              select new SelectionList
                              {
                                  IDValueField = x.CategoryID,
                                  DisplayText = x.Description
                              };
                return results.ToList();
            }
        }

        //public List<CategoryDTO> Category_CategoryAndVendors()
        //{

        //}
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<CategoryDTO> Category_VendorCatalog_Get(int vendorID)
        {
            using (var context = new eRaceContext())
            {
                var results = from vc in context.VendorCatalogs
                              group vc by vc.Product.Category.Description into Group
                              orderby Group.Key
                              select new CategoryDTO
                              {
                                  Category = Group.Key,
                                  Products = (from x in Group
                                              where x.VendorID == vendorID
                                              select new VendorCatalogPOCO
                                              {
                                                  //ProductId = x.Product.ProductID,
                                                  ProductName = x.Product.ItemName,
                                                  Reorder = x.Product.ReOrderLevel,
                                                  InStock = x.Product.QuantityOnHand,
                                                  // ItemPrice = x.Product.ItemPrice,
                                                  OnOrder = x.Product.QuantityOnHand,
                                                  Size = x.OrderUnitType + " " + "(" + x.OrderUnitSize + ")"
                                              }).ToList()

                              };

                return results.ToList();
            }
        }
    }
}
