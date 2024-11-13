using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SW
{
    public partial class Dashboard : Page
    {
            string connectionString = "Data Source=JARVIS;Initial Catalog=SW;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            CarregarDados();
        }
        private void CarregarDados()
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                //// Exemplo de consulta para os 4 cards
                SqlCommand cmd = new SqlCommand("SELECT COUNT(ComplaintNr) FROM Queixas", con);
                lblNrQueixas.Text = cmd.ExecuteScalar().ToString();

                //cmd = new SqlCommand("SELECT COUNT(EstruturaID) FROM EstruturasAfectadas", con);
                //lblEstruturas.Text = cmd.ExecuteScalar().ToString();

                //cmd = new SqlCommand("SELECT COUNT(LugarID) FROM LugaresEspirituais", con);
                //lblLugares.Text = cmd.ExecuteScalar().ToString();

                //cmd = new SqlCommand("SELECT COUNT(CAFID) FROM CAFs WHERE Sexo = 'Feminino' AND EstadoCivil = 'Viúva'", con);
                //lblCAF.Text = cmd.ExecuteScalar().ToString();

                // Dados para os gráficos
                //CarregarGraficoPAPs(con);
                //CarregarGraficoCAFs(con);
                CarregarGraficoQueixas(con);
                CarregarGraficoSex(con);
                CarregarGraficoTypes(con);
                CarregarGraficoStatus(con);

                // Carregar os dados do banco de dados e convertê-los em JSON
                string jsonData = GetPAPData();
                ClientScript.RegisterStartupScript(this.GetType(), "initializeMap", $"initializeMap({jsonData});", true);
            }
        }

        private string GetPAPData()
        {
            List<object> locations = new List<object>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Nome, Latitude, Longitude FROM PAP";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        locations.Add(new
                        {
                            CaseID = reader["Nome"].ToString(),
                            Latitude = reader["Latitude"].ToString(),
                            Longitude = reader["Longitude"].ToString()
                        });
                    }
                }
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(locations);
        }


        private void CarregarGraficoPAPs(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("SELECT Dia, COUNT(PAPID) FROM Queixas GROUP BY Dia", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Serializar os dados para Chart.js
            JavaScriptSerializer js = new JavaScriptSerializer();
            string labels = js.Serialize(dt.AsEnumerable().Select(r => r["Dia"].ToString()).ToArray());
            string data = js.Serialize(dt.AsEnumerable().Select(r => Convert.ToInt32(r["COUNT"])).ToArray());

            // Gerar o script para Chart.js
            ClientScript.RegisterStartupScript(this.GetType(), "barChart", $"barChart.data.labels = {labels}; barChart.data.datasets[0].data = {data}; barChart.update();", true);
        } 
        
        private void CarregarGraficoQueixas(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("SELECT DateComplaint, COUNT(DateComplaint) as cnt FROM Queixas GROUP BY DateComplaint", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Serializar os dados para Chart.js
            JavaScriptSerializer js = new JavaScriptSerializer();
            string labels = js.Serialize(dt.AsEnumerable().Select(r => r["DateComplaint"].ToString()).ToArray());
            string data = js.Serialize(dt.AsEnumerable().Select(r => Convert.ToInt32(r["cnt"])).ToArray());

            // Gerar o script para Chart.js
            ClientScript.RegisterStartupScript(this.GetType(), "lineChartQueixas", $"barChartt.data.labels = {labels}; barChartt.data.datasets[0].data = {data}; barChartt.update();",true);
        }
        
        
        private void CarregarGraficoStatus(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("SELECT Status, COUNT(ComplaintNr) as cnt FROM Queixas GROUP BY Status", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Serializar os dados para Chart.js
            JavaScriptSerializer js = new JavaScriptSerializer();
            string labels = js.Serialize(dt.AsEnumerable().Select(r => r["Status"].ToString()).ToArray());
            string data = js.Serialize(dt.AsEnumerable().Select(r => Convert.ToInt32(r["cnt"])).ToArray());

            // Gerar o script para Chart.js
            ClientScript.RegisterStartupScript(this.GetType(), "lineChartStatus", $"lineChartStatus.data.labels = {labels}; lineChartStatus.data.datasets[0].data = {data}; lineChartStatus.update();",true);
        }
        
        
        private void CarregarGraficoTypes(SqlConnection con)
        {
            //SqlCommand cmd = new SqlCommand("SELECT TypeofComplaint, COUNT(ComplaintNr) as cnt FROM Queixas GROUP BY TypeofComplaint", con);
            SqlCommand cmd = new SqlCommand("Select TypeofComplaint, (Count(TypeofComplaint)* 100 / (Select Count(*) From Queixas)) as cnt From Queixas Group By TypeofComplaint", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Serializar os dados para Chart.js
            JavaScriptSerializer js = new JavaScriptSerializer();
            string labels = js.Serialize(dt.AsEnumerable().Select(r => r["TypeofComplaint"].ToString()).ToArray());
            string data = js.Serialize(dt.AsEnumerable().Select(r => Convert.ToInt32(r["cnt"])).ToArray());

            // Gerar o script para Chart.js
            ClientScript.RegisterStartupScript(this.GetType(), "TypeChart", $"TypeChart.data.labels = {labels}; TypeChart.data.datasets[0].data = {data}; TypeChart.update();",true);
        }

        private void CarregarGraficoCAFs(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("SELECT Sexo, COUNT(CAFID) FROM CAFs GROUP BY Sexo", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Serializar os dados para Chart.js
            JavaScriptSerializer js = new JavaScriptSerializer();
            string data = js.Serialize(dt.AsEnumerable().Select(r => Convert.ToInt32(r["COUNT"])).ToArray());

            // Gerar o script para Chart.js
            ClientScript.RegisterStartupScript(this.GetType(), "pieChart", $"pieChart.data.datasets[0].data = {data}; pieChart.update();", true);
        }
        
        private void CarregarGraficoSex(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("SELECT Gender, COUNT(ComplaintNr) as cnt FROM Queixas GROUP BY Gender", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Serializar os dados para Chart.js
            JavaScriptSerializer js = new JavaScriptSerializer();
            string data = js.Serialize(dt.AsEnumerable().Select(r => Convert.ToInt32(r["cnt"])).ToArray());

            // Gerar o script para Chart.js
            ClientScript.RegisterStartupScript(this.GetType(), "sexChart", $"sexChart.data.datasets[0].data = {data}; sexChart.update();", true);
        }
        
    }
}