<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="WebApp.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Our Planning For Security</h1>

    <div class="row">
        <div class="col-md-12">
            <h2>Team Members</h2>
            <table class="table table-hover">
                <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Name</th>
                        <th scope="col">Password</th>
                        <th scope="col">Setup/Security Role ✔️❌ </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <th scope="row">1</th>
                        <td>Walter</td>
                        <td>Receiving</td>
                        <td><kbd>Food Service</kbd></td>
                    </tr>
                    <tr>
                        <th scope="row">2</th>
                        <td>Pavlo</td>
                        <td>Sales</td>
                        <td><kbd>Clerk</kbd></td>
                    </tr>
                    <tr>
                        <th scope="row">3</th>
                        <td>Blaise</td>
                        <td>Purchasing</td>
                        <td><kbd>Director</kbd></td>
                    </tr>
                    <tr>
                        <th scope="row">4</th>
                        <td>Milos</td>
                        <td>Racing</td>
                        <td><kbd>Race Coordinator</kbd></td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="col-md-12 alert alert-success">
            <h3>Connection String</h3>
            <code>connectionString = "Data Source=.;</Code><br />
            <code>Initial Catalog = eRace; </code><br />
            <code>Integrated Security = true"</code><br />
            <code>providerName = "System.Data.SqlClient"</code><br />
        </div>
    </div>
</asp:Content>
