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
using eRaceSystem.BLL.Receiving;
#endregion

namespace WebApp.Receiving
{
    public partial class Receiving : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //EmployeeName = null;
            if (Request.IsAuthenticated)
            {

                UserNameLabel2.Text = User.Identity.Name;
                if (User.IsInRole("Food Service"))
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

        protected void VendorFetch_Click(object sender, EventArgs e)
        {
            if (VendorDDL.SelectedValue == null)
            {
                MessageUserControl.ShowInfo("Missing Data", "Select a PO from the drop down list");
            }
            else
            {
                MessageUserControl.TryRun(() =>
                {
                    UnorderedItemsListView.Items.Clear();
                    UnOrderedItemController sysmanager = new UnOrderedItemController();
                    sysmanager.Remove_UnorderedItems();
                    UnorderedItemsListView.DataBind();

                    ReceivingPanel.Visible = true;

                    VendorsController sysmgr = new VendorsController();
                    var info = sysmgr.Vendor_Get(int.Parse(VendorDDL.SelectedValue));

                    VendorName.Text = info.VendorName;
                    AddressAndCity.Text = info.AddressandCity;
                    Contact.Text = info.ContactName;
                    PhoneNumber.Text = info.Phone;

                    ReasonCommentReasonMessage.Text = "";
                    // also need to make sure if quantity oustanding if = 0 make it not show outstanding for that specific row
                }, "Orders Search", "Order selected successful");
            }
        }

        protected void ForceClose_Click(object sender, EventArgs e)
        {
            var CloseOrder = new List<OrderList>();
            var OrderID = int.Parse(VendorDDL.SelectedValue);
            var reason = ReasonCommentReasonMessage.Text;

            if (string.IsNullOrEmpty(ReasonCommentReasonMessage.Text) || OrderID == 0)
            {
                MessageUserControl.ShowInfo("Must enter a reason for why you would like to close this order and make sure you have a vendor selected");
            }
            else
            {
                var closeorder = new OrderList();

                foreach (GridViewRow row in ReceivingGridView.Rows)
                {
                    var closelist = new OrderList();

                    var ProductID = row.FindControl("ProductID") as Label;
                    var QuantityOutstanding = row.FindControl("QuantityOutstanding") as Label;

                    closelist.ProductID = int.Parse(ProductID.Text);
                    closelist.QuantityOutstanding = int.Parse(QuantityOutstanding.Text);
                    closelist.QuantityOutstanding = 0;

                    CloseOrder.Add(closelist);
                }

                ReceivingPanel.Visible = false;

                MessageUserControl.TryRun(() =>
                {
                    var closeordercontroller = new OrderDetailsController();
                    closeordercontroller.Force_Closure(OrderID, reason);
                }, "Closure Message Sucess", "The order is now closed");

                VendorDDL.DataBind();
            }
        }

        protected void RecieveShipmentButton_Click(object sender, EventArgs e)
        {
            var OrderList = new List<OrderList>();
            var OrderID = int.Parse(VendorDDL.SelectedValue);

            if (OrderID == 0)
            {
                MessageUserControl.ShowInfo("Must Select an order from the drop down list");
            }
            else
            {
                //else
                //{
                foreach (GridViewRow row in ReceivingGridView.Rows)
                {
                    var orderlist = new OrderList();

                    var ProductID = row.FindControl("ProductID") as Label;
                    var RecievedUnits = row.FindControl("RecievedUnits") as TextBox;
                    var RejectedUnits = row.FindControl("RejectedUnits") as TextBox;
                    var Reason = row.FindControl("Reason") as TextBox;
                    var SalvagedItems = row.FindControl("SalvagedItems") as TextBox;

                    orderlist.ProductID = int.Parse(ProductID.Text);
                    orderlist.ReceivedUnits = int.Parse(RecievedUnits.Text);
                    orderlist.RejectedUnits = int.Parse(RejectedUnits.Text);
                    orderlist.Reason = Reason.Text;
                    orderlist.SalvagedItems = int.Parse(SalvagedItems.Text);

                    //if (int.Parse(RecievedUnits.Text) <= )
                    //{

                    //}
                    if (orderlist.ReceivedUnits > 0 || orderlist.RejectedUnits > 0)
                    {
                        OrderList.Add(orderlist);
                    }
                }

                ReceivingPanel.Visible = false;

                MessageUserControl.TryRun(() =>
                {
                    #region Creating New RecieveOrder
                        //Creating Recieve Order (Grab employeeid of this account) (Works)
                        var username = User.Identity.Name;
                    SecurityController securitymgr = new SecurityController();
                    int? employeeid = securitymgr.GetCurrentUserEmployeeId(username);
                    EmployeeController sysmgrs = new EmployeeController();
                    Employee info = sysmgrs.Employee_Get(employeeid.Value);
                    int employeeID = info.EmployeeID;
                    var addcontroller = new RecieveOrdersController();
                    addcontroller.AddingNewRecieveOrder(OrderID, employeeID);
                    #endregion

                    #region Putting form textboxs into database
                    var controller = new OrderController();
                    controller.ReceivingShipment(OrderID, OrderList);
                    #endregion

                    #region Refresh UnOrderItems CRUD
                    UnorderedItemsListView.Items.Clear();
                    UnOrderedItemController sysmanager = new UnOrderedItemController();
                    sysmanager.Remove_UnorderedItems();
                    UnorderedItemsListView.DataBind();
                    #endregion
                }, "Receiving Shipment Has Been Saved", "Transaction Complete");

                VendorDDL.DataBind();
                //}
            }
        }
    }
}