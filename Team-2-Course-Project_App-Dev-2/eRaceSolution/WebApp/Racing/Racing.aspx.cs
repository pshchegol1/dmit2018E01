using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using eRaceSystem.BLL;
using eRace.Data.Entities;
using eRace.Data.POCOs;
using DMIT2018Common.UserControls;
using WebApp.Security;
#endregion

namespace WebApp.Racing
{
    public partial class Racing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //EmployeeName = null;
            if (Request.IsAuthenticated)
            {
                UserNameLabel2.Text = User.Identity.Name;
                if (User.IsInRole("Race Coordinator"))
                {
                    var username = User.Identity.Name;
                    SecurityController securitymgr = new SecurityController();
                    int? employeeid = securitymgr.GetCurrentUserEmployeeId(username);
                    if (employeeid.HasValue)
                    {
                        MessageUserControl.TryRun(() =>
                        {
                            EmployeeController sysmgr = new EmployeeController();
                            Employee info = sysmgr.Employee_Get(employeeid.Value);
                            //EmployeeName.Text = info.FullName;
                            UserNameLabel2.Text = info.FullName;
                        });
                    }
                    else
                    {
                        MessageUserControl.ShowInfo("Unregistered User", "This user is not a registered customer");
                        //EmployeeName.Text = "Unregistered User";
                    }
                }
                else
                {
                    //redirect to a page that states no authorization fot the request action
                    Response.Redirect("~/Security/AccessDenied.aspx");
                }
            }
            else
            {
                //redirect to login page
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            DateTime date = Calendar1.SelectedDate;
            MessageUserControl.TryRun(() =>
            {
                RaceController sysmgr = new RaceController();
                List<RaceList> datainfo = sysmgr.List_RacesForSelectedDate(date);

                //RaceListView.DataSourceID = null;
                //RaceGridView.DataSource = datainfo;
                SchedulePanel.Visible = true;
                RaceListView.DataBind();            
            });
        }

        protected void RaceDriver_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            //int raceid;
            //int.Parse(raceid) = (RaceListView.SelectedValue);
            MessageUserControl.TryRun(() =>
            {
                DriverController sysmgr = new DriverController();
                //List<RaceDetail> datainfo = sysmgr.RaceDetail_GetByRaceID(raceid);

                //RaceListView.DataSourceID = null;
                //DriverListView.DataSource = datainfo;
                RaceRosterPanel.Visible = true;
                DriverListView.DataBind();
                
                
            });
        }

        protected void MemberDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void DriverListView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int raceid = int.Parse(e.CommandArgument.ToString());
            int memberid;
            int carid;
            decimal racefee;
            decimal rentalfee;
            string employeeid = User.Identity.Name;
            MessageUserControl.TryRun(() => { });
            
                
        }

        protected void RecordRaceTimesButton_Click(object sender, EventArgs e)
        {
            MessageUserControl.TryRun(() =>
            {
                RaceController sysmgr = new RaceController();
                RaceRosterPanel.Visible = true;
                RaceTimesListView.DataBind();

            });
        }
    }
}