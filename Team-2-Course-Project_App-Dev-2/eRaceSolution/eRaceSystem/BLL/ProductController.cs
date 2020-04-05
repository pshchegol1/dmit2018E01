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
    public class ProductController
    {
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<SelectionList> List_Products(int categoryid)
        {
            using(var context = new eRaceContext())
            {
                var results = from x in context.Products
                              where x.CategoryID == categoryid
                              orderby x.ItemName
                              select new SelectionList
                              {
                                  IDValueField = x.ProductID,
                                  DisplayText = x.ItemName
                              };
                return results.ToList();
            }

        }


    }
}
