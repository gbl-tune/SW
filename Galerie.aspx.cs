using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace SW
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        // Caminho fixo para as imagens
        private const string CAMINHO_IMAGENS = @"C:\Images";
        private const int ItensPorPagina = 12;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificar se o diretório existe
                if (!Directory.Exists(CAMINHO_IMAGENS))
                {
                    // Mostrar mensagem de erro se o diretório não existir
                    MostrarMensagemErro($"Diretório de imagens não encontrado: {CAMINHO_IMAGENS}");
                    return;
                }

                CarregarTodasImagens();
            }
        }

        private void MostrarMensagemErro(string mensagem)
        {
            // Limpar a fonte de dados
            rptImagens.DataSource = null;
            rptImagens.DataBind();

            // Mostrar mensagem de erro
            Label lblErro = new Label
            {
                Text = mensagem,
                CssClass = "erro-mensagem"
            };

            // Adicionar a mensagem de erro ao container
            pnlConteudo.Controls.Add(lblErro);
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            string termoPesquisa = txtPesquisa.Text.Trim().ToLower();
            CarregarImagensFiltradas(termoPesquisa);
        }

        private void CarregarTodasImagens()
        {
            List<ImagemInfo> imagens = ObterImagens("");
            BindImagens(imagens);
        }

        private void CarregarImagensFiltradas(string termoPesquisa)
        {
            List<ImagemInfo> imagens = ObterImagens(termoPesquisa);
            BindImagens(imagens);
        }

        private void BindImagens(List<ImagemInfo> imagens)
        {
            // Implementação de paginação
            int paginaAtual = 1;
            if (ViewState["PaginaAtual"] != null)
            {
                paginaAtual = Convert.ToInt32(ViewState["PaginaAtual"]);
            }

            var imagensPaginadas = imagens
                .Skip((paginaAtual - 1) * ItensPorPagina)
                .Take(ItensPorPagina)
                .ToList();

            // Verificar se há imagens
            if (!imagensPaginadas.Any())
            {
                MostrarMensagemErro("Nenhuma imagem encontrada.");
                return;
            }

            rptImagens.DataSource = imagensPaginadas;
            rptImagens.DataBind();

            // Configurar controles de paginação
            ConfigurarPaginacao(imagens.Count, paginaAtual);
        }

        private void ConfigurarPaginacao(int totalImagens, int paginaAtual)
        {
            int totalPaginas = (int)Math.Ceiling((double)totalImagens / ItensPorPagina);

            // Configurar botões de paginação
            btnPaginaAnterior.Enabled = paginaAtual > 1;
            btnProximaPagina.Enabled = paginaAtual < totalPaginas;

            lblPaginaAtual.Text = $"Página {paginaAtual} de {totalPaginas}";
            ViewState["PaginaAtual"] = paginaAtual;
        }

        protected void btnPaginaAnterior_Click(object sender, EventArgs e)
        {
            int paginaAtual = Convert.ToInt32(ViewState["PaginaAtual"] ?? 1);
            if (paginaAtual > 1)
            {
                ViewState["PaginaAtual"] = paginaAtual - 1;
                CarregarImagensFiltradas(txtPesquisa.Text.Trim().ToLower());
            }
        }

        protected void btnProximaPagina_Click(object sender, EventArgs e)
        {
            int paginaAtual = Convert.ToInt32(ViewState["PaginaAtual"] ?? 1);
            ViewState["PaginaAtual"] = paginaAtual + 1;
            CarregarImagensFiltradas(txtPesquisa.Text.Trim().ToLower());
        }

        private List<ImagemInfo> ObterImagens(string filtro)
        {
            List<ImagemInfo> imagens = new List<ImagemInfo>();

            try
            {
                // Extensões de imagem suportadas
                string[] extensoesPermitidas = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };

                var arquivos = Directory.GetFiles(CAMINHO_IMAGENS)
                    .Where(f => extensoesPermitidas.Contains(Path.GetExtension(f).ToLower()));

                foreach (string arquivo in arquivos)
                {
                    string nomeArquivo = Path.GetFileName(arquivo);
                    string descricao = Path.GetFileNameWithoutExtension(arquivo);

                    // Verificar se o filtro está vazio ou se o nome contém o filtro
                    if (string.IsNullOrEmpty(filtro) ||
                        descricao.ToLower().Contains(filtro))
                    {
                        imagens.Add(new ImagemInfo
                        {
                            NomeArquivo = nomeArquivo,
                            CaminhoCompleto = arquivo,
                            Descricao = descricao
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Log do erro (você pode substituir por seu método de log)
                System.Diagnostics.Debug.WriteLine($"Erro ao carregar imagens: {ex.Message}");
            }

            // Ordenar por nome do arquivo
            return imagens.OrderBy(i => i.NomeArquivo).ToList();
        }
    }

    public class ImagemInfo
    {
        public string NomeArquivo { get; set; }
        public string CaminhoCompleto { get; set; }
        public string Descricao { get; set; }
    }
}
