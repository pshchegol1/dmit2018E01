<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Receiving.aspx.cs" Inherits="WebApp.Receiving.Receiving" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    <h1>Receiving</h1>

    <div class="row">
        <div style="text-align: right">
            <asp:Label ID="Label1" runat="server" Font-Size="XX-Large" Text=""><i class="fa fa-user-circle" aria-hidden="true"></i></asp:Label>
            <asp:Label ID="UserNameLabel2" runat="server" Font-Size="Large" ForeColor="tomato" Text=""></asp:Label>
        </div>
        <hr />
    </div>
    <br /><br />
    <div class="row">
        <asp:Label ID="VendorLabel" runat="server" Font-Size="Large">Select Vendor</asp:Label>
    </div>
    <br />
    <div class="row">
        <asp:DropDownList ID="VendorDDL" runat="server" 
            DataSourceID="VendorListDDLODS" 
            DataTextField="DisplayText" 
            DataValueField="IDValueField">
        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="VendorFetch" runat="server" Text="Open" Width="80px" CausesValidation="false" OnClick="VendorFetch_Click" />
    </div>
    <br />
    <asp:Panel ID="ReceivingPanel" runat="server" visible="false">
        <div class="row">
            <div class="">
                <asp:Label ID="VendorName" runat="server" Text="" width="200px" style="text-align: right"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="AddressAndCity" runat="server" Text=""></asp:Label>
            </div>
            <br />
            <div class="">
                <asp:Label ID="Contact" runat="server" Text="" width="200px" style="text-align: right"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="PhoneNumber" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <div style="text-align: right">
            <asp:Button ID="RecieveShipmentButton" runat="server" Text="Recieve Shipment" Width="180px" Height="30px" CausesValidation="false"
                BackColor="LightBlue" ForeColor="Black" BorderColor="Black" OnClick="RecieveShipmentButton_Click" />
        </div>
        <br />
        <%--<asp:ValidationSummary ID="ValidationSummaryInsertGridView" runat="server" ValidationGroup="GridValid"
            HeaderText="Correct the following concerns when inserting a record into the unordered items" />

        <asp:RequiredFieldValidator ID="RequiredRecievedUnits" runat="server" 
            ErrorMessage="Received Units is required" Display="None" ValidationGroup="GridValid"
            ControlToValidate="RecievedUnits">
        </asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompareReceivedUnits" runat="server" 
            ErrorMessage="Recieved Units must be 0 or greater and it cannot be a string" Display="None"
            SetFocusOnError="true" ControlToValidate="RecievedUnits" ValidationGroup="GridValid"
            Type="Double" Operator="GreaterThanEqual" ValueToCompare="0.00">
        </asp:CompareValidator>

        <asp:RegularExpressionValidator ID="RegItemName" runat="server"
            ErrorMessage="Comment is limited to 100 characters" Display="None" ValidationGroup="GridValid"
            ControlToValidate="Reason" ValidationExpression="^.{0,100}$">
        </asp:RegularExpressionValidator>--%>
        <asp:GridView ID="ReceivingGridView" runat="server" AutoGenerateColumns="False" DataSourceID="ReceivingListODS" CssClass="table table-condensed table-hover">
            <Columns>
                <asp:TemplateField HeaderText="ProductID" SortExpression="ProductID" Visible="False">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("ProductID") %>' ID="ProductID"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item" SortExpression="Item">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Item") %>' ID="Item"></asp:Label>
                    </ItemTemplate> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="QuantityOrdered" SortExpression="QuantityOrdered">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("QuantityOrdered") %>' ID="QuantityOrdered"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="OrderedUnits" SortExpression="OrderedUnits">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("OrderedUnits") %>' ID="OrderedUnits"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="QuantityOutstanding" SortExpression="QuantityOutstanding">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("QuantityOutstanding")%>' ID="QuantityOutstanding"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ReceivedUnits" SortExpression="ReceivedUnits">
                    <ItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("ReceivedUnits") %>' ID="RecievedUnits" Width="20px"></asp:TextBox>
                        <asp:Label runat="server" Text='<%# Bind("ReceivedUnitsLabel") %>' ID="Label7"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RejectedUnits" SortExpression="RejectedUnits">
                    <ItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("RejectedUnits") %>' ID="RejectedUnits" Width="20px"></asp:TextBox>&nbsp;
                        <asp:TextBox runat="server" Text='<%# Bind("Reason") %>' ID="Reason" Width="300px"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SalvagedItems" SortExpression="SalvagedItems">
                    <ItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("SalvagedItems") %>' ID="SalvagedItems" Width="20px"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <br /> 
        <h3 style="width:400px">Unordered Items</h3>
        <br />
        <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server"
            HeaderText="Correct the following concerns when inserting a record into the unordered items" />
        <div class="row">
            <div class="col-md-6">
            <!-- Listview crud w/Insert and delete DONT FORGET TO INCLUDE VALIDATION -->
                <asp:ListView ID="UnorderedItemsListView" runat="server" DataSourceID="UnOrderedItemsODS" InsertItemPosition="LastItem" DataKeyNames="ItemID">
                    <AlternatingItemTemplate>
                        <tr style="background-color: #FFF8DC; text-align: center">
                            <td>
                                <asp:Label Text='<%# Eval("ItemName") %>' runat="server" ID="ItemNameLabel" style="width: 280px" />
                            </td>
                            <td>
                                <asp:Label Text='<%# Eval("VendorProductID") %>' runat="server" ID="VendorProductIDLabel" style="width: 90px" /></td>
                            <td>
                                <asp:Label Text='<%# Eval("Quantity") %>' runat="server" ID="QuantityLabel" style="width: 60px" /></td>
                            <td>
                                <asp:LinkButton ID="DeleteButton" runat="server" CssClass="btn" CommandName="Delete" CausesValidation="false" CommandArgument='<%# Eval("ItemID") %>'>
                                    <span aria-hidden="true" class="glyphicon glyphicon-minus">&nbsp;</span>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <EmptyDataTemplate>
                        <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <InsertItemTemplate>
                        <asp:RequiredFieldValidator ID="RequiredItemName" runat="server" 
                            ErrorMessage="Item Name is required" Display="None"
                            ControlToValidate="ItemNameTextBoxI">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegItemName" runat="server"
                            ErrorMessage="Item Name is limited to 50 characters" Display="None"
                            ControlToValidate="ItemNameTextBoxI" ValidationExpression="^.{1,50}$">
                        </asp:RegularExpressionValidator>

                        <asp:RequiredFieldValidator ID="RequiredVendorProductID" runat="server" 
                            ErrorMessage="Vendor ID is required" Display="None" 
                            ControlToValidate="VendorProductIDTextBoxI">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegVendorProductID" runat="server"
                            ErrorMessage="Vendor ID is limited to 25 characters" Display="None"
                            ControlToValidate="VendorProductIDTextBoxI" ValidationExpression="^.{1,25}$">
                        </asp:RegularExpressionValidator>

                        <asp:RequiredFieldValidator ID="RequiredQuantity" runat="server" 
                            ErrorMessage="Quantity is required" Display="None" 
                            ControlToValidate="QuantityTextBoxI">
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareQuantity" runat="server" 
                            ErrorMessage="Quantity must be 0 or greater and it cannot be a string" Display="None"
                            SetFocusOnError="true" ControlToValidate="QuantityTextBoxI"
                            Type="Double" Operator="GreaterThanEqual" ValueToCompare="0.00">
                        </asp:CompareValidator>

                        <tr style="text-align: center">
                            <td>
                                <asp:TextBox Text='<%# Bind("ItemName") %>' runat="server" ID="ItemNameTextBoxI" style="width: 280px" /></td>
                            <td>
                                <asp:TextBox Text='<%# Bind("VendorProductID") %>' runat="server" ID="VendorProductIDTextBoxI" style="width: 90px" /></td>
                            <td>
                                <asp:TextBox Text='<%# Bind("Quantity") %>' runat="server" ID="QuantityTextBoxI" style="width: 60px" /></td>
                            <td>
                                <asp:LinkButton ID="InsertButton" runat="server" CommandName="Insert" CssClass="btn" CommandArgument='<%# Eval("ItemID") %>'>
                                    <span aria-hidden="true" class="glyphicon glyphicon-plus">&nbsp;</span>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <tr style="background-color: #DCDCDC; color: #000000; text-align: center">
                            <td>
                                <asp:Label Text='<%# Eval("ItemName") %>' runat="server" ID="ItemNameLabel" style="width: 280px" /></td>
                            <td>
                                <asp:Label Text='<%# Eval("VendorProductID") %>' runat="server" ID="VendorProductIDLabel" style="width: 90px" /></td>
                            <td>
                                <asp:Label Text='<%# Eval("Quantity") %>' runat="server" ID="QuantityLabel" style="width: 60px" /></td>
                            <td>
                                <asp:LinkButton ID="DeleteButton" runat="server" CssClass="btn" CommandName="Delete" CausesValidation="false" CommandArgument='<%# Eval("ItemID") %>'>
                                    <span aria-hidden="true" class="glyphicon glyphicon-minus">&nbsp;</span>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table runat="server">
                            <tr runat="server">
                                <td runat="server">
                                    <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                                        <tr runat="server" style="background-color: #DCDCDC; color: #000000;">
                                            <th runat="server" width="300px">Item Name</th>
                                            <th runat="server" width="90px">Vendor ID</th>
                                            <th runat="server" width="70px">Quantity</th>
                                            <th runat="server" width="40px"></th>
                                        </tr>
                                        <tr runat="server" id="itemPlaceholder"></tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server">
                                <td runat="server" style="text-align: center; background-color: #CCCCCC; font-family: Verdana, Arial, Helvetica, sans-serif; color: #000000;"></td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <SelectedItemTemplate>
                        <tr style="background-color: #008A8C; font-weight: bold; color: #FFFFFF; text-align: center">
                            <td>
                                <asp:Label Text='<%# Eval("ItemName") %>' runat="server" ID="ItemNameLabel" style="width: 280px" /></td>
                            <td>
                                <asp:Label Text='<%# Eval("VendorProductID") %>' runat="server" ID="VendorProductIDLabel" style="width: 90px" /></td>
                            <td>
                                <asp:Label Text='<%# Eval("Quantity") %>' runat="server" ID="QuantityLabel" style="width: 60px" /></td>
                            <td>
                                <asp:LinkButton ID="DeleteButton" runat="server" CssClass="btn" CommandName="Delete" CausesValidation="false" CommandArgument='<%# Eval("ItemID") %>'>
                                    <span aria-hidden="true" class="glyphicon glyphicon-minus">&nbsp;</span>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </SelectedItemTemplate>
                </asp:ListView>
            </div>
            <div class="col-md-1">
                <asp:Button ID="ForceClose" runat="server" Text="Force Close" CausesValidation="false"
                    OnClientClick="return confirm('Are you sure you wish to force close this order?')" OnClick="ForceClose_Click" />
            </div>
            <div class="col-md-5">
                <asp:TextBox ID="ReasonCommentReasonMessage" CssClass="form-control" TextMode="multiline" Columns="80" Rows="5" runat="server">
                </asp:TextBox>
            </div>
        </div>
    </asp:Panel>

    <asp:ObjectDataSource ID="VendorListDDLODS" runat="server"
        OldValuesParameterFormatString="original_{0}"
        SelectMethod="List_VendorNamesWithOrderID"
        TypeName="eRaceSystem.BLL.OrdersController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ReceivingListODS" runat="server"
        OldValuesParameterFormatString="original_{0}"
        SelectMethod="List_VendorOrdersListSelection"
        TypeName="eRaceSystem.BLL.Receiving.OrderController">
        <SelectParameters>
            <asp:ControlParameter ControlID="VendorDDL" PropertyName="SelectedValue" Type="Int32" Name="orderid"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="UnOrderedItemsODS" runat="server"
        SelectMethod="List_GetUnorderedItems"
        TypeName="eRaceSystem.BLL.UnOrderedItemController" 
        DataObjectTypeName="eRace.Data.Entities.UnOrderedItem" 
        DeleteMethod="UnorderedItem_Delete" 
        InsertMethod="Insert_ReturnItems" 
        OldValuesParameterFormatString="original_{0}">
    </asp:ObjectDataSource>
</asp:Content>