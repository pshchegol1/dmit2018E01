<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="">
        <h1>Team-D</h1>
    </div>
    <div class="row">
        <div class="col-12">
            <img alt="" class="" style="width: 960px" src="images/logo-v2.jpg" />
        </div>
    </div>

    <div class="row">
        <div class="col-md-6">
            <h2>Team Members</h2>
            <table class="table table-hover">
                <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Name</th>
                        <th scope="col">Subsystem</th>
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
        <div class="col-md-6">
            <h2>Known Issues 🚧 </h2>
            <p class="hr">
            </p>
            <table class="table table-hover">
                <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">📝 Issue Description</th>
                        <th scope="col">On Subsystem</th>
                        <th scope="col">Status</th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="">
                        <th scope="row">1</th>
                        <td>Adding item from Inventory to Order</td>
                        <td>Purchasing</td>
                        <td>Not Done ❗️ </td>
                    </tr>
                    <tr class="">
                        <th scope="row">2</th>
                        <td>Loading a new Order</td>
                        <td>Purchasing</td>
                        <td>Not Done ❗️ </td>
                    </tr>
                </tbody>
            </table>

        </div>
    </div>

</asp:Content>
