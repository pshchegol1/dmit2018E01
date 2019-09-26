<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisplayArtistAlbums.aspx.cs" Inherits="WebApp.SamplePages.DisplayArtistAlbums" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Enter your Artist Name (partial)"></asp:Label>
    &nbsp;&nbsp;
    <asp:TextBox ID="ArtistName" runat="server" placeholder="artist name"></asp:TextBox>
    &nbsp;&nbsp;
    <asp:Button ID="Fetch" runat="server" Text="Fetch" />

    <asp:GridView ID="ArtistAlbumList" runat="server" AutoGenerateColumns="False" DataSourceID="ArtistAlbumListODS" AllowPaging="True" PageSize="5">
        <Columns>
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title"></asp:BoundField>
            <asp:BoundField DataField="ArtistName" HeaderText="ArtistName" SortExpression="ArtistName"></asp:BoundField>
            <asp:BoundField DataField="RYear" HeaderText="RYear" SortExpression="RYear"></asp:BoundField>
            <asp:BoundField DataField="RLabel" HeaderText="RLabel" SortExpression="RLabel"></asp:BoundField>
        </Columns>
    </asp:GridView>

    <asp:ObjectDataSource ID="ArtistAlbumListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Album_AlbumsOfArtist" TypeName="ChinookSystem.BLL.AlbumController">

    </asp:ObjectDataSource>
</asp:Content>
