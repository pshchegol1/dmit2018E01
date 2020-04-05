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
    public class UnOrderedItemController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<UnOrderedItem> List_GetUnorderedItems()
        {
            using (var context = new eRaceContext())
            {
                return context.UnOrderedItems.ToList();
            }
        }

        public UnOrderedItem UnOrderdItem_GetItemID(int itemid)
        {
            using (var context = new eRaceContext())
            {
                return context.UnOrderedItems.Find(itemid);
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public int Insert_ReturnItems(UnOrderedItem item)
        {
            using (var context = new eRaceContext())
            {
                context.UnOrderedItems.Add(item);
                context.SaveChanges();

                return item.ItemID;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public int UnorderedItem_Delete(UnOrderedItem item)
        {
            return UnorderedItem_Delete(item.ItemID);
        }
        public int UnorderedItem_Delete(int itemid)
        {
            using (var context = new eRaceContext())
            {
                var existing = context.UnOrderedItems.Find(itemid);

                if (existing == null)
                {
                    throw new Exception("Item does not exist. No Delete required");
                }
                else
                {
                    context.UnOrderedItems.Remove(existing);

                    return context.SaveChanges();
                }
            }
        }

        public void Remove_UnorderedItems()
        {
            using (var context = new eRaceContext())
            {
                var removeunordereditems = (from x in context.UnOrderedItems
                                           select x).ToList();

                foreach (var removeitem in removeunordereditems)
                {
                    var RemovingItem = context.UnOrderedItems.Find(removeitem.ItemID);
                    var letsremoveitem = context.UnOrderedItems.Remove(RemovingItem);
                }
                context.SaveChanges();
            }
        }
    }
}
