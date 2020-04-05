<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sales.aspx.cs" Inherits="WebApp.Sales.Sales" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    <h1>Sales</h1>

    <div class="row">
        <div style="text-align: right">
            <asp:Label ID="Label1" runat="server" Font-Size="XX-Large" Text=""><i class="fa fa-user-circle" aria-hidden="true"></i></asp:Label>
            <asp:Label ID="UserNameLabel2" runat="server" Font-Size="Large" ForeColor="tomato" Text=""></asp:Label>
        </div>
        <hr />
    </div>
    <br />
    <br />
    <%--========================================================Controls===========================================================================--%>

     <div class="col-md-6">
    <asp:DropDownList ID="CategoryDDL" runat="server" DataSourceID="CategoryDDLODS" AutoPostBack="true" DataTextField="DisplayText"  DataValueField="IDValueField" OnSelectedIndexChanged="CategoryDDL_SelectedIndexChanged" AppendDataBoundItems="true">
        <asp:ListItem Value="0">Select a Category</asp:ListItem>
    </asp:DropDownList>
    &nbsp;&nbsp;
    <asp:DropDownList ID="ProductDDL" runat="server" DataSourceID="ProductDDLODS" DataTextField="DisplayText" DataValueField="IDValueField">
         
    </asp:DropDownList>
    &nbsp;&nbsp;
    <asp:TextBox TextMode="Number" ID="QuantityTextBox" runat="server" Width="40px" Text="1"></asp:TextBox>
    &nbsp;
    <%--<asp:Button ID="AddButton"  runat="server" Text="Add" Width="100px" BackColor="Aqua" ForeColor="Black" />--%>
    <asp:LinkButton ID="AddButtonn" runat="server" BackColor="Aqua" ForeColor="Black" CssClass="btn" CommandArgument='<%# Eval("ProductID") %>' OnClick="AddButtonn_Click"> 
    <span aria-hidden="true" class="glyphicon glyphicon-plus">&nbsp;</span>Add

    </asp:LinkButton>
   </div>
     <div class="col">
    <asp:Panel ID="PanelInvoice" runat="server">
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
   <div class="row">
   <div class="col-sm-2"> <asp:Label ID="Label2" runat="server" Text="Invoice Number:" AssociatedControlID="InvoiceIDText"></asp:Label></div>
    <div class="col-sm-9"><asp:TextBox ID="InvoiceIDText" runat="server" Enabled="false" Width="150"></asp:TextBox></div>
   </div>

     <div class="row">
     <div class="col-sm-2">    <asp:Label ID="Label8" runat="server" Text="Invoice Date" AssociatedControlID="InvoiceDateLabel1"></asp:Label></div>
       
     <div class="col-sm-9">   <asp:TextBox ID="InvoiceDateLabel1" runat="server" Enabled="false"></asp:TextBox></div>
     </div>

   <div class="row">
       <div class="col-sm-2">  <asp:Label ID="Label7" runat="server" Text="Invoice Total" AssociatedControlID="InvoiceTotalLabel2"></asp:Label> </div>
   
      <div class="col-sm-9">    <asp:TextBox ID="InvoiceTotalLabel2" runat="server" Enabled="false"></asp:TextBox>  </div>
  </div>
    </asp:Panel>
         </div>
    <%--============================================================GridView===========================================================================--%>
    <br />
    <br />
    <br />
    <br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <asp:GridView ID="CartSalesGridView" runat="server" AutoGenerateColumns="False" BorderColor="Black" OnRowCommand="CartSalesGridView_RowCommand" CssClass="table table-condensed table-hover">

        <Columns>
            <asp:TemplateField HeaderText="ProductID" SortExpression="ProductID" Visible="false">

                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Bind("ProductID") %>' ID="ProductID" Width="125px"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Product" SortExpression="Product">

                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Bind("Product") %>' ID="ProductName" Width="125px"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity" SortExpression="Quantity">

                <ItemTemplate>
                    <asp:TextBox runat="server" Text='<%# Bind("Quantity") %>' ID="Quantity" Width="50px"></asp:TextBox>
                    <asp:LinkButton ID="RefreshButton" runat="server" ForeColor="ForestGreen" CommandName="Refresh" CommandArgument='<%# Bind("ProductID") %>' ValidationGroup="NumericValidate">
                           <span aria-hidden="true" class="glyphicon glyphicon-refresh"></span>
                    </asp:LinkButton>

                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                         ControlToValidate="Quantity"
                         ErrorMessage="Only numeric allowed." ForeColor="Red"
                         ValidationExpression="^[0-9]*$" ValidationGroup="NumericValidate">*
                    </asp:RegularExpressionValidator>

                </ItemTemplate>

            </asp:TemplateField>
            <asp:TemplateField HeaderText="Price" SortExpression="Price">

                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Bind("Price", "{0:C}") %>' ID="PriceID" Width="50px"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Amount" SortExpression="Amount">

                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Bind("Amount", "{0:C}") %>' ID="AmountID" Width="50px"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="" SortExpression="">

                <ItemTemplate>
                  
                    <asp:LinkButton ID="RemoveButton" runat="server" Width="10px" ForeColor="Red" CommandName="Remove" CommandArgument='<%# Bind("ProductID") %>'>
                           <span aria-hidden="true" class="glyphicon glyphicon-remove"></span>
                    </asp:LinkButton>
                </ItemTemplate>

            </asp:TemplateField>

        </Columns>
    </asp:GridView>

 <asp:Panel ID="PanelPaymentButtons" runat="server">
    <br /><br /><br />
    <asp:Button ID="PaymentButton1" runat="server" Text="Payment"  Width="80px" Height="60px" BackColor="Green" ForeColor="White" OnClick="PaymentButton1_Click"/>
      &nbsp;&nbsp;
    <asp:Button ID="ClearButton1" runat="server" Text="Clear Cart" Width="100px" OnClick="ClearButton1_Click" />

   <br /><br /><br /> 
 
<%--===============================Totals=========================================================================================================--%>
<div class="row">
 <div class="col-md-2"> 
    <asp:Label ID="SubtotalLabel" runat="server" Text="Subtotal" AssociatedControlID="SubtotalText"></asp:Label>
    <asp:Label ID="SubtotalText" runat="server" Text=""></asp:Label>
</div>

 <div class="col-md-2"> 
  <asp:Label ID="TaxLabel" runat="server" Text="Tax" AssociatedControlID="TaxText"></asp:Label>
    <asp:Label ID="TaxText" runat="server" Text=""></asp:Label>
 </div>
    <div class="col-md-2"> 
      <asp:Label ID="TotalLabel" runat="server" Text="Total" AssociatedControlID="TotalText"></asp:Label>
    <asp:Label ID="TotalText" runat="server" Text=""></asp:Label>
 </div>
  </div>
 </asp:Panel>
 <br /><br /><br /> 
 <br /><br /><br /> 
 <br /><br /><br /> 
 <br /><br /><br /> 
  <br /><br /><br /> 
 <br /><br /><br /> 
<%--===============================Refunds=========================================================================================================--%>
 <h2>Refunds</h2>
 <br /><br /><br /> 
    
    <asp:TextBox ID="OriginalInvoiceTexBox" runat="server" placeholder="Original Invoice#"></asp:TextBox>

    <asp:Button ID="InvoiceButton" runat="server" Text="Lookup Invoice" OnClick="InvoiceButton_Click" ValidationGroup="check" />

    <asp:Button ID="ClearInvoiceButton" runat="server" Text="Clear" OnClick="ClearInvoiceButton_Click" />

    <%--=============================GridView==Refunds=========================================================================================================--%>


    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="OriginalInvoiceTexBox"
        ErrorMessage="Please Enter Numbers Only " ForeColor="Red"  ValidationExpression="^\d+$" ValidationGroup="check"></asp:RegularExpressionValidator>

    <asp:GridView ID="RefundsGridView" runat="server" AutoGenerateColumns="False" CssClass="table table-condensed table-hover">
        <Columns>

          <asp:TemplateField HeaderText="ID" SortExpression="InvoiceDetailID">

                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Bind("InvoiceDetailID") %>' ID="invoiceIDLabel"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Product" SortExpression="Product">

                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Bind("Product") %>' ID="Label1"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity" SortExpression="Quantity">

                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Bind("Quantity") %>' ID="Label2"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Price" SortExpression="Price">

                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Bind("Price") %>' ID="Label3"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Amount" SortExpression="Amount">

                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Bind("Amount") %>' ID="Label4"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ReStockChg" SortExpression="ReStockChg">

                <ItemTemplate>
                       <asp:CheckBox ID="SelectedResChg" runat="server" />
                    <asp:Label runat="server" Text='<%# Bind("ReStockChg") %>' ID="Label5"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Reason" SortExpression="Reason">

                <ItemTemplate>
                     <asp:CheckBox ID="SelectedReason" runat="server" />
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    <asp:Label runat="server" Text='<%# Bind("Reason") %>' ID="Label6"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>
    <asp:Button ID="RefundButton" runat="server" Text="Refund"  Width="80px" Height="60px" BackColor="Green" ForeColor="White" />
    <asp:Panel ID="RefundTotalsPanel" runat="server">
    <asp:Label ID="RefundSubTotalLabel" runat="server" Text="Subtotal" AssociatedControlID ="RefundTextSubtotal"></asp:Label>
    <asp:Label ID="RefundTextSubtotal" runat="server"></asp:Label>

    <asp:Label ID="RefundTaxLabel" runat="server" Text="Tax"></asp:Label>
    <asp:Label ID="RefundTaxText" runat="server"></asp:Label>

    <asp:Label ID="RefundTotalLabel" runat="server" Text="Refund Total"></asp:Label>
    <asp:Label ID="RefundTotalText" runat="server"></asp:Label>
    </asp:Panel>
<%--================================================================ODS==========================================================================--%>
    <asp:ObjectDataSource ID="CategoryDDLODS" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="List_Category"
        TypeName="eRaceSystem.BLL.CategoryController"></asp:ObjectDataSource>

<%--    <asp:ObjectDataSource ID="ProductDDLODS" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="List_Products" TypeName="eRaceSystem.BLL.ProductController"></asp:ObjectDataSource>--%>

    <asp:ObjectDataSource ID="ProductDDLODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="List_Products" TypeName="eRaceSystem.BLL.ProductController">
        <SelectParameters>
            <asp:ControlParameter ControlID="CategoryDDL" PropertyName="SelectedValue" DefaultValue="select" Name="categoryid" Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="CarItemtODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ShoppingCart_OrderList" TypeName="eRaceSystem.BLL.ShoppingCartController"></asp:ObjectDataSource>

<%--    <asp:ObjectDataSource ID="RefundsGridViewODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="InvoiceInformation_Table" TypeName="eRaceSystem.BLL.InvoiceRefundsController">
        <SelectParameters>
            <asp:Parameter DefaultValue="fd" Name="invoiceid" Type="Int32"></asp:Parameter>
        </SelectParameters>
    </asp:ObjectDataSource>--%>

</asp:Content>
