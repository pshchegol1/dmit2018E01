<%@ Page Title="Repeater Nested Query" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RepeaterDisplay.aspx.cs" Inherits="WebApp.SamplePages.RepeaterDisplay" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Displaying a Nested Linq Query in a Repeater</h1>

    <asp:Repeater ID="AlbumTrucksList" runat="server" DataSourceID="AlbumTrucksListODS" ItemType="ChinookSystem.Data.DTOs.AlbumDTO">
        <HeaderTemplate>
            <h3>Albums and Trucks</h3>
        </HeaderTemplate>
        <ItemTemplate>
            <h5><strong>Album: <%# Item.AlbumTitle%></strong></h5>
            <p> <strong>Artist: <%# Item.AlbumArtist%></strong> <strong>
                # of Trucks: <%# Item.Trackcount%> 
                Play time: <%# Item.PlayTime%></strong>

            </p>
            <asp:GridView ID="TruckList" runat="server" DataSource="<%# Item.AlbumTracks %>"
                 CssClass="table" GridLines="Horizontal" BorderStyle="None">
                
            </asp:GridView>
        </ItemTemplate>
        <FooterTemplate>
            &copy; DMIT2018 NAIT Course All Rights Reserved
        </FooterTemplate>

    </asp:Repeater>

    <asp:ObjectDataSource ID="AlbumTrucksListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Album_AlbumAndTrucks" TypeName="ChinookSystem.BLL.AlbumController">

    </asp:ObjectDataSource>
</asp:Content>
