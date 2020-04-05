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

namespace WebApp.Purchasing
{
    public partial class Purchasing : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            decimal subtotal = 0m;
            decimal tax = 0m;
            CalculateInitTotals(subtotal, tax);

            if (Request.IsAuthenticated)
            {
                UserNameLabel2.Text = User.Identity.Name;
                if (User.IsInRole("Director"))
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


        public void CalculateInitTotals(decimal subtotal, decimal tax)
        {

            decimal total = subtotal + tax;

            txtSubTotal.Text = string.Format("{0:C2}", subtotal);
            txtTax.Text = string.Format("{0:C2}", tax);
            txtTotal.Text = string.Format("{0:C2}", total);
        }


        #region Calculate Total from Selected Active Order
        public void CalculateTotal(OrderDetailsController controller, int vendorid)
        {
            controller = new OrderDetailsController();
            var countTotal = controller.Get_Active_Order(vendorid);
            var subtotal = countTotal.Sum(x => x.OrderQuantity * x.UnitCost);
            var tax = subtotal * (decimal)0.05;
            var total = subtotal + tax;

            txtSubTotal.Text = subtotal.ToString("c");
            txtTax.Text = tax.ToString("c");
            txtTotal.Text = total.ToString("c");


        }
        #endregion

        protected void btnSelectVendor_Click(object sender, EventArgs e)
        {
            if (VendorDDL.SelectedIndex == 0)
            {
                pnlInventory.Visible = false;
                pnlVendorResults.Visible = false;
                MessageUserControl.ShowInfo("Missing Data", "Select a vendor from the drop down list");
            }
            else
            {
                MessageUserControl.TryRun(() =>
                {
                    pnlInventory.Visible = true;
                    pnlVendorResults.Visible = true;
                    txtComments.Text = "Comments";

                    // Vendors Controller to get orders
                    VendorsController sysmgr = new VendorsController();
                    OrderDetailsController controller = new OrderDetailsController();
                    int vendorId = int.Parse(VendorDDL.SelectedValue);
                    var info = sysmgr.Purchasing_Vendor_Get(vendorId);
                    lblVendorName.Text = info.Name;
                    lblVendorContact.Text = info.Contact;
                    lblPhone.Text = info.Phone;
                    //txtComments.Text = info.Orders.Comment;
                    // Calculate Total
                    CalculateTotal(controller, vendorId);


                    var data = controller.Get_Active_Order(int.Parse(VendorDDL.SelectedValue));
                    OrderDetailsGridView.DataSource = data;
                    OrderDetailsGridView.DataBind();
                }, "Vendors Selection", "Active Order Retrieved");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            MessageUserControl.TryRun(() =>
            {

            }, "Saving", "Order Saved");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Purchasing/Purchasing.aspx");
        }

        protected void OrderDetailsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            OrderDetailsController controller = new OrderDetailsController();
            int vendorId = int.Parse(VendorDDL.SelectedValue);

            if (e.CommandName.Equals("Refresh"))
            {

                int OrderDetailID = int.Parse(e.CommandArgument.ToString());
                int qty = 0;
                decimal unitcost = 0;
                decimal perItemCost = 0;
                decimal unitSize = 0;
                Label yourlabel = null;

                for (int rowIndex = 0; rowIndex < OrderDetailsGridView.Rows.Count; rowIndex++)
                {
                    if ((OrderDetailsGridView.Rows[rowIndex].FindControl("lblOrderDetailID") as Label).Text == e.CommandArgument.ToString())
                    {
                        qty = int.Parse((OrderDetailsGridView.Rows[rowIndex].FindControl("txtQuantity") as TextBox).Text);

                        //string z = (OrderDetailsGridView.Rows[rowIndex].FindControl("txtUnitCost") as TextBox).Text;
                        //string zRemove = z.Remove(0, 0);
                        unitcost = decimal.Parse((OrderDetailsGridView.Rows[rowIndex].FindControl("txtUnitCost") as TextBox).Text);

                        string x = (OrderDetailsGridView.Rows[rowIndex].FindControl("lblPerItemCost") as Label).Text;
                        string xRemove = x.Remove(0, 1);
                        perItemCost = decimal.Parse(xRemove);

                        string y = (OrderDetailsGridView.Rows[rowIndex].FindControl("lblUnitSize") as Label).Text;
                        string yRemove = y.Remove(2);
                        unitSize = decimal.Parse(yRemove);


                        // For the warning message


                        yourlabel = (OrderDetailsGridView.Rows[rowIndex].FindControl("lblCostWarning") as Label);
                        //bind your label to data here




                    }
                }
                if (qty <= 0)
                {
                    MessageUserControl.ShowInfo("Quantity can not be a negative number");
                }
                else if (perItemCost > (unitcost / unitSize))
                {
                    MessageUserControl.ShowInfo("Per-item cost can not be greater than selling price");
                    yourlabel.Visible = true;
                    CalculateTotal(controller, vendorId);
                }
                else
                {

                    MessageUserControl.TryRun(() =>
                    {
                        controller.Quantity_Refresh(OrderDetailID, qty, unitcost);
                        List<ListVendorOrders> datainfo = controller.Get_Active_Order(vendorId);
                        OrderDetailsGridView.DataSource = datainfo;
                        OrderDetailsGridView.DataBind();
                        CalculateTotal(controller, vendorId);
                    }, "Refreshing", "Item Quantity Updated");


                }
            }
            else
            {
                int OrderDetailID = int.Parse(e.CommandArgument.ToString());
                MessageUserControl.TryRun(() =>
                {

                    controller.Delete_ProductItem(OrderDetailID);

                    List<ListVendorOrders> datainfo = controller.Get_Active_Order(vendorId);
                    OrderDetailsGridView.DataSource = datainfo;
                    OrderDetailsGridView.DataBind();
                    CalculateTotal(controller, vendorId);
                }, "Deleting Product", "Item Has been Removed from Order");
            }
        }



        // THIS METHOD ADD PRODUCTS FROM INVENTORY TO ORDER
        protected void vendorCategoryCatalog_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            // Collect the required data fo the event
            OrderDetailsController sysmgr = new OrderDetailsController();

            // obtain the productid Selected Inventory Item
            int productID = int.Parse(e.CommandArgument.ToString());

            // obtain the CurrentActive Order ID
            int orderID = int.Parse(((Label)OrderDetailsGridView.Rows[0].FindControl("lblOrderID")).Text);
            //txtComments.Text = "Order ID, this is for testing purpose: " + int.Parse(orderID);

            // Qty should be equal to 1 at first
            int qty = 1;

            // Obtain the vendor ID
            int vendorID = int.Parse(VendorDDL.SelectedValue.ToString());

            // Obtain the cost 
            decimal itemPrice = 5;

            MessageUserControl.TryRun(() =>
            {
                OrderDetailsController sysm = new OrderDetailsController();
                sysm.Add_ProductToOrder(productID, orderID, qty, itemPrice);

                List<ListVendorOrders> datainfo = sysm.Get_Active_Order(vendorID);
                OrderDetailsGridView.DataSource = datainfo;
                OrderDetailsGridView.DataBind();
                CalculateTotal(sysm, vendorID);

            }, "Adding to order", $"Product added to the order");


        }

        protected void ProductList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

            int orderID = int.Parse(((Label)OrderDetailsGridView.Rows[0].FindControl("lblOrderID")).Text);
            MessageUserControl.TryRun(() =>
            {

                OrderDetailsController controller = new OrderDetailsController();
                int vendorId = int.Parse(VendorDDL.SelectedValue);
                controller.Delete_ProductItem(orderID);

                List<ListVendorOrders> datainfo = controller.Get_Active_Order(vendorId);
                OrderDetailsGridView.DataSource = datainfo;
                OrderDetailsGridView.DataBind();
                CalculateTotal(controller, vendorId);
            }, "Deleting Order", "Order Has been Removed");
        }

    }
}