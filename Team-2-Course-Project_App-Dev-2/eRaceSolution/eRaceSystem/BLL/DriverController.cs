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
    public class DriverController
    {
        #region Queries
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<DriverList> List_DriversForSelectedRace(int raceid)
        {
            using (var context = new eRaceContext())
            {
                var results = from x in context.RaceDetails
                              where x.RaceID == raceid
                              select new DriverList
                              {
                                  RaceID = x.RaceID,
                                  RaceDetailID = x.RaceDetailID,
                                  FullName = x.Member.FirstName + " " + x.Member.LastName,
                                  RaceFee = x.RaceFee,
                                  RentalFee = x.RentalFee,
                                  Placement = x.Place == null ? 0 : x.Place,
                                  Refunded = x.Refund
                              };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<RaceDetail> RaceDetail_GetByRaceID(int raceid)
        {
            using (var context = new eRaceContext())
            {
                var results = from x in context.RaceDetails
                              where x.RaceID == raceid
                              select x;
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<CarClassList> CarClass_List()
        {
            using (var context = new eRaceContext())
            {
                var results = from x in context.CarClasses
                              select new CarClassList
                              {
                                 CarClassID = x.CarClassID,
                                 ClassName = x.CarClassName
                              };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<CarClass> CarClass_FindByCertification(char certificationlevel)
        {
            using (var context = new eRaceContext())
            {
                var results = from x in context.CarClasses
                              where x.CertificationLevel.Equals(certificationlevel)
                              select new CarClass
                              {
                                  CarClassID = x.CarClassID,
                                  CarClassName = x.CarClassName
                              };
                return results.ToList();
            }
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<CarList> Car_List()
        {
            using (var context = new eRaceContext())
            {
                var results = from x in context.Cars
                              select new CarList
                              {
                                  CarID = x.CarID,
                                  SerialNumber = x.SerialNumber,
                              };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<MemberList> Member_List()
        {
            using (var context = new eRaceContext())
            {
                var results = from x in context.Members
                              select new MemberList
                              {
                                  MemberID = x.MemberID,
                                  MemberName = x.FirstName + " " + x.LastName
                              };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Certification> CertificationLevel_FindByMember(int memberid)
        {
            using (var context = new eRaceContext())
            {
                var results = from x in context.Members
                              where x.MemberID == memberid
                              select new Certification
                              {
                                  CertificationLevel = x.CertificationLevel
                              };
                return results.ToList();
            }
        }



        #endregion

        #region CRUD
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public int Driver_Add(RaceDetail driver)
        {
            using (var context = new eRaceContext())
            { 
                    //yet to develop the business logic and error handling
                    context.RaceDetails.Add(driver);   //staging
                    context.SaveChanges();      //committed
                    return driver.RaceDetailID;        //return new id value
            }

        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public int Driver_Update(RaceDetail driver)
        {
            using (var context = new eRaceContext())
            {
                
                context.Entry(driver).State =
                System.Data.Entity.EntityState.Modified;
                return context.SaveChanges();  
            }

        }
        #endregion


    }
}
