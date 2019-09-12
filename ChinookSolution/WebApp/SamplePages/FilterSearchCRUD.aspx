<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FilterSearchCRUD.aspx.cs" Inherits="WebApp.SamplePages.FilterSearchCRUD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Review CRUD</h1>

    <div class="row">
        <div class="col-sm-offset-3">
            <asp:Label ID="label1" runat="server" Text="Select an artist to view albums:"></asp:Label>&nbsp
            <asp:DropDownList ID="ArtistList" runat="server"></asp:DropDownList>

        </div>
    </div>
</asp:Content>
