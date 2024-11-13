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
        // String de conexão com o banco de dados
        private string uploadFolder = "C:\\Users\\gabri\\Pictures\\";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarImagens();
                // Opcional: carregar uma lista inicial de imagens ou deixar vazio até a pesquisa
            }
        }
        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            string papid = txtPAPid.Text.Trim();

            // Verifica se o PAPid é válido
            if (!string.IsNullOrEmpty(papid))
            {
                // Carregar e exibir imagens da pasta do PAPid
                CarregarImagens(papid);
            }
            else
            {
                // Limpa a galeria se o PAPid for inválido
                rptImagens.DataSource = null;
                rptImagens.DataBind();
            }
        }

        private void CarregarImagens(string papid)
        {
            // Caminho completo da subpasta do PAPid
            //string folderPath = Server.MapPath(uploadFolder + papid);
            string folderPath = Path.Combine(uploadFolder, papid);

            // Verifica se a pasta existe
            if (Directory.Exists(folderPath))
            {
                // Obtém os caminhos das imagens na pasta e seleciona até 10 imagens
                var images = Directory.GetFiles(folderPath)
                                      .Take(10)
                              .Select(imgPath => new
                              {
                                  ImagePath = uploadFolder + papid + "/" + Path.GetFileName(imgPath)
                              })
                              .ToList();


                // Vincula os caminhos das imagens ao Repeater para exibição
                rptImagens.DataSource = images;
                rptImagens.DataBind();
            }
            else
            {
                // Limpa o Repeater se a pasta não existir
                rptImagens.DataSource = null;
                rptImagens.DataBind();
            }
        }
        private void CarregarImagens()
        {
            // Caminho completo para a pasta de imagens fora do projeto
            string pastaDeFotos = "C:\\Users\\gabri\\Pictures\\";

            // Verifica se a pasta existe
            if (Directory.Exists(pastaDeFotos))
            {
                // Obtém todos os arquivos de imagem na pasta
                string[] imagens = Directory.GetFiles(pastaDeFotos, "*.jpg")
                    .Concat(Directory.GetFiles(pastaDeFotos, "*.png"))
                    .Concat(Directory.GetFiles(pastaDeFotos, "*.jpeg"))
                    .Concat(Directory.GetFiles(pastaDeFotos, "*.gif"))
                    .ToArray();

                // Converte os caminhos completos para caminhos virtuais
                var imagensVirtuais = imagens.Select(img =>
                    pastaDeFotos + Path.GetFileName(img)).ToList();

                // Define a fonte de dados para o Repeater
                ImagemRepeater.DataSource = imagensVirtuais;
                ImagemRepeater.DataBind();
            }
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //string caminhoPasta = @"C:\Imagens\MinhaPasta\";
            string caminhoPasta = "C:\\Users\\gabri\\Pictures\\";

            if (Request.AppRelativeCurrentExecutionFilePath.StartsWith("~/Pictures/"))
            {
                string nomeArquivo = Path.GetFileName(Request.AppRelativeCurrentExecutionFilePath);
                string caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

                if (File.Exists(caminhoCompleto))
                {
                    // Envia o arquivo diretamente
                    Response.Clear();
                    Response.ContentType = GetContentType(caminhoCompleto);
                    Response.WriteFile(caminhoCompleto);
                    Response.End();
                }
            }
        }

        private string GetContentType(string path)
        {
            string ext = Path.GetExtension(path).ToLowerInvariant();
            switch (ext)
            {
                case ".png": return "image/png";
                case ".jpg":
                case ".jpeg": return "image/jpeg";
                case ".gif": return "image/gif";
                default: return "application/octet-stream";
            }
        }
    }
}