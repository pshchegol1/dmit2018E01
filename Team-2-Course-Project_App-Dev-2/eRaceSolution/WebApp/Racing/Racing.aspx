<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Racing.aspx.cs" Inherits="WebApp.Racing.Racing" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br />
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    <h1>Racing</h1>

    <div class="row">
        <div style="text-align: right">
            <asp:Label ID="Label1" runat="server" Font-Size="XX-Large" Text=""><i class="fa fa-user-circle" aria-hidden="true"></i></asp:Label>
            <asp:Label ID="UserNameLabel2" runat="server" Font-Size="Large" ForeColor="tomato" Text=""></asp:Label>
        </div>
        <hr />
    </div>

    <div class="row">
        <div class="col-md-4">
            <h3>Calendar</h3>
            <asp:Calendar ID="Calendar1" runat="server" Width="300px" Height="300px"
                OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
        </div>
        <asp:Panel ID="SchedulePanel" runat="server" Visible="false">
            <div class="col-md-8">
            <h3>Race Schedule</h3>
            <asp:ListView ID="RaceListView" runat="server" 
                DataSourceID="RaceListODS" 
                OnItemCommand="RaceDriver_ItemCommand"
                DataKeyNames="RaceID">
                <AlternatingItemTemplate>
                    <tr style="background-color: #FFFFFF; color: #284775; text-align:center;">
                        <td>
                            <asp:Button ID="SelectButton" runat="server" Text="View"
                            CommandName="Select" 
                            CommandArgument='<%# Eval("RaceID") %>' /></td>
                        <td>
                            <asp:Label Text='<%# Eval("RaceTime", "{0:hh tt}") %>' runat="server" ID="RaceTimeLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Competition") %>' runat="server" ID="CompetitionLabel" Width="350px" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Run") %>' runat="server" ID="RunLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("NumberOfDrivers") %>' runat="server" ID="NumberOfDriversLabel" /></td>
                    </tr>
                </AlternatingItemTemplate>
                <EmptyDataTemplate>
                    <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                        <tr>
                            <td><blockquote>Races are only run on weekends and holidays.
                            No races run on this day</blockquote>
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr style="background-color: #E0FFFF; color: #333333; text-align:center;">
                        <td><asp:Button ID="SelectButton" runat="server" Text="View"
                            CommandName="Select" 
                            CommandArgument='<%# Eval("RaceID") %>' /></td>
                        <td>
                            <asp:Label Text='<%# Eval("RaceTime", "{0:hh tt}") %>' runat="server" ID="RaceTimeLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Competition") %>' runat="server" ID="CompetitionLabel" Width="350px"/></td>
                        <td>
                            <asp:Label Text='<%# Eval("Run") %>' runat="server" ID="RunLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("NumberOfDrivers") %>' runat="server" ID="NumberOfDriversLabel" /></td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table runat="server">
                        <tr runat="server">
                            <td runat="server">
                                <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                                    <tr runat="server" style="background-color: #E0FFFF; color: #333333;">
                                        <th runat="server">RaceID</th>
                                        <th runat="server">RaceTime</th>
                                        <th runat="server">Competition</th>
                                        <th runat="server">Run</th>
                                        <th runat="server">NumberOfCars</th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder"></tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server">
                            <td runat="server" style="text-align: center; background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif; color: #FFFFFF"></td>
                        </tr>
                    </table>
                </LayoutTemplate>
                <SelectedItemTemplate>
                    <tr style="background-color: #E2DED6; font-weight: bold; color: #333333; text-align:center;">
                        <td>
                            <asp:Label Text='<%# Eval("RaceID") %>' runat="server" ID="RaceIDLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("RaceTime", "{0:hh tt}") %>' runat="server" ID="RaceTimeLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Competition") %>' runat="server" ID="CompetitionLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Run") %>' runat="server" ID="RunLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("NumberOfDrivers") %>' runat="server" ID="NumberOfDriversLabel" /></td>
                    </tr>
                </SelectedItemTemplate>
            </asp:ListView>
        </div>
        </asp:Panel>
        </div>
        <br /><br />
    <div class="row">
        <asp:Panel ID="RaceRosterPanel" runat="server" Visible="false">
         <div class="col-lg-7">
            <h3>Roster</h3>
            <div style="text-align: right">
            <asp:Button ID="RecordRaceTimesButton" runat="server" 
                Text="Record Race Times" Width="180px" 
                Height="30px" CausesValidation="false"
                BackColor="LightBlue" ForeColor="Black" BorderColor="Black" OnClick="RecordRaceTimesButton_Click"/>
            </div>
            <br />
             <asp:ListView ID="DriverListView" runat="server"
                 DataSourceID="DriverListODS"
                 OnItemCommand="DriverListView_ItemCommand"
                 Style="text-align: center" InsertItemPosition="LastItem"
                 DataKeyNames="RaceDetailID">
                 <AlternatingItemTemplate>
                     <tr style="background-color: #FFFFFF; color: #284775; text-align:center;">
                         <td>
                             <asp:Button Text="Edit" runat="server" ID="EditButton" CommandName="Edit"/></td>
                         <td>
                             <asp:Label Text='<%# Eval("FullName") %>' runat="server" ID="FullNameLabel" /></td>
                         <td>
                             <asp:Label Text='<%# String.Format("{0:0.00}", Eval("RaceFee")) %>' runat="server" ID="RaceFeeLabel" Width="100px"/></td>
                         <td>
                             <asp:Label Text='<%# String.Format("{0:0.00}", Eval("RentalFee")) %>' runat="server" ID="RentalFeeLabel" Width="100px"/></td>
                         <td>
                             <asp:Label Text='<%# Eval("Placement") %>' runat="server" ID="PlacementLabel" Width="100px"/></td>
                         <td>
                             <asp:CheckBox Checked='<%# Eval("Refunded") %>' runat="server" ID="RefundedCheckBox" Enabled="false" /></td>
                     </tr>
                 </AlternatingItemTemplate>
                 <EditItemTemplate>
                     <tr style="background-color: #999999; text-align:center;">
                         <td>
                             <asp:Button runat="server" CommandName="Update" Text="Update" ID="UpdateButton" />
                             <asp:Button runat="server" CommandName="Cancel" Text="Cancel" ID="CancelButton" />
                         </td>
                         <td>
                             <asp:TextBox Text='<%# Bind("FullName") %>' runat="server" ID="FullNameTextBox" /></td>
                         <td>
                             <asp:TextBox Text='<%# Bind("RaceFee") %>' runat="server" ID="RaceFeeTextBox" Width="100px"/></td>
                         <td>
                             <asp:TextBox Text='<%# Bind("RentalFee") %>' runat="server" ID="RentalFeeTextBox" Width="100px"/></td>
                         <td>
                             <asp:TextBox Text='<%# Bind("Placement") %>' runat="server" ID="PlacementTextBox" Width="100px"/></td>
                         <td>
                             <asp:CheckBox Checked='<%# Bind("Refunded") %>' runat="server" ID="RefundedCheckBox" /></td>
                     </tr>
                 </EditItemTemplate>
                 <EmptyDataTemplate>
                     <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; text-align:center;">
                         <tr>
                             <td><blockquote>No drivers signed up for this race</blockquote></td>
                         </tr>
                     </table>
                 </EmptyDataTemplate>
                 <InsertItemTemplate>
                     <tr style="text-align:center;">
                         <td>
                             <asp:LinkButton ID="AddDriverToRace" runat="server" 
                                 Text="Add" CommandName="Insert"
                             CssClass="btn" CommandArgument='<%# Eval("RaceDetailID") %>'>
                             </asp:LinkButton>
                             <asp:LinkButton runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" />
                         </td>
                         <td>
                             <asp:DropDownList ID="MemberDDL" runat="server"
                                 DataSourceID="MemberListODS"
                                 DataTextField="MemberName"
                                 DataValueField="MemberID"
                                 AutoPostBack="true"
                                 DataKeyNames="MemberID"
                                 SelectedValue='<%# Bind("MemberID") %>'
                                 OnSelectedIndexChanged="MemberDDL_SelectedIndexChanged" ></asp:DropDownList>
                             </td>
                         <td>
                             <asp:Label Text='<%# Bind("RaceFee") %>' runat="server" ID="RaceFeeTextBox" Width="100px" Enabled="true"/><%# Eval ("RaceFee") %></td>
                         <td>
                             <asp:DropDownList ID="CarClassDDL" runat="server"
                                 DataSourceID="CarClassesListODS"
                                 DataTextField="ClassName"
                                 DataValueField="CarClassID"
                                 AutoPostBack="true"
                                 SelectedValue='<%# Bind("CarClassID") %>'></asp:DropDownList>
                             </td>
                         <td>
                             <asp:DropDownList ID="CarListDDL" runat="server"
                                 DataSourceID="CarListODS"
                                 DataTextField="SerialNumber"
                                 DataValueField="CarID"
                                 SelectedValue='<%# Bind("CarID") %>'></asp:DropDownList>
                             </td>
                     </tr>
                 </InsertItemTemplate>
                 <ItemTemplate>
                     <tr style="background-color: #E0FFFF; color: #333333; text-align:center;">
                         <td>
                             <asp:Button Text="Edit" runat="server" ID="EditButton" CommandName="Edit"/></td>
                         <td>
                             <asp:Label Text='<%# Eval("FullName") %>' runat="server" ID="FullNameLabel" /></td>
                         <td>
                             <asp:Label Text='<%# String.Format("{0:0.00}", Eval("RaceFee")) %>' runat="server" ID="RaceFeeLabel" Width="100px"/></td>
                         <td>
                             <asp:Label Text='<%# String.Format("{0:0.00}", Eval("RentalFee")) %>' runat="server" ID="RentalFeeLabel" Width="100px"/></td>
                         <td>
                             <asp:Label Text='<%# Eval("Placement") %>' runat="server" ID="PlacementLabel" Width="100px"/></td>
                         <td>
                             <asp:CheckBox Checked='<%# Eval("Refunded") %>' runat="server" ID="RefundedCheckBox" Enabled="false" /></td>
                     </tr>
                 </ItemTemplate>
                 <LayoutTemplate>
                     <table runat="server" style="text-align:center;">
                         <tr runat="server" style="text-align:center;">
                             <td runat="server">
                                 <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif; text-align:center;" border="1">
                                     <tr runat="server" style="background-color: #E0FFFF; color: #333333; text-align:center;">
                                         <th runat="server" style="color:#E0FFFF">xxxxxxxxxxxxxxx</th>
                                         <th runat="server" style="text-align:center">FullName</th>
                                         <th runat="server" style="text-align:center">RaceFee</th>
                                         <th runat="server" style="text-align:center">RentalFee</th>
                                         <th runat="server" style="text-align:center">Placement</th>
                                         <th runat="server" style="text-align:center">Refunded</th>
                                     </tr>
                                     <tr runat="server" id="itemPlaceholder" style="text-align:center;"></tr>
                                 </table>
                             </td>
                         </tr>
                         <tr runat="server">
                             <td runat="server" style="text-align: center; background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif; color: #FFFFFF"></td>
                         </tr>
                     </table>
                 </LayoutTemplate>
                 <SelectedItemTemplate>
                     <tr style="background-color: #E2DED6; font-weight: bold; color: #333333; text-align:center;">
                         <td>
                             <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                         </td>
                         <td>
                             <asp:Label Text='<%# Eval("RaceID") %>' runat="server" ID="RaceIDLabel" /></td>
                         <td>
                             <asp:Label Text='<%# Eval("RaceDetailID") %>' runat="server" ID="RaceDetailIDLabel" /></td>
                         <td>
                             <asp:Label Text='<%# Eval("FullName") %>' runat="server" ID="FullNameLabel" /></td>
                         <td>
                             <asp:Label Text='<%# String.Format("{0:0.00}", Eval("RaceFee")) %>' runat="server" ID="RaceFeeLabel" Width="100px"/></td>
                         <td>
                             <asp:Label Text='<%# String.Format("{0:0.00}" ,Eval("RentalFee")) %>' runat="server" ID="RentalFeeLabel" Width="100px"/></td>
                         <td>
                             <asp:Label Text='<%# Eval("Placement") %>' runat="server" ID="PlacementLabel" Width="100px"/></td>
                         <td>
                             <asp:CheckBox Checked='<%# Eval("Refunded") %>' runat="server" ID="RefundedCheckBox" Enabled="false" /></td>
                     </tr>
                 </SelectedItemTemplate>
             </asp:ListView>
         </div>
        </asp:Panel>

        <asp:Panel ID="RaceTimesPanel" runat="server" Visible="false">
         <div class="col-lg-7">
            <h3>Race Times</h3>
             <asp:Button ID="SaveTimesButton" runat="server" 
                Text="Record Race Times" Width="180px" 
                Height="30px" CausesValidation="false"
                BackColor="LightBlue" ForeColor="Black" BorderColor="Black"/>
         </div>
            <br />
            <asp:ListView ID="RaceTimesListView" runat="server" DataSourceID="RaceTimesListODS"></asp:ListView>

          </asp:Panel>
    </div>


    <%--ObjectDataSources--%>
    <asp:ObjectDataSource ID="RaceListODS" runat="server"
        OldValuesParameterFormatString="original_{0}"
        SelectMethod="List_RacesForSelectedDate"
        TypeName="eRaceSystem.BLL.RaceController">
        <SelectParameters>
            <asp:ControlParameter ControlID="Calendar1" PropertyName="SelectedDate"
                Name="date" Type="DateTime"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="DriverListODS" runat="server"
        OldValuesParameterFormatString="original_{0}"
        SelectMethod="List_DriversForSelectedRace"
        TypeName="eRaceSystem.BLL.DriverController" 
        
        InsertMethod="Driver_Add" 
        UpdateMethod="Driver_Update">
        <SelectParameters>
            <asp:ControlParameter Name="raceid" 
                ControlID="RaceListView" PropertyName="SelectedValue" 
                Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="MemberListODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Member_List" 
        TypeName="eRaceSystem.BLL.DriverController">
    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="CertificationListODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="CertificationLevel_FindByMember" 
        TypeName="eRaceSystem.BLL.DriverController">
        <SelectParameters>
            <asp:Parameter Name="memberid" Type="Int32"></asp:Parameter>
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="RaceTimesListODS" runat="server"
        OldvaluesParameterFormatString="original_{0}"
        SelectMethod="List_TimesForSelectedRaceDetail"
        TypeName="eRaceSystem.BLL.RaceController">
        <SelectParameters>
            <asp:ControlParameter ControlID="DriverListView" Name="racedetailid"
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
  


    <asp:ObjectDataSource ID="CarClassesListODS" runat="server"
        OldValuesParameterFormatString="original_{0}"
        SelectMethod="CarClass_List"
        TypeName="eRaceSystem.BLL.DriverController">
    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="CarListODS" runat="server"
        OldValuesParameterFormatString="original_{0}"
        SelectMethod="Car_List" 
        TypeName="eRaceSystem.BLL.DriverController">
    </asp:ObjectDataSource>


</asp:Content>
