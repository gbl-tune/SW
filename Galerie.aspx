<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Galerie.aspx.cs" Inherits="SW.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2>Galeria de Imagens</h2>
        <div class="mb-3">
            <label for="txtPAPid" class="form-label">Pesquisar por PAPid:</label>
            <asp:TextBox ID="txtPAPid" runat="server" CssClass="form-control" placeholder="Digite o PAPid para pesquisa"></asp:TextBox>
        </div>
        <div class="mb-3">
            <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CssClass="btn btn-primary" OnClick="btnPesquisar_Click" />
        </div>

        <!-- Galeria de Imagens -->
        <div class="row" id="galeria">
            <asp:Repeater ID="rptImagens" runat="server">
                <ItemTemplate>
                    <div class="col-md-3 mb-4">
                        <div class="card">
                            <img src='<%# Eval("ImagePath") %>' class="card-img-top" alt="Imagem" />
                        </div>
                        <div class="image-container">
                            <asp:Image ID="imgTeste" runat="server" ImageUrl="C:\Users\gabri\Pictures\__.jpg" Width="150px" Height="150px" />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

    <div>
        <h1>Galeria de Imagens</h1>
        <asp:Repeater ID="ImagemRepeater" runat="server">
            <HeaderTemplate>
                <div class="image-gallery">
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Image ID="ImagemItem" runat="server"
                    ImageUrl='<%# Container.DataItem %>'
                    CssClass="image-item" />
            </ItemTemplate>
            <FooterTemplate>
                </div>
               
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <img src="~C:/Pictures/download (1).png" />
    <asp:Image ID="imgLocal" runat="server" ImageUrl="C:\Pictures\p16826253_b_v13_ab.jpg" />
        
</asp:Content>
