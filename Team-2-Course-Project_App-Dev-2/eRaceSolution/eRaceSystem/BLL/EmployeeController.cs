using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using eRaceSystem.DAL;
using eRace.Data.Entities;
using System.ComponentModel;
using DMIT2018Common.UserControls;
using eRace.Data.POCOs;
using eRace.Data;    //DTOs;       we will add these when DTO folder is not empty
#endregion

namespace eRaceSystem.BLL
{
    [DataObject]
    public class EmployeeController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<SelectionList> Employee_ListNames()
        {
            using (var context = new eRaceContext())
            {
                var employeelist = from x in context.Employees
                                   orderby x.LastName, x.FirstName
                                   select new SelectionList
                                   {
                                       DisplayText = x.LastName + ", " + x.FirstName,
                                       IDValueField = x.EmployeeID
                                   };
                return employeelist.ToList();
            }
        }

        public List<EmployeeList> ListOfEmployees()
        {
            using (var context = new eRaceContext())
            {
                var listofemployees = from x in context.Employees
                                      select new EmployeeList
                                      {
                                          UserName = x.FirstName.Substring(0, 1) + x.LastName,
                                          Email = x.FirstName.Substring(0, 1) + x.LastName + "@erace.ca",
                                          EmployeeId = x.EmployeeID,
                                          Role = x.Position.Description

                                      };
                return listofemployees.ToList();

            }
        }

        public Employee Employee_Get(int employeeid)
        {
            using (var context = new eRaceContext())
            {
                return context.Employees.Find(employeeid);
            }
        }
    }
}
