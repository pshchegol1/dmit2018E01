using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#region Additional Namespaces
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Configuration;
using System.Data.Entity;
using WebApp.Models;
using eRaceSystem.BLL;
using eRace.Data.POCOs;
#endregion

namespace WebApp.Security
{
    public class SecurityDbContextInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            #region Seed the roles
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var startupRoles = ConfigurationManager.AppSettings["startupRoles"].Split(';');
            foreach (var role in startupRoles)
                roleManager.Create(new IdentityRole { Name = role });

            PositionsController sysmgr = new PositionsController();
            List<string> employeeroles = sysmgr.Positions_GetList();
            foreach (var role in employeeroles)
                roleManager.Create(new IdentityRole { Name = role });
            #endregion

            #region Seed the users
            string adminUser = ConfigurationManager.AppSettings["adminUserName"];
            string adminRole = ConfigurationManager.AppSettings["adminRole"];
            string adminEmail = ConfigurationManager.AppSettings["adminEmail"];
            string adminPassword = ConfigurationManager.AppSettings["adminPassword"];
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var result = userManager.Create(new ApplicationUser
            {
                UserName = adminUser,
                Email = adminEmail
            }, adminPassword);
            if (result.Succeeded)
                userManager.AddToRole(userManager.FindByName(adminUser).Id, adminRole);


            string userPassword = ConfigurationManager.AppSettings["customerPassword"];

            //result = userManager.Create(new ApplicationUser
            //{
            //    UserName = "HansenB",
            //    Email = "HansenB@hotmail.somewhere.ca",
            //    CustomerId = 4
            //}, userPassword);
            //if (result.Succeeded)
            //    userManager.AddToRole(userManager.FindByName("HansenB").Id, "Customers");
            

            EmployeeController sysmgremp = new EmployeeController();
            List<EmployeeList> employeeusernames = sysmgremp.ListOfEmployees();
            foreach (var username in employeeusernames)
            {
                result = userManager.Create(new ApplicationUser
                {
                    UserName = username.UserName,
                    Email = username.Email,
                    EmployeeId = username.EmployeeId
                }, userPassword);
                if (result.Succeeded)
                    userManager.AddToRole(userManager.FindByName(username.UserName).Id, username.Role);
            }
                #endregion
                // ... etc. ...

                base.Seed(context);
        }
    }
}