<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateUser.aspx.cs" Inherits="SW.CreateUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <title>Create User</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />

    <form id="form2"  class="container mt-5">
        <div>

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
        </div>
        
        
        <div>   
              <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="True" 
            DataKeyNames="Id" 
            OnRowEditing="gvUsuarios_RowEditing" 
            OnRowUpdating="gvUsuarios_RowUpdating"
            OnRowCancelingEdit="gvUsuarios_RowCancelingEdit" 
            OnRowDeleting="gvUsuarios_RowDeleting">
            
            <Columns>
               <%-- <asp:BoundField DataField="Nome" HeaderText="Nome" SortExpression="Nome" />
                
                <!-- Coluna para o Email -->
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                
                <!-- Coluna para a Data de Registro (somente leitura) -->
                <asp:BoundField DataField="DataRegistro" HeaderText="Data de Registro" 
                    SortExpression="DataRegistro" ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" />
                
                <!-- Coluna com botões de edição -->--%>
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>





        </div>
    </form>
</asp:Content>
