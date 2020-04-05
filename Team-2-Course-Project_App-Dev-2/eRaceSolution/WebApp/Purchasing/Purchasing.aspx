<%@ Page Title="Purchasing" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Purchasing.aspx.cs" Inherits="WebApp.Purchasing.Purchasing" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <h1>Purchasing</h1>
    </div>
    <div class="row">
        <div style="text-align: right">
            <asp:Label ID="Label1" runat="server" Font-Size="XX-Large" Text=""><i class="fa fa-user-circle" aria-hidden="true"></i></asp:Label>
            <asp:Label ID="UserNameLabel2" runat="server" Font-Size="Large" ForeColor="tomato" Text=""></asp:Label>
        </div>
        <hr />
    </div>
    <div class="row">
        <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Correct the following concerns" ValidationGroup="NumericValidate" CssClass="alert alert-danger" />
        <hr />
    </div>
    <%--purchase order form start from here--%>
    <div class="row">
        <div class="col-12 col-md-7">
            <div class="row">
                <h4>Purchase Order</h4>
                <hr />
            </div>
            <%--Vendor Selection section--%>
            <div class="row">
                <div class="col-md-3">
                    <div class="row">
                        <asp:DropDownList ID="VendorDDL" CssClass="form-control btn btn-primary dropdown-toggle" runat="server" DataSourceID="vendorListODS"
                            DataTextField="DisplayText" DataValueField="IDValueField" AppendDataBoundItems="true">

                            <asp:ListItem Value="0">Select a Vendor ..</asp:ListItem>
                        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnSelectVendor" runat="server" CssClass="btn btn-light" Text="Select" OnClick="btnSelectVendor_Click" />
                </div>
                <div class="col-md-1"></div>
                <div class="col-md-2">
                    <asp:LinkButton ID="btnPlaceOrder" runat="server" CssClass="btn btn-success" Text="Place Order" ToolTip=""></asp:LinkButton>
                </div>
                <div class="col-md-1">
                    <asp:LinkButton ID="btnSave" runat="server" CssClass="btn btn-success" ToolTip="Save Order" OnClick="btnSave_Click" Style="height: 35px">
                    <i class="fa fa-cart-arrow-down" aria-hidden="true"></i>
                    </asp:LinkButton>
                </div>
                <div class="col-md-1">
                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-warning" ToolTip="Cancel Order" OnClick="btnCancel_Click">
                    <i class="fa fa-undo" aria-hidden="true"></i>
                    </asp:LinkButton>
                </div>
                <div class="col-md-1">
                    <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Bind("OrderID") %>' OnClick="btnDelete_Click" CssClass="btn btn-danger" ToolTip="Delete Order">
                    <i class="fa fa-trash" aria-hidden="true"></i>
                    </asp:LinkButton>
                </div>
            </div>


            <div class="row">
                <div class="col-md-9">
                    <span>
                        <asp:Label ID="lblVendorName" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        &nbsp; 
                        <asp:Label ID="lblVendorContact" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                        &nbsp; 
                        <asp:Label ID="lblPhone" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                    </span>
                    <asp:TextBox ID="txtComments" CssClass="form-control" Columns="50" TextMode="MultiLine" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label5" runat="server" Text="Subtotal"></asp:Label>
                    <asp:TextBox ID="txtSubTotal" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:Label ID="Label6" runat="server" Text="Tax"></asp:Label>
                    <asp:TextBox ID="txtTax" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:Label ID="Label7" runat="server" Text="Subtotal"></asp:Label>
                    <asp:TextBox ID="txtTotal" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

            </div>
            <br>
            <asp:Panel ID="pnlVendorResults" runat="server" Visible="False">
                <asp:GridView ID="OrderDetailsGridView" CssClass="table table-hover" GridLines="Horizontal"
                    BorderStyle="None" runat="server" AutoGenerateColumns="False"
                    OnRowCommand="OrderDetailsGridView_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="" SortExpression="">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnPlaceOrder" runat="server"
                                    CssClass="btn btn-danger"
                                    CommandArgument='<%# Bind("OrderDetailID") %>'
                                    ToolTip="Remove Order" CommandName="Remove">
                                              <span aria-hidden="true" class="glyphicon glyphicon-remove"
                                                style="color: white"></span>
                                </asp:LinkButton>
                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Product ID" SortExpression="ProductName" Visible="false">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("ProductID") %>' ID="TextBox1"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Bind("OrderDetailID") %>' ID="lblOrderDetailID" Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Product ID" SortExpression="ProductName" Visible="false">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("OrderID") %>' ID="TextBox1"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Bind("OrderID") %>' ID="lblOrderID" Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ProductName" SortExpression="ProductName">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("ProductName") %>' ID="TextBox1"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Bind("ProductName") %>' ID="Label1" Font-Size="Small"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="OrderQuantity" SortExpression="OrderQuantity">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("OrderQuantity") %>' ID="txtOrderQty"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtQuantity" CssClass="form-control" Text='<%# Bind("OrderQuantity") %>' runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UnitSize" SortExpression="UnitSize">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("UnitSize") %>' ID="TextBox3"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("UnitSize","{0} per case") %>' ID="lblUnitSize"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UnitCost" SortExpression="UnitCost">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("UnitCost") %>' ID="TextBox4"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:TextBox runat="server" CssClass="form-control" Text='<%# Bind("UnitCost","{0:0.00}")%>' ID="txtUnitCost"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PerItemCost" SortExpression="PerItemCost">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("PerItemCost") %>' ID="TextBox5"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="RefreshButton" runat="server" CssClass=""
                                    CommandName="Refresh" CommandArgument='<%# Bind("OrderDetailID") %>' ValidationGroup="NumericValidate">
                                    <span aria-hidden="true" class="glyphicon glyphicon-refresh" style="color:forestgreen"></span>
                                </asp:LinkButton>

                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                    ControlToValidate="txtQuantity"
                                    ErrorMessage="Only numeric allowed." ForeColor="Red"
                                    ValidationExpression="^[0-9]*$" ValidationGroup="NumericValidate">*
                                </asp:RegularExpressionValidator>

                                <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                    ControlToValidate="txtUnitCost"
                                    ErrorMessage="Only numeric allowed." ForeColor="Red"
                                    ValidationExpression="^[0-9]*$" ValidationGroup="NumericValidate">*
                                </asp:RegularExpressionValidator>--%>
                                <asp:Label ID="lblCostWarning" runat="server" Visible="false" Text="Label"><span aria-hidden="true" style="color: red">!!</span></asp:Label>
                                <asp:Label runat="server" Text='<%#  Bind("PerItemCost", "{0:C}") %>' ID="lblPerItemCost"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ExtendedCost" SortExpression="ExtendedCost">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("ExtendedCost") %>' ID="TextBox6"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Bind("ExtendedCost") %>' ID="Label6"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <EmptyDataTemplate>
                        <div align="center">No order records found.</div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </asp:Panel>
            <%--end of vendor results--%>
        </div>
        <div class="col-12 col-md-5">
            <asp:Panel ID="pnlInventory" runat="server" Visible="False">
                <div class="row">
                    <h4>Inventory</h4>
                    <hr />
                    <asp:Repeater ID="vendorCategoryCatalog" runat="server"
                        DataSourceID="rptaVendorCatalogODS"
                        OnItemCommand="vendorCategoryCatalog_ItemCommand"
                        ItemType="eRace.Data.DTOs.CategoryDTO">
                        <ItemTemplate>
                            <h5><strong><%# Item.Category %></strong></h5>
                            <asp:GridView ID="ProductList"
                                DataSource="<%# Item.Products %>"
                                runat="server" CssClass="table" GridLines="Horizontal" BorderStyle="None" AllowPaging="False" OnPageIndexChanging="ProductList_PageIndexChanging">
                                <Columns>
                                    <%--<asp:BoundField DataField="ProductId" />--%>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnPlaceOrder" runat="server" CssClass="btn btn-success" Text="Place Order"
                                                ToolTip="Add to Order" CommandName="ProductIDonRepeater">
                                                
                                                <span aria-hidden="true" class="glyphicon glyphicon-plus"
                                                style="color: white"></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                </Columns>
                                <EmptyDataTemplate>
                                    <div align="center">No product records found for this vendor. </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ItemTemplate>

                    </asp:Repeater>
                </div>
            </asp:Panel>
        </div>
    </div>

    <%--ODS's--%>
    <asp:ObjectDataSource ID="vendorListODS" runat="server"
        OldValuesParameterFormatString="original_{0}"
        SelectMethod="vendors_List"
        TypeName="eRaceSystem.BLL.VendorsController"></asp:ObjectDataSource>

    <asp:ObjectDataSource ID="rptaVendorCatalogODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Category_VendorCatalog_Get" TypeName="eRaceSystem.BLL.CategoryController">
        <SelectParameters>
            <asp:ControlParameter ControlID="VendorDDL" PropertyName="SelectedValue" Name="vendorID" Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
