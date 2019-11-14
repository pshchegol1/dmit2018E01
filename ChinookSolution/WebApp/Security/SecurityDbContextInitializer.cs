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
using ChinookSystem.BLL;
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

            //Take roles from your database such as a positions table, or off some other data record
            //We have title column on the employees table which hold the roles
            EmployeeController sysmgr = new EmployeeController();

            List<string> employeeroles = sysmgr.Employees_GetTitles();
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

            //Hard Coding a new user
            string newUserPassword = ConfigurationManager.AppSettings["customerPassword"];
          
            result = userManager.Create(new ApplicationUser
            {
                UserName = "HansenB",
                Email = "HansenB@hotmail.somewhere.ca",
                CustomerId = 4
            }, newUserPassword);
            if (result.Succeeded)
                userManager.AddToRole(userManager.FindByName("HansenB").Id, "Customers");

            //Seeding Employees from the Employee table.
            //TODO:
            //Retrieve a List<Employee> from the databasse.
            //foreach employee
            //UserName such as LastName and FirstInitial possible add a number
            //Email of Employee or Null or add @Chinook.somewhere.ca
            //Employee ID is the pkey of the Employee record
            //Use the AppSettings newUserPassword for the password
            //Succeeded, role can come from the Employee record 

            #endregion

            // ... etc. ...

            base.Seed(context);
        }
    }
}