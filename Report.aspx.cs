using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;


namespace SW
{
    public partial class Report : System.Web.UI.Page
    {
        string constr = "Data Source=JARVIS;Initial Catalog=SW;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void exeldoc(string query)
        {
            {

                using (SqlConnection con = new SqlConnection(constr))

                using (SqlCommand cmd = new SqlCommand(query)) //indicar o nome da tabela, neste caso é Comércio
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt, "Queixas"); // substitui Comércio pelo nome da folha
                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AppendHeader("content-disposition", "attachment;filename=Funcionariso.csv");//Digite no lugar de Comércio e Serviços o nome do ficheiro excel
                                using (MemoryStream MyMemoryStream = new MemoryStream())
                                {
                                    wb.SaveAs(MyMemoryStream);
                                    MyMemoryStream.WriteTo(Response.OutputStream);
                                    Response.Flush();
                                    Response.End();
                                    //Label.Text = "Exportação feita com sucesso!";
                                }
                            }
                        }
                    }

                }
            }

        }

        protected void Exel_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Queixas";
            exeldoc(query);

        }
    }
}