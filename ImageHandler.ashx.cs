using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SW
{
    /// <summary>
    /// Summary description for ImageHandler
    /// </summary>
    public class ImageHandler : IHttpHandler
    {

        private const string CAMINHO_IMAGENS = @"C:\Images";

        public void ProcessRequest(HttpContext context)
        {
            string nomeImagem = context.Request.QueryString["img"];

            if (string.IsNullOrEmpty(nomeImagem))
            {
                context.Response.StatusCode = 404;
                return;
            }

            string caminhoImagem = Path.Combine(CAMINHO_IMAGENS, nomeImagem);

            if (!File.Exists(caminhoImagem))
            {
                context.Response.StatusCode = 404;
                return;
            }

            // Definir o tipo de conteúdo com base na extensão
            string extensao = Path.GetExtension(nomeImagem).ToLower();
            switch (extensao)
            {
                case ".jpg":
                case ".jpeg":
                    context.Response.ContentType = "image/jpeg";
                    break;
                case ".png":
                    context.Response.ContentType = "image/png";
                    break;
                case ".gif":
                    context.Response.ContentType = "image/gif";
                    break;
                case ".bmp":
                    context.Response.ContentType = "image/bmp";
                    break;
                case ".webp":
                    context.Response.ContentType = "image/webp";
                    break;
                default:
                    context.Response.StatusCode = 404;
                    return;
            }

            // Servir a imagem
            context.Response.WriteFile(caminhoImagem);
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}