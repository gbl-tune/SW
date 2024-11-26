<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Galerie.aspx.cs" Inherits="SW.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
    .search-container {
        margin: 20px 0;
        text-align: center;
    }

    .images-container {
        display: flex;
        flex-wrap: wrap;
        justify-content: center;
        gap: 20px;
        margin-bottom: 20px;
    }

    .image-item {
        width: 200px;
        border: 1px solid #ddd;
        border-radius: 8px;
        overflow: hidden;
        box-shadow: 0 2px 5px rgba(0,0,0,0.1);
        margin-bottom: 15px;
    }

    .image-item img {
        width: 100%;
        height: 200px;
        object-fit: cover;
    }

    .image-description {
        text-align: center;
        padding: 10px;
        background-color: #f9f9f9;
        white-space: nowrap;
        overflow: hidden;
        text-overflow: ellipsis;
    }

    .pagination-container {
        display: flex;
        justify-content: center;
        align-items: center;
        gap: 15px;
        margin-top: 20px;
    }

    .erro-mensagem {
        color: red;
        text-align: center;
        display: block;
        margin: 20px 0;
        font-weight: bold;
    }
</style>
<%--    <div class="container mt-4">
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
    <asp:Image ID="imgLocal" runat="server" ImageUrl="C:\Pictures\p16826253_b_v13_ab.jpg" />--%>

      <asp:Panel ID="pnlConteudo" runat="server">
        <div class="search-container">
            <asp:TextBox ID="txtPesquisa" runat="server" placeholder="Digite sua pesquisa"></asp:TextBox>
            <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" OnClick="btnPesquisar_Click" />
        </div>

        <div class="images-container">
            <asp:Repeater ID="rptImagens" runat="server">
                <ItemTemplate>
                    <div class="image-item">
                        <img src='<%# GetImageUrl(Eval("CaminhoCompleto")) %>' alt='<%# Eval("Descricao") %>' />
                        <div class="image-description">
                            <%# Eval("Descricao") %>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <div class="pagination-container">
            <asp:Button ID="btnPaginaAnterior" runat="server" Text="Anterior" OnClick="btnPaginaAnterior_Click" />
            <asp:Label ID="lblPaginaAtual" runat="server" Text=""></asp:Label>
            <asp:Button ID="btnProximaPagina" runat="server" Text="Próxima" OnClick="btnProximaPagina_Click" />
        </div>
    </asp:Panel>

<script runat="server">

    protected string GetImageUrl(object caminho)
    {
        if (caminho == null) return string.Empty;
        
        // Converter caminho do sistema de arquivos para URL virtual
        string nomeArquivo = System.IO.Path.GetFileName(caminho.ToString());
        return $"/ImageHandler.ashx?img={nomeArquivo}";
    }
</script>

        
</asp:Content>
