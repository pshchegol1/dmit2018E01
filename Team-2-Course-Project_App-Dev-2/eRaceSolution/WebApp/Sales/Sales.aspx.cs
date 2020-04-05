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
using eRaceSystem.DAL;
#endregion

namespace WebApp.Sales
{
    public partial class Sales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RefundButton.Visible = false;
            RefundTotalsPanel.Visible = false;
            PanelInvoice.Visible = false;
            PanelPaymentButtons.Visible = false;

            //EmployeeName = null;
            if (Request.IsAuthenticated)
            {



                UserNameLabel2.Text = User.Identity.Name;
                if (User.IsInRole("Clerk"))
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



        protected void AddButtonn_Click(object sender, EventArgs e)
        {
            CartSalesGridView.Enabled = true;
            PanelPaymentButtons.Visible = true;
            Product products = new Product();
            products.QuantityOnOrder = int.Parse(QuantityTextBox.Text);

            if (string.IsNullOrEmpty(CategoryDDL.SelectedValue) || string.IsNullOrEmpty(ProductDDL.SelectedValue))
            {
                MessageUserControl.ShowInfo("Required Data", "Please Select Category and Product");
            }
            else if (CategoryDDL.SelectedIndex == 0)
            {
                MessageUserControl.ShowInfo("Please Select a Category");
            }
            //else if(ProductDDL.SelectedIndex == 0)
            //{
            //    MessageUserControl.ShowInfo("Please Select a Product");
            //}
            else if(products.QuantityOnOrder <= 0)
            {
                MessageUserControl.ShowInfo("Required Data", "Please Enter Quantity, Should be greater or equal 1");
                PanelPaymentButtons.Visible = false;
            }
            else
            {
                string category = CategoryDDL.SelectedValue;
                
                int qty = int.Parse(QuantityTextBox.Text);
                int productid = int.Parse(ProductDDL.SelectedValue);

                var username = User.Identity.Name;
                SecurityController securitymgr = new SecurityController();
                int? employeeid = securitymgr.GetCurrentUserEmployeeId(username);
                EmployeeController sysmgrs = new EmployeeController();
                Employee info = sysmgrs.Employee_Get(employeeid.Value);

                int employeeID = info.EmployeeID;

               

                
                MessageUserControl.TryRun(() =>
                {
                    ShoppingCartController sysmgr = new ShoppingCartController();
                    sysmgr.Add_ItemToCart(employeeID, productid, qty);

                    List<ShoppingCart> datainfo = sysmgr.ShoppingCart_OrderList();
                    CartSalesGridView.DataSource = datainfo;
                    CartSalesGridView.DataBind();
                }, "Adding Item", "Item has been added to the cart");

                var controller = new ShoppingCartController();
                var countTotal = controller.ShoppingCart_OrderList();
                SubtotalText.Text = countTotal.Sum(x => x.Quantity * x.Price).ToString("C");
                TaxText.Text = countTotal.Sum(t => t.Quantity * t.Price * decimal.Parse(0.05.ToString())).ToString("C");
                TotalText.Text = countTotal.Sum(tos => tos.Quantity * tos.Price * decimal.Parse(0.05.ToString()) + (tos.Quantity * tos.Price)).ToString("C");


            }
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            List<int> producttodelete = new List<int>();




        }

        protected void RefreshButton_Click(object sender, EventArgs e)
        {
            


        }

        protected void CartSalesGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Refresh"))
            {
                PanelPaymentButtons.Visible = true;
                int productid = int.Parse(e.CommandArgument.ToString());
                int qty = 0;
                for (int rowindex = 0; rowindex < CartSalesGridView.Rows.Count; rowindex++)
                {
                    if ((CartSalesGridView.Rows[rowindex].FindControl("ProductID") as Label).Text == e.CommandArgument.ToString())
                    {
                        qty = int.Parse((CartSalesGridView.Rows[rowindex].FindControl("Quantity") as TextBox).Text);
                    }
                }
                if( qty <= 0 )
                {
                    MessageUserControl.ShowInfo("Quantity Cannot be negative number or zero, Please add quantity or remove the item from your Cart.");
                }
                else
                {
                    var username = User.Identity.Name;
                    SecurityController securitymgr = new SecurityController();
                    int? employeeid = securitymgr.GetCurrentUserEmployeeId(username);
                    EmployeeController sysmgrs = new EmployeeController();
                    Employee info = sysmgrs.Employee_Get(employeeid.Value);

                    int employeeID = info.EmployeeID;

                    MessageUserControl.TryRun(() =>
                    {
                        ShoppingCartController sysmgr = new ShoppingCartController();

                        //Call for adjusting quantity
                        sysmgr.Quantity_Refresh(employeeID, productid, qty);

                        List<ShoppingCart> datainfo = sysmgr.ShoppingCart_OrderList();
                        CartSalesGridView.DataSource = datainfo;
                        CartSalesGridView.DataBind();
                    }, "Refreshing", "Item Quantity Updated");
                    var controller = new ShoppingCartController();
                    var countTotal = controller.ShoppingCart_OrderList();
                    SubtotalText.Text = countTotal.Sum(x => x.Quantity * x.Price).ToString();
                    TaxText.Text = countTotal.Sum(t => t.Quantity * t.Price * decimal.Parse(0.05.ToString())).ToString();
                    TotalText.Text = countTotal.Sum(tos => tos.Quantity * tos.Price * decimal.Parse(0.05.ToString()) + (tos.Quantity * tos.Price)).ToString();
                }

                


            }
            else
            {
                PanelPaymentButtons.Visible = true;
                int productid = int.Parse(e.CommandArgument.ToString());


                var username = User.Identity.Name;
                SecurityController securitymgr = new SecurityController();
                int? employeeid = securitymgr.GetCurrentUserEmployeeId(username);
                EmployeeController sysmgrs = new EmployeeController();
                Employee info = sysmgrs.Employee_Get(employeeid.Value);

                int employeeID = info.EmployeeID;

                MessageUserControl.TryRun(() =>
                {
                    ShoppingCartController sysmgr = new ShoppingCartController();

                    sysmgr.Delete_ProductItem(employeeID, productid);

                    List<ShoppingCart> datainfo = sysmgr.ShoppingCart_OrderList();
                    CartSalesGridView.DataSource = datainfo;
                    CartSalesGridView.DataBind();
                }, "Deleting Product", "Item Has been Removed from the Cart");
                var controller = new ShoppingCartController();
                var countTotal = controller.ShoppingCart_OrderList();
                SubtotalText.Text = countTotal.Sum(x => x.Quantity * x.Price).ToString("C");
                TaxText.Text = countTotal.Sum(t => t.Quantity * t.Price * decimal.Parse(0.05.ToString())).ToString("C");
                TotalText.Text = countTotal.Sum(tos => tos.Quantity * tos.Price * decimal.Parse(0.05.ToString()) + (tos.Quantity * tos.Price)).ToString("C");
            }
        }

        protected void ClearButton1_Click(object sender, EventArgs e)
        {

            var username = User.Identity.Name;
            SecurityController securitymgr = new SecurityController();
            int? employeeid = securitymgr.GetCurrentUserEmployeeId(username);
            EmployeeController sysmgrs = new EmployeeController();
            Employee info = sysmgrs.Employee_Get(employeeid.Value);

            int employeeID = info.EmployeeID;



            MessageUserControl.TryRun(() =>
            {
                ShoppingCartController sysmgr = new ShoppingCartController();

                sysmgr.ClearButton_Shopping(employeeID);

                List<ShoppingCart> datainfo = sysmgr.ShoppingCart_OrderList();
                CartSalesGridView.DataSource = datainfo;
               
                AddButtonn.Enabled = true;
                //InvoiceIDText.Text = null;
                //InvoiceIDText.Visible = false;
                CartSalesGridView.Enabled = true;
                CartSalesGridView.DataBind();
                QuantityTextBox.Text= "1";
                PanelInvoice.Visible = false;
                CategoryDDL.Enabled = true;
                ProductDDL.Enabled = true;
            }, "Empty", "Shopping Cart is Empty Now");


        }

        protected void PaymentButton1_Click(object sender, EventArgs e)
        {
           MessageUserControl.TryRun(() =>
           {
               PanelPaymentButtons.Visible = true;
              
               var username = User.Identity.Name;
               SecurityController securitymgr = new SecurityController();
               int? employeeid = securitymgr.GetCurrentUserEmployeeId(username);
               EmployeeController sysmgrs = new EmployeeController();
               Employee info = sysmgrs.Employee_Get(employeeid.Value);

               int employeeID = info.EmployeeID;
               var controller = new ShoppingCartController();
               var price = controller.ShoppingCart_OrderList();
               var totals = price.Sum(x => x.Amount).ToString();
               var ItemListCart = new List<ShoppingCart>();
              
               foreach (GridViewRow item in CartSalesGridView.Rows)
               {
                   var cartItems = new ShoppingCart();
                   var productid = (item.FindControl("ProductID") as Label).Text;
                   var qty = item.FindControl("Quantity") as TextBox;

                   cartItems.ProductID = int.Parse(productid);
                   cartItems.Quantity = int.Parse(qty.Text);

                   ItemListCart.Add(cartItems);

               }

               var cartcontroller = new ShoppingCartController();
               var countTotal = cartcontroller.ShoppingCart_OrderList();
               var subtotal = countTotal.Sum(x => x.Quantity * x.Price).ToString();

               controller.Payment_Order(employeeID, decimal.Parse(totals), ItemListCart);

              

             
               CartSalesGridView.Enabled = false;
               
               AddButtonn.Enabled = false;

               var ids = controller.InvoiceID_Display(employeeID);
               CategoryDDL.Enabled = false;
               ProductDDL.Enabled = false;
               PanelInvoice.Visible = true;
               InvoiceIDText.Text = ids.InvoiceID.ToString();
               InvoiceDateLabel1.Text = ids.Date.ToString();
               InvoiceTotalLabel2.Text = ids.Total.ToString();

           },"Success!","Thank you for shopping! Order Placed");
        }

        protected void InvoiceButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(OriginalInvoiceTexBox.Text))
            {
                MessageUserControl.ShowInfo("Please Enter Original Invoice Number to Display Information.");
            }
            else
            {
                RefundButton.Visible = true;
                RefundTotalsPanel.Visible = true;
                var controller = new InvoiceRefundsController();
                var refunddata = controller.InvoiceInformation_Table(int.Parse(OriginalInvoiceTexBox.Text));
                RefundsGridView.DataSource = refunddata;
                RefundsGridView.Visible = true;
                RefundsGridView.DataBind();

                var refundInvoiceController = new InvoiceRefundsController();
                int invoiceid = int.Parse(OriginalInvoiceTexBox.Text) ;
;                var countTotal = refundInvoiceController.InvoiceInformation_Table(invoiceid);
                RefundTextSubtotal.Text = countTotal.Sum(x => x.Quantity * x.Price - x.ReStockChg).ToString("C");
                RefundTaxText.Text = countTotal.Sum(t => t.Quantity * t.Price * decimal.Parse(0.05.ToString())).ToString("C");
                RefundTotalText.Text = countTotal.Sum(tos => tos.Quantity * tos.Price * decimal.Parse(0.05.ToString()) + (tos.Quantity * tos.Price)).ToString("C");
            }




        }

        protected void CategoryDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CategoryDDL.SelectedIndex == 0)
            {
                ProductDDL.SelectedIndex = 0;
                //ProductDDL.Enabled = false;
                CategoryDDL.DataBind();
                ProductDDL.DataBind();
            }
            else
            {
                ProductDDL.Enabled = true;
                
            }
        }

        protected void ClearInvoiceButton_Click(object sender, EventArgs e)
        {
            
            RefundsGridView.Visible = false;
        }
    }
}