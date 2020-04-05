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
using eRace.Data;
#endregion

namespace eRaceSystem.BLL
{
    [DataObject]
    public class RaceController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<RaceList> List_RacesForSelectedDate(DateTime date)
        {
            using (var context = new eRaceContext())
            {
                var results = from x in context.Races
                              where x.RaceDate.Month == date.Month
                              && x.RaceDate.Day == date.Day
                              select new RaceList
                              {
                                  RaceID = x.RaceID,
                                  RaceTime = x.RaceDate,
                                  Competition = x.Certification.Description + " - " + x.Comment,
                                  Run = x.Run,
                                  NumberOfDrivers = x.RaceDetails.Count()
                              };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<PenaltyList> Penalties_List()
        {
            using (var context = new eRaceContext())
            {
                var results = from x in context.RacePenalties
                              select new PenaltyList
                              {
                                  Description = x.Description
                              };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<TimesList> List_TimesForSelectedRaceDetail(int racedetailid)
        {
            using (var context = new eRaceContext())
            {
                var results = from x in context.RaceDetails
                              where x.RaceDetailID == racedetailid
                              select new TimesList
                              {
                                  RaceDetailID = x.RaceDetailID,
                                  FullName = x.Member.FirstName + " " + x.Member.LastName,
                                  Time = x.RunTime,
                                  Penalty = x.PenaltyID,
                              };
                return results.ToList();
            }
        }

        public void Add_DriverToRaceAndGenerateInvoice(int raceid, int memberid, int carid, int employeeid, decimal racefee, decimal rentalfee)
        {
            using (var context = new eRaceContext())
            {
                List<string> reasons = new List<string>();
                RaceDetail exists = (from x in context.RaceDetails
                                     where x.MemberID == memberid
                                     select x).FirstOrDefault();

                if (exists == null)
                {
                    exists = new RaceDetail();
                    exists.RaceID = raceid;
                    exists.MemberID = memberid;
                    exists.RaceFee = racefee;
                    exists.RentalFee = rentalfee;
                    exists.CarID = carid;


                    exists = context.RaceDetails.Add(exists);
                }
                else
                {
                    reasons.Add("Driver already registered for this race");
                }

                if (reasons.Count() > 0)
                {
                    throw new BusinessRuleException("Adding driver to race", reasons);
                }
                else
                {
                    Invoice newInvoice = new Invoice();
                    newInvoice.EmployeeID = employeeid;
                    newInvoice.InvoiceDate = DateTime.Today;
                    newInvoice.SubTotal = racefee + rentalfee;
                    context.Invoices.Add(newInvoice);

                    context.SaveChanges();
                }

                
            }
        }
    }
        
}
