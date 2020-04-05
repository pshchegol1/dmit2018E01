using eRaceSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.BLL
{
    public class PositionsController
    {
        public List<string> Positions_GetList()
        {
            using (var context = new eRaceContext())
            {
                var results = from x in context.Positions
                              select x.Description;
                return results.ToList();
            }
        }
    }
}
