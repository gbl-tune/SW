<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateUser.aspx.cs" Inherits="SW.CreateUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <title>Create User</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />

    <form id="form2"  class="container mt-5">
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtUsername">Username:</asp:Label>
            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtPassword">Password:</asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ddlCategory">Category:</asp:Label>
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>
        <asp:Button ID="btnCreateUser" runat="server" Text="Create User" OnClick="btnCreateUser_Click" CssClass="btn btn-primary" />
        <asp:Label ID="lblMessage" runat="server" CssClass="text-success"></asp:Label>
    </form>
</asp:Content>
